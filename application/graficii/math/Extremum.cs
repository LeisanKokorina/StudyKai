using System;
namespace application
{
	public class Extremum
	{
		public Extremum()
		{
		}
		public static double FindMin(del f, double a, double b)//метод золотого сечения
		{
			double e= 0.0000001;
			double y1,y2;
			double s = (1 + Math.Sqrt(5))/2;
			double x1 = b - (b-a)/s;
			double x2 = a + (b-a)/s;
			y1 = f(x1);
			y2 = f(x2);
			while(Math.Abs(b-a)>e)
			{
				if (y1>=y2)
				{
					a = x1;
					x1 = x2;
					y1 = y2;
					x2 = a + (b-a)/s;
					y2 = f(x2);

				}
				else
				{
					b = x2;
					x2 = x1;
					y2 = y1;
					x1 = b -(b-a)/s;
					y1 = f(x1);

				}
			}
			return (a+b)/2;
			
		}
		public static double FindMax(del f, double a, double b)
		{
			double e= 0.0000001;
			double y1,y2;
			double s = (1 + Math.Sqrt(5))/2;
			double x1 = b - (b-a)/s;
			double x2 = a + (b-a)/s;
			y1 = f(x1);
			y2 = f(x2);
			while(Math.Abs(b-a)>e)
			{
				if (y1<=y2)
				{
					a = x1;
					x1 = x2;
					y1 = y2;
					x2 = a + (b-a)/s;
					y2 = f(x2);

				}
				else
				{
					b = x2;
					x2 = x1;
					y2 = y1;
					x1 = b -(b-a)/s;
					y1 = f(x1);

				}
			}
			return (a+b)/2;
			
		}
		public static double MethDrobMin (del f, double x0, double h )
		{
			double eps = 0.00001;
			double x1;
			double x2=x0;
			
			while (Math.Abs(h) > eps)
			{
				x1=x2;
				x2=x1+h;
				if(f(x1)<f(x2) ) h=-h/3;
			}
			return x2;
		}
		public static double MethDrobMax (del f, double x0, double h )
		{
			double eps = 0.00001;
			double x1;
			double x2=x0;
			
			while (Math.Abs(h) > eps)
			{
				x1=x2;
				x2=x1+h;
				if(f(x1)>f(x2) ) h=-h/3;
			}
			return x2;
		}
	}
}
