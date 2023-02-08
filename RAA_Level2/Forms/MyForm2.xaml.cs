using Autodesk.Revit.DB;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace RAA_Level2
{
    /// <summary>
    /// Interaction logic for Window.xaml
    /// </summary>
    public partial class MyForm2 : Window
    {
        public DocumentPage myDoc;
        public MyForm2(string testText, DocumentPage doc)
        {
            InitializeComponent();
            myDoc = doc;

            lblLabel.Content = testText + doc.PathName;

            foreach (string item in listBoxItem)
            {
                listBox.Items.Add(item);
            }

            cmbBoxViews.Items.Add("This is my first comboBOX item");

        }

        public void DocumentTest()
        {
            FilteredElementCollector collector = new FilteredElementCollector(myDoc);
            collector.OfCategory(BuiltInCategory.OST_Views);
            collector.WhereElementIsNotElementType();

            foreach (View currentView in collector)
            {
                listBox.Items.Add(currentView.Name);
                cmbBox.Items.Add(currentView.Name);
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            DocumentTest();
        }
    }
}