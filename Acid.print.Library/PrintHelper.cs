﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acid.print.Library
{
    public class PrintHelper
    {
        private static string printName = "Gprinter GP-3120TU";
        public static void print(string code)
        {
            PrintSDK.openport(printName);                                           //Open specified printer driver
            PrintSDK.setup("40", "11.9", "4", "8", "0", "0", "0");                          　　 //Setup the media size and sensor type info        
            PrintSDK.clearbuffer();
            PrintSDK.barcode("20", "0", "128", "30", "1", "0", "2", "2", code);
            PrintSDK.printlabel("1", "10");                                                    //Print labels
            PrintSDK.closeport();
        }
    }
}
