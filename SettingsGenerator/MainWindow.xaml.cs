using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;

namespace SettingsGenerator;

public partial class MainWindow : Window
{
    private string selectedFolderPath;

    public MainWindow()
    {
        InitializeComponent();
        selectedFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    }

    private void UpdateInterval_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        // Only allow numeric input
        Regex regex = new Regex("[^0-9]+"); //regex that matches disallowed text
        e.Handled = regex.IsMatch(e.Text);
    }

    private void CreateFile(object sender, RoutedEventArgs e)
    {
        Dictionary<string, string> sensorTypeDict = new Dictionary<string, string>();
        sensorTypeDict.Add("CO2 - (SCD30)", "CO2");
        sensorTypeDict.Add("Temperature and Humidity - (HDC1080)", "TEMPANDHUMIDITY");
        sensorTypeDict.Add("VOC - (SGP30)", "VOC");

        Dictionary<string, string> debugDictionary = new Dictionary<string, string>();
        debugDictionary.Add("Aan", "true");
        debugDictionary.Add("Uit", "false");

        string fileName = "settings.ini";
        string wifiSSID = WiFiSSID.Text;
        string wifiPassword = WiFiPassword.Text;
        string room = Room.Text;
        string updateInterval = UpdateInterval.Text;
        string debugMode = debugDictionary[DebugMode.Text];

        if(string.IsNullOrWhiteSpace(wifiSSID) ||
            string.IsNullOrWhiteSpace(wifiPassword) ||
            string.IsNullOrWhiteSpace(SensorType.Text) ||
            string.IsNullOrWhiteSpace(room))
        {
            MessageBox.Show("Vul alstublieft alle velden in.", "Validation error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        string sensorType = sensorTypeDict[SensorType.Text];

        if (string.IsNullOrWhiteSpace(updateInterval))
        {
            updateInterval = "20000";
        }

        if (string.IsNullOrWhiteSpace(debugMode))
        {
            debugMode = "false";
        }

        var dialog = new OpenFolderDialog();
        dialog.ShowDialog();
        selectedFolderPath = Path.GetFullPath(dialog.FolderName);

        File.WriteAllText(selectedFolderPath + "\\" + fileName, 
        "WIFI-SSID=" + wifiSSID + 
        "\nWIFI-PASSWORD=" + wifiPassword + 
        "\nSENSOR-TYPE=" + sensorType + 
        "\nROOM=" + room + 
        "\nUPDATE-TIME=" + updateInterval +
        "\nDEBUG-MODE=" + debugMode
        );

        MessageBox.Show("Settings bestand is succesvol gegenereerd.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}
