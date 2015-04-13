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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Ekonometria
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Data : Page
    {
        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

        public Data()
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            int count = Find_Last_Empty();

            for (int i = 1; i < count; i++)
            {
                TextBlockData.Text += "x" + i + ": " + (string)localSettings.Values["x" + i] + "      " + "y" + i + ": " + (string)localSettings.Values["y" + i] + "\n";
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void ButtonRemoveLast_Click(object sender, RoutedEventArgs e)
        {
            int count = Find_Last_Empty();
            count--;
            localSettings.Values["x" + count] = null;
            localSettings.Values["y" + count] = null;

            Frame.Navigate(typeof(Data));
        }

        private void ButtonRemoveAll_Click(object sender, RoutedEventArgs e)
        {
            int count = Find_Last_Empty();

            for (int i = 0; i <= count; i++)
            {
                localSettings.Values["x" + i] = null;
                localSettings.Values["y" + i] = null;
            }

            Frame.Navigate(typeof(Data));
        }
    }
}
