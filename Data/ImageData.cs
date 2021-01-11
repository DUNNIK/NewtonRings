using System.Net.Mime;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace Data
{
    public static class ImageData
    {
        public static int X;
        public static int Y;
        public static int PictureNum;
        public static Image<Bgr, byte> InputImage;
        public static Image<Bgr, byte> ImageForContour;
        public static Image<Gray, byte> GrayImage;
        public static readonly VectorOfVectorOfPoint Contours = new VectorOfVectorOfPoint();
        public static Image<Gray, byte> GrayWithoutEffect;

        public static void CloneForContour(Image<Bgr, byte> inputImage)
        {
            ImageForContour = new Image<Bgr, byte>(inputImage.Data);
        }
    }
}