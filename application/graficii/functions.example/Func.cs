using System;
namespace application
{
	class Func {
		public static double f(double x) {
			return (double)Calculation.Calc(MainForm.OPZ, x.ToString());			
		}
		public static double ff(double x) {			
			return x*x*x*x*5;			
		}
	}
}
