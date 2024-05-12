using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.UIA3;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace KeyAll.core
{
    class Root
    {
        static Form f = new Form();
        static String[] flags = new string[3];
        static List<String> aconf = new List<String>();
        static void Main(string[] args)
        {
            // Load config file first **THIS IS GETTING CHANGED**
            /*string conf = Directory.GetCurrentDirectory() + "\\KeyAll.conf";
            if (File.Exists(conf)) 
            {
                using (StreamReader sr = File.OpenText(conf))
                {
                    string ln;
                    while ((ln = sr.ReadLine()) != null)
                    {
                        aconf.Add(ln);
                    }
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Missing KeyAll.conf, using default configuration");
            }*/

            // Default cmd visibility is none, but will check if using cli first
            int w_visibility = 0;
            if (args.Length > 0)
            {
                int i = 0;
                foreach (var arg in args)
                {
                    flags[i] = arg;
                    i++;
                }
                i = 0;
                for (int l = 0; l < flags.Length; l++)
                {
                    // Here we will add cli arguments
                    switch (flags[i])
                    {
                        case "-cli":
                            w_visibility = 5;
                            break;
                        case "-add-keystroke":
                            break;
                    }
                }
            }

            [DllImport("kernel32.dll")]
            static extern IntPtr GetConsoleWindow();

            [DllImport("user32.dll")]
            static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
            var handle = GetConsoleWindow();
            ShowWindow(handle, w_visibility);


            // Register keystrokes, create event handler and keep console running
            // I'm trying to actually use the config file here, and I'm still figuring that out, until then we're hardcoding each hotkey here on its own line.
            // string[] keystroke = ConfigurationManager.AppSettings["Close"].Split(',');
            Hkd.RegisterHotKey(Keys.A, KeyModifiers.Alt);
            Hkd.RegisterHotKey(Keys.F, KeyModifiers.Alt);
            Hkd.HotKeyPressed += new EventHandler<HotKeyEventArgs>(HotKeyManager_HotKeyPressed);
            Console.ReadLine();


            // Open notepad, find close button and press it
            var application = FlaUI.Core.Application.Launch("Notepad.exe");

            var mainWindow = application.GetMainWindow(new UIA3Automation());
            ConditionFactory cf = new ConditionFactory(new UIA3PropertyLibrary());

            mainWindow.FindFirstDescendant(cf.ByName("Close")).AsButton().Click();

            // I'm testing with having an overlay here, this is being a bit weird
            /*f.BackColor = Color.White;
            f.FormBorderStyle = FormBorderStyle.None;
            f.Bounds = Screen.PrimaryScreen.Bounds;
            f.TopMost = true;

            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.Run(f);*/
        }
        static void HotKeyManager_HotKeyPressed(object sender, HotKeyEventArgs e)
        {
            // Go through HotKeyEventArgs to see what keystroke was pressed and execute code
            switch (e.Key)
            {
                // Eventually these will be dynamic to allow customization of keybindings through config file.
                // Close application with Alt+A
                case Keys.A when e.Modifiers == KeyModifiers.Alt:
                    System.Windows.Forms.MessageBox.Show("Closing");
                    Environment.Exit(0);
                    break;
                // Run test code with Alt+F
                case Keys.F when e.Modifiers == KeyModifiers.Alt:
                    System.Windows.Forms.MessageBox.Show("Bam");
                    //f.TopMost = true;
                    break;
            }
        }
    }
}