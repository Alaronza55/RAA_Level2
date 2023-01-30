#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.Windows.Documents;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Win32;

#endregion

namespace RAA_Level2
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            // Step 1 : put any code needed for the form here

            // Step 2 : open form
            MyForm currentForm = new MyForm()
            {
                Width = 500,
                Height = 450,
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen,
                Topmost = true,
            };

            currentForm.ShowDialog();

            //Step3 : get form data and do something
            if(currentForm.DialogResult == false)
            {
                //do something -> Close the addin
                return Result.Cancelled;
            }

            //Print a window with response
            string filename = currentForm.GetCsvFile();

            string viewtypes = currentForm.GetViewTypes();

            string getunits = currentForm.GetUnits();

            TaskDialog.Show("Input", "File: " + filename);


            TaskDialog.Show("View Types", "View Types: " + viewtypes);


            TaskDialog.Show("Units:", "Units: " + getunits);


            //Import CSV
            //Read all elements of CSV -> Big Bucket
            string[] dataArray = System.IO.File.ReadAllLines(filename);
            
            foreach( string data in dataArray )
            {
                string[] cellstring = data.Split(',');
                string[] levelName = cellstring[0];
            }


            // Process Data




            return Result.Succeeded;
        }


        public static String GetMethod()
        {
            var method = MethodBase.GetCurrentMethod().DeclaringType?.FullName;
            return method;
        }
    }
}
