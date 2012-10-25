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
using Emprestae.Models;

namespace Emprestae.Pages
{
    public partial class SearchUser : PhoneApplicationPage
    {
        EmprestaeWebService emprestae;
        public SearchUser()
        {
            InitializeComponent();
            emprestae = (Application.Current as App).emprestae;
        }

        private void BuscarButton_OnClick(object sender, EventArgs e)
        {
            loadingLayer.Visibility = Visibility.Visible;
            emprestae.GetAllUserByName(input.Text, success, error);
        }

        private void amigoItem_OnClick(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string email = ((sender as Border).DataContext as UserArray).user.email;
            NavigationService.Navigate(new Uri("/Pages/ViewUser.xaml?friendEmail=" + email, UriKind.Relative));
        }

        #region Callbacks

        void success(UserResponse response)
        {
            Dispatcher.BeginInvoke( () =>
            {
                loadingLayer.Visibility = Visibility.Collapsed;
                if (response != null)
                    amigosListBox.ItemsSource = response.users;
            });
        }

        void error()
        {
        }

        #endregion
    }
}