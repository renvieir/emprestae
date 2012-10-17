using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using System.ComponentModel;
using Microsoft.Phone.Tasks;
using System.Globalization;
using System.Windows.Markup;

namespace INdT.Views
{
    public partial class About : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string ApplicationTitle
        {
            get { return (string)GetValue(ApplicationTitleProperty); }
            set { SetValue(ApplicationTitleProperty, value); notifyPropertyChanged("ApplicationTitle"); }
        }

        // Using a DependencyProperty as the backing store for ApplicationTitle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ApplicationTitleProperty =
            DependencyProperty.Register("ApplicationTitle", typeof(string), typeof(About), new PropertyMetadata(string.Empty));

        public double ApplicationVersion
        {
            get { return (double)GetValue(ApplicationVersionProperty); }
            set { SetValue(ApplicationVersionProperty, value); notifyPropertyChanged("ApplicationVersion"); }
        }

        // Using a DependencyProperty as the backing store for ApplicationVersion.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ApplicationVersionProperty =
            DependencyProperty.Register("ApplicationVersion", typeof(double), typeof(About), new PropertyMetadata(0d));

        public DateTime ReleaseDate
        {
            get { return (DateTime)GetValue(ReleaseDateProperty); }
            set { SetValue(ReleaseDateProperty, value); notifyPropertyChanged("ReleaseDate"); }
        }

        // Using a DependencyProperty as the backing store for ReleaseDate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ReleaseDateProperty =
            DependencyProperty.Register("ReleaseDate", typeof(DateTime), typeof(About), new PropertyMetadata(DateTime.MinValue));

        public PageOrientation ApplicationOrientation
        {
            get { return (PageOrientation)GetValue(ApplicationOrientationProperty); }
            set { SetValue(ApplicationOrientationProperty, value); notifyPropertyChanged("ApplicationOrientation"); }
        }

        // Using a DependencyProperty as the backing store for ApplicationOrientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ApplicationOrientationProperty =
            DependencyProperty.Register("ApplicationOrientation", typeof(PageOrientation), typeof(About), new PropertyMetadata(PageOrientation.Portrait));

        public Style ApplicationTitleStyle
        {
            get { return (Style)GetValue(ApplicationTitleStyleProperty); }
            set { SetValue(ApplicationTitleStyleProperty, value); notifyPropertyChanged("ApplicationTitleStyle"); }
        }

        // Using a DependencyProperty as the backing store for ApplicationTitleStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ApplicationTitleStyleProperty =
            DependencyProperty.Register("ApplicationTitleStyle", typeof(Style), typeof(About), new PropertyMetadata(null));

        public Style AboutTitleStyle
        {
            get { return (Style)GetValue(AboutTitleStyleProperty); }
            set { SetValue(AboutTitleStyleProperty, value); notifyPropertyChanged("AboutTitleStyle"); }
        }

        // Using a DependencyProperty as the backing store for AboutTitleStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AboutTitleStyleProperty =
            DependencyProperty.Register("AboutTitleStyle", typeof(Style), typeof(About), new PropertyMetadata(null));

        public Style VersionDateStyle
        {
            get { return (Style)GetValue(VersionDateStyleProperty); }
            set { SetValue(VersionDateStyleProperty, value); notifyPropertyChanged("VersionDateStyle"); }
        }

        // Using a DependencyProperty as the backing store for VersionDateStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VersionDateStyleProperty =
            DependencyProperty.Register("VersionDateStyle", typeof(Style), typeof(About), new PropertyMetadata(null));

        public Style DescriptionStyle
        {
            get { return (Style)GetValue(DescriptionStyleProperty); }
            set { SetValue(DescriptionStyleProperty, value); notifyPropertyChanged("DescriptionStyle"); }
        }

        // Using a DependencyProperty as the backing store for DescriptionStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DescriptionStyleProperty =
            DependencyProperty.Register("DescriptionStyle", typeof(Style), typeof(About), new PropertyMetadata(null));

        public Style WorksBetterButtonStyle
        {
            get { return (Style)GetValue(WorksBetterButtonStyleProperty); }
            set { SetValue(WorksBetterButtonStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WorksBetterButtonStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WorksBetterButtonStyleProperty =
            DependencyProperty.Register("WorksBetterButtonStyle", typeof(Style), typeof(About), new PropertyMetadata(null, worksBetterButtonStyleChanged));

        public Style ContactUsStyle
        {
            get { return (Style)GetValue(ContactUsStyleProperty); }
            set { SetValue(ContactUsStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ContactUsStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContactUsStyleProperty =
            DependencyProperty.Register("ContactUsStyle", typeof(Style), typeof(About), new PropertyMetadata(null));

        public Style AboutGridStyle
        {
            get { return (Style)GetValue(AboutGridStyleProperty); }
            set { SetValue(AboutGridStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AboutGridStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AboutGridStyleProperty =
            DependencyProperty.Register("AboutGridStyle", typeof(Style), typeof(About), new PropertyMetadata(null));

        public DataTemplate DescriptionTemplate
        {
            get { return (DataTemplate)GetValue(DescriptionTemplateProperty); }
            set { SetValue(DescriptionTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DescriptionTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DescriptionTemplateProperty =
            DependencyProperty.Register("DescriptionTemplate", typeof(DataTemplate), typeof(About), new PropertyMetadata(null, descriptionTemplatePropertyChanged));

        private static void worksBetterButtonStyleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as About).worksBetterButtonStyleChanged(e);
        }

        private static void descriptionTemplatePropertyChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as About).descriptionTemplatePropertyChanged(e);
        }

        public About()
        {
            InitializeComponent();

            DataContext = this;

            ApplicationTitleStyle = ApplicationTitleStyle == null ? this.Resources["ApplicationTitleStyle"] as Style : ApplicationTitleStyle;
            AboutTitleStyle = AboutTitleStyle == null ? this.Resources["AboutTitleStyle"] as Style : AboutTitleStyle;
            VersionDateStyle = VersionDateStyle == null ? this.Resources["VersionDateStyle"] as Style : VersionDateStyle;
            DescriptionStyle = DescriptionStyle == null ? this.Resources["DescriptionStyle"] as Style : DescriptionStyle;
            ContactUsStyle = ContactUsStyle == null ? this.Resources["ContactUsStyle"] as Style : ContactUsStyle;
            AboutGridStyle = AboutGridStyle == null ? this.Resources["AboutGridStyle"] as Style : AboutGridStyle;

            //if (DeviceStatus.DeviceManufacturer.IndexOf("Nokia", StringComparison.InvariantCultureIgnoreCase) >= 0)
            //{
                //nokiaLogo.Visibility = Visibility.Visible;
            //}

            Loaded += aboutLoaded;
        }

        private void faleConoscoButtonClicked(object sender, RoutedEventArgs e)
        {
            string subject = "Contact us – App " + ApplicationTitle + ", version " + ApplicationVersion.ToString("##.#") + ", Nokia Windows Phone";

            if (CultureInfo.CurrentCulture.Name.Equals("pt-BR"))
            {
                subject = "Fale conosco – App " + ApplicationTitle + ", versão " + ApplicationVersion.ToString("##.#") + ", Nokia Windows Phone";
            }
            else if (CultureInfo.CurrentCulture.Name.Contains("es"))
            {
                subject = "Contáctenos – App " + ApplicationTitle + ", versión " + ApplicationVersion.ToString("##.#") + ", Nokia Windows Phone";
            }

            try
            {
                EmailComposeTask emailTask = new EmailComposeTask();
                emailTask.Subject = subject;
                emailTask.To = "indt_nwp@live.com";
                emailTask.Show();
            }
            catch { }
        }

        private void worksBetterWithNokiaButtonClicked(object sender, RoutedEventArgs e)
        {
            bool isBrazilianLocale = CultureInfo.CurrentCulture.Name.Equals("pt-BR");
            string url = "http://bit.ly/lumia_global";
            if (isBrazilianLocale)
            {
                url = "http://bit.ly/lumia_nwp";
            }
            Uri link = new Uri(url, UriKind.Absolute);
            try
            {
                WebBrowserTask browserTask = new WebBrowserTask();
                browserTask.Uri = link;
                browserTask.Show();
            }
            catch { }
        }

        private void downloadAppsButtonClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                MarketplaceSearchTask marketPlaceSearchTask = new MarketplaceSearchTask();
                marketPlaceSearchTask.ContentType = MarketplaceContentType.Applications;
                marketPlaceSearchTask.SearchTerms = "INdT";
                marketPlaceSearchTask.Show();
            }
            catch { }
        }

        private void notifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void aboutLoaded(object sender, RoutedEventArgs e)
        {
            Loaded -= aboutLoaded;
            i18n();
        }

        private void i18n()
        {
            string currentCultureInfo = CultureInfo.CurrentCulture.Name;
            XmlLanguage componentLanguage = this.Language;
            if (componentLanguage != null && !string.IsNullOrEmpty(componentLanguage.IetfLanguageTag))
            {
                currentCultureInfo = componentLanguage.IetfLanguageTag;
            }

            if (currentCultureInfo.Equals("pt-BR", StringComparison.InvariantCultureIgnoreCase))
            {
                AboutTitle.Text = "sobre";
                VersionAndDate.Text = "versão";
                
                string template = "<ControlTemplate xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' TargetType=\"Button\"> <TextBlock Text=\"fale conosco\" FontFamily=\"Segoe WP Light\" FontSize=\"42\"/></ControlTemplate>";
                ContactUs.Template = (ControlTemplate)XamlReader.Load(template);

                template = "<ControlTemplate xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' TargetType=\"Button\"> <Image Source=\"/INdT.Views;component/Images/About/download.png\" Stretch=\"UniformToFill\" Width=\"430\"/></ControlTemplate>";
                DownloadAppsButton.Template = (ControlTemplate)XamlReader.Load(template);
                
            }
            else if (currentCultureInfo.Contains("es"))
            {
                AboutTitle.Text = "sobre";
                VersionAndDate.Text = "versión";

                string template = "<ControlTemplate xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' TargetType=\"Button\"> <TextBlock Text=\"contact us\" FontFamily=\"Segoe WP Light\" FontSize=\"42\"/></ControlTemplate>";
                ContactUs.Template = (ControlTemplate)XamlReader.Load(template);

                template = "<ControlTemplate xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' TargetType=\"Button\"> <Image Source=\"/INdT.Views;component/Images/About/downloadUs.png\" Stretch=\"UniformToFill\" Width=\"430\"/></ControlTemplate>";
                DownloadAppsButton.Template = (ControlTemplate)XamlReader.Load(template);
            }
            else
            {
                AboutTitle.Text = "about";
                VersionAndDate.Text = "version";
                
                string template = "<ControlTemplate xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' TargetType=\"Button\"> <TextBlock Text=\"contact us\" FontFamily=\"Segoe WP Light\" FontSize=\"42\"/></ControlTemplate>";
                ContactUs.Template = (ControlTemplate)XamlReader.Load(template);

                template = "<ControlTemplate xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' TargetType=\"Button\"> <Image Source=\"/INdT.Views;component/Images/About/downloadUs.png\" Stretch=\"UniformToFill\" Width=\"430\"/></ControlTemplate>";
                DownloadAppsButton.Template = (ControlTemplate)XamlReader.Load(template);
            }
        }

        private void worksBetterButtonStyleChanged(DependencyPropertyChangedEventArgs e)
        {
            WorksBetter.Style = e.NewValue as Style;
        }

        private void descriptionTemplatePropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            Description.ContentTemplate = e.NewValue as DataTemplate;
        }
    }
}