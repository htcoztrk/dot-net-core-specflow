using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevProject.Configurations
{
    internal class AppConficReader
    {

        public static double GetElementLoadTimeout()
        {
            return 30.0;
        }

        public static double GetPageLoadTimeout()
        {
            return 90;
        }

    }
}
