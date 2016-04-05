using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficPoliceDesktopApp.Utilities
{
   public static class DateParser
    {

       public static string parseDateSqlFormat(DateTime dt)
       {
           return dt.ToString("yyyy-MM-dd");
       }

    }
}
