using System;

namespace Glass.Design.Pcl.Core
{
    public struct Point
    {
        internal double _x;
        internal double _y;

        /// <summary>
        /// Gets or sets the <see cref="P:System.Windows.Point.X"/>-coordinate value of this <see cref="T:System.Windows.Point"/> structure.
        /// </summary>
        /// 
        /// <returns>
        /// The <see cref="P:System.Windows.Point.X"/>-coordinate value of this <see cref="T:System.Windows.Point"/> structure.  The default value is 0.
        /// </returns>
        public double X
        {
            
            
            get
            {
                return this._x;
            }
            
            set
            {
                this._x = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="P:System.Windows.Point.Y"/>-coordinate value of this <see cref="T:System.Windows.Point"/>.
        /// </summary>
        /// 
        /// <returns>
        /// The <see cref="P:System.Windows.Point.Y"/>-coordinate value of this <see cref="T:System.Windows.Point"/> structure.  The default value is 0.
        /// </returns>
        public double Y
        {
            
            get
            {
                return this._y;
            }
            
            set
            {
                this._y = value;
            }
        }

        /// <summary>
        /// Creates a new <see cref="T:System.Windows.Point"/> structure that contains the specified coordinates.
        /// </summary>
        /// <param name="x">The x-coordinate of the new <see cref="T:System.Windows.Point"/> structure. </param><param name="y">The y-coordinate of the new <see cref="T:System.Windows.Point"/> structure. </param>
        
        public Point(double x, double y)
        {
            this._x = x;
            this._y = y;
        }

        /// <summary>
        /// Creates a <see cref="T:System.Windows.Size"/> structure with a <see cref="P:System.Windows.Size.Width"/> equal to this point's <see cref="P:System.Windows.Point.X"/> value and a <see cref="P:System.Windows.Size.Height"/> equal to this point's <see cref="P:System.Windows.Point.Y"/> value.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Windows.Size"/> structure with a <see cref="P:System.Windows.Size.Width"/> equal to this point's <see cref="P:System.Windows.Point.X"/> value and a <see cref="P:System.Windows.Size.Height"/> equal to this point's <see cref="P:System.Windows.Point.Y"/> value.
        /// </returns>
        /// <param name="point">The point to convert.</param>
        public static explicit operator Size(Point point)
        {
            return new Size(Math.Abs(point._x), Math.Abs(point._y));
        }

        /// <summary>
        /// Creates a <see cref="T:System.Windows.Vector"/> structure with an <see cref="P:System.Windows.Vector.X"/> value equal to the point's <see cref="P:System.Windows.Point.X"/> value and a <see cref="P:System.Windows.Vector.Y"/> value equal to the point's <see cref="P:System.Windows.Point.Y"/> value.
        /// </summary>
        /// 
        /// <returns>
        /// A vector with an <see cref="P:System.Windows.Vector.X"/> value equal to the point's <see cref="P:System.Windows.Point.X"/> value and a <see cref="P:System.Windows.Vector.Y"/> value equal to the point's <see cref="P:System.Windows.Point.Y"/> value.
        /// </returns>
        /// <param name="point">The point to convert.</param>
        public static explicit operator Vector(Point point)
        {
            return new Vector(point._x, point._y);
        }

        /// <summary>
        /// Compares two <see cref="T:System.Windows.Point"/> structures for equality.
        /// </summary>
        /// 
        /// <returns>
        /// true if both the <see cref="P:System.Windows.Point.X"/> and <see cref="P:System.Windows.Point.Y"/> coordinates of <paramref name="point1"/> and <paramref name="point2"/> are equal; otherwise, false.
        /// </returns>
        /// <param name="point1">The first <see cref="T:System.Windows.Point"/> structure to compare.</param><param name="point2">The second <see cref="T:System.Windows.Point"/> structure to compare.</param>
        public static bool operator ==(Point point1, Point point2)
        {
            if (point1.X == point2.X)
                return point1.Y == point2.Y;
            else
                return false;
        }

        /// <summary>
        /// Compares two <see cref="T:System.Windows.Point"/> structures for inequality.
        /// </summary>
        /// 
        /// <returns>
        /// true if <paramref name="point1"/> and <paramref name="point2"/> have different <see cref="P:System.Windows.Point.X"/> or <see cref="P:System.Windows.Point.Y"/> coordinates; false if <paramref name="point1"/> and <paramref name="point2"/> have the same <see cref="P:System.Windows.Point.X"/> and <see cref="P:System.Windows.Point.Y"/> coordinates.
        /// </returns>
        /// <param name="point1">The first point to compare.</param><param name="point2">The second point to compare.</param>
        public static bool operator !=(Point point1, Point point2)
        {
            return !(point1 == point2);
        }

        /// <summary>
        /// Translates the specified <see cref="T:System.Windows.Point"/> by the specified <see cref="T:System.Windows.Vector"/> and returns the result.
        /// </summary>
        /// 
        /// <returns>
        /// The result of translating the specified point by the specified vector.
        /// </returns>
        /// <param name="point">The point to translate.</param><param name="vector">The amount by which to translate <paramref name="point"/>.</param>
        public static Point operator +(Point point, Vector vector)
        {
            return new Point(point._x + vector._x, point._y + vector._y);
        }

        /// <summary>
        /// Subtracts the specified <see cref="T:System.Windows.Vector"/> from the specified <see cref="T:System.Windows.Point"/> and returns the resulting <see cref="T:System.Windows.Point"/>.
        /// </summary>
        /// 
        /// <returns>
        /// The difference between <paramref name="point"/> and <paramref name="vector"/>.
        /// </returns>
        /// <param name="point">The point from which <paramref name="vector"/> is subtracted. </param><param name="vector">The vector to subtract from <paramref name="point1"/></param>
        public static Point operator -(Point point, Vector vector)
        {
            return new Point(point._x - vector._x, point._y - vector._y);
        }

        /// <summary>
        /// Subtracts the specified <see cref="T:System.Windows.Point"/> from another specified <see cref="T:System.Windows.Point"/> and returns the difference as a <see cref="T:System.Windows.Vector"/>.
        /// </summary>
        /// 
        /// <returns>
        /// The difference between <paramref name="point1"/> and <paramref name="point2"/>.
        /// </returns>
        /// <param name="point1">The point from which <paramref name="point2"/> is subtracted.</param><param name="point2">The point to subtract from <paramref name="point1"/>.</param>
        public static Vector operator -(Point point1, Point point2)
        {
            return new Vector(point1._x - point2._x, point1._y - point2._y);
        }


        /// <summary>
        /// Compares two <see cref="T:System.Windows.Point"/> structures for equality.
        /// </summary>
        /// 
        /// <returns>
        /// true if <paramref name="point1"/> and <paramref name="point2"/> contain the same <see cref="P:System.Windows.Point.X"/> and <see cref="P:System.Windows.Point.Y"/> values; otherwise, false.
        /// </returns>
        /// <param name="point1">The first point to compare.</param><param name="point2">The second point to compare.</param>
        public static bool Equals(Point point1, Point point2)
        {
            if (point1.X.Equals(point2.X))
                return point1.Y.Equals(point2.Y);
            else
                return false;
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is a <see cref="T:System.Windows.Point"/> and whether it contains the same coordinates as this <see cref="T:System.Windows.Point"/>.
        /// </summary>
        /// 
        /// <returns>
        /// true if <paramref name="o"/> is a <see cref="T:System.Windows.Point"/> and contains the same <see cref="P:System.Windows.Point.X"/> and <see cref="P:System.Windows.Point.Y"/> values as this <see cref="T:System.Windows.Point"/>; otherwise, false.
        /// </returns>
        /// <param name="o">The <see cref="T:System.Object"/> to compare.</param>
        public override bool Equals(object o)
        {
            if (o == null || !(o is Point))
                return false;
            else
                return Point.Equals(this, (Point)o);
        }

        /// <summary>
        /// Compares two <see cref="T:System.Windows.Point"/> structures for equality.
        /// </summary>
        /// 
        /// <returns>
        /// true if both <see cref="T:System.Windows.Point"/> structures contain the same <see cref="P:System.Windows.Point.X"/> and <see cref="P:System.Windows.Point.Y"/> values; otherwise, false.
        /// </returns>
        /// <param name="value">The point to compare to this instance.</param>
        public bool Equals(Point value)
        {
            return Point.Equals(this, value);
        }

        /// <summary>
        /// Returns the hash code for this <see cref="T:System.Windows.Point"/>.
        /// </summary>
        /// 
        /// <returns>
        /// The hash code for this <see cref="T:System.Windows.Point"/> structure.
        /// </returns>
        public override int GetHashCode()
        {
            return this.X.GetHashCode() ^ this.Y.GetHashCode();
        }
     


        /// <summary>
        /// Offsets a point's <see cref="P:System.Windows.Point.X"/> and <see cref="P:System.Windows.Point.Y"/> coordinates by the specified amounts.
        /// </summary>
        /// <param name="offsetX">The amount to offset the point's<see cref="P:System.Windows.Point.X"/> coordinate. </param><param name="offsetY">The amount to offset thepoint's <see cref="P:System.Windows.Point.Y"/> coordinate.</param>
        public void Offset(double offsetX, double offsetY)
        {
            this._x += offsetX;
            this._y += offsetY;
        }

        /// <summary>
        /// Adds a <see cref="T:System.Windows.Vector"/> to a <see cref="T:System.Windows.Point"/> and returns the result as a <see cref="T:System.Windows.Point"/> structure.
        /// </summary>
        /// 
        /// <returns>
        /// Returns the sum of <paramref name="point"/> and <paramref name="vector"/>.
        /// </returns>
        /// <param name="point">The <see cref="T:System.Windows.Point"/> structure to add.</param><param name="vector">The <see cref="T:System.Windows.Vector"/> structure to add. </param>
        public static Point Add(Point point, Vector vector)
        {
            return new Point(point._x + vector._x, point._y + vector._y);
        }

        /// <summary>
        /// Subtracts the specified <see cref="T:System.Windows.Vector"/> from the specified <see cref="T:System.Windows.Point"/> and returns the resulting <see cref="T:System.Windows.Point"/>.
        /// </summary>
        /// 
        /// <returns>
        /// The difference between <paramref name="point"/> and <paramref name="vector"/>.
        /// </returns>
        /// <param name="point">The point from which <paramref name="vector"/> is subtracted.</param><param name="vector">The <paramref name="vector"/> to subtract from <paramref name="point"/>.</param>
        public static Point Subtract(Point point, Vector vector)
        {
            return new Point(point._x - vector._x, point._y - vector._y);
        }

        /// <summary>
        /// Subtracts the specified <see cref="T:System.Windows.Point"/> from another specified <see cref="T:System.Windows.Point"/> and returns the difference as a <see cref="T:System.Windows.Vector"/>.
        /// </summary>
        /// 
        /// <returns>
        /// The difference between <paramref name="point1"/> and <paramref name="point2"/>.
        /// </returns>
        /// <param name="point1">The point from which <paramref name="point2"/> is subtracted. </param><param name="point2">The point to subtract from <paramref name="point1"/>.</param>
        public static Vector Subtract(Point point1, Point point2)
        {
            return new Vector(point1._x - point2._x, point1._y - point2._y);
        }


    }
}