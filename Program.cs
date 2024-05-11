﻿using System;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.UIA3;
using FlaUI.UIA3.EventHandlers;

namespace KeyAll
{
    class Program
    {
        static void Main(string[] args)
        {
            var application = FlaUI.Core.Application.Launch("Notepad.exe");

            var mainWindow = application.GetMainWindow(new UIA3Automation());
            ConditionFactory cf = new ConditionFactory(new UIA3PropertyLibrary());

            mainWindow.FindFirstDescendant(cf.ByName("Close")).AsButton().Click();

        }
    }
}