using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleTextRecognizer
{
    public partial class ProcessingWindow : UserControl
    {
        public ProcessingWindow()
        {
            InitializeComponent();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void StartCapture_Click(object sender, EventArgs e)
        {
            //if (!IsScreenProcessStarted)
            //{
            //    cr = new CaptureRegion(startCaptureVideo);
            //    cr.ShowDialog();
            //    StartCapture.Text = "Остановить обработку";
            //    IsScreenProcessStarted = true;
            //}
            //else
            //{
            //    VideoCapture.Enabled = false;
            //    StartCapture.Text = "Начать обработку";
            //    IsScreenProcessStarted = false;
            //}
        }

    }
}
