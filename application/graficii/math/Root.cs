using System;
namespace application
{
	public class Root
	{
		private Root()
		{}
		public static double Polov_Del(del f, double a, double b, double eps)
		{
			double x=0, fa, fb, fc, c;
			fa = f(a);
			fb = f(b);
			if (fa * fb > 0)
			{
				Console.WriteLine("корней нет");
				return x;
			}
			while (Math.Abs(b-a)>eps)
			{
				c = (a + b)/2;
				fc = f(c);
				if(fa * fc > 0)
				{
					a = c;
					fa = fc;
				}
				else {
					b = c;
				}
			}
			x = (a + b)/2;
			return x;
		}
		public static double Newton_meth(del f, del2 df, double xn, double eps)
		{
			double x1  = xn - f(xn)/df(f,xn);
			double x0 = xn;
			while((x0-x1)>eps)
			{
				x0 = x1;
				x1 = x1 -f(x1)/df(f,xn);
			}
			return x1;
		}
	}
}
