using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using OxyPlot;
using OxyPlot.Series;

namespace Ekonometria
{

    public sealed partial class Result : Page
    {
        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;


        public Result()
        {
            this.InitializeComponent();
        }

        private int Find_Last_Empty()
        {
            int i = 1;
            while (true)
            {
                if (localSettings.Values["x" + i] == null) return i;
                i++;
            }
        }

        private int Count_Sum_x()
        {
            int last = Find_Last_Empty();
            int sum = 0;
            for (int i = 1; i < last; i++)
            {
                string val = localSettings.Values["x" + i].ToString();
                sum += Convert.ToInt16(val);
            }
            return sum;
        }

        private int Count_Sum_y()
        {
            int last = Find_Last_Empty();
            int sum = 0;
            for (int i = 1; i < last; i++)
            {
                string val = localSettings.Values["y" + i].ToString();
                sum += Convert.ToInt32(val);
            }
            return sum;
        }

        private double Count_Average_x()
        {
            int last = Find_Last_Empty();
            double sum = 0;
            for (int i = 1; i < last; i++)
            {
                string val = localSettings.Values["x" + i].ToString();
                sum += Convert.ToDouble(val);
            }
            if ((last - 1) == 0)
            {
                return 0;
            }
            return (sum / (last-1));
        }

        private double Count_Average_y()
        {
            int last = Find_Last_Empty();
            double sum = 0;
            for (int i = 1; i < last; i++)
            {
                string val = localSettings.Values["y" + i].ToString();
                sum += Convert.ToDouble(val);
            }
            if ((last - 1) == 0)
            {
                return 0;
            }
            return (sum / (last - 1));
        }

        private double Count_Beta()
        {
            int last = Find_Last_Empty();
            double sum = 0;
            double sumM = 0;
            double avex = Count_Average_x();
            double avey = Count_Average_y();

            for (int i = 1; i < last; i++)
            {
                string valx = localSettings.Values["x" + i].ToString();
                string valy = localSettings.Values["y" + i].ToString();
                sumM += ((Convert.ToDouble(valx) - avex) * (Convert.ToDouble(valx) - avex));
            }

            for (int i = 1; i < last; i++)
            {
                string valx = localSettings.Values["x" + i].ToString();
                string valy = localSettings.Values["y" + i].ToString();
                sum += ((Convert.ToDouble(valx) - avex) * (Convert.ToDouble(valy) - avey));
            }
            return sum/sumM;
        }

        private double Count_Alpha()
        {
            double avey = Count_Average_y();
            double avex = Count_Average_x();
            double Beta = Count_Beta();
            return avey - Beta * avex;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SumX.Text = Count_Sum_x().ToString();
            SumY.Text = Count_Sum_y().ToString();
            if (Count_Average_x().ToString().Length > 4)
            {
                AveX.Text = Count_Average_x().ToString().Substring(0, 4);
            }
            else
            {
                AveX.Text = Count_Average_x().ToString();
            }
            if (Count_Average_y().ToString().Length > 4)
            {
                AveY.Text = Count_Average_y().ToString().Substring(0, 4);
            }
            else
            {
                AveY.Text = Count_Average_y().ToString();
            }
            if (Count_Beta().ToString().Length > 4)
            {
                Beta.Text = Count_Beta().ToString().Substring(0, 4);
            }
            else
            {
                 Beta.Text = Count_Beta().ToString();
            }

            if (Count_Alpha().ToString().Length > 4)
            {
                Alph.Text = Count_Alpha().ToString().Substring(0, 4);
            }
            else
            {
                Alph.Text = Count_Alpha().ToString();
            }
            

            
        }


        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }

    public class MainViewModel
    {

        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

        private int Find_Last_Empty()
        {
            int i = 1;
            while (true)
            {
                if (localSettings.Values["x" + i] == null) return i;
                i++;
            }
        }

        private int Count_Sum_x()
        {
            int last = Find_Last_Empty();
            int sum = 0;
            for (int i = 1; i < last; i++)
            {
                string val = localSettings.Values["x" + i].ToString();
                sum += Convert.ToInt16(val);
            }
            return sum;
        }

        private int Count_Sum_y()
        {
            int last = Find_Last_Empty();
            int sum = 0;
            for (int i = 1; i < last; i++)
            {
                string val = localSettings.Values["y" + i].ToString();
                sum += Convert.ToInt32(val);
            }
            return sum;
        }

        private double Count_Average_x()
        {
            int last = Find_Last_Empty();
            int sum = 0;
            for (int i = 1; i < last; i++)
            {
                string val = localSettings.Values["x" + i].ToString();
                sum += Convert.ToInt32(val);
            }
            if ((last - 1) == 0)
            {
                return 0;
            }
            return sum / (last-1);
        }

        private double Count_Average_y()
        {
            int last = Find_Last_Empty();
            int sum = 0;
            for (int i = 1; i < last; i++)
            {
                string val = localSettings.Values["y" + i].ToString();
                sum += Convert.ToInt32(val);
            }
            if ((last - 1) == 0)
            {
                return 0;
            }
            return sum / (last - 1);
        }

        private double Count_Beta()
        {
            int last = Find_Last_Empty();
            double sum = 0;
            double sumM = 0;
            double avex = Count_Average_x();
            double avey = Count_Average_y();

            for (int i = 1; i < last; i++)
            {
                string valx = localSettings.Values["x" + i].ToString();
                string valy = localSettings.Values["y" + i].ToString();
                sumM += ((Convert.ToDouble(valx) - avex) * (Convert.ToDouble(valx) - avex));
            }

            for (int i = 1; i < last; i++)
            {
                string valx = localSettings.Values["x" + i].ToString();
                string valy = localSettings.Values["y" + i].ToString();
                sum += ((Convert.ToDouble(valx) - avex) * (Convert.ToDouble(valy) - avey));
            }
            return sum / sumM;
        }

        private double Count_Alpha()
        {
            double avey = Count_Average_y();
            double avex = Count_Average_x();
            double Beta = Count_Beta();
            return avey - Beta * avex;
        }

        private int minimal(){
            int min = 99999;
            int i = 1;
            while (localSettings.Values["x" + i] != null)
            {
                string val = localSettings.Values["x" + i].ToString(); 
                if (Convert.ToInt32(val) < min)
                {
                    min = Convert.ToInt32(val);
                }
                i++;
            }
            return min;
        }

        private int maximum()
        {
            int max = 0;
            int i = 1;
            while (localSettings.Values["x" + i] != null)
            {
                string val = localSettings.Values["x" + i].ToString();
                if (Convert.ToInt32(val) > max)
                {
                    max = Convert.ToInt32(val);
                }
                i++;
            }
            return max;
        }

        private double function(double x)
        {
          double a = Count_Alpha();
          double b = Count_Beta();
          return b * x + a;
        }



        public MainViewModel()
        {
            if (localSettings.Values["x1"] == null)
            {
                this.MyModel = new PlotModel { };
            }
            else {
                int min = minimal();
                int max = maximum();
                this.MyModel = new PlotModel { };
                var lineSeries = new LineSeries();
                int i = 1;

                while (localSettings.Values["x" + i] != null)
                {
                    string valx = localSettings.Values["x" + i].ToString();
                    string valy = localSettings.Values["y" + i].ToString();
                    lineSeries.Points.Add(new DataPoint(Convert.ToDouble(valx), Convert.ToDouble(valy)));
                    i++;
                }
                lineSeries.Color = OxyPlot.OxyColors.Transparent;
                lineSeries.MarkerFill = OxyPlot.OxyColors.SteelBlue;
                lineSeries.MarkerType = OxyPlot.MarkerType.Circle;
                string a = null;
                string b = null;
                if (Count_Beta().ToString().Length > 4)
                {
                    b = Count_Beta().ToString().Substring(0, 4);
                }
                else
                {
                    b = Count_Beta().ToString();
                }

                if (Count_Alpha().ToString().Length > 4)
                {
                    a = Count_Alpha().ToString().Substring(0, 4);
                }
                else
                {
                    a = Count_Alpha().ToString();
                }
                if(Convert.ToDouble(a)>0){
                    this.MyModel.Series.Add(new FunctionSeries(function, min - 2, max + 2, 0.1, "f(x)=" + b + "x+" + a));
                }
                else{
                    this.MyModel.Series.Add(new FunctionSeries(function, min - 2, max + 2, 0.1, "f(x)=" + b + "x" + a));
                }
                
                this.MyModel.Series.Add(lineSeries);
            }
        }

        public PlotModel MyModel { get; private set; }
    }
}
