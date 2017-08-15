using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;

namespace ProjectWinphone2017
    {
    public partial class SettingsPage : PhoneApplicationPage
        {

        private IsolatedStorageSettings settings;

        public SettingsPage()
            {
            InitializeComponent();
            settings = IsolatedStorageSettings.ApplicationSettings;
            }

        protected override void OnNavigatedTo(NavigationEventArgs e)
            {
            System.Diagnostics.Debug.WriteLine("into the app");

            try
                {
                System.Diagnostics.Debug.WriteLine("Retrieving values:");
                backAudio.IsChecked = (bool)settings["backaudio"];
                }

            catch (KeyNotFoundException)
                {
                System.Diagnostics.Debug.WriteLine("First time in the app");
                settings.Add("backaudio", false);
                settings.Save();
                }

            finally
                {
                System.Diagnostics.Debug.WriteLine("Exiting, so save now");
                settings["backaudio"] = backAudio.IsChecked;
                settings.Save();
                }

            }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
            {

            }

        private void Accent_Click(object sender, RoutedEventArgs e)
            {

            try
                {
                settings.Add("email", "someone@contoso.com");
                //tbResults.Text = "Settings stored.";
                }
            catch (ArgumentException)
                {
               // tbResults.Text = ex.Message;
                }
            }

        private void Transparent_Click(object sender, RoutedEventArgs e)
            {
            
            }

        }
    }