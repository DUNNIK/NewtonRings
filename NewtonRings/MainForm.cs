using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using BLL;
using BLL.Csv;
using BLL.ImageMethods;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using NewtonRings.Exceptions;
using Point = Data.Point;

namespace NewtonRings
{
    public partial class Form1 : Form
    {
        private enum GraphType
        {
            MonochromaticRadiationN1,
            MonochromaticRadiationN2,
            TwoWaves,
            WavelengthRange,
            DependenceRadiusRings,
            VisibilityFunctionTwoWaves,
            VisibilityFunctionWavelengthRange,
            VisibilityFunctionPicture,
            PictureGraphic
        }

        private bool _dotPlot = false;
        private GraphType _currentGraph;
        private readonly PicturesForm _picturesForm = new PicturesForm();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var openFileDialog1 = new OpenFileDialog {Filter = @"Csv File(*.csv)|*.csv|All files(*.*)|*.*"};

            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            var fileName = openFileDialog1.FileName;
            var data = CsvReader.ReadCsv(fileName);
            DataController.AddFirstData(data);
            DataController.AddFirstPoints(CsvConverter.ConvertDataToPoints(data));
            button1.Text = fileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var openFileDialog1 = new OpenFileDialog {Filter = @"Csv File(*.csv)|*.csv|All files(*.*)|*.*"};

            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            var fileName = openFileDialog1.FileName;
            var data = CsvReader.ReadCsv(fileName);
            DataController.AddSecondData(data);
            DataController.AddSecondPoints(CsvConverter.ConvertDataToPoints(data));
            button2.Text = fileName;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var openFileDialog1 = new OpenFileDialog {Filter = @"Csv File(*.csv)|*.csv|All files(*.*)|*.*"};

            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            var fileName = openFileDialog1.FileName;
            var data = CsvReader.ReadCsv(fileName);
            DataController.AddThirdData(data);
            DataController.AddThirdPoints(CsvConverter.ConvertDataToPoints(data));
            button3.Text = fileName;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var openFileDialog1 = new OpenFileDialog {Filter = @"Csv File(*.csv)|*.csv|All files(*.*)|*.*"};

            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            var fileName = openFileDialog1.FileName;
            var data = CsvReader.ReadCsv(fileName);
            DataController.AddFourthData(data);
            DataController.AddFourthPoints(CsvConverter.ConvertDataToPoints(data));
            button4.Text = fileName;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            _currentGraph = GraphType.MonochromaticRadiationN1;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            _currentGraph = GraphType.MonochromaticRadiationN2;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            _currentGraph = GraphType.TwoWaves;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            _currentGraph = GraphType.WavelengthRange;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            _currentGraph = GraphType.DependenceRadiusRings;
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            _currentGraph = GraphType.VisibilityFunctionTwoWaves;
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            _currentGraph = GraphType.VisibilityFunctionWavelengthRange;
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            try
            {
                switch (_currentGraph)
                {
                    case GraphType.MonochromaticRadiationN1:
                        if (_dotPlot)
                        {
                            await CreateOneLineDot(DataController.GetFirstPoints());
                            break;
                        }
                        await CreateOneLine(DataController.GetFirstPoints());
                        break;
                    case GraphType.MonochromaticRadiationN2:
                        if (_dotPlot)
                        {
                            await CreateOneLineDot(DataController.GetSecondPoints());
                            break;
                        }
                        await CreateOneLine(DataController.GetSecondPoints());
                        break;
                    case GraphType.TwoWaves:
                        if (_dotPlot)
                        {
                            await CreateOneLineDot(DataController.GetThirdPoints());
                            break;
                        }
                        await CreateOneLine(DataController.GetThirdPoints());
                        break;
                    case GraphType.WavelengthRange:
                        if (_dotPlot)
                        {
                            await CreateOneLineDot(DataController.GetFourthPoints());
                            break;
                        }
                        await CreateOneLine(DataController.GetFourthPoints());
                        break;
                    case GraphType.DependenceRadiusRings:
                        if (_dotPlot)
                        {
                            await CreateTwoLinesDot(
                                DataHandler.LinePointsInRadiusDependence(
                                    DataHandler.FindMaxInAllData(DataController.GetFirstPoints()),
                                    DataHandler.FindMinInAllData(DataController.GetFirstPoints())),
                                DataHandler.LinePointsInRadiusDependence(
                                    DataHandler.FindMaxInAllData(DataController.GetSecondPoints()),
                                    DataHandler.FindMinInAllData(DataController.GetSecondPoints())));
                            break;
                        }
                        await CreateTwoLines(
                            DataHandler.LinePointsInRadiusDependence(
                                DataHandler.FindMaxInAllData(DataController.GetFirstPoints()),
                                DataHandler.FindMinInAllData(DataController.GetFirstPoints())),
                            DataHandler.LinePointsInRadiusDependence(
                                DataHandler.FindMaxInAllData(DataController.GetSecondPoints()),
                                DataHandler.FindMinInAllData(DataController.GetSecondPoints())));
                        break;
                    case GraphType.VisibilityFunctionTwoWaves:
                        if (textBox1.Text == null) throw new TextBoxException();
                        var opticalPathDifference = VisibilityFunctionMethods.OpticalPathDifference(
                            VisibilityFunctionMethods.FindRAverage(
                                DataHandler.FindMaxInAllData(DataController.GetThirdPoints()),
                                DataHandler.FindMinInAllData(DataController.GetThirdPoints())),
                            DataHandler.FindRByN(
                                DataHandler.FindMaxInAllData(DataController.GetThirdPoints()),
                                double.Parse(textBox1.Text)));
                        var vExperimental = VisibilityFunctionMethods.FindVExperimental(
                            DataHandler.FindMaxInAllData(DataController.GetThirdPoints()),
                            DataHandler.FindMinInAllData(DataController.GetThirdPoints()));
                        var vTheoretical = VisibilityFunctionMethods.FindVTheoretical(
                            opticalPathDifference,
                            VisibilityFunctionMethods.FindL(opticalPathDifference),
                            VisibilityFunctionMethods.FindErrorL(
                                opticalPathDifference,
                                VisibilityFunctionMethods.FindL(opticalPathDifference)));
                        if (_dotPlot)
                        {
                            await CreateTwoLinesDot(
                                VisibilityFunctionMethods.CreatePointForVisibility(
                                    opticalPathDifference,
                                    vExperimental),
                                VisibilityFunctionMethods.CreatePointForVisibility(
                                    opticalPathDifference,
                                    vTheoretical));
                            break;
                        }
                        await CreateTwoLines(
                            VisibilityFunctionMethods.CreatePointForVisibility(
                                opticalPathDifference,
                                vExperimental),
                            VisibilityFunctionMethods.CreatePointForVisibility(
                                opticalPathDifference,
                                vTheoretical)
                        );
                        break;
                    case GraphType.VisibilityFunctionWavelengthRange:
                        if (textBox1.Text == null) throw new TextBoxException();
                        opticalPathDifference = VisibilityFunctionMethods.OpticalPathDifference(
                            VisibilityFunctionMethods.FindRAverage(
                                DataHandler.FindMaxInAllData(DataController.GetFourthPoints()),
                                DataHandler.FindMinInAllData(DataController.GetFourthPoints())),
                            DataHandler.FindRByN(
                                DataHandler.FindMaxInAllData(DataController.GetFourthPoints()),
                                double.Parse(textBox1.Text)));
                        vExperimental = VisibilityFunctionMethods.FindVExperimental(
                            DataHandler.FindMaxInAllData(DataController.GetFourthPoints()),
                            DataHandler.FindMinInAllData(DataController.GetFourthPoints()));
                        vTheoretical = VisibilityFunctionMethods.FindVTheoretical(
                            opticalPathDifference,
                            VisibilityFunctionMethods.FindL(opticalPathDifference),
                            VisibilityFunctionMethods.FindErrorL(
                                opticalPathDifference,
                                VisibilityFunctionMethods.FindL(opticalPathDifference)));
                        if (_dotPlot)
                        {
                            await CreateTwoLinesDot(
                                VisibilityFunctionMethods.CreatePointForVisibility(
                                    opticalPathDifference,
                                    vExperimental),
                                VisibilityFunctionMethods.CreatePointForVisibility(
                                    opticalPathDifference,
                                    vTheoretical));
                            break;
                        }
                        await CreateTwoLines(
                            VisibilityFunctionMethods.CreatePointForVisibility(
                                opticalPathDifference,
                                vExperimental),
                            VisibilityFunctionMethods.CreatePointForVisibility(
                                opticalPathDifference,
                                vTheoretical)
                        );
                        break;
                    case GraphType.VisibilityFunctionPicture:
                        if (textBox1.Text == null) throw new TextBoxException();
                        
                        if (ImageMethods.FindCircleNum() == 0 && ImageMethods.FindCircleCenterX() == 0 && ImageMethods.FindCircleCenterY() == 0)
                        {
                            MessageBox.Show(@"Попробуйте другую картинку. На этой не получается найти полное кольцо");
                            break;
                        }
                        ImageMethods.DrawContours();
                        _picturesForm.GetCenter(ImageMethods.FindCircleCenterX(), ImageMethods.FindCircleCenterY());
                        _picturesForm.GetOutputPicture(DataController.GetImageWithContour());
                        
                        var points = ImageMethods.FindPointsForVisibilityFunction();
                        var approximation = ImageMethods.ApproximationPoints(points);
                        
                        opticalPathDifference = VisibilityFunctionMethods.OpticalPathDifference(
                            VisibilityFunctionMethods.FindRAverage(
                                DataHandler.FindMaxInAllData(approximation),
                                DataHandler.FindMinInAllData(approximation)),
                            DataHandler.FindRByN(
                                DataHandler.FindMaxInAllData(approximation),
                                double.Parse(textBox1.Text)));
                        vExperimental = VisibilityFunctionMethods.FindVExperimental(
                            DataHandler.FindMaxInAllData(approximation),
                            DataHandler.FindMinInAllData(approximation));
                        if (_dotPlot)
                        {
                            await CreateOneLineDot(DataHandler.SortPointsByX(VisibilityFunctionMethods.CreatePointForVisibility(
                                opticalPathDifference,
                                vExperimental)));
                            break;
                        }
                        await CreateOneLine(DataHandler.SortPointsByX(VisibilityFunctionMethods.CreatePointForVisibility(
                            opticalPathDifference,
                            vExperimental)));
                        break;
                    case GraphType.PictureGraphic:
                        if (ImageMethods.FindCircleNum() == 0 && ImageMethods.FindCircleCenterX() == 0 && ImageMethods.FindCircleCenterY() == 0)
                        {
                            MessageBox.Show(@"Попробуйте другую картинку. На этой не получается найти полное кольцо");
                            break;
                        }
                        ImageMethods.DrawContours();
                        _picturesForm.GetCenter(ImageMethods.FindCircleCenterX(), ImageMethods.FindCircleCenterY());
                        _picturesForm.GetOutputPicture(DataController.GetImageWithContour());
                        
                        points = ImageMethods.FindPointsForVisibilityFunction();
                        approximation = ImageMethods.ApproximationPoints(points);
                        if (_dotPlot)
                        {
                            await CreateTwoLinesDot(points, approximation);
                            break;
                        }
                        //await CreateOneLine(points);
                        await CreateTwoLines(points, approximation);
                        //await CreateOneLine(approximation);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
        private async Task CreateOneLine(List<Point> points)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[2].Points.Clear();
            chart1.Series[3].Points.Clear();
            chart1.BorderWidth = 2;

            foreach (var result in points)
            {
                await Task.Delay(1);
                chart1.Series[0].Points.AddXY(result.X, result.Y);
            }
        }
        private async Task CreateTwoLines(List<Point> points1, List<Point> points2)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[2].Points.Clear();
            chart1.Series[3].Points.Clear();
            chart1.BorderWidth = 2;
            foreach (var result in points1)
            {
                await Task.Delay(1);
                chart1.Series[0].Points.AddXY(result.X, result.Y);
            }
            foreach (var result in points2)
            {
                await Task.Delay(1);
                chart1.Series[1].Points.AddXY(result.X, result.Y);
            }
        }
        private async Task CreateOneLineDot(List<Point> points)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[2].Points.Clear();
            chart1.Series[3].Points.Clear();
            chart1.BorderWidth = 2;

            foreach (var result in points)
            {
                await Task.Delay(1);
                chart1.Series[2].Points.AddXY(result.X, result.Y);
            }
        }
        private async Task CreateTwoLinesDot(List<Point> points1, List<Point> points2)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[2].Points.Clear();
            chart1.Series[3].Points.Clear();
            chart1.BorderWidth = 2;
            foreach (var result in points1)
            {
                await Task.Delay(1);
                chart1.Series[2].Points.AddXY(result.X, result.Y);
            }
            foreach (var result in points2)
            {
                await Task.Delay(1);
                chart1.Series[3].Points.AddXY(result.X, result.Y);
            }
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9')
            {
                return;
            }
            if (e.KeyChar == '.')
            {
                e.KeyChar = ',';
            }

            if (e.KeyChar == ',')
            {
                if (textBox1.Text.IndexOf(',') != -1)
                {
                    e.Handled = true;
                }
                return;
            }

            if (Char.IsControl( (e.KeyChar)))
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    button1.Focus();
                }
                return;
            }
            e.Handled = true;
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9')
            {
                return;
            }
            if (e.KeyChar == '.')
            {
                e.KeyChar = ',';
            }

            if (e.KeyChar == ',')
            {
                if (textBox2.Text.IndexOf(',') != -1)
                {
                    e.Handled = true;
                }
                return;
            }

            if (Char.IsControl( (e.KeyChar)))
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    button1.Focus();
                }
                return;
            }
            e.Handled = true;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9')
            {
                return;
            }
            if (e.KeyChar == '.')
            {
                e.KeyChar = ',';
            }

            if (e.KeyChar == ',')
            {
                if (textBox3.Text.IndexOf(',') != -1)
                {
                    e.Handled = true;
                }
                return;
            }

            if (Char.IsControl( (e.KeyChar)))
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    button1.Focus();
                }
                return;
            }
            e.Handled = true;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9')
            {
                return;
            }
            if (e.KeyChar == '.')
            {
                e.KeyChar = ',';
            }

            if (e.KeyChar == ',')
            {
                if (textBox4.Text.IndexOf(',') != -1)
                {
                    e.Handled = true;
                }
                return;
            }

            if (Char.IsControl( (e.KeyChar)))
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    button1.Focus();
                }
                return;
            }
            e.Handled = true;
        }

        private void chart_GetToolTipText(object sender, ToolTipEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(e.Text))
                return;

            Console.WriteLine(e.HitTestResult.ChartElementType);

            switch (e.HitTestResult.ChartElementType)
            {
                case ChartElementType.DataPoint:
                case ChartElementType.DataPointLabel:
                case ChartElementType.Gridlines:
                case ChartElementType.Axis:
                case ChartElementType.TickMarks:
                case ChartElementType.PlottingArea:
                    var area = chart1.ChartAreas[0];

                    var areaPosition = area.Position;

                    var areaRect = new RectangleF(areaPosition.X * chart1.Width / 100,
                        areaPosition.Y * chart1.Height / 100,
                        areaPosition.Width * chart1.Width / 100, areaPosition.Height * chart1.Height / 100);

                    var innerPlot = area.InnerPlotPosition;

                    double x = area.AxisX.Minimum +
                               (area.AxisX.Maximum - area.AxisX.Minimum) *
                               (e.X - areaRect.Left - innerPlot.X * areaRect.Width / 100) /
                               (innerPlot.Width * areaRect.Width / 100);
                    double y = area.AxisY.Maximum -
                               (area.AxisY.Maximum - area.AxisY.Minimum) *
                               (e.Y - areaRect.Top - innerPlot.Y * areaRect.Height / 100) /
                               (innerPlot.Height * areaRect.Height / 100);

                    Console.WriteLine($@"{x:F2} {y:F2}");
                    e.Text = $"{x:F2} {y:F2}";
                    break;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var openFileDialog1 = new OpenFileDialog {Filter = @"Bmp(*.bmp)|*.bmp"};

            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            var fileName = openFileDialog1.FileName;
            try
            {
                var inputImage = new Image<Bgr, byte>(fileName);
                _picturesForm.Show();
                _picturesForm.GetInputPicture(inputImage);
                ImageMethods.AnalisePicture(inputImage);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            
            button6.Text = fileName;
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            _currentGraph = GraphType.VisibilityFunctionPicture;
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            _currentGraph = GraphType.PictureGraphic;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                _dotPlot = true;
            }
            else
            {
                _dotPlot = false;
            }
        }
    }
}