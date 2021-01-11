using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;

namespace NewtonRings
{
    public partial class PicturesForm : Form
    {
        public PicturesForm()
        {
            InitializeComponent();
        }

        public void GetInputPicture(Image<Bgr, byte> inputPicture)
        {
            pictureBox1.Image = inputPicture.Bitmap;
        }
        public void GetOutputPicture(Image<Gray, byte> outputPicture)
        {
            pictureBox2.Image = outputPicture.Bitmap;
        }
        public void GetOutputPicture(Image<Bgr, byte> outputPicture)
        {
            pictureBox2.Image = outputPicture.Bitmap;
        }

        public void GetCenter(int x, int y)
        {
            var text = "Circle Center: " + $"X: {x}\t" + $"Y: {y}" + "\n";
            richTextBox1.Text += text;
        }
    }
}