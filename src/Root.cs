using System;
using System.Drawing;
using System.Runtime.InteropServices;
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
        static Form f = new Form();
        static String[] flags;
        static void Main(string[] args)
        {
            // Default cmd visibility is none, but check if using cli first
            int w_visibility = 0;
            if (args.Length > 0)
            {
                foreach (var arg in args) { flags.Append(arg); }
                if (flags[0] == "-cli")
                {
                    w_visibility = 5;
                }
            }
            [DllImport("kernel32.dll")]
            static extern IntPtr GetConsoleWindow();

            [DllImport("user32.dll")]
            static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
            var handle = GetConsoleWindow();
            ShowWindow(handle, w_visibility);


            // Register keystrokes, create event handler and keep console running
            Hkd.RegisterHotKey(Keys.A, KeyModifiers.Alt);
            Hkd.RegisterHotKey(Keys.F, KeyModifiers.Alt);
            Hkd.HotKeyPressed += new EventHandler<HotKeyEventArgs>(HotKeyManager_HotKeyPressed);
            Console.ReadLine();


            // Open notepad, find close button and press it
            var application = FlaUI.Core.Application.Launch("Notepad.exe");

            var mainWindow = application.GetMainWindow(new UIA3Automation());
            ConditionFactory cf = new ConditionFactory(new UIA3PropertyLibrary());

            mainWindow.FindFirstDescendant(cf.ByName("Close")).AsButton().Click();
            
            // I'm testing with having an overlay here, can't even tell if this is working
            f.BackColor = Color.White;
            f.FormBorderStyle = FormBorderStyle.None;
            f.Bounds = Screen.PrimaryScreen.Bounds;
            f.TopMost = true;

            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.Run(f);
        }
        static void HotKeyManager_HotKeyPressed(object sender, HotKeyEventArgs e)
        {
            // Go through HotKeyEventArgs to see what keystroke was pressed and execute code
            switch (e.Key)
            {
                // Eventually these will be dynamic to allow customization of keybindings through config file.
                // Close application with Alt+A
                case Keys.A when e.Modifiers == KeyModifiers.Alt:
                    Environment.Exit(0);
                    break;
                // Run test code with Alt+F
                case Keys.F when e.Modifiers == KeyModifiers.Alt:
                    System.Windows.Forms.MessageBox.Show("Bam");
                    f.TopMost = true;
                    break;
            }
        }
    }
}