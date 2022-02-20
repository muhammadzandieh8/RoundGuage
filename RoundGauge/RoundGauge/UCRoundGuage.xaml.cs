using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.ComponentModel;
using System.Windows.Shapes;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace RoundGauge
{
    /// <summary>
    /// Interaction logic for UCRoundGuage.xaml
    /// </summary>
    /// 
    public partial class UCRoundGuage : UserControl
    {
        #region Params
        public gaugeViewModel _GuageVM = new gaugeViewModel();
        #region ZSize
        public int ZSize
        {
            get { return (int)GetValue(ZSizeProperty); }
            set
            {
                SetValue(ZSizeProperty, value);
                myUCRoundGuage_Loaded(null, null);
            }
        }
        public static DependencyProperty ZSizeProperty = DependencyProperty.Register("ZSize", typeof(int), typeof(UCRoundGuage), new PropertyMetadata(230, OnChangeZSize));
        private static void OnChangeZSize(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ct = d as UCRoundGuage;

            ct.UpdateZSize((int)e.NewValue);
        }
        public void UpdateZSize(int _size)
        {

            MainPlate.Width = ZSize - ZBorder;
            MainPlate.Height = ZSize - ZBorder;

            Arrow.Width = ZArrowWidth;
            Arrow.Height = MainPlate.Width / 2;
            Arrow.Margin = new Thickness(0, Arrow.Height, 0, 0);

            BehindPin.Width = MainPlate.Width / 8;
            BehindPin.Height = MainPlate.Height / 8;

            FrontPin.Width = MainPlate.Width / 10;
            FrontPin.Height = MainPlate.Height / 10;

            alio.CenterX = MainPlate.Width / 16;
            alio.CenterY = MainPlate.Height / 16;


            MainCanvas.Children.Clear();
            BigDegreelLine();
            SmalDegreelLine();
        }
        #endregion Zwidth
        #region ZBorder
        public int ZBorder
        {
            get { return (int)GetValue(ZBorderProperty); }
            set
            {
                SetValue(ZBorderProperty, value);
                myUCRoundGuage_Loaded(null, null);
            }
        }
        public static DependencyProperty ZBorderProperty = DependencyProperty.Register("ZBorder", typeof(int), typeof(UCRoundGuage), new FrameworkPropertyMetadata(30));
        #endregion
        #region ZArrowWidth
        public int ZArrowWidth
        {
            get { return (int)GetValue(ZArrowWidthProperty); }
            set
            {
                SetValue(ZArrowWidthProperty, value);
                myUCRoundGuage_Loaded(null, null);
            }
        }
        public static DependencyProperty ZArrowWidthProperty = DependencyProperty.Register("ZArrowWidth", typeof(int), typeof(UCRoundGuage), new FrameworkPropertyMetadata(10));
        #endregion
        #region ZSizeOfLabel
        public int ZSizeOfLabel
        {
            get { return (int)GetValue(ZSizeOfLabelProperty); }
            set
            {
                SetValue(ZSizeOfLabelProperty, value);
                myUCRoundGuage_Loaded(null, null);
            }
        }
        public static DependencyProperty ZSizeOfLabelProperty = DependencyProperty.Register("ZSizeOfLabel", typeof(int), typeof(UCRoundGuage), new FrameworkPropertyMetadata(30));
        #endregion ZSizeOfLabel
        #region ZNumberOfLargeDegreeLines
        public int ZNumberOfLargeDegreeLines
        {
            get { return (int)GetValue(ZNumberOfLargeDegreeLinesProperty); }
            set
            {
                SetValue(ZNumberOfLargeDegreeLinesProperty, value);
                myUCRoundGuage_Loaded(null, null);
            }
        }
        public static DependencyProperty ZNumberOfLargeDegreeLinesProperty = DependencyProperty.Register("ZNumberOfLargeDegreeLines", typeof(int), typeof(UCRoundGuage), new FrameworkPropertyMetadata(8));
        #endregion ZNumberOfLargeDegreeLines
        #region ZNumberOfSmallDegreeLinesh
        public int ZNumberOfSmallDegreeLinesh
        {
            get { return (int)GetValue(ZNumberOfSmallDegreeLinesProperty); }
            set
            {
                SetValue(ZNumberOfSmallDegreeLinesProperty, value);
            }
        }
        public static DependencyProperty ZNumberOfSmallDegreeLinesProperty = DependencyProperty.Register("ZNumberOfSmallDegreeLinesh", typeof(int), typeof(UCRoundGuage), new FrameworkPropertyMetadata(180));
        #endregion
        #endregion Params
        #region Form Loaded
        private void myUCRoundGuage_Loaded(object sender, RoutedEventArgs e)
        {
            MainPlate.Width = ZSize - ZBorder;
            MainPlate.Height = ZSize - ZBorder;

            Arrow.Width = ZArrowWidth;
            Arrow.Height = MainPlate.Width / 2;
            Arrow.Margin = new Thickness(0, Arrow.Height, 0, 0);

            BehindPin.Width = MainPlate.Width / 8;
            BehindPin.Height = MainPlate.Height / 8;

            FrontPin.Width = MainPlate.Width / 10;
            FrontPin.Height = MainPlate.Height / 10;

            alio.CenterX = MainPlate.Width / 16;
            alio.CenterY = MainPlate.Height / 16;

            BigDegreelLine();
            SmalDegreelLine();
        }
        #endregion
        #region BigDegreelLine
        private void BigDegreelLine()
        {
            double radius = MainPlate.Width / 2;

            double min = 0; double max = ZNumberOfLargeDegreeLines;
            double step = 360.0 / (max - min);


            for (int i = 0; i < max - min; i++)
            {
                radius = MainPlate.Width / 2;
                Line lineScale = new Line
                {
                    X1 = ((radius - 20) * Math.Cos(i * step * Math.PI / 180)) + radius,
                    Y1 = ((radius - 20) * Math.Sin(i * step * Math.PI / 180)) + radius,
                    X2 = (radius * Math.Cos(i * step * Math.PI / 180)) + radius,
                    Y2 = (radius * Math.Sin(i * step * Math.PI / 180)) + radius,
                    Stroke = Brushes.DarkGray,
                    StrokeThickness = 4
                };
                #region Draw Number
                radius = MainPlate.Width / 2;
                //radius -= 30;
                double X1 = ((radius - 20 - ZSizeOfLabel) * Math.Sin(i * step * Math.PI / 180)) + radius;
                double Y1 = ((radius - 20 - ZSizeOfLabel) * -Math.Cos(i * step * Math.PI / 180)) + radius;
                double X2 = ((radius - ZSizeOfLabel) * Math.Sin(i * step * Math.PI / 180)) + radius;
                double Y2 = ((radius - ZSizeOfLabel) * -Math.Cos(i * step * Math.PI / 180)) + radius;
                Label lbl = new Label();
                lbl.Content = 360 / max * i;
                lbl.VerticalAlignment = VerticalAlignment.Top;
                lbl.HorizontalAlignment = HorizontalAlignment.Left;
                lbl.Width = ZSizeOfLabel;
                lbl.Height = ZSizeOfLabel;
                lbl.Margin = new Thickness(X2 - ZSizeOfLabel / 2, Y2 - ZSizeOfLabel / 2, 0, 0);
                lbl.Foreground = Brushes.WhiteSmoke;
                #endregion
                MainCanvas.Children.Add(lineScale);
                MainCanvas.Children.Add(lbl);

            }

        }
        #endregion BigDegreelLine
        #region SmalDegreelLine
        private void SmalDegreelLine()
        {
            double radius = MainPlate.Width / 2;

            double min = 0; double max = ZNumberOfSmallDegreeLinesh;
            double step = 360.0 / (max - min);

            for (int i = 0; i < max - min; i++)
            {
                Line lineScale = new Line
                {
                    X1 = ((radius - 10) * Math.Cos(i * step * Math.PI / 180)) + radius,
                    Y1 = ((radius - 10) * Math.Sin(i * step * Math.PI / 180)) + radius,
                    X2 = (radius * Math.Cos(i * step * Math.PI / 180)) + radius,
                    Y2 = (radius * Math.Sin(i * step * Math.PI / 180)) + radius,
                    Stroke = Brushes.DarkGray,
                    StrokeThickness = 2
                };

                MainCanvas.Children.Add(lineScale);
            }

        }
        #endregion
        public UCRoundGuage()
        {
            InitializeComponent();
            this.DataContext = _GuageVM;
        }
    }
    #region gaugeViewModel
    public class gaugeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        public gaugeViewModel()
        {
            Value = 0;
            Angle = 0;
        }
        int _angle;
        public int Angle
        {
            get
            {
                return _angle;
            }
            private set
            {
                _angle = value;
                NotifyPropertyChanged("Angle");
            }
        }
        int _value;
        public int Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                Angle = value;
                NotifyPropertyChanged("Value");
            }
        }
        int _CurrentValue;
        public int CurrentValue
        {
            get
            {
                return _CurrentValue;
            }
            set
            {
                _CurrentValue = value;
                Value = (value + 180) % 360;
                Angle = value;
                NotifyPropertyChanged("CurrentValue");
            }
        }
    }
    #endregion
}
