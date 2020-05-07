﻿

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Extensions.Hosting;
using Microsoft.Toolkit.Wpf.UI.XamlHost;
using SampleApp.Contracts.Services;
using SampleApp.Models;
using SampleApp.XamlIsland;

using WUX = Windows.UI.Xaml;
using WUXD = Windows.UI.Xaml.Data;

namespace SampleApp.Controls
{
    // For info about hosting a custom UWP control in a WPF app using XAML Islands read this doc
    // https://docs.microsoft.com/en-us/windows/apps/desktop/modernize/host-custom-control-with-xaml-islands
    public partial class CustomControl : UserControl
    {
        private readonly IThemeSelectorService _themeSelectorService;
        private IHost _host => ((App)App.Current).ApplicationHost;
        private bool _useDarkTheme;
        private SolidColorBrush _backgroundColor;
        private CustomControlUniversal _universalControl;

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(CustomControl), new PropertyMetadata(string.Empty));

        public CustomControl()
        {
            InitializeComponent();
            _themeSelectorService = _host.Services.GetService(typeof(IThemeSelectorService)) as IThemeSelectorService;
            _themeSelectorService.ThemeChanged += OnThemeChanged;
            GetColors();
        }
        private void OnThemeChanged(object sender, System.EventArgs e)
        {
            GetColors();
            ApplyColors();
        }

        private void OnChildChanged(object sender, EventArgs e)
        {
            if (sender is WindowsXamlHost host && host.GetUwpInternalObject() is CustomControlUniversal xamlIsland)
            {
                _universalControl = xamlIsland;
                ApplyColors();
                _universalControl.SetBinding(CustomControlUniversal.TextProperty, new WUXD.Binding() { Path = new WUX.PropertyPath(nameof(Text)), Mode = WUXD.BindingMode.TwoWay });
            }
        }
        private void GetColors()
        {
            _backgroundColor = _themeSelectorService.GetColor("MahApps.Brushes.Control.Background");
            _useDarkTheme = _themeSelectorService.GetCurrentTheme() == AppTheme.Dark;
        }

        private void ApplyColors()
        {
            _universalControl.BackgroundColor = Converters.ColorConverter.FromSystemColor(_backgroundColor);
            _universalControl.UseDarkTheme = _useDarkTheme;
        }
    }
}
