using System;
namespace application
{
	public class Differential
	{
		private Differential()
		{
		}
		public static double Proizvodnaya1( del f, double x)
		{
			double dx = 0.00001;
			double P1 = (f(x+dx)-f(x-dx))/(2* dx);
			return P1;
		}
	}
}
