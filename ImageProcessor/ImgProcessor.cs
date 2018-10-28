using System;
using System.Collections.Generic;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;


namespace ImageProcessor

{
    public class ImgProcessor
    {
        public void pictureDetect(Bitmap currFrame)
        {
            using(Image<Gray, Byte> img = new Image<Rgb, byte>(currFrame).Convert<Gray, Byte>()){
                Image<Gray, byte> sobel = new Image<Gray, byte>(currFrame);
                Image<Gray, byte> imgout = img.CopyBlank();
                //apply filters img
                sobel = img.Sobel(1, 0, 3).Add(imgout.Sobel(0, 1, 3)).AbsDiff(new Gray(0.0)).Convert<Gray, byte>().ThresholdBinary(new Gray(50), new Gray(255));
                OnSobelFilterReady(new ImgOut(sobel));

                Mat SE = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, new Size(10, 2), new Point(-1, -1));
                sobel = sobel.MorphologyEx(Emgu.CV.CvEnum.MorphOp.Dilate, SE, new Point(-1, -1), 1, Emgu.CV.CvEnum.BorderType.Reflect, new MCvScalar(255));
                OnRegionImgReady(new ImgOut(sobel));

                //find counturs
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

                foreach (var r in list)
                {
                    CvInvoke.Rectangle(imgout, r, new MCvScalar(255, 255, 255), -1);
                }
                OnImg3Ready(new ImgOut(imgout));

                imgout = img.Copy(imgout);
                //find counturs
                OnCoppedFromRegionImgReady(new ImgOut(imgout));

                //           imgout = imgout.Sobel(1, 0, 3).Add(imgout.Sobel(0, 1, 3)).AbsDiff(new Gray(0.0)).Convert<Gray, Byte>();

                //SE = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, new Size(10, 2), new Point(1, 1));
                //imgout = imgout.MorphologyEx(Emgu.CV.CvEnum.MorphOp.Erode, SE, new Point(1, 1), 1, Emgu.CV.CvEnum.BorderType.Reflect, new MCvScalar(255));

                Itt.TryDecode(list, imgout);
            }


        }

        protected virtual void OnRegionImgReady(ImgOut e)
        {
            RegionImgReady?.Invoke(this, e);
        }

        protected virtual void OnCoppedFromRegionImgReady(ImgOut e)
        {
            CoppedFromRegionImgReady?.Invoke(this, e);
        }

        protected virtual void OnImg3Ready(ImgOut e)
        {
            Img3Ready?.Invoke(this, e);

        }
        protected virtual void OnSobelFilterReady(ImgOut e)
        {
            SobelFilterReady?.Invoke(this, e);
        }
        public event ImgReady SobelFilterReady;
        public event ImgReady RegionImgReady;
        public event ImgReady CoppedFromRegionImgReady;
        public event ImgReady Img3Ready;

        bool TextProcessStaretd = false;
        ImgToText itt = new ImgToText();

        public ref ImgToText Itt { get  => ref itt; }
    }
    public class ImgOut : EventArgs
    {
        public ImgOut(Image<Gray, Byte> d)
        {
            Img = d.ToBitmap();
        }
        public Bitmap Img { get; set; }
    }
    public delegate void ImgReady(Object sender, ImgOut e);



}
