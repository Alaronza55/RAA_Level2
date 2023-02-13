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
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RAA_Level_2_Skills
{
    /// <summary>
    /// Interaction logic for Window.xaml
    /// </summary>
    public partial class MyForm2 : Window
    {
        public Document myDoc;
        public MyForm2(string testText, Document doc, List<string> listBoxItems)
        {
            InitializeComponent();
            myDoc = doc;

            if (testText == "" && listBoxItems == null)
                PopulateControls();
            else
            {
                lblLabel.Content = testText + doc.PathName;

                foreach (string item in listBoxItems)
                {
                    lbxText.Items.Add(item);
                }

                cmbViews.Items.Add(testText);
            }

            cmbViews.SelectedIndex = 0;

        }

        public void PopulateControls()
        {
            FilteredElementCollector collector = new FilteredElementCollector(myDoc);
            collector.OfCategory(BuiltInCategory.OST_Views);
            collector.WhereElementIsNotElementType();

            foreach (View currentView in collector)
            {
                lbxText.Items.Add(currentView.Name);
                cmbViews.Items.Add(currentView.Name);
            }

        }
        public string GetSelectedComboboxItem()
        {
            return cmbViews.SelectedItem.ToString();
        }

        public List<string> GetSelectedListboxItems()
        {
            List<string> returnList = new List<string>();

            foreach (var item in lbxText.SelectedItems)
            {
                returnList.Add(item.ToString());
            }

            return returnList;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}