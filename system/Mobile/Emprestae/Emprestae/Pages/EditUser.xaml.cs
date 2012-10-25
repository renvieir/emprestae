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
using System.Device.Location;

namespace Emprestae.Pages
{
    public partial class EditUser : PhoneApplicationPage
    {
        EmprestaeWebService emprestae;
        GeoCoordinateWatcher watcher;
        private bool locationUpdated = false;

        public EditUser()
        {
            InitializeComponent();
            emprestae = (Application.Current as App).emprestae;

            if (watcher == null)
            {
                watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High); // using high accuracy
                watcher.MovementThreshold = 20; // use MovementThreshold to ignore noise in the signal
            }
            watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(watcher_PositionChanged);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            nome.Text = emprestae.userInfo.nome;
            newEmail.Text = emprestae.userInfo.email;
            newSenha.Password = emprestae.password;
            newSenha2.Password = emprestae.password;
            location.Text = String.Format("({0}, {1})", emprestae.userInfo.addressLat, emprestae.userInfo.addressLong); 
        }

        void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            emprestae.userInfo.addressLat = e.Position.Location.Latitude.ToString();
            emprestae.userInfo.addressLong = e.Position.Location.Longitude.ToString();
            location.Text = String.Format("({0}, {1})", emprestae.userInfo.addressLat, emprestae.userInfo.addressLong);
            watcher.Stop();
            loadingLayer.Visibility = Visibility.Collapsed;
        }

        private void ChoosePicture_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void CapturePicture_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void ConcluirButton_OnClick(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(nome.Text) ||
                String.IsNullOrEmpty(newEmail.Text) ||
                String.IsNullOrEmpty(newSenha.Password))
            {
                MessageBox.Show("Preencha os campos vazios");
            }
            else
            {
                emprestae.userInfo.nome = nome.Text;
                emprestae.userInfo.email = newEmail.Text;

                if ((newSenha.Password != newSenha2.Password) || String.IsNullOrEmpty(newEmail.Text))
                {
                    MessageBox.Show("Erro: as senhas não correspondem");
                    newSenha2.Focus();
                }
                else
                {
                    emprestae.UpdateUser(newSenha.Password, success, error);
                }
            }
        }

        private void CancelarButton_OnClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/Home.xaml",UriKind.Relative));
        }

        void success(Response response)
        {
            Dispatcher.BeginInvoke(() => {
                NavigationService.Navigate(new Uri("/Pages/Home.xaml", UriKind.Relative));            
            });
        }

        void error()
        { }

        private void newSenha2_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (newSenha2.Password != newSenha.Password)
            {
                newSenha2.Background = new SolidColorBrush(Colors.Red);
            }
            else
            {
                newSenha2.Background = new SolidColorBrush(Colors.Green);
            }
        }

        private void LocalizarButton_OnClick(object sender, EventArgs e)
        {
            loadingLayer.Visibility = Visibility.Visible;
            watcher.Start();
        }

        private void MudarSenhaButton_OnClick(object sender, RoutedEventArgs e)
        {
            mudarSenhaButton.Visibility = Visibility.Collapsed;
            senhaPanel.Visibility = Visibility.Visible;
        }
    }
}