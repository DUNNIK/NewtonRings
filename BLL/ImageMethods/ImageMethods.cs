using System;
using System.Collections.Generic;
using Data;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using static Data.ImageData;

namespace BLL.ImageMethods
{
    public static class ImageMethods
    {
        public static int FindCircleCenterX()
        {
            return X;
        }

        public static int FindCircleCenterY()
        {
            return Y;
        }

        public static int FindCircleNum()
        {
            return PictureNum;
        }
        private static byte FindMinData(Image<Gray, byte> gray)
        {
            var min = byte.MaxValue;
            foreach (var data in gray.Data)
            {
                if (data < min)
                {
                    min = data;
                }
            }
            return min;
        }
        private static byte FindMaxData(Image<Gray, byte> gray)
        {
            var max = byte.MinValue;
            foreach (var data in gray.Data)
            {
                if (data > max)
                {
                    max = data;
                }
            }
            return max;
        }

        public static void AnalisePicture(Image<Bgr, byte> inputImage)
        {
            DataController.AddInputImage(inputImage);
            
            GrayImage = inputImage.SmoothGaussian(9).Convert<Gray, byte>();
            GrayWithoutEffect = GrayImage.Clone();
            GrayImage =
                GrayImage.ThresholdBinary(new Gray(FindMinData(GrayImage) + 18), new Gray(255));
            
            var hierarchy = new Mat();
            
            CvInvoke.FindContours(GrayImage, Contours, hierarchy, RetrType.External,
                ChainApproxMethod.LinkRuns);
            int numMax = 0,xMax = 0, yMax = 0;
            for (int i = 0; i < Contours.Size; i++)
            {
                double perimeter = CvInvoke.ArcLength(Contours[i], true);
                            
                VectorOfPoint approximation = new VectorOfPoint();
                            
                CvInvoke.ApproxPolyDP(Contours[i], approximation, 0.04 * perimeter, true);
                            
                Moments moments = CvInvoke.Moments(Contours[i]);
                            
                var x = (int) (moments.M10 / moments.M00);
                var y = (int) (moments.M01 / moments.M00);

                if (perimeter > 1000 && perimeter < 4000 && approximation.Size >= 6)
                {
                    numMax = i;
                    xMax = x;
                    yMax = y;
                }
            }

            X = xMax;
            Y = yMax;
            PictureNum = numMax;
        }

        public static void DrawContours()
        {
            CvInvoke.DrawContours(ImageForContour, Contours, PictureNum,
                new MCvScalar(0, 0, 255), 3);
        }

        public static List<Point> FindPointsForVisibilityFunction()
        {
            GrayWithoutEffect = GrayWithoutEffect.ThresholdBinary(new Gray(FindMinData(GrayWithoutEffect) + 30),
                new Gray(FindMaxData(GrayWithoutEffect) + 30));
            var result = new List<Point>();
            for (var i = X; i < InputImage.Width - 1; i++)
            {
                //var y = (double)GrayWithoutEffect.Data[Y - 1, i, 0] / 255;
                var y = (0.299 * InputImage.Data[Y - 1, i, 0] + 0.587 * InputImage.Data[Y - 1, i, 1] +
                         0.114 * InputImage.Data[Y - 1, i, 2]);
                var x = (double)(i - X);
                result.Add(new Point(x, y));
            }

            return result;
        }

        public static List<Point> ApproximationPoints(List<Point> points)
        {
            var points1 = new Points(points);
            var drawingPoints = points1.ConvertToDrawingPoints();
            var vectorPoints = new VectorOfPoint(drawingPoints);
            VectorOfPoint approximation = new VectorOfPoint();
            CvInvoke.ApproxPolyDP(vectorPoints, approximation, DataHandler.FindMaxInAllData(points).Count * 0.03, true);
            return ConvertApproximationToPoints(approximation);
        }

        private static List<Point> ConvertApproximationToPoints(VectorOfPoint approximation)
        {
            var result = new List<Point>();
            for (int i = 0; i < approximation.Size; i++)
            {
                result.Add(new Point(approximation[i].X, approximation[i].Y));
            }

            return result;
        }
    }
}