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
        ApplicationBarIconButton searchFriendButton, addFriendButton, searchObjButton, addObjButton;

        public Home()
        {
            InitializeComponent();
            searchFriendButton = CreateButton("search", "appbar.feature.search.rest.png");
            searchObjButton = CreateButton("search", "appbar.feature.search.rest.png");
            addFriendButton = CreateButton("add", "appbar.add.rest.png");
            addObjButton = CreateButton("add", "appbar.add.rest.png");

            addObjButton.Click += AddObjButton_OnClick;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            emprestae.GetUserInfo(success, error);
            emprestae.GetAllUsersButMe(success, error);

            // remove all pages before it
            while (NavigationService.BackStack.Count() > 0)
                NavigationService.RemoveBackEntry();
            
        }

        void success(UserResponse response)
        {
            Dispatcher.BeginInvoke(() => {
                if (response.status != 0)
                {
                    userPanel.DataContext = response.users[0];
                    amigosListBox.ItemsSource = response.users;
                }
            });
        }

        void error()
        { 
        }

        private void Panorama_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = (sender as Panorama).SelectedIndex;

            switch (index)
            {
                case 1:
                    ApplicationBar.Buttons.Remove(searchFriendButton);
                    ApplicationBar.Buttons.Remove(addFriendButton);
                    ApplicationBar.Buttons.Add(searchObjButton);
                    ApplicationBar.Buttons.Add(addObjButton);
                    break;
                case 2:
                    ApplicationBar.Buttons.Remove(searchObjButton);
                    ApplicationBar.Buttons.Remove(addObjButton);
                    ApplicationBar.Buttons.Add(searchFriendButton);
                    ApplicationBar.Buttons.Add(addFriendButton);
                    break;
                default:
                    ApplicationBar.Buttons.Remove(searchObjButton);
                    ApplicationBar.Buttons.Remove(addObjButton);
                    ApplicationBar.Buttons.Remove(searchFriendButton);
                    ApplicationBar.Buttons.Remove(addFriendButton);
                    break;
            }
        }

        private ApplicationBarIconButton CreateButton(string title, string image)
        {
            return new ApplicationBarIconButton()
            {
                IsEnabled = true,
                IconUri = new Uri("/Images/"+image,UriKind.Relative),
                Text = title
            };
        }

        #region Buttons Methods

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/Info.xaml", UriKind.Relative));
        }

        private void AddObjButton_OnClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/AddObjeto.xaml", UriKind.Relative));
        }

        #endregion

    }
}