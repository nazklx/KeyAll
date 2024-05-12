using System;
using System.Windows.Forms;
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
            // Register keystroke, create event
            Hkd.RegisterHotKey(Keys.A, KeyModifiers.Alt);
            Hkd.HotKeyPressed += new EventHandler<HotKeyEventArgs>(HotKeyManager_HotKeyPressed);
            Console.ReadLine();


            // Open notepad, find close button and press it
            var application = FlaUI.Core.Application.Launch("Notepad.exe");

            var mainWindow = application.GetMainWindow(new UIA3Automation());
            ConditionFactory cf = new ConditionFactory(new UIA3PropertyLibrary());

            mainWindow.FindFirstDescendant(cf.ByName("Close")).AsButton().Click();

        }
        static void HotKeyManager_HotKeyPressed(object sender, HotKeyEventArgs e)
        {
            // Go through HotKeyEventArgs to see what keystroke was pressed and execute code
            switch (e.Key)
            {
                case Keys.A when e.Modifiers == KeyModifiers.Alt:
                    System.Windows.Forms.MessageBox.Show("Bam");
                    Environment.Exit(0);
                    break;
            }
        }
    }
}