using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// тестовые функции для объекта vector2d

namespace ConsoleAppTest
{
    public static class TestUtil
    {
        
        public static int[] RandomIndices(int Count, Random r, int max_index)
        {
            int[] v = new int[Count];
            for (int k = 0; k < Count; ++k)
                v[k] = r.Next() % max_index;
            return v;
        }

        public static Vector2d[] RandomPoints2(int Count, Vector2d center, double scale = 1.0)
        {
            Random r = new Random();
            Vector2d[] v = new Vector2d[Count];
            for (int k = 0; k < Count; ++k)
                //v[k] = new Vector2d(scale * 2.0 * (r.NextDouble() - 0.5), scale * 2.0 * (r.NextDouble() - 0.5)) + center;
                v[k] = new Vector2d((int)(-(r.Next() % 50)+30), (int)(-(r.Next()%20) + 40));
            return v;
        }

        public static void ViewVector2d(Vector2d vd)
        {            
            Console.WriteLine("v = (" + vd.x + "; " + vd.y + ")");
        }

        public static void Operation()
        {
            Vector2d[] vd2 = RandomPoints2(2, new Vector2d(1.0, 1.0));

            for(int i=0; i<vd2.Count(); i++)
            {
                if(i==0)Console.WriteLine("v["+i+"] = ("+vd2[i].x+"; "+vd2[i].y+")");
                else Console.WriteLine("v[" + i + "] = (" + vd2[i].x + "; " + vd2[i].y + ")");
            }

            Console.WriteLine("Перемещение точки v[0] вдоль v[1]");
            Vector2d v_move = vd2[0].Moving(vd2[1]);
            ViewVector2d(v_move);

            Console.WriteLine("Поворот точки v[0] вокруг v[1]");
            Vector2d v_rotation = vd2[0].Rotation(vd2[1], MathUtil.PIf/6);
            ViewVector2d(v_rotation);

            Console.WriteLine("Перпендикулярность векторов v[0] и v[1]");
            double res = vd2[0].Dot(vd2[1]);
            Console.WriteLine("v[0]*v[1] = "+ res + "; "+((Math.Abs(res)<0.0001)?"перпендикулярны": "не перпендикулярны"));
            


            return;
        }



        public static double[] RandomScalars(int Count, Random r, Interval1d range)
        {
            double[] v = new double[Count];
            for (int k = 0; k < Count; ++k)
            {
                v[k] = range.a + r.NextDouble() * range.Length;
            }
            return v;
        }



    }
}
