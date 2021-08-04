using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace Lab2_Paint_
{
    enum Instrument
    {
        brush,
        line
    }
    struct Line
    {
        public Point pointStart;
        public Point pointEnd;
        public bool drawing;
    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            brushСolor = Brushes.Black;
            currentInstrument = Instrument.brush;
        }

        Lab2_Paint_.Line line;
        Brush brushСolor;
        Instrument currentInstrument;
        int thickness;
        private void StartDrawingLine(object sender, MouseButtonEventArgs e)
        {
            if (currentInstrument == Instrument.brush)
                return;

            line.pointStart = e.GetPosition(this);
            line.drawing = false;
            canvas.EditingMode = InkCanvasEditingMode.None;
        }

        private void UpdateLine(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (currentInstrument == Instrument.brush || line.pointStart.Y == 0)
                return;

            if (line.drawing == true)
                return;

            canvas.EditingMode = InkCanvasEditingMode.Ink;
            line.pointEnd = e.GetPosition(this);
            var _line = new System.Windows.Shapes.Line();
            _line.X1 = line.pointStart.X;
            _line.X2 = line.pointEnd.X;
            _line.Y1 = line.pointStart.Y - 85;
            _line.Y2 = line.pointEnd.Y - 85;
            _line.Stroke = brushСolor;
            _line.StrokeThickness = thickness;
            canvas.Children.Add(_line);
            if (canvas.Children.Count > 2)
                canvas.Children.RemoveAt(canvas.Children.Count - 2);
        }

        private void DrawLine(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (line.pointStart.Y == 0)
                return;

            var _line = new System.Windows.Shapes.Line();
            _line.X1 = line.pointStart.X;
            _line.X2 = line.pointEnd.X;
            _line.Y1 = line.pointStart.Y - 85;
            _line.Y2 = line.pointEnd.Y - 85;
            _line.Stroke = brushСolor;
            _line.StrokeThickness = thickness;
            canvas.Children.Add(_line);
            line.pointStart = new Point(0, 0);
        }

        private void ChangeColor(object sender, RoutedEventArgs e)
        {
            var dlg = new ColorDialog();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                brushСolor = new SolidColorBrush(Color.FromArgb(dlg.Color.A, dlg.Color.R, dlg.Color.G, dlg.Color.B));
                canvas.DefaultDrawingAttributes.Color = Color.FromArgb(dlg.Color.A, dlg.Color.R, dlg.Color.G, dlg.Color.B);
                buttonChangeColor.Background = brushСolor;
            }
        }

        private void SelectedInstrumentLine(object sender, RoutedEventArgs e)
        {
            currentInstrument = Instrument.line;
        }

        private void SelectedInstrumentBrush(object sender, RoutedEventArgs e)
        {
            currentInstrument = Instrument.brush;
        }

        private void ChangeThickness(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ((Slider)sender).SelectionEnd = e.NewValue;
            thickness = (int)e.NewValue;
            if (thicknessValue == null)
                return;
            
            thicknessValue.Text = ((int)e.NewValue).ToString();
        }
    }
}
