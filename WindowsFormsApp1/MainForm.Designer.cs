namespace SimpleTextRecognizer
{
    partial class MainForm
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.ProcessingTab = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.multiPagePannel1 = new SimpleTextRecognizer.MultiPagePannel();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(12, 202);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(155, 70);
            this.button1.TabIndex = 10;
            this.button1.Text = "Настройки фильтров";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ProcessingTab
            // 
            this.ProcessingTab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ProcessingTab.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ProcessingTab.Location = new System.Drawing.Point(12, 126);
            this.ProcessingTab.Name = "ProcessingTab";
            this.ProcessingTab.Size = new System.Drawing.Size(155, 70);
            this.ProcessingTab.TabIndex = 12;
            this.ProcessingTab.Text = "Обработка";
            this.ProcessingTab.UseVisualStyleBackColor = true;
            this.ProcessingTab.Click += new System.EventHandler(this.ProcessingTab_Click);
            // 
            // button2
            // 
            this.button2.BackgroundImage = global::SimpleTextRecognizer.Properties.Resources.ShutDown;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.Location = new System.Drawing.Point(12, 538);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(155, 133);
            this.button2.TabIndex = 15;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // multiPagePannel1
            // 
            this.multiPagePannel1.Location = new System.Drawing.Point(173, 31);
            this.multiPagePannel1.Name = "multiPagePannel1";
            this.multiPagePannel1.Size = new System.Drawing.Size(910, 640);
            this.multiPagePannel1.TabIndex = 18;
            this.multiPagePannel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseDown);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Algerian", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 82);
            this.label1.TabIndex = 19;
            this.label1.Text = "Simple Text Recognizer";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(1094, 680);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.multiPagePannel1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.ProcessingTab);
            this.Controls.Add(this.button1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(120)))), ((int)(((byte)(138)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.Name = "MainForm";
            this.Text = "Simple Text Recognizer";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseDown);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button ProcessingTab;
        private System.Windows.Forms.Button button2;
        private MultiPagePannel multiPagePannel1;
        private System.Windows.Forms.Label label1;
    }
}

