using System;
namespace application
{
	public class RungeKutta
	{
		private RungeKutta()
		{
		}
		public static int rk4(del3 f, double []x, double []y, int nmax, double x0, double xk, double y0, double h)
		{
			int i=0, n=(int)((xk-x0)/h);
			if(n>nmax) return -1;
			double k0, k1, k2, k3;
			x[0]=x0;
			y[0]=y0;
			for(i=0; i<n; i++)
			{
				k0=h*f(x[i], y[i]);
				k1=h*f(x[i]+h/2, y[i]+k0/2);
				k2=h*f(x[i]+h/2, y[i]+k1/2);
				k3=h*f(x[i]+h, y[i]+k2);
				y[i+1]=y[i]+(k0+2*k1+2*k2+k3)/6;
				x[i+1]=x[i]+h;
			}
			return n+1;
		}
		public static int rk4_M(dele f_M, double []x, double [,]y, int nmax, int m, double x0, double xk, double []y0, double h)
		{
			int i=0, j, n=(int)((xk-x0)/h);
			if(n>nmax) return -1;
			double []k0 = new double[m];
			double []k1 = new double[m];
			double []k2 = new double[m];
			double []k3 = new double[m];
			double []yy = new double[m];
			
			x[0]=x0;
			for(j=0; j<m; j++){
				y[0,j]=y0[j];
			}

			for(i=0; i<n; i++)
			{
				for(j=0; j<m; j++)
					yy[j]=y[i,j];
				f_M(x[i], yy, k0);
				for(j=0; j<m; j++)
					k0[j] *= h;

				for(j=0; j<m; j++)
					yy[j]=y[i,j] + k0[j]/2;
				f_M(x[i]+h/2, yy, k1);
				for(j=0; j<m; j++)
					k1[j] *= h;

				for(j=0; j<m; j++)
					yy[j]=y[i,j] + k1[j]/2;
				f_M(x[i]+h/2, yy, k2);
				for(j=0; j<m; j++)
					k2[j] *= h;

				for(j=0; j<m; j++)
					yy[j]=y[i,j] + k2[j]/2;
				f_M(x[i]+h/2, yy, k3);
				for(j=0; j<m; j++)
					k3[j] *= h;
				for(j=0; j<m; j++)
					y[i+1,j]=y[i,j]+(k0[j]+2*k1[j]+2*k2[j]+k3[j])/6;
				x[i+1]=x[i]+h;
			}
			return n+1;
		}
	}
	public class Euler
	{
		private Euler()
		{
		}
		public static int EulerMeth(del3 f, double []x, double []y, int nmax, double x0, double xk, double y0, double h)
		{
			int i=0, n=(int)((xk-x0)/h);
			if(n>nmax) return -1;
			
			x[0]=x0;
			y[0]=y0;
			for(i=0; i<n; i++)
			{
				y[i+1]=y[i]+h* f(x[i], y[i]);
				x[i+1]=x[i]+h;
			}
			return n+1;
		}
		
		public static int Ejler_M(dele f_M, double []x, double [,]y, int nmax, int m, double x0, double xk, double []y0, double h)
		{
			int i=0, j, n=(int)((xk-x0)/h);
			if(n>nmax) return -1;
			double [] k0 = new double[m];
			double [] yy = new double[m];
			x[0]=x0;
			for(j=0; j<m; j++){
				y[0,j]=y0[j];
			}

			for(i=0; i<n; i++)
			{
				for(j=0; j<m; j++)
					yy[j]=y[i,j];
				f_M(x[i], yy, k0);
				for(j=0; j<m; j++)
					y[i+1,j]=y[i,j]+ h*k0[j];
				x[i+1]=x[i]+h;
			}
			return n+1;
		}
	}
}
