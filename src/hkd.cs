﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlaUI.Core;

namespace KeyAll.core
{
    internal class Hkd
    {
        public Hkd()
        {
            // Daemon for button presses/keystrokes
            Application.Launch("Notepad.exe");
        }
    }
}
