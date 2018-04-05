using System;
namespace application
{
	public class Integral
	{
		private Integral()
		{}
		public static double Opr_Integral(del f, double a,double b, int n){
			double s, x, h, f1, f2, f3, I;
			f1 = f(a);
			f2 = f(b);
			h = (b - a) / n;//делим отрезок на равные части
			s = 0;
			for (int i = 1; i < n; i++)
			{
				x = a + h * i;
				f3 = f(x);
				s = s + f3;
			}
			I=h*( ((f1 + f2)/2) + s );
			return I;
		}
		public static double Simpson_Integral(del f, double a,double b,int n){
			double h =(b - a)/n, s=f(a) + f(b), s2 = 0, s4 = 0, x = a + h;
			for(int i=1; i<n-1; i+=2)
			{
				s4 += f(x);
				x += h;
				s2 += f(x);
				x += h;
				
			}
			s4 += f(x);
			s += (s4 * 4 + s2 * 2);
			return s * h/3;
		}
	}
}