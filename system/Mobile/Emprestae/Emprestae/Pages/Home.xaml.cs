using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Windows.Navigation;
using Emprestae.Models;
using System.Diagnostics;

namespace Emprestae.Pages
{
    public partial class Home : PhoneApplicationPage
    {
        EmprestaeWebService emprestae = (Application.Current as App).emprestae;
        public Home()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (emprestae.userInfo == null)
            {
                emprestae.GetUserInfo(success, error);
            }
            else
            {
                userPanel.DataContext = emprestae.userInfo;
            }

            emprestae.GetAllUsersButMe(success, error);
        }

        void success(UserInfo response)
        {
            Dispatcher.BeginInvoke(() => {
                userPanel.DataContext = response;
            });
        }

        void success(UserResponse response)
        {
            Dispatcher.BeginInvoke(() => {
                if (response.status != 0)
                {
                    //friendListBox.ItemsSource = response.users;
                }
            });
        }

        void error()
        { 
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/Mapa.xaml",UriKind.Relative));
        }
    }
}