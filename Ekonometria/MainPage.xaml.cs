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

        public MainPage()
        {
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
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
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
