using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
namespace OpenCvYuzTanima
{
    public partial class Form1 : Form
    {
        // Kamera bağlantı nesnesi
        private VideoCapture? _capture;

        // Yüz tespiti için Haar Cascade sınıflandırıcısı
        private CascadeClassifier? _faceCascade;

        // Kameradan okunan görüntü karesini tutan matris
        private Mat _frame = new Mat();

        // Kameranın çalışıp çalışmadığını tutan bayrak
        private bool _isRunning = false;

        public Form1()
        {
            InitializeComponent();

            // Form kapanınca kaynakları temizle
            this.FormClosing += Form1_FormClosing;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // XML dosyasının yolu
            string cascadePath = "haarcascade_frontalface_default.xml";

            // Dosya yoksa hata ver ve butonu devre dışı bırak
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

            // XML dosyasını yükleyerek sınıflandırıcıyı oluştur
            _faceCascade = new CascadeClassifier(cascadePath);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (_isRunning) return;

            // 0 numaralı kamerayı aç (ilk kamera)
            _capture = new VideoCapture(0, VideoCapture.API.DShow);

            // Kamera açılamadıysa hata ver
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

            // UI boşa düştüğünde ProcessFrame'i çağır
            Application.Idle += ProcessFrame;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopCamera();
        }

        private void ProcessFrame(object? sender, EventArgs e)
        {
            if (_capture == null || !_isRunning) return;

            // Kameradan bir kare oku
            _capture.Read(_frame);
            if (_frame.IsEmpty) return;

            // Kareyi renkli görüntüye çevir
            using var image = _frame.ToImage<Bgr, byte>();

            // Yüz tespiti için gri tona çevir
            using var grayImage = image.Convert<Gray, byte>();

            // Kontrastı iyileştir (karanlık ortamlarda daha iyi tespit için)
            grayImage._EqualizeHist();

            // Gri görüntüde yüzleri ara ve dikdörtgen listesi döndür
            Rectangle[] faces = _faceCascade!.DetectMultiScale(
                grayImage,
                scaleFactor: 1.1,   // Her adımda görüntüyü %10 küçült
                minNeighbors: 4,    // Yanlış tespiti azaltmak için komşu sayısı
                minSize: new Size(60, 60)  // En küçük algılanacak yüz boyutu
            );

            // Bulunan her yüzün etrafına kırmızı dikdörtgen çiz
            foreach (Rectangle face in faces)
                image.Draw(face, new Bgr(0, 0, 255), thickness: 2);

            // Görüntüyü PictureBox'ın anlayacağı Bitmap formatına çevir
            var bmp = new System.Drawing.Bitmap(
                image.Width,
                image.Height,
                System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            var bmpData = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                System.Drawing.Imaging.ImageLockMode.WriteOnly,
                bmp.PixelFormat);

            // OpenCV görüntü verisini Bitmap belleğine kopyala
            image.Mat.CopyTo(new Mat(
                image.Height, image.Width,
                DepthType.Cv8U, 3,
                bmpData.Scan0,
                bmpData.Stride));

            bmp.UnlockBits(bmpData);

            // Eski görüntüyü sil, yeni kareyi ekrana yansıt
            var oldImage = pictureBoxCamera.Image;
            pictureBoxCamera.Image = bmp;
            oldImage?.Dispose();

            // Algılanan yüz sayısını etikette göster
            lblFaceCount.Text = faces.Length > 0 ? $"🔴 {faces.Length} yüz algılandı" : "";
        }

        private void StopCamera()
        {
            if (!_isRunning) return;

            // ProcessFrame'in tekrar çağrılmasını engelle
            Application.Idle -= ProcessFrame;

            _isRunning = false;

            // Kamera bağlantısını kapat
            _capture?.Dispose();
            _capture = null;

            // Ekrandaki görüntüyü temizle
            var oldImage = pictureBoxCamera.Image;
            pictureBoxCamera.Image = null;
            oldImage?.Dispose();

            btnStart.Enabled = true;
            btnStop.Enabled = false;
            lblStatus.Text = "Durduruldu.";
        }

        private void Form1_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // Form kapanırken kamerayı ve tüm nesneleri temizle
            StopCamera();
            _frame.Dispose();
            _faceCascade?.Dispose();
        }
    }
}
