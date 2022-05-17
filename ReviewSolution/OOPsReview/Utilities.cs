using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPsReview
{
    public static class Utilities
    {
        //  static classes are NOT instantiated by the outside user developer
        //  static class items are referenced using the classname.xxx
        //  methods within this class have the keyword static in their signature
        //  static classes are shared between all outside users at the SAME time.
        //  DO NOT consider saving data within a static class BECAUSE you cannot be certain it will be there when you use the class again
        //  consider placing GENERIC re-usable methods within a static class

        //  sample of overloaded methods
        //  the method signature
        public static bool IsZeroPositive(int value)
        {
            bool valid = true;
            if (value < 0)
            {
                valid = false;
            }
            return valid;
        }

        public static bool IsZeroPositive(double value)
        {
            bool valid = true;
            if (value < 0)
            {
                valid = false;
            }
            return valid;
        }

        public static bool IsZeroPositive(decimal value)
        {
            bool valid = true;
            if (value < 0)
            {
                valid = false;
            }
            return valid;
        }
    }
}
