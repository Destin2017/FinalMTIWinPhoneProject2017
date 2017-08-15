using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace ProjectWinphone2017
    {
    public partial class MenuPage : PhoneApplicationPage
        {
        public MenuPage()
            {
            InitializeComponent();
            }

        private void GoToNotes_button_Click(object sender, RoutedEventArgs e)
            {

            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));

            }

        private void GoToSettings_button_Click(object sender, RoutedEventArgs e)
            {

            NavigationService.Navigate(new Uri("/SettingsPage.xaml", UriKind.Relative));

            }

        private void GoToAbout_button_Click(object sender, RoutedEventArgs e)
            {

            NavigationService.Navigate(new Uri("/AboutPage.xaml", UriKind.Relative));

            }
        }
    }