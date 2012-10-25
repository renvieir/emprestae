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
using Microsoft.Phone.Shell;

namespace Emprestae.Pages
{
    public partial class Home : PhoneApplicationPage
    {
        EmprestaeWebService emprestae = (Application.Current as App).emprestae;
        ApplicationBarIconButton searchButton, addButton, editUserButton;
        int pIndex;
        
        public Home()
        {
            InitializeComponent();
            searchButton = CreateButton("search", "appbar.feature.search.rest.png");
            addButton = CreateButton("add", "appbar.add.rest.png");

            editUserButton = CreateButton("edit", "appbar.edit.rest.png");

            addButton.Click += AddButton_OnClick;
            searchButton.Click += SearchButton_OnClick;
            editUserButton.Click += EditUserButton_OnClick;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            waitingView.Visibility = Visibility.Visible;
            emprestae.GetUserInfo(emprestae.userInfo.email, success, error);

            if (panoramaEmprestae.SelectedIndex == 0)
            {
                try
                {
                    ApplicationBar.Buttons.Add(editUserButton);
                }
                catch { }
            }

            // remove all pages before it
            while (NavigationService.BackStack.Count() > 0)
                NavigationService.RemoveBackEntry();
            
        }

        private void Panorama_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            pIndex = (sender as Panorama).SelectedIndex;

            switch (pIndex)
            {
                case 0:
                    ApplicationBar.Buttons.Remove(searchButton);
                    //ApplicationBar.Buttons.Remove(addButton);
                    try
                    {
                        ApplicationBar.Buttons.Add(editUserButton);
                    }
                    catch {}
                    break;
                default:
                    ApplicationBar.Buttons.Remove(editUserButton);
                    try
                    {
                        ApplicationBar.Buttons.Add(searchButton);
                        //ApplicationBar.Buttons.Add(addButton);
                    }
                    catch { }
                    break;
            }
        }


        #region Callbacks

        void successObj(ObjResponse response)
        {
            Dispatcher.BeginInvoke(() => {
                waitingView.Visibility = Visibility.Collapsed;
                if (response.status != 0)
                {
                    livrosListBox.ItemsSource = response.livros;
                    filmesListBox.ItemsSource = response.filmes;
                    jogosListBox.ItemsSource = response.jogos;
                }
            });
        }

        void successFriends(UserResponse response)
        {
            Dispatcher.BeginInvoke(() => {
                if (response!= null)
                {
                    amigosListBox.ItemsSource = response.users;
                    emprestae.userFriends = response.users;
                }
            });
        }

        void success(UserResponse response)
        {
            Dispatcher.BeginInvoke(() => {
                waitingView.Visibility = Visibility.Collapsed;
                if (response.status != 0)
                {
                    emprestae.GetUserObjs(emprestae.userInfo.idusuario, successObj, error);
                    emprestae.GetFriends(successFriends, error);
                    userPanel.DataContext = response.users[0];
                }
            });
        }

        void error()
        { 
        }

        #endregion

        #region Buttons Methods

        private void InfoButton_OnClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/Info.xaml", UriKind.Relative));
        }

        private void SearchButton_OnClick(object sender, EventArgs e)
        {
            switch (pIndex)
            {
                case 1:
                    NavigationService.Navigate(new Uri("/Pages/SearchObj.xaml", UriKind.Relative));
                    break;
                case 2:
                    NavigationService.Navigate(new Uri("/Pages/SearchUser.xaml", UriKind.Relative));
                    break;
            }
        }

        private void AddButton_OnClick(object sender, EventArgs e)
        {
            switch (pIndex)
            {
                case 1:
                    NavigationService.Navigate(new Uri("/Pages/SearchObj.xaml", UriKind.Relative));
                    break;
                case 2:
                    NavigationService.Navigate(new Uri("/Pages/SearchObj.xaml", UriKind.Relative));
                    break;
            }
        }

        private void EditUserButton_OnClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/EditUser.xaml", UriKind.Relative));
        }

        #endregion

        private ApplicationBarIconButton CreateButton(string title, string image)
        {
            return new ApplicationBarIconButton()
            {
                IsEnabled = true,
                IconUri = new Uri("/Images/" + image, UriKind.Relative),
                Text = title
            };
        }

        private void MapButton_OnClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/Map.xaml", UriKind.Relative));
        }

        private void amigoItem_OnClick(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string email = ((sender as Border).DataContext as UserArray).user.email;
            NavigationService.Navigate(new Uri("/Pages/ViewUser.xaml?friendEmail="+email,UriKind.Relative));
        }
    }
}