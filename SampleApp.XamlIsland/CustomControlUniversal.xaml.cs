﻿using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace SampleApp.XamlIsland
{
    public sealed partial class CustomControlUniversal : UserControl
    {
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public Brush BackgroundColor
        {
            get { return (Brush)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }

        public bool UseDarkTheme
        {
            get { return (bool)GetValue(UseDarkThemeProperty); }
            set { SetValue(UseDarkThemeProperty, value); }
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(CustomControlUniversal), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty BackgroundColorProperty = DependencyProperty.Register(nameof(BackgroundColor), typeof(Brush), typeof(CustomControlUniversal), new PropertyMetadata(null));

        public static readonly DependencyProperty UseDarkThemeProperty = DependencyProperty.Register(nameof(UseDarkTheme), typeof(bool), typeof(CustomControlUniversal), new PropertyMetadata(false, OnUseDarkThemePropertyChanged));

        public CustomControlUniversal()
        {
            this.InitializeComponent();
        }

        private static void OnUseDarkThemePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CustomControlUniversal control && e.NewValue is bool useDarkTheme)
            {
                control.RequestedTheme = useDarkTheme ? ElementTheme.Dark : ElementTheme.Light;
            }
        }
    }
}
