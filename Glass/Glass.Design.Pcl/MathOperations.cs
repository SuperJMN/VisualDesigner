using System;

namespace Glass.Design.Pcl
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

        public static int SquareRounding(double value, double total, int levels)
        {
            var hotSpot = value / total * levels;
            var roundedHotSpot = Math.Round(hotSpot);

            return (int)roundedHotSpot;
        }
    }
}