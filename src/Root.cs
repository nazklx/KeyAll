using System;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.UIA3;
using FlaUI.UIA3.EventHandlers;
using KeyAll.src;

namespace KeyAll.core
{
    class Root
    {
        static void Main(string[] args)
        {
            // Initialize other classes
            Hkd hkdaemon = new Hkd();
            Tbm taskbarmenu = new Tbm();

            // Open notepad, find close button and press it
            var application = Application.Launch("Notepad.exe");

            var mainWindow = application.GetMainWindow(new UIA3Automation());
            ConditionFactory cf = new ConditionFactory(new UIA3PropertyLibrary());

            mainWindow.FindFirstDescendant(cf.ByName("Close")).AsButton().Click();

        }
    }
}