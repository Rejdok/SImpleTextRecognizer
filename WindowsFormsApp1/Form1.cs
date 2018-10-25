using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.Structure;
namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        void startCaptureVideo(Tuple<Point, Size> rez)
        {
            CaptureRegion = rez;
            VideoCapture = new Timer();
            VideoCapture.Interval = 1000 / 30;
            VideoCapture.Tick += new EventHandler(OnTick);
            VideoCapture.Enabled = true;
        }
        public Form1()
        {
            InitializeComponent();
            GCTimer = new Timer();
            GCTimer.Interval = 500;
            GCTimer.Tick += new EventHandler(OnGcTick);
            GCTimer.Start();
            tesseract = new Emgu.CV.OCR.Tesseract("", "rus", Emgu.CV.OCR.OcrEngineMode.TesseractOnly);
            currText = new List<String>();

        }
        private void OnTick(object sender, EventArgs e)
        {
            imgMtx.WaitOne();
            currFrame = new Bitmap(CaptureRegion.Item2.Width, CaptureRegion.Item2.Height);
            Graphics f = Graphics.FromImage(currFrame);
            f.CopyFromScreen(CaptureRegion.Item1.X, CaptureRegion.Item1.Y, 0, 0, currFrame.Size);
            pictureBox1.Image = new Bitmap(currFrame);
            imgMtx.ReleaseMutex();

        }
        void OnGcTick(object sender, EventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        private void StartCapture_Click(object sender, EventArgs e)
        {
            this.cr = new CaptureRegion(startCaptureVideo);
            cr.Show();
        }
        private void TextUpdate()
        { 
            richTextBox1.Invoke((MethodInvoker)delegate(){
                richTextBox1.Clear();
                foreach(var i in currText)
                {
                    richTextBox1.AppendText(i);
                }
            });
        }
        Bitmap currFrame;
        Tuple<Point, Size> CaptureRegion;
        CaptureRegion cr;
        Timer VideoCapture;
        Timer GCTimer;
        Timer TextDetector;
        System.Threading.Mutex imgMtx = new System.Threading.Mutex();
        Emgu.CV.OCR.Tesseract tesseract;
        bool textProcessStaretd = false;
        List<String> currText;
        void OnTickTextDetector(object sender, EventArgs e)
        {
            imgMtx.WaitOne();

            Image<Bgr, Byte> img = new Image<Bgr, byte>(currFrame);
            Image<Gray, Byte> sobel = new Image<Gray, byte>(currFrame);
            sobel = img.Convert<Gray, byte>().Sobel(1, 0, 3).AbsDiff(new Gray(0.0)).Convert<Gray, byte>().ThresholdBinary(new Gray(50), new Gray(255));
            Mat SE = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, new Size(10, 2), new Point(-1, -1));
            sobel = sobel.MorphologyEx(Emgu.CV.CvEnum.MorphOp.Dilate, SE, new Point(-1, -1), 1, Emgu.CV.CvEnum.BorderType.Reflect, new MCvScalar(255));
            Emgu.CV.Util.VectorOfVectorOfPoint contours = new Emgu.CV.Util.VectorOfVectorOfPoint();
            Mat m = new Mat();
            CvInvoke.FindContours(sobel, contours, m, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);
            List<Rectangle> list = new List<Rectangle>();
            for (int i = 0; i < contours.Size; i++)
            {
                Rectangle brect = CvInvoke.BoundingRectangle(contours[i]);
                double ar = brect.Width / brect.Height;
                if (ar > 2 && brect.Width > 25 && brect.Height > 8 && brect.Height < 100)
                {
                    list.Add(brect);
                }
            }

            Image<Bgr, byte> imgout = img.CopyBlank();
            if (!textProcessStaretd)
            {
                textProcessStaretd = true;
                List<Rectangle> _list = list;
                var _currFrame = new Bitmap(currFrame);
                new System.Threading.Thread(delegate ()
                {
                    currText = new List<string>();
                    foreach (var r in _list)
                    {
                        Bitmap region = _currFrame.Clone(r, currFrame.PixelFormat);
                        Image<Bgr, Byte> regim = new Image<Bgr, Byte>(region);
                        tesseract.SetImage(regim);
                        tesseract.Recognize();
                        currText.Add(tesseract.GetUTF8Text());
                    }
                    TextUpdate();
                    textProcessStaretd = false;
                }).Start();
            }
            foreach (var r in list)
            {
                CvInvoke.Rectangle(img, r, new MCvScalar(0, 0, 255), 2);
                CvInvoke.Rectangle(imgout, r, new MCvScalar(0, 255, 255), -1);
            }
            imgout._And(img);
            pictureBox2.Image = imgout.Bitmap;
            imgMtx.ReleaseMutex();
        }

        private void ProcessScreen_Click(object sender, EventArgs e)
        {
            TextDetector = new Timer();
            TextDetector.Interval = 1000 / 30;
            TextDetector.Tick += new EventHandler(OnTickTextDetector);
            TextDetector.Start();
            TextDetector.Enabled = true;
        }
    }

}

