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
using System.ComponentModel;

namespace INdT.Views
{
    public partial class WaitingView : UserControl, INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler OnAnimationCompleted;

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for message.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(WaitingView), new PropertyMetadata("Aguarde..."));

        public Style MessageTextBlockStyle
        {
            get { return (Style)GetValue(MessageTextBlockStyleProperty); }
            set { SetValue(MessageTextBlockStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MessageTextBlockStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageTextBlockStyleProperty =
            DependencyProperty.Register("MessageTextBlockStyle", typeof(Style), typeof(WaitingView), new PropertyMetadata(null, messageTextBlockStylePropertyChanged));

        public Style ProgressBarStyle
        {
            get { return (Style)GetValue(ProgressBarStyleProperty); }
            set { SetValue(ProgressBarStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProgressBarStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProgressBarStyleProperty =
            DependencyProperty.Register("ProgressBarStyle", typeof(Style), typeof(WaitingView), new PropertyMetadata(null, progressBarStylePropertyChanged));

        public ControlTemplate ContentControlTemplate
        {
            get { return (ControlTemplate)GetValue(ContentControlTemplateProperty); }
            set { SetValue(ContentControlTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ContentControlTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentControlTemplateProperty =
            DependencyProperty.Register("ContentControlTemplate", typeof(ControlTemplate), typeof(WaitingView), new PropertyMetadata(null, contentControlTemplatePropertyChanged));

        private bool _binded;

        public WaitingView()
            : base()
        {
            InitializeComponent();
            DataContext = this;

            //ProgressBarStyle = ProgressBarStyle == null ? this.Resources["ProgressBarStyle"] as Style : ProgressBarStyle;
            //MessageTextBlockStyle = MessageTextBlockStyle == null ? this.Resources["MessageTextBlockStyle"] as Style : MessageTextBlockStyle;

            LayoutRoot.Children.Remove(progressBar);
            bindCloseAnimation();
        }

        public void bindCloseAnimation()
        {
            if (!_binded)
            {
                closeAnimation.Completed += closeAnimationCompleted;
                _binded = true;
            }
        }

        public void unbindCloseAnimation()
        {
            if (_binded)
            {
                closeAnimation.Completed -= closeAnimationCompleted;
                _binded = false;
            }
        }

        private void closeAnimationCompleted(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            progressBar.IsIndeterminate = false;
            if (progressBar.Parent != null)
            {
                LayoutRoot.Children.Remove(progressBar);
            }
            if (OnAnimationCompleted != null)
            {
                OnAnimationCompleted(this, null);
            }
        }

        public void show()
        {
            if (progressBar.Parent == null)
            {
                LayoutRoot.Children.Add(progressBar);
            }
            progressBar.IsIndeterminate = true;
            this.Visibility = Visibility.Visible;
            closeAnimation.Stop();
            showAnimation.Begin();
        }

        public void close()
        {
            this.Visibility = Visibility.Visible;
            closeAnimation.Begin();
        }

        public void Dispose()
        {
            LayoutRoot.Children.Clear();
            Resources.Clear();
            unbindCloseAnimation();

            showAnimation.Stop();
            GC.SuppressFinalize(showAnimation);
            showAnimation = null;

            closeAnimation.Stop();
            GC.SuppressFinalize(closeAnimation);
            closeAnimation = null;

            GC.SuppressFinalize(this);
        }

        private static void messageTextBlockStylePropertyChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            (sender as WaitingView).messageTextBlockStylePropertyChanged(args);
        }

        private void messageTextBlockStylePropertyChanged(DependencyPropertyChangedEventArgs args)
        {
            notifyPropertyChanged("MessageTextBlockStyle");
        }

        private static void progressBarStylePropertyChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            (sender as WaitingView).progressBarStylePropertyChanged(args);
        }

        private void progressBarStylePropertyChanged(DependencyPropertyChangedEventArgs args)
        {
            notifyPropertyChanged("ProgressBarStyle");
        }

        private static void contentControlTemplatePropertyChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as WaitingView).contentControlTemplatePropertyChanged(e);
        }

        private void contentControlTemplatePropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            contentControl.Template = ContentControlTemplate;
        }

        protected void notifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
