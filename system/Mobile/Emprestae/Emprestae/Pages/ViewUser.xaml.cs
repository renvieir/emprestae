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
using Microsoft.Phone.Shell;

namespace Emprestae.Pages
{
    public partial class ViewUser : PhoneApplicationPage
    {
        EmprestaeWebService emprestae;
        ApplicationBarIconButton requestFriendButton, acceptFriendButton, unFriendButton;

        bool isFriend = false;
        UserArray friendInfo;
        string requestStatus;

        public ViewUser()
        {
            InitializeComponent();
            emprestae = (Application.Current as App).emprestae;
            requestFriendButton = new ApplicationBarIconButton() 
            { 
                Text = "solicitar",
                IconUri = new Uri("/Images/appbar.add.rest.png", UriKind.Relative)
            };
            acceptFriendButton = new ApplicationBarIconButton() 
            {
                Text = "aceitar",
                IconUri = new Uri("/Images/appbar.check.rest.png", UriKind.Relative)
            };
            unFriendButton = new ApplicationBarIconButton() 
            {
                Text = "desfazer",
                IconUri = new Uri("/Images/appbar.minus.rest.png", UriKind.Relative)
            };

            requestFriendButton.Click += RequestFriend_OnClick;
            acceptFriendButton.Click += AcceptFriend_OnClick;
            unFriendButton.Click += UnFriend_OnClick;

        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            loadingLayer.Visibility = Visibility.Visible;
            
            string friendEmail = NavigationContext.QueryString["friendEmail"];
            friendInfo = emprestae.userFriends.First(x => x.user.email == friendEmail);

            if (friendInfo != null)
            {
                userPanel.DataContext = friendInfo;
                isFriend = true;
                emprestae.GetUserObjs(friendInfo.user.idusuario, successObjs, error);
            }
            else
            {
                isFriend = false;
                emprestae.GetUserInfo(friendEmail, successUser, error);
                requestStatus = NavigationContext.QueryString["requestStatus"];
            }

        }

        #region Callbacks

        void successUser(UserResponse response)
        {
            Dispatcher.BeginInvoke(() =>
            {
                loadingLayer.Visibility = Visibility.Collapsed;
                if (response != null)
                {
                    friendInfo = response.users[0];
                    userPanel.DataContext = friendInfo;
                    loadingLayer.Visibility = Visibility.Visible;
                    emprestae.GetUserObjs(response.users[0].user.idusuario, successObjs, error);
                }
            });
        }

        void successObjs(ObjResponse response)
        {
            Dispatcher.BeginInvoke(() => 
            {
                loadingLayer.Visibility = Visibility.Collapsed;
                if (response != null)
                {
                    livrosListBox.ItemsSource = response.livros;
                    filmesListBox.ItemsSource = response.filmes;
                    jogosListBox.ItemsSource = response.jogos;
                }
                ConstructAppBar();
            });
        }

        void success(Response response)
        {
            MessageBox.Show("Operação realizada");
            NavigationService.Navigate(new Uri("/Pages/Home.xaml", UriKind.Relative));
        }

        void error()
        { }

        #endregion

        #region Button Mehtods

        private void ConstructAppBar()
        {
            if (isFriend)
            {
                try
                {
                    ApplicationBar.Buttons.Add(unFriendButton);
                }
                catch(Exception e) { }
            }
            else
            {
                switch (requestStatus)
                {
                    case "none":
                        try
                        {
                            ApplicationBar.Buttons.Add(requestFriendButton);
                        }
                        catch { }                    
                        break;
                    case "received":
                        try
                        {
                            ApplicationBar.Buttons.Add(acceptFriendButton);
                        }
                        catch { }                    
                        break;
                    default:
                        ApplicationBar.Buttons.Remove(acceptFriendButton);
                        ApplicationBar.Buttons.Remove(requestStatus);
                        ApplicationBar.Buttons.Remove(unFriendButton);
                        break;
                }
            }
        }

        private void RequestFriend_OnClick(object sender, EventArgs e)
        {
            emprestae.RequestFriend(friendInfo.user.idusuario.ToString(), success, error);
        }

        private void AcceptFriend_OnClick(object sender, EventArgs e)
        {
            emprestae.AcceptFriend(friendInfo.user.idusuario.ToString(), success, error);
        }

        private void UnFriend_OnClick(object sender, EventArgs e)
        {
            emprestae.UnFriend(friendInfo.user.idusuario.ToString(), success, error);
        }

        #endregion
    }
}