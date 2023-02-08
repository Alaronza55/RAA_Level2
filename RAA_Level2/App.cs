#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Versioning;
using System.Windows.Markup;

#endregion

namespace RAA_Level2
{
    internal class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication app)
        {
            // 1. Create ribbon tab
            try
            {
                app.CreateRibbonTab("RAA_My first plugin");
            }
            catch (Exception)
            {
                Debug.Print("Tab already exists.");
            }

            // 2. Create ribbon panel 
            RibbonPanel panel = Utils.CreateRibbonPanel(app, "RAA_My first plugin", "Revit Tools");

            // 3. Create button data instances
            ButtonDataClass myButtonData = new ButtonDataClass("Autolevels", "Autolevels", Command.GetMethod(), Properties.Resources.City_32, Properties.Resources.City_32, "This is a tooltip");
            ButtonDataClass myButtonData2 = new ButtonDataClass("MyButton2", "My Button2", Command2.GetMethod(), Properties.Resources.City_32, Properties.Resources.City_32, "This is a tooltip");
            
            // 4. Create buttons
            PushButton myButton = panel.AddItem(myButtonData.Data) as PushButton;
            PushButton myButton2 = panel.AddItem(myButtonData2.Data) as PushButton;
            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }


    }
}
