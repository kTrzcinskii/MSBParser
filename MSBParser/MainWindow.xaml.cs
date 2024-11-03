using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Xml;
using System.Xml.Linq;

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
            string fileContent = File.ReadAllText(openFileDialog.FileName).Trim();
            EditorRichTextBox.Document.Blocks.Clear();
            EditorRichTextBox.Document.Blocks.Add(new Paragraph(new Run(fileContent)));
            var syntaxHighlighter = new SyntaxHighlighter(EditorRichTextBox);
            try
            {
                var parser = new Parser(openFileDialog.FileName);
                var project = parser.Parse();
                syntaxHighlighter.HighlightContent(project);
                ErrorsList.ItemsSource = syntaxHighlighter.ErrorsList;
            }
            catch (XmlException exception)
            {
                syntaxHighlighter.HighlighXmlError();
                ErrorsList.ItemsSource = new List<string> { $"Failed to parse XML file: {exception.Message}" };
            }
        }
    }
}