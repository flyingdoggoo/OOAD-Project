using System;
using System.Windows.Forms;
using CalendarApp.Data; 

namespace CalendarApp
{
    static class Program
    {
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new login()); 
        }
    }
}