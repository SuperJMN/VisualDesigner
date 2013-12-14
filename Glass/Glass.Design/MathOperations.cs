using System;

namespace Glass.Design
{
    public static class MathOperations
    {
        public static double NearestMultiple(double number, double multipleBase)
        {
            var division = Math.Round(number / multipleBase);
            return division * multipleBase;
        }

        public static double Snap(double input, double attraction, double maxDistance)
        {
            var distance = Math.Abs(input - attraction);
            return distance >= maxDistance ? input : attraction;
        }
    }
}