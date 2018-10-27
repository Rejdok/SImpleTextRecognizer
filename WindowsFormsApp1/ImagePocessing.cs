using System;
using System.Windows.Forms;

namespace SimpleTextRecognizer
{
    public partial class ImagePocessing : Form
    {
        public ImagePocessing()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        public ref PictureBox PictureBox1 { get { return ref this.pictureBox1; } }
        public ref PictureBox PictureBox2 { get { return ref this.pictureBox2; } }
    }
}
