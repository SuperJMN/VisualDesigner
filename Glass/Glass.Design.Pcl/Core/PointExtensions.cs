namespace Glass.Design.Pcl.Core
{
    public static class PointExtensions
    {
        public static IPoint Subtract(this IPoint point, IVector vector)
        {
            return ServiceLocator.CoreTypesFactory.CreatePoint(point.X - vector.X, point.Y - vector.Y);
        }

        public static IPoint Subtract(this IPoint point, IPoint vector)
        {
            return ServiceLocator.CoreTypesFactory.CreatePoint(point.X - vector.X, point.Y - vector.Y);
        }
        public static IPoint Add(this IPoint point, IPoint vector)
        {
            return ServiceLocator.CoreTypesFactory.CreatePoint(point.X + vector.X, point.Y + vector.Y);
        }

        public static IPoint Add(this IPoint point, IVector vector)
        {
            return ServiceLocator.CoreTypesFactory.CreatePoint(point.X + vector.X, point.Y + vector.Y);
        }
    }
}