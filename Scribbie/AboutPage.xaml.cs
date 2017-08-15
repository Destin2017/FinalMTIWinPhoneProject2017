using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

namespace ProjectWinphone2017
    {
    public partial class AboutPage : PhoneApplicationPage
        {
        public AboutPage()
            {
            InitializeComponent();
            }

        private void EmailCompose_Click(object sender, RoutedEventArgs e)
            {

            EmailComposeTask emailComposeTask = new EmailComposeTask();

            emailComposeTask.Subject = "message subject";
            emailComposeTask.Body = "message body";
            emailComposeTask.To = "destintous@example.com";
            emailComposeTask.Cc = "joseph@example.com";
            emailComposeTask.Bcc = "sharon@example.com";

            emailComposeTask.Show();
            }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {

        }
        }
    }