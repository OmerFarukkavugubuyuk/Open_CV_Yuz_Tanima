using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;    
namespace OpenCvYuzTanima
{
    public partial class Form1 : Form
    {
        // ──────────────────────────────────────────
        // Alanlar (Fields)
        // ──────────────────────────────────────────

        /// <summary>
        /// Web kamerasına bağlantı nesnesi.
        /// </summary>
        private VideoCapture? _capture;

        /// <summary>
        /// Haar Cascade yüz sınıflandırıcısı.
        /// </summary>
        private CascadeClassifier? _faceCascade;

        /// <summary>
        /// Kameradan gelen ham kareyi tutmak için Mat nesnesi.
        /// Her frame'de yeniden kullanılır → gereksiz GC baskısı önlenir.
        /// </summary>
        private Mat _frame = new Mat();

        /// <summary>
        /// Kamera çalışıyor mu?
        /// </summary>
        private bool _isRunning = false;

        // ──────────────────────────────────────────
        // Yapıcı & Form Yükleme
        // ──────────────────────────────────────────

        public Form1()
        {
            InitializeComponent();

            // Form kapanırken kaynakları serbest bırak
            this.FormClosing += Form1_FormClosing;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Haar Cascade XML dosyasını yükle.
            // Emgu.CV.runtime.windows paketi bu dosyayı
            // "haarcascades\" klasörüne kopyalar.

            string cascadePath = "haarcascade_frontalface_default.xml";

            if (!System.IO.File.Exists(cascadePath))
            {
                MessageBox.Show(
                    $"Cascade dosyası bulunamadı:\n{cascadePath}\n\n" +
                    "Emgu.CV.runtime.windows NuGet paketinin yüklü olduğundan " +
                    "ve 'Copy to Output Directory' ayarının yapıldığından emin olun.",
                    "Dosya Hatası",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                btnStart.Enabled = false;
                return;
            }

            _faceCascade = new CascadeClassifier(cascadePath);
        }

        // ──────────────────────────────────────────
        // Buton Olayları
        // ──────────────────────────────────────────

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (_isRunning) return;

            // 0 = varsayılan (ilk) kamera
            _capture = new VideoCapture(0, VideoCapture.API.DShow);

            if (!_capture.IsOpened)
            {
                MessageBox.Show("Kamera açılamadı!", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                _capture.Dispose();
                _capture = null;
                return;
            }

            _isRunning = true;
            btnStart.Enabled = false;
            btnStop.Enabled = true;
            lblStatus.Text = "Kamera çalışıyor...";

            // Application.Idle: UI thread boşa düştüğünde çağrılır.
            // Bu yöntem formu dondurmaz; ayrı bir Thread veya
            // async döngüye gerek kalmaz.
            Application.Idle += ProcessFrame;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopCamera();
        }

        // ──────────────────────────────────────────
        // Ana Frame İşleme Döngüsü
        // ──────────────────────────────────────────

        /// <summary>
        /// Application.Idle event handler'ı.
        /// UI thread her boşaldığında bir kare okur ve yüz tespiti yapar.
        /// </summary>
        private void ProcessFrame(object? sender, EventArgs e)
        {
            if (_capture == null || !_isRunning) return;

            _capture.Read(_frame);
            if (_frame.IsEmpty) return;

            using var image = _frame.ToImage<Bgr, byte>();
            using var grayImage = image.Convert<Gray, byte>();
            grayImage._EqualizeHist();

            Rectangle[] faces = _faceCascade!.DetectMultiScale(
                grayImage,
                scaleFactor: 1.1,
                minNeighbors: 4,
                minSize: new Size(60, 60)
            );

            foreach (Rectangle face in faces)
                image.Draw(face, new Bgr(0, 0, 255), thickness: 2);

            // ── ToBitmap() yerine manuel dönüşüm ──────────────────
            var bmp = new System.Drawing.Bitmap(
                image.Width,
                image.Height,
                System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            var bmpData = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                System.Drawing.Imaging.ImageLockMode.WriteOnly,
                bmp.PixelFormat);

            image.Mat.CopyTo(new Mat(
                image.Height, image.Width,
                DepthType.Cv8U, 3,
                bmpData.Scan0,
                bmpData.Stride));

            bmp.UnlockBits(bmpData);
            // ──────────────────────────────────────────────────────

            var oldImage = pictureBoxCamera.Image;
            pictureBoxCamera.Image = bmp;
            oldImage?.Dispose();

            lblFaceCount.Text = faces.Length > 0 ? $"🔴 {faces.Length} yüz algılandı" : "";
        }


        // ──────────────────────────────────────────
        // Temizlik (Dispose / Memory Leak Önleme)
        // ──────────────────────────────────────────

        /// <summary>
        /// Kamerayı ve tüm kaynakları güvenli şekilde durdurur.
        /// </summary>
        private void StopCamera()
        {
            if (!_isRunning) return;

            // Idle event'ini kaldır — artık frame işlenmeyecek
            Application.Idle -= ProcessFrame;

            _isRunning = false;

            // VideoCapture'ı dispose et
            _capture?.Dispose();
            _capture = null;

            // PictureBox'taki son görüntüyü temizle
            var oldImage = pictureBoxCamera.Image;
            pictureBoxCamera.Image = null;
            oldImage?.Dispose();

            btnStart.Enabled = true;
            btnStop.Enabled = false;
            lblStatus.Text = "Durduruldu.";
        }

        private void Form1_FormClosing(object? sender, FormClosingEventArgs e)
        {
            StopCamera();

            // Mat ve CascadeClassifier nesnelerini dispose et
            _frame.Dispose();
            _faceCascade?.Dispose();
        }
    }
}