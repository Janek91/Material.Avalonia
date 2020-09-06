﻿using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MaterialXamlToolKit.Avalonia.Themes;
using MaterialXamlToolKit.Avalonia.Themes.Base;

namespace AvaloniaRipple {
    public class MainWindow : Window {
        private PaletteHelper _paletteHelper;

        public MainWindow() {
            InitializeComponent();
        }

        private void InitializeComponent() {
            AvaloniaXamlLoader.Load(this);
            _paletteHelper = new PaletteHelper();
            this.FindControl<CheckBox>("BaseThemeCheckBox").IsChecked = _paletteHelper.GetTheme().GetBaseTheme() == BaseThemeMode.Dark;
        }

        void BaseThemeChanged(object sender, RoutedEventArgs args) {
            if (sender is CheckBox checkBox) {
                var theme = _paletteHelper.GetTheme();
                var baseThemeMode = checkBox.IsChecked!.Value switch {
                    true  => BaseThemeMode.Dark,
                    false => BaseThemeMode.Light
                };
                theme.SetBaseTheme(baseThemeMode.GetBaseTheme());
                _paletteHelper.SetTheme(theme);
            }
        }
    }
}