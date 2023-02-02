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
    public partial class MyForm : Window
    {
        public MyForm()
        {
            InitializeComponent();
        }

        // Select button Event
        private void btnselect_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.InitialDirectory = @"C:\";
            openFile.Filter = "csv files (*.csv)|*.csv";

            if (openFile.ShowDialog() == true)
            {
                tbxFile.Text = openFile.FileName;
            }
            else
            {
                tbxFile.Text = "";
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this. DialogResult = false;
            this.Close();
        }

        public string GetCsvFile()
            { 
            return tbxFile.Text;
            }

        public string GetViewTypes()
        {
            if (floor.IsChecked == true && ceiling.IsChecked == false)
            { 
                string floorplans = "Floor Plans";
                return floorplans;
            }

            else if (floor.IsChecked == true && ceiling.IsChecked == true)
            {
                string floorandceilingplans = "Floor and Ceiling Plans";
                return floorandceilingplans;
            }

            else if (floor.IsChecked == false && ceiling.IsChecked == true)
            {
                string ceilingplans = "Ceiling Plans";
                return ceilingplans;
            }

            else
            {
                string noPlans = "No View Types have been selected : No views will be created";
                return noPlans;
            }

        }


        public string GetUnits()
        {
            if (metric.IsChecked == true)
                return metric.Content.ToString();
            else
                return imperial.Content.ToString();  
        }

    }
}
