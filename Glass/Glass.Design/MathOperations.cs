using System;

namespace Glass.Design
{
    public static class MathOperations
    {
        public static double NearestMultiple(double number, int multipleBase)
        {
            var division = Math.Round(number / multipleBase);
            return division * multipleBase;
        }
    }
}