using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
