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
using System.Device.Location;
using Microsoft.Phone.Controls.Maps;
using Emprestae.Models;

namespace Emprestae.Pages
{
    public partial class Mapa : PhoneApplicationPage
    {
        string Id = "Am7_Ny4RRJv9BqjFmb0rMt7sLMsOBlRR9AtqF9kjY-m4i1WgX-Cfzgjg8yF666mw";
        EmprestaeWebService emprestae;
        GeoCoordinateWatcher watcher;
        GeoCoordinate myLocation;

        public Mapa()
        {
            InitializeComponent();
            emprestae = (Application.Current as App).emprestae;
            myLocation = new GeoCoordinate(
                Convert.ToDouble(emprestae.userInfo.addressLat),
                Convert.ToDouble(emprestae.userInfo.addressLong));
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            mapa.CredentialsProvider = new ApplicationIdCredentialsProvider(Id);
            mapa.Center = myLocation;
            mapa.Children.Add(
                new Pushpin() { 
                    Location = myLocation,
                    Content = "eu"
                });
            emprestae.GetCloseUsers(success, error);

            // The watcher variable was previously declared as type GeoCoordinateWatcher. 
            if (watcher == null)
            {
                watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High); // using high accuracy
                watcher.MovementThreshold = 20; // use MovementThreshold to ignore noise in the signal
            }
//            watcher.StatusChanged += new EventHandler<GeoPositionStatusChangedEventArgs>(watcher_StatusChanged);
            watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(watcher_PositionChanged);
            watcher.Start();
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            watcher.Stop();
        }

        void success(UserResponse response)
        {
            Dispatcher.BeginInvoke(() => {
                loadingLayer.Visibility = Visibility.Collapsed;
                if (response != null)
                {
                    for (int i = 0; i < response.users.Length; i++)
                    {
                        User user = response.users[i].user;
                        GeoCoordinate location = new GeoCoordinate(
                            Convert.ToDouble(user.addressLat),
                            Convert.ToDouble(user.addressLong));

                        mapa.Children.Add(
                            new Pushpin() { 
                                Location = location,
                                Content = user.nome
                            });
                        
                    }
                }
            });
        }

        void error()
        { }

        #region Watcher Methods

        // Event handler for the GeoCoordinateWatcher.StatusChanged event.
        //void watcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        //{
        //    switch (e.Status)
        //    {
        //        case GeoPositionStatus.Disabled:
        //            // The Location Service is disabled or unsupported.
        //            // Check to see whether the user has disabled the Location Service.
        //            if (watcher.Permission == GeoPositionPermission.Denied)
        //            {
        //                // The user has disabled the Location Service on their device.
        //                statusTextBlock.Text = "you have this application access to location.";
        //            }
        //            else
        //            {
        //                statusTextBlock.Text = "location is not functioning on this device";
        //            }
        //            break;

        //        case GeoPositionStatus.Initializing:
        //            // The Location Service is initializing.
        //            // Disable the Start Location button.
        //            //startLocationButton.IsEnabled = false;
        //            break;

        //        case GeoPositionStatus.NoData:
        //            // The Location Service is working, but it cannot get location data.
        //            // Alert the user and enable the Stop Location button.
        //            statusTextBlock.Text = "location data is not available.";
        //            //stopLocationButton.IsEnabled = true;
        //            break;

        //        case GeoPositionStatus.Ready:
        //            // The Location Service is working and is receiving location data.
        //            // Show the current position and enable the Stop Location button.
        //            statusTextBlock.Text = "location data is available.";
        //            //stopLocationButton.IsEnabled = true;
        //            break;
        //    }
        //}

        void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            //latitudeTextBlock.Text = e.Position.Location.Latitude.ToString("0.000");
            //longitudeTextBlock.Text = e.Position.Location.Longitude.ToString("0.000");
//            mapa.Center = e.Position.Location;
        }

        #endregion
    }
}