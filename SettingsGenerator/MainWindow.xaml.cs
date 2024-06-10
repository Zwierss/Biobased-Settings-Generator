using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace SettingsGenerator;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void UpdateInterval_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        // Only allow numeric input
        Regex regex = new Regex("[^0-9]+"); //regex that matches disallowed text
        e.Handled = regex.IsMatch(e.Text);
    }
}
