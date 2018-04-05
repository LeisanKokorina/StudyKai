using System;
namespace application
{
	class Runge{
		public static double f(double x,double y){
			return (Math.Sin(x)*y);
		}
		public static void f_M(double x, double []y, double []y1)
		{
			y1[0]=y[0]-1;
			y1[1]=y[0]+2*y[1]-3;
		}
	}
}
