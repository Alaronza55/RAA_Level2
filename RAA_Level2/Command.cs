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
using System.Windows.Media.Animation;

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
            if(currentForm.GetCsvFile() == "")
            {
                //do something -> Close the addin
                TaskDialog.Show("Error", "The item in the number column is not a number");
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

            List<string[]> datalist = new List<string[]>();

            foreach( string data in dataArray )
            {
                string[] cellstring = data.Split(',');
                datalist.Add(cellstring);
                //string[] levelName = cellstring[0];
                //string[] levelFeet = cellstring[1];
                //string[] levelMeter = cellstring[2];
            }

            //Remove header Row
            datalist.RemoveAt(0);

            //If Imperial

            if (currentForm.GetUnits() == "Imperial")
            {
                // Make a transaction for REVIT
                Transaction transaction = new Transaction(doc);
                transaction.Start("Create Level");

                //go through the data in the csv and do something
                foreach (string[] currentArrayin in datalist)
                {
                    string textName = currentArrayin[0];
                    string numberFeet = currentArrayin[1];
                    string numberMeters = currentArrayin[2];


                    double actualNumber = 0;
                    bool convertNumber = double.TryParse(numberFeet, out actualNumber);

                    if (convertNumber == false)
                    {
                        TaskDialog.Show("Error", "The item in the number column is not a number");
                    }

                    //Conversion Techniek
                    double metricConvert = actualNumber * 3.28084;

                    // Create level -> Default in decimal feet in REVIT API
                    Level currentLevel = Level.Create(doc, actualNumber);
                    currentLevel.Name = textName;

                    ViewFamilyType planVFT = GetViewFamilyTypeByName(doc, "Floor Plan", ViewFamily.FloorPlan);
                    ViewFamilyType ceilingPlanVFT = GetViewFamilyTypeByName(doc, "Ceiling Plan", ViewFamily.CeilingPlan);

                    if (currentForm.GetViewTypes() == "Floor Plans")
                    {
                        ViewPlan plan = ViewPlan.Create(doc, planVFT.Id, currentLevel.Id);
                    }

                    else if (currentForm.GetViewTypes() == "Floor and Ceiling Plans")
                    {
                        ViewPlan plan = ViewPlan.Create(doc, planVFT.Id, currentLevel.Id);
                        ViewPlan ceilingPlan = ViewPlan.Create(doc, ceilingPlanVFT.Id, currentLevel.Id);
                    }

                    else if (currentForm.GetViewTypes() == "Ceiling Plans")
                    {
                        ViewPlan ceilingPlan = ViewPlan.Create(doc, ceilingPlanVFT.Id, currentLevel.Id);
                    }

                    else
                    {

                    }


                }
                // Commit the changes to the model and throw them out
                transaction.Commit();
                transaction.Dispose();


                return Result.Succeeded;
            }
            
            else
            // If Metric
            {
                // Make a transaction for REVIT
                Transaction transaction = new Transaction(doc);
                transaction.Start("Create Level");

                //go through the data in the csv and do something
                foreach (string[] currentArrayin in datalist)
                {
                    string textName = currentArrayin[0];
                    string numberFeet = currentArrayin[1];
                    string numberMeters = currentArrayin[2];


                    double actualNumber = 0;
                    bool convertNumber = double.TryParse(numberMeters, out actualNumber);

                    if (convertNumber == false)
                    {
                        TaskDialog.Show("Error", "Please choose a CSV file");
                    }

                    //Conversion Techniek
                    double metricConvert = actualNumber * 3.28084;

                    // Create level -> Default in decimal feet in REVIT API
                    Level currentLevel = Level.Create(doc, metricConvert);
                    currentLevel.Name = textName;


                    ViewFamilyType planVFT = GetViewFamilyTypeByName(doc, "Floor Plan", ViewFamily.FloorPlan);
                    ViewFamilyType ceilingPlanVFT = GetViewFamilyTypeByName(doc, "Ceiling Plan", ViewFamily.CeilingPlan);


                    if (currentForm.GetViewTypes() == "Floor Plans")
                    {
                        ViewPlan plan = ViewPlan.Create(doc, planVFT.Id, currentLevel.Id);
                    }

                    else if (currentForm.GetViewTypes() == "Floor and Ceiling Plans")
                    {
                        ViewPlan plan = ViewPlan.Create(doc, planVFT.Id, currentLevel.Id);
                        ViewPlan ceilingPlan = ViewPlan.Create(doc, ceilingPlanVFT.Id, currentLevel.Id);
                    }

                    else if (currentForm.GetViewTypes() == "Ceiling Plans")
                    {
                        ViewPlan ceilingPlan = ViewPlan.Create(doc, ceilingPlanVFT.Id, currentLevel.Id);
                    }

                    else
                    {
                    
                    }

                }
                // Commit the changes to the model and throw them out
                transaction.Commit();
                transaction.Dispose();


                return Result.Succeeded;
            }
        }


        private ViewFamilyType GetViewFamilyTypeByName(Document doc, string typeName, ViewFamily viewFamily)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfClass(typeof(ViewFamilyType));

            foreach (ViewFamilyType currentVFT in collector)
            {
                if (currentVFT.Name == typeName && currentVFT.ViewFamily == viewFamily)
                {
                    return currentVFT;
                }
            }

            return null;
        }

        public static String GetMethod()
        {
            var method = MethodBase.GetCurrentMethod().DeclaringType?.FullName;
            return method;
        }
    }
}
