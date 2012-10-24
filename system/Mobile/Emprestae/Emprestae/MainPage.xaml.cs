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
using System.Diagnostics;

namespace Emprestae
{
    public partial class MainPage : PhoneApplicationPage
    {
        EmprestaeWebService emprestae;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            emprestae = (Application.Current as App).emprestae;
        }

        private void ApplicationBarOkButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> arg = new Dictionary<string, object>();
            loadingSplash.Visibility = Visibility.Visible;
            if (pivot.SelectedIndex == 0)
            {
                arg.Add("email", email.Text);
                arg.Add("pwd", senha.Text);
                emprestae.CheckUser(arg,success, error);
            }
            else
            {
                arg.Add("nome", nome.Text);
                arg.Add("email", newEmail.Text);
                arg.Add("pwd", newSenha.Text);
                emprestae.CreateUser(arg,success, error);
            }
        }

        private void success(Response response)
        {
            Dispatcher.BeginInvoke(() =>
            {
                loadingSplash.Visibility = Visibility.Collapsed;
                Debug.WriteLine(response);
                if (response.status == 1)
                {
                    NavigationService.Navigate(new Uri("/Pages/Home.xaml", UriKind.Relative));
                }
                else
                {
                    MessageBox.Show("Tivemos um erro com o servidor, verifique seus dados e tente novamente mais tarde");
                }
            });
        }



        private void error()
        {
            loadingSplash.Visibility = Visibility.Collapsed;
            Dispatcher.BeginInvoke(() =>
            {
                MessageBox.Show("Tivemos um erro com o servidor, verifique seus dados e tente novamente mais tarde");
            });
        }

        private void ApplicationBarInfoButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/Info.xaml",UriKind.Relative));
        }
    }
}