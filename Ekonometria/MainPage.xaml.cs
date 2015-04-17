using System;
using System.Windows;
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
using Windows.UI.Popups;
using Windows.Storage;
using System.Text;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
using Windows.UI.StartScreen;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace Ekonometria
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

        private void TileUpdate(String text){
            StringBuilder sb = new StringBuilder("<tile>");
            sb.Append("<visual version=\"2\">");
            sb.Append("<binding template=\"TileSquare150x150Text04\" fallback=\"TileSquareText04\">");
            sb.Append("<image id=\"1\" src=\"ms-appx:///Assets/Logo.scale-240.png\"/>");
            sb.Append("<text id='1'>"+text+"</text>");
            sb.Append("</binding>");
            sb.Append("</visual>");
            sb.Append("</tile>");
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(sb.ToString());
            TileNotification tileNotification = new TileNotification(xmldoc);
            TileUpdater tileUpdator = TileUpdateManager.CreateTileUpdaterForApplication();
            tileUpdator.Update(tileNotification);
        }

        public MainPage()
        {

            TileUpdate("No data");

            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

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


        private async void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            if (InputX.Text == "" || InputY.Text == "")
            {
                MessageDialog msgbox = new MessageDialog("X or Y is empty");
                await msgbox.ShowAsync(); 
            }
            else
            {
                int pos = Find_Last_Empty();
                TileUpdate("Count: "+(pos));
                localSettings.Values["x" + pos] = InputX.Text;
                localSettings.Values["y" + pos] = InputY.Text;
                InputX.Text = "";
                InputY.Text = "";
            }

        }

        private void ButtonSeeData_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Data));
        }

        private async void ButtonCompute_Click(object sender, RoutedEventArgs e)
        {

            int last = Find_Last_Empty();
            if (last < 4)
            {
                MessageDialog msgbox = new MessageDialog("You need at least 3 points");
                await msgbox.ShowAsync(); 
            }
            else
            {
                Frame.Navigate(typeof(Result));
            }

            
        }
    }
}
