namespace SimpleTextRecognizer
{
    partial class ProcessingWindow
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.TranslatedText = new System.Windows.Forms.RichTextBox();
            this.ProcessedText = new System.Windows.Forms.RichTextBox();
            this.StartCapture = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // TranslatedText
            // 
            this.TranslatedText.Location = new System.Drawing.Point(533, 309);
            this.TranslatedText.Name = "TranslatedText";
            this.TranslatedText.Size = new System.Drawing.Size(320, 276);
            this.TranslatedText.TabIndex = 17;
            this.TranslatedText.Text = "";
            // 
            // ProcessedText
            // 
            this.ProcessedText.Location = new System.Drawing.Point(171, 309);
            this.ProcessedText.Name = "ProcessedText";
            this.ProcessedText.Size = new System.Drawing.Size(320, 276);
            this.ProcessedText.TabIndex = 13;
            this.ProcessedText.Text = "";
            // 
            // StartCapture
            // 
            this.StartCapture.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StartCapture.Location = new System.Drawing.Point(30, 27);
            this.StartCapture.Name = "StartCapture";
            this.StartCapture.Size = new System.Drawing.Size(135, 53);
            this.StartCapture.TabIndex = 10;
            this.StartCapture.Text = "Начать обработку";
            this.StartCapture.UseVisualStyleBackColor = true;
            this.StartCapture.Click += new System.EventHandler(this.StartCapture_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(533, 27);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(320, 257);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 12;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(171, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(320, 257);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(171, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Захваченный регион";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(530, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Выделенный текст";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(171, 290);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Обработанный текст";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(530, 293);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Перевод";
            // 
            // ProcessingWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TranslatedText);
            this.Controls.Add(this.ProcessedText);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.StartCapture);
            this.Name = "ProcessingWindow";
            this.Size = new System.Drawing.Size(916, 633);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        internal System.Windows.Forms.Button StartCapture;
        internal System.Windows.Forms.PictureBox pictureBox2;
        internal System.Windows.Forms.PictureBox pictureBox1;
        internal System.Windows.Forms.RichTextBox TranslatedText;
        internal System.Windows.Forms.RichTextBox ProcessedText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}
