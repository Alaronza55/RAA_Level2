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
using System.Windows.Media.Media3D;
using Windows = System.Windows;

#endregion

namespace RAA_Level_2_Skills
{
    [Transaction(TransactionMode.Manual)]
    public class Command2 : IExternalCommand
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

            // put any code needed for the form here

            // open form
            List<string> strings = new List<string> { "abc", "bcd", "cde", "def" };
            MyForm2 currentForm = new MyForm2("", doc, null)
            {
                Width = 800,
                Height = 450,
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen,
                Topmost = true,
            };

            currentForm.ShowDialog();

            List<Reference> refList = new List<Reference>();
            bool flag = true;

            while (flag == true)
            {
                try
                {
                    Reference curRef = uidoc.Selection.PickObject(ObjectType.Element, "Pick an item");
                    refList.Add(curRef);
                }
                catch (Exception)
                {
                    flag = false;
                }
            }

            string returnString = "There are " + refList.Count.ToString() + " selected elements";
            List<string> returnStrings = currentForm.GetSelectedListboxItems();

            MyForm2 currentForm2 = new MyForm2(returnString, doc, returnStrings)
            {
                Width = 800,
                Height = 450,
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen,
                Topmost = true,
            };

            currentForm2.ShowDialog();

            string returnString2 = currentForm2.GetSelectedComboboxItem();
            List<string> returnStrings2 = currentForm2.GetSelectedListboxItems();

            // get form data and do something

            return Result.Succeeded;
        }

        public static String GetMethod()
        {
            var method = MethodBase.GetCurrentMethod().DeclaringType?.FullName;
            return method;
        }
    }
}
