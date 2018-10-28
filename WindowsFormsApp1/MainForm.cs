using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ImageProcessor;
using TextTranslator;
using System.Threading;
using System.Runtime.InteropServices;

namespace SimpleTextRecognizer
{
    public partial class MainForm : Form
    {
        void AddToMultiPageControl(Control control)
        {
            control.MouseDown += MainForm_MouseDown;
            multiPagePannel1.AddControl(control);
        }
        public MainForm()
        {

            InitializeComponent();
            InitGarbageCollectorTimer();

            imp.CoppedFromRegionImgReady += Imp_CoppedFromRegionImgReady;
            imp.SobelFilterReady += Imp_SobelFilterReady;
            imp.Img3Ready += Imp_Img3Ready;
            imp.RegionImgReady += Imp_RegionImgReady;
            imp.Itt.TextReady += Itt_TextReady;

            processingWindow.StartCapture.Click += StartCapture_Click;

            AddToMultiPageControl(processingWindow);
            AddToMultiPageControl(filterSettings);
            multiPagePannel1.SetActive(processingWindow);

            WideoCaptureThread = new Thread(WideoCapture);
            WideoCaptureThread.Start();

        }

        private void Imp_RegionImgReady(object sender, ImgOut e)
        {
            filterSettings.pictureBox2.Image = e.Img;
        }

        private void Imp_Img3Ready(object sender, ImgOut e)
        {
            filterSettings.pictureBox3.Image = e.Img;
        }

        private void Imp_SobelFilterReady(object sender, ImgOut e)
        {
            filterSettings.pictureBox1.Image = e.Img;
        }

        void startCaptureVideo(Tuple<Point, Size> rez)
        {
            CaptureRegion = rez;
            VideoCapture = new System.Windows.Forms.Timer();
            VideoCapture.Interval = 1000 / 30;
            VideoCapture.Tick += new EventHandler(OnVideoCaptureTick);
            VideoCapture.Enabled = true;
        }
        

        private void InitGarbageCollectorTimer()
        {
            GCTimer = new System.Windows.Forms.Timer();
            GCTimer.Interval = 500;
            GCTimer.Tick += new EventHandler(OnGcTick);
            GCTimer.Start();
        }

        private void StartCapture_Click(object sender, EventArgs e)
        {
            if (!IsScreenProcessStarted)
            {
                cr = new CaptureRegion(startCaptureVideo);
                cr.ShowDialog();
                processingWindow.StartCapture.Text = "Остановить обработку";
                IsScreenProcessStarted = true;
            }
            else
            {
                VideoCapture.Enabled = false;
                processingWindow.StartCapture.Text = "Начать обработку";
                IsScreenProcessStarted = false;
            }
        }

        private void Itt_TextReady(object sender, TextOut e)
        {
            processingWindow.ProcessedText.Invoke((MethodInvoker)(() => processingWindow.ProcessedText.Clear()));
            processingWindow.TranslatedText.Invoke((MethodInvoker)(() => processingWindow.ProcessedText.Clear()));
            foreach (var i in e.Text)
                processingWindow.ProcessedText.Invoke((MethodInvoker)(() => processingWindow.ProcessedText.AppendText(i)));
            foreach (var i in trntr.TranslateText(e.Text))
                processingWindow.TranslatedText.Invoke((MethodInvoker)(() => processingWindow.ProcessedText.AppendText(i)));
        }

        private void Imp_CoppedFromRegionImgReady(object sender, ImgOut e)
        {
            processingWindow.pictureBox2.Image = e.Img;
        }

        private void OnVideoCaptureTick(object sender, EventArgs e)
        {
            
            tickReady.Set();
        }
        unsafe private void WideoCapture()
        {
            while (!ShoudTerminate)
            {
                tickReady.WaitOne();
                Bitmap currFrame = new Bitmap(CaptureRegion.Item2.Width, CaptureRegion.Item2.Height);
                {
                    using (Graphics f = Graphics.FromImage(currFrame))
                    {
                        f.CopyFromScreen(CaptureRegion.Item1.X, CaptureRegion.Item1.Y, 0, 0, currFrame.Size);
                    }
                    imp.pictureDetect(new Bitmap(currFrame));
                    processingWindow.pictureBox1.Invoke((MethodInvoker)(() => processingWindow.pictureBox1.Image = currFrame));
                    
                }
                tickReady.Reset();
            }
        }

        void OnGcTick(object sender, EventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
   
        Tuple<Point, Size> CaptureRegion;
        
        ImgProcessor imp = new ImgProcessor();
        Translator trntr = new Translator();

        CaptureRegion cr;

        Thread WideoCaptureThread;
        System.Windows.Forms.Timer VideoCapture;

        System.Windows.Forms.Timer GCTimer;

        ProcessingWindow processingWindow = new ProcessingWindow();
        FilterSettings filterSettings = new FilterSettings();

        private readonly System.Threading.EventWaitHandle tickReady = new System.Threading.AutoResetEvent(false);
        int trhType = 8;

        bool IsScreenProcessStarted = false;
        bool ShoudTerminate = false;


        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void ProcessingTab_Click(object sender, EventArgs e)
        {
            multiPagePannel1.SetActive(processingWindow);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        ~MainForm()
        {
            ShoudTerminate = true;
            WideoCaptureThread.Join();
        }
        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg,int wparam, int lparam);

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            multiPagePannel1.SetActive(filterSettings);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }

}

