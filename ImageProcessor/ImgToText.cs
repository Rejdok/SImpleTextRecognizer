using System;
using System.Collections.Generic;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;
using System.Threading;
namespace ImageProcessor
{
    public class ImgToText
    {
        public ImgToText()
        {
            ImgProcessor = new Thread(decodeTextFromImg);
            ImgProcessor.Start();
        }
        public void TryDecode(List<Rectangle> TextRegions, Image<Gray, byte> FiltredImg)
        {
            if (!imgReady.WaitOne(0))
            {
                this.TextRegions = TextRegions;
                this.FiltredImg = FiltredImg;
                imgReady.Set();
            }
        }
        static int RegisonCompare(Rectangle lhs, Rectangle lgs)
        {
            if (lhs.Top < lhs.Top)
            {
                return 1;
            }
            else if (lhs.Left < lhs.Left)
            {
                return 1;
            }
            if(lhs.Left == lhs.Left&& lhs.Top  == lhs.Top)
            {
                return 0;
            }
            return -1;
        } 
        private void decodeTextFromImg()
        {
            while (!ShoudTerminate) {
                imgReady.WaitOne();
                currText.Clear();
                var imgs = new System.Collections.Concurrent.ConcurrentQueue<Image<Gray, Byte>>();
                TextRegions.Sort(RegisonCompare);
                foreach (var r in TextRegions)
                {
                    var region = FiltredImg.Copy(r);
                    imgs.Enqueue(region);
                };

                foreach (var r in imgs)
                {
                    tesseract.SetImage(r);
                    tesseract.Recognize();
                    currText.Add(tesseract.GetUTF8Text());
                };
                OnTextReady(new TextOut(currText));
                imgReady.Reset();
            }
        }
        protected virtual void OnTextReady(TextOut e)
        {
            TextReady?.Invoke(this, e);
        }

        public event TextReady TextReady;
        //data
        private Thread ImgProcessor;
        private List<Rectangle> TextRegions;
        private Image<Gray, byte> FiltredImg;
        //img decoding
        private List<string> currText = new List<string>();
        private Emgu.CV.OCR.Tesseract tesseract = new Emgu.CV.OCR.Tesseract("", "rus", Emgu.CV.OCR.OcrEngineMode.Default);
        //thread control
        private bool ShoudTerminate = false;
        private readonly System.Threading.EventWaitHandle imgReady = new System.Threading.AutoResetEvent(false);
        ~ImgToText()
        {
            ShoudTerminate = true;
            ImgProcessor.Join();
        }
    }

    public class TextOut : EventArgs
    {
        public TextOut(List<string> text)
        {
            this.Text = text;
        }
        public List<string> Text { get; }
    }
    public delegate void TextReady(Object sender, TextOut e);
}
