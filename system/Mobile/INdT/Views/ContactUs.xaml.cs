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
using Microsoft.Phone.Tasks;

namespace INdT.Views
{
    public partial class ContactUs : UserControl
    {


        public string ApplicationTitle
        {
            get { return (string)GetValue(ApplicationTitleProperty); }
            set { SetValue(ApplicationTitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ApplicationTitle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ApplicationTitleProperty =
            DependencyProperty.Register("ApplicationTitle", typeof(string), typeof(ContactUs), new PropertyMetadata(String.Empty));




        public Style ApplicationTitleStyle
        {
            get { return (Style)GetValue(ApplicationTitleStyleProperty); }
            set { SetValue(ApplicationTitleStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ApplicationTitleStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ApplicationTitleStyleProperty =
            DependencyProperty.Register("ApplicationTitleStyle", typeof(Style), typeof(ContactUs), new PropertyMetadata(null));



        public Style ContactUsTitleStyle
        {
            get { return (Style)GetValue(ContactUsTitleStyleProperty); }
            set { SetValue(ContactUsTitleStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ContactUsTitleStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContactUsTitleStyleProperty =
            DependencyProperty.Register("ContactUsTitleStyle", typeof(Style), typeof(ContactUs), new PropertyMetadata(null));



        public Style FacebookTwitterStyle
        {
            get { return (Style)GetValue(FacebookTwitterStyleProperty); }
            set { SetValue(FacebookTwitterStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FacebookTwitterStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FacebookTwitterStyleProperty =
            DependencyProperty.Register("FacebookTwitterStyle", typeof(Style), typeof(ContactUs), new PropertyMetadata(null));




        public Style FacebookTwitterDescriptionStyle
        {
            get { return (Style)GetValue(FacebookTwitterDescriptionStyleProperty); }
            set { SetValue(FacebookTwitterDescriptionStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FacebookTwitterDescriptionStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FacebookTwitterDescriptionStyleProperty =
            DependencyProperty.Register("FacebookTwitterDescriptionStyle", typeof(Style), typeof(ContactUs), new PropertyMetadata(null));

        

        public Style DescriptionStyle
        {
            get { return (Style)GetValue(DescriptionStyleProperty); }
            set { SetValue(DescriptionStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DescriptionStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DescriptionStyleProperty =
            DependencyProperty.Register("DescriptionStyle", typeof(Style), typeof(ContactUs), new PropertyMetadata(null));



        public Style ImageGuruStyle
        {
            get { return (Style)GetValue(ImageGuruStyleProperty); }
            set { SetValue(ImageGuruStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageGuruStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageGuruStyleProperty =
            DependencyProperty.Register("ImageGuruStyle", typeof(Style), typeof(ContactUs), new PropertyMetadata(null));



        public Style FacebookTwitterPanel
        {
            get { return (Style)GetValue(FacebookTwitterPanelProperty); }
            set { SetValue(FacebookTwitterPanelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FacebookTwitterPanel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FacebookTwitterPanelProperty =
            DependencyProperty.Register("FacebookTwitterPanel", typeof(Style), typeof(ContactUs), new PropertyMetadata(null));

        

        public ContactUs()
        {
            InitializeComponent();

            

            ApplicationTitleStyle = ApplicationTitleStyle == null ? this.Resources["ApplicationTitleStyle"] as Style : ApplicationTitleStyle;
            ContactUsTitleStyle = ContactUsTitleStyle == null ? this.Resources["ContactUsTitleStyle"] as Style : ContactUsTitleStyle;
            FacebookTwitterStyle = FacebookTwitterStyle == null ? this.Resources["FacebookTwitterStyle"] as Style : FacebookTwitterStyle;
            FacebookTwitterDescriptionStyle = FacebookTwitterDescriptionStyle == null ? this.Resources["FacebookTwitterDescriptionStyle"] as Style : FacebookTwitterDescriptionStyle;
            DescriptionStyle = DescriptionStyle == null ? this.Resources["DescriptionStyle"] as Style : DescriptionStyle;
            ImageGuruStyle = ImageGuruStyle == null ? this.Resources["ImageGuruStyle"] as Style : ImageGuruStyle;
            FacebookTwitterPanel = FacebookTwitterPanel == null ? this.Resources["FacebookTwitterPanel"] as Style : FacebookTwitterPanel;

            DataContext = this;
        }

        private void socialButton_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {
            try
            {
                string url;

                if (((StackPanel)sender).Name == "facebookButton")
                    url = "http://m.facebook.com/indtappsguru";
                else
                    url = "http://mobile.twitter.com/indtappsguru";


                WebBrowserTask browser = new WebBrowserTask();
                browser.Uri = new Uri(url, UriKind.Absolute);
                browser.Show();
            }
            catch
            {
            }
        }
    }
}
