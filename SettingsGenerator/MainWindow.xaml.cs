using System;
using System.IO;
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

    private void CreateFile(object sender, RoutedEventArgs e)
    {
        string fileName = "settings.ini";
        string wifiSSID = WiFiSSID.Text;
        string wifiPassword = WiFiPassword.Text;
        string sensorType = SensorType.Text;
        string room = Room.Text;
        string updateInterval = UpdateInterval.Text;

        if(string.IsNullOrWhiteSpace(wifiSSID) ||
            string.IsNullOrWhiteSpace(wifiPassword) ||
            string.IsNullOrWhiteSpace(sensorType) ||
            string.IsNullOrWhiteSpace(room))
        {
            MessageBox.Show("Vul alstublieft alle velden in.", "Validation error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

            File.WriteAllText(fileName, 
            "WIFI-SSID=" + wifiSSID + 
            "\nWIFI-PASSWORD=" + wifiPassword + 
            "\nSENSOR-TYPE=" + sensorType + 
            "\nROOM=" + room + 
            "\nUPDATE-TIME="+updateInterval
            );

        MessageBox.Show("Settings file has been created successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}
