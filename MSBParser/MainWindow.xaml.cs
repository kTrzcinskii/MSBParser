using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace MSBParser;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void OpenCsprojFile_Click(object sender, RoutedEventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog()
        {
            Filter = "MSBuild files (*.csproj)|*.csproj|All files (*.*)|*.*"
        };

        if (openFileDialog.ShowDialog() == true)
        {
            string fileContent = File.ReadAllText(openFileDialog.FileName);

            var parser = new Parser(openFileDialog.FileName);
            var project = parser.Parse();

            EditorTextBox.Text = fileContent;
        }
    }
}