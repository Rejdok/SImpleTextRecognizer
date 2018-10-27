using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ImageProcessor;
using TextTranslator;

namespace SimpleTextRecognizer
{
    public partial class MainForm : Form
    {

        void startCaptureVideo(Tuple<Point, Size> rez)
        {
            CaptureRegion = rez;
            VideoCapture = new Timer();
            VideoCapture.Interval = 1000 / 30;
            VideoCapture.Tick += new EventHandler(OnVideoCaptureTick);
            VideoCapture.Enabled = true;
        }
        public MainForm()
        {
            InitializeComponent();
            GCTimer = new Timer();
            GCTimer.Interval = 500;
            GCTimer.Tick += new EventHandler(OnGcTick);
            GCTimer.Start();
            imp.CoppedFromRegionImgReady += Imp_CoppedFromRegionImgReady;
            imp.Itt.TextReady += Itt_TextReady;
            imp.SobelFilterReady += (ImgReady)(((Object obj,ImgOut e) => imgProc.PictureBox1.Image=e.Img));
            currText = new List<String>();
        }

        private void Itt_TextReady(object sender, TextOut e)
        {
            ProcessedText.Invoke((MethodInvoker)(() => ProcessedText.Clear()));
            TranslatedText.Invoke((MethodInvoker)(() => ProcessedText.Clear()));
            foreach (var i in e.Text)
                ProcessedText.Invoke((MethodInvoker)( ()=>ProcessedText.AppendText(i) ) );
            foreach (var i in trntr.TranslateText(e.Text))
                TranslatedText.Invoke((MethodInvoker)(() => ProcessedText.AppendText(i)));
        }

        private void Imp_CoppedFromRegionImgReady(object sender, ImgOut e)
        {
            pictureBox2.Image = e.Img;
        }

        private void OnVideoCaptureTick(object sender, EventArgs e)
        {
            Bitmap currFrame = new Bitmap(CaptureRegion.Item2.Width, CaptureRegion.Item2.Height);
            {
                using (Graphics f = Graphics.FromImage(currFrame))
                {
                    f.CopyFromScreen(CaptureRegion.Item1.X, CaptureRegion.Item1.Y, 0, 0, currFrame.Size);
                }
                pictureBox1.Image = currFrame;
                imp.pictureDetect(new Bitmap(currFrame));
            }
        }
        void OnGcTick(object sender, EventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        private void StartCapture_Click(object sender, EventArgs e)
        {
            if (!IsScreenProcessStarted)
            {
                cr = new CaptureRegion(startCaptureVideo);
                cr.ShowDialog();
                StartCapture.Text = "Остановить обработку";
                IsScreenProcessStarted = true;
            }
            else
            {
                VideoCapture.Enabled = false;
                StartCapture.Text = "Начать обработку";
                IsScreenProcessStarted = false;
            }
        }
        private void TextUpdate()
        {
            ProcessedText.Invoke((MethodInvoker)delegate ()
            {
                ProcessedText.Clear();
                foreach (var i in currText)
                {
                    ProcessedText.AppendText(i);
                }
            });
        }

   
        Tuple<Point, Size> CaptureRegion;

        ImgProcessor imp = new ImgProcessor();
        Translator trntr = new Translator();

        CaptureRegion cr;

        ImagePocessing imgProc = new ImagePocessing();

        Timer VideoCapture;
        Timer GCTimer;

        int trhType = 8;
        List<String> currText;
        bool IsScreenProcessStarted = false;
        private void ProcessScreen_Click(object sender, EventArgs e)
        {
            if (!IsScreenProcessStarted)
            {
                imgProc.ShowDialog();
                IsScreenProcessStarted = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            imgProc.ShowDialog();
        }
    }

}

