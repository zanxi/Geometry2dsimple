using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTest
{
    public struct Vector2d : IComparable<Vector2d>, IEquatable<Vector2d>
    {
        public double x;
        public double y;
        // инициалиализация вектора-точки с координатами (x,y)
        public Vector2d(double f) { x = y = f; }
        public Vector2d(double x, double y) { this.x = x; this.y = y; }
        public Vector2d(double[] v2) { x = v2[0]; y = v2[1]; }
        public Vector2d(float f) { x = y = f; }
        public Vector2d(float x, float y) { this.x = x; this.y = y; }
        public Vector2d(float[] v2) { x = v2[0]; y = v2[1]; }
        public Vector2d(Vector2d copy) { x = copy.x; y = copy.y; }
        
        // задание контанст
        // начало координат
        static public readonly Vector2d Zero = new Vector2d(0.0f, 0.0f);
        static public readonly Vector2d One = new Vector2d(1.0f, 1.0f);
        // орта абсцисс
        static public readonly Vector2d AxisX = new Vector2d(1.0f, 0.0f);
        // орта ординат
        static public readonly Vector2d AxisY = new Vector2d(0.0f, 1.0f);
        static public readonly Vector2d MaxValue = new Vector2d(double.MaxValue, double.MaxValue);
        static public readonly Vector2d MinValue = new Vector2d(double.MinValue, double.MinValue);

        // задаем угол вектра-точки
        public static Vector2d FromAngleRad(double angle)
        {
            return new Vector2d(Math.Cos(angle), Math.Sin(angle));
        }

        // перевод угла в градусах
        public static Vector2d FromAngleDeg(double angle)
        {
            angle *= MathUtil.Deg2Rad;
            return new Vector2d(Math.Cos(angle), Math.Sin(angle));
        }


        public double this[int key]
        {
            get { return (key == 0) ? x : y; }
            set { if (key == 0) x = value; else y = value; }
        }

        // поворот относительно точки v2 на угол phi
        public Vector2d Rotation(Vector2d v2,double phirad)
        {
            return new Vector2d(
                (x-v2.x)*Math.Cos(phirad) - (y-v2.y) * Math.Sin(phirad) + v2.x,
                (x-v2.x) * Math.Sin(phirad) + (y-v2.y) * Math.Cos(phirad))+ v2.y;
        }

        // перемещение точки вдоль вектора
        public Vector2d Moving(Vector2d v2)
        {
            return new Vector2d(v2.x - x, v2.y - y);
        }


        // квадрат длины вектора
        public double LengthSquared
        {
            get { return x * x + y * y; }
        }

        // длина вектора
        public double Length
        {
            get { return (double)Math.Sqrt(LengthSquared); }
        }

        // нормализация вектора ~A = A/|A|
        public double Normalize(double epsilon = MathUtil.Epsilon)
        {
            double length = Length;
            if (length > epsilon)
            {
                double invLength = 1.0 / length;
                x *= invLength;
                y *= invLength;
            }
            else
            {
                length = 0;
                x = y = 0;
            }
            return length;
        }
        public Vector2d Normalized
        {
            get
            {
                double length = Length;
                if (length > MathUtil.Epsilon)
                {
                    double invLength = 1 / length;
                    return new Vector2d(x * invLength, y * invLength);
                }
                else
                    return Vector2d.Zero;
            }
        }

        // проверка нормализации вектора
        public bool IsNormalized
        {
            get { return Math.Abs((x * x + y * y) - 1) < MathUtil.ZeroTolerance; }
        }

        // проверить бесконеный ли вектор
        public bool IsFinite
        {
            get { double f = x + y; return double.IsNaN(f) == false && double.IsInfinity(f) == false; }
        }

        public void Round(int nDecimals)
        {
            x = Math.Round(x, nDecimals);
            y = Math.Round(y, nDecimals);
        }

        // скалярное умножение вектора на вектор v2
        public double Dot(Vector2d v2)
        {
            return x * v2.x + y * v2.y;
        }


        /// <summary>
        /// returns cross-product of this vector with v2 (same as DotPerp)
        /// </summary>
        public double Cross(Vector2d v2)
        {
            return x * v2.y - y * v2.x;
        }


        /// <summary>
        /// возвращает правосторонний вектор, т.е. повернутый на 90 градусов вправо
        /// </summary>
		public Vector2d Perp
        {
            get { return new Vector2d(y, -x); }
        }

        /// <summary>
        /// возвращает нормализованный правосторонний вектор, т.е. повернутый на 90 градусов вправо
        /// </summary>
		public Vector2d UnitPerp
        {
            get { return new Vector2d(y, -x).Normalized; }
        }

        /// <summary>
        /// возвращает скалярное произведение этого вектора с v2.Perp
        /// </summary>
		public double DotPerp(Vector2d v2)
        {
            return x * v2.y - y * v2.x;
        }

        // угол вектора
        public double AngleD(Vector2d v2)
        {
            double fDot = MathUtil.Clamp(Dot(v2), -1, 1);
            return Math.Acos(fDot) * MathUtil.Rad2Deg;
        }

        // угол между векторами
        public static double AngleD(Vector2d v1, Vector2d v2)
        {
            return v1.AngleD(v2);
        }

        // угол между векторами
        public double AngleR(Vector2d v2)
        {
            double fDot = MathUtil.Clamp(Dot(v2), -1, 1);
            return Math.Acos(fDot);
        }

        // угол между векторами
        public static double AngleR(Vector2d v1, Vector2d v2)
        {
            return v1.AngleR(v2);
        }


        // квадрат расстояние между точками
        public double DistanceSquared(Vector2d v2)
        {
            double dx = v2.x - x, dy = v2.y - y;
            return dx * dx + dy * dy;
        }
        // расстояние между точками
        public double Distance(Vector2d v2)
        {
            double dx = v2.x - x, dy = v2.y - y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        // задаем вектор
        public void Set(Vector2d o)
        {
            x = o.x; y = o.y;
        }

        // задаем вектор
        public void Set(double fX, double fY)
        {
            x = fX; y = fY;
        }

        // прибавляем вектор o
        public void Add(Vector2d o)
        {
            x += o.x; y += o.y;
        }

        // отнимаем вектор o
        public void Subtract(Vector2d o)
        {
            x -= o.x; y -= o.y;
        }


        // инверсия вектора
        public static Vector2d operator -(Vector2d v)
        {
            return new Vector2d(-v.x, -v.y);
        }


        // операции с векторами
        public static Vector2d operator +(Vector2d a, Vector2d o)
        {
            return new Vector2d(a.x + o.x, a.y + o.y);
        }
        public static Vector2d operator +(Vector2d a, double f)
        {
            return new Vector2d(a.x + f, a.y + f);
        }

        public static Vector2d operator -(Vector2d a, Vector2d o)
        {
            return new Vector2d(a.x - o.x, a.y - o.y);
        }
        public static Vector2d operator -(Vector2d a, double f)
        {
            return new Vector2d(a.x - f, a.y - f);
        }

        public static Vector2d operator *(Vector2d a, double f)
        {
            return new Vector2d(a.x * f, a.y * f);
        }
        public static Vector2d operator *(double f, Vector2d a)
        {
            return new Vector2d(a.x * f, a.y * f);
        }
        public static Vector2d operator /(Vector2d v, double f)
        {
            return new Vector2d(v.x / f, v.y / f);
        }
        public static Vector2d operator /(double f, Vector2d v)
        {
            return new Vector2d(f / v.x, f / v.y);
        }


        public static Vector2d operator *(Vector2d a, Vector2d b)
        {
            return new Vector2d(a.x * b.x, a.y * b.y);
        }
        public static Vector2d operator /(Vector2d a, Vector2d b)
        {
            return new Vector2d(a.x / b.x, a.y / b.y);
        }


        public static bool operator ==(Vector2d a, Vector2d b)
        {
            return (a.x == b.x && a.y == b.y);
        }
        public static bool operator !=(Vector2d a, Vector2d b)
        {
            return (a.x != b.x || a.y != b.y);
        }
        public override bool Equals(object obj)
        {
            return this == (Vector2d)obj;
        }
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = (int)2166136261;
                // Suitable nullity checks etc, of course :)
                hash = (hash * 16777619) ^ x.GetHashCode();
                hash = (hash * 16777619) ^ y.GetHashCode();
                return hash;
            }
        }
        public int CompareTo(Vector2d other)
        {
            if (x != other.x)
                return x < other.x ? -1 : 1;
            else if (y != other.y)
                return y < other.y ? -1 : 1;
            return 0;
        }
        public bool Equals(Vector2d other)
        {
            return (x == other.x && y == other.y);
        }


        public bool EpsilonEqual(Vector2d v2, double epsilon)
        {
            return Math.Abs(x - v2.x) <= epsilon &&
                   Math.Abs(y - v2.y) <= epsilon;
        }


        public static Vector2d Lerp(Vector2d a, Vector2d b, double t)
        {
            double s = 1 - t;
            return new Vector2d(s * a.x + t * b.x, s * a.y + t * b.y);
        }
        public static Vector2d Lerp(ref Vector2d a, ref Vector2d b, double t)
        {
            double s = 1 - t;
            return new Vector2d(s * a.x + t * b.x, s * a.y + t * b.y);
        }


        public override string ToString()
        {
            return string.Format("{0:F8} {1:F8}", x, y);
        }



    }




}
