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

namespace INdT.Views
{
    public partial class LoadingView : UserControl
    {


        public string LoadingText
        {
            get { return (string)GetValue(LoadingTextProperty); }
            set { SetValue(LoadingTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LoadingText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LoadingTextProperty =
            DependencyProperty.Register("LoadingText", typeof(string), typeof(LoadingView), new PropertyMetadata("loading"));




        public Style LoadingTextStyle
        {
            get { return (Style)GetValue(LoadingTextStyleProperty); }
            set { SetValue(LoadingTextStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LoadingTextStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LoadingTextStyleProperty =
            DependencyProperty.Register("LoadingTextStyle", typeof(Style), typeof(LoadingView), new PropertyMetadata(null));

        


        public Style ProgressBarStyle
        {
            get { return (Style)GetValue(ProgressBarStyleProperty); }
            set { SetValue(ProgressBarStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProgressBarStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProgressBarStyleProperty =
            DependencyProperty.Register("ProgressBarStyle", typeof(Style), typeof(LoadingView), new PropertyMetadata(null));
        

        public LoadingView()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
