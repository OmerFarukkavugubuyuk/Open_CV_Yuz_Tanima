namespace OpenCvYuzTanima
{
    partial class Form1
    {
        /// <summary>
        /// Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Kullanılan kaynakları temizler.
        /// </summary>
        /// <param name="disposing">Yönetilen kaynaklar silinmeli mi?</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Tasarımcı desteği için gerekli metot.
        /// Bu metodun içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBoxCamera = new System.Windows.Forms.PictureBox();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblFaceCount = new System.Windows.Forms.Label();

            // ── pictureBoxCamera düzenini geçici olarak askıya al ──
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCamera)).BeginInit();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();

            // ════════════════════════════════════════════════════════
            // pictureBoxCamera
            // Kameradan gelen görüntünün gösterileceği alan.
            // ════════════════════════════════════════════════════════
            this.pictureBoxCamera.BackColor = System.Drawing.Color.Black;
            this.pictureBoxCamera.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxCamera.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxCamera.Name = "pictureBoxCamera";
            this.pictureBoxCamera.Size = new System.Drawing.Size(900, 560);
            this.pictureBoxCamera.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxCamera.TabIndex = 0;
            this.pictureBoxCamera.TabStop = false;

            // ════════════════════════════════════════════════════════
            // panelBottom
            // Alt kontrol çubuğu (butonlar + durum etiketleri).
            // ════════════════════════════════════════════════════════
            this.panelBottom.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.panelBottom.Controls.Add(this.btnStart);
            this.panelBottom.Controls.Add(this.btnStop);
            this.panelBottom.Controls.Add(this.lblStatus);
            this.panelBottom.Controls.Add(this.lblFaceCount);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 560);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.panelBottom.Size = new System.Drawing.Size(900, 50);
            this.panelBottom.TabIndex = 1;

            // ════════════════════════════════════════════════════════
            // btnStart
            // Kamerayı başlatan düğme.
            // ════════════════════════════════════════════════════════
            this.btnStart.BackColor = System.Drawing.Color.FromArgb(0, 150, 80);
            this.btnStart.FlatAppearance.BorderSize = 0;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnStart.ForeColor = System.Drawing.Color.White;
            this.btnStart.Location = new System.Drawing.Point(10, 8);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(120, 34);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "▶  Başlat";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);

            // ════════════════════════════════════════════════════════
            // btnStop
            // Kamerayı durduran düğme.
            // ════════════════════════════════════════════════════════
            this.btnStop.BackColor = System.Drawing.Color.FromArgb(180, 40, 40);
            this.btnStop.FlatAppearance.BorderSize = 0;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnStop.ForeColor = System.Drawing.Color.White;
            this.btnStop.Location = new System.Drawing.Point(140, 8);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(120, 34);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "■  Durdur";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Enabled = false;   // Başlangıçta pasif
            this.btnStop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);

            // ════════════════════════════════════════════════════════
            // lblStatus
            // Genel durum mesajı (sol alt).
            // ════════════════════════════════════════════════════════
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.ForeColor = System.Drawing.Color.Silver;
            this.lblStatus.Location = new System.Drawing.Point(275, 15);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(200, 20);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Hazır — Başlat düğmesine basın.";

            // ════════════════════════════════════════════════════════
            // lblFaceCount
            // Algılanan yüz sayısını gösteren etiket (sağ köşe).
            // ════════════════════════════════════════════════════════
            this.lblFaceCount.Anchor = System.Windows.Forms.AnchorStyles.Right |
                                          System.Windows.Forms.AnchorStyles.Top;
            this.lblFaceCount.AutoSize = false;
            this.lblFaceCount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFaceCount.ForeColor = System.Drawing.Color.FromArgb(255, 80, 80);
            this.lblFaceCount.Location = new System.Drawing.Point(700, 15);
            this.lblFaceCount.Name = "lblFaceCount";
            this.lblFaceCount.Size = new System.Drawing.Size(185, 20);
            this.lblFaceCount.TabIndex = 3;
            this.lblFaceCount.Text = "";
            this.lblFaceCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // ════════════════════════════════════════════════════════
            // Form1
            // ════════════════════════════════════════════════════════
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(900, 610);
            this.Controls.Add(this.pictureBoxCamera);   // Fill → önce ekle
            this.Controls.Add(this.panelBottom);        // Bottom → sonra ekle
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Yüz Tespiti — Emgu CV";
            this.Load += new System.EventHandler(this.Form1_Load);

            // ── Düzeni geri etkinleştir ──
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCamera)).EndInit();
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        // ── Kontrol tanımlamaları ──────────────────────────────────
        private System.Windows.Forms.PictureBox pictureBoxCamera;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblFaceCount;
    }
}