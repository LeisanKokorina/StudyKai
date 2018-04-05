using System;
using System.Drawing;
using System.Collections.Generic;
namespace application
{
	public class Grafic
	{
		//список линий
		public List <LineGraf> spisokLines = new List<LineGraf>();
		//заголовок для всего графика
		public string title_graf;
		//конструктор
		public Grafic()
		{
			title_graf="График";
		}
		//метод по добавлению новой линии
		public void Add(LineGraf line)
		{
			spisokLines.Add(line);
		}
		//очистка списка линий
		public void Clear()
		{
			spisokLines.Clear();
		}
		//рисование  графика
		public void Draw(Graphics g, Rectangle r)
		{
			Point [] masPoint;
			int i;
			int dx1=50, dx2=20, dy1=5, dy2=30;
			int x0=r.Left+dx1, xk=r.Right-dx2, y0=r.Top+dy1, yk=r.Bottom-dy2;
			MainForm.x0MainForm=x0;
			MainForm.xkMainForm=xk;
			MainForm.y0MainForm=y0;
			MainForm.ykMainForm=yk;
			float minx, miny, maxx, maxy;
			float minx1, miny1, maxx1, maxy1;
			spisokLines[0].minmax(out minx, out miny, out maxx, out maxy);
			for(i=1; i<spisokLines.Count; ++i)
			{
				spisokLines[i].minmax(out minx1, out miny1, out maxx1, out maxy1);
				if(minx1<minx) minx=minx1;
				if(miny1<miny) miny=miny1;
				if(maxx1>maxx) maxx=maxx1;
				if(maxy1>maxy) maxy=maxy1;
			}
			MainForm.minxMainForm=minx;
			MainForm.minyMainForm=miny;
			MainForm.maxxMainForm=maxx;
			MainForm.maxyMainForm=maxy;
			Pen p2 = new Pen(Color.Black,3);
			//внешняя граница графика
			g.DrawRectangle(p2,x0,y0,xk-x0,yk-y0);
			
			Pen p3 = new Pen(Color.Black,1);
			double h, y1, yt, x1, xt;
			int n;
			FontFamily fontFamily = new FontFamily("Times New Roman");
			Font font = new Font(fontFamily, 14, FontStyle.Italic | FontStyle.Bold, GraphicsUnit.Point);
			Brush b1 = new SolidBrush(Color.Blue);
			fNumberLines(miny ,maxy ,  out  h,  out  y1,  out  n);
			for(yt=y1, i=0; i<n; i++)			// Горизонтальные линии
			{
				int ytt=(int) (y0 + (1-(yt-miny)/(maxy-miny))*(yk-y0));
				g.DrawLine(p3, x0, ytt, xk, ytt);
				
				string s= String.Format("{0:f2}", yt);
				if(yt>=-1.0e-15) s=" "+s;
				SizeF size = g.MeasureString(s,font);
				g.DrawString(s,font,b1,5,ytt-(size.Height)/2);
				yt+=h;
			}
			
			fNumberLines(minx ,maxx ,  out  h,  out  x1,  out  n);
			for(xt=x1,i=0; i<n; i++)			// Вертикальные линии
			{
				int xtt = (int) (x0 + (xt-minx)/(maxx-minx)*(xk-x0));
				
				g.DrawLine(p3, xtt, y0, xtt,yk);
				string s= String.Format("{0:f2}", xt);
				if(xt>=-1.0e-15) s=" "+s;
				SizeF size = g.MeasureString(s,font);
				g.DrawString(s,font,b1,xtt-(size.Width)/2, yk);
				xt+=h;
			}
			for(i=0; i<spisokLines.Count; ++i)
			{
				Pen p = new Pen(spisokLines[i].Cvet,spisokLines[i].Tol);
				int nn = spisokLines[i].KolPoint;
				masPoint = new Point[nn];
				float x, y;
				PointF pf;
				for(int j=0; j < nn; ++j)
				{
					pf = spisokLines[i].spisok[j];
					x=pf.X; y=pf.Y;
					masPoint[j].X = (int) (x0 + (x-minx)/(maxx-minx)*(xk-x0));
					masPoint[j].Y = (int) (y0 + (1-(y-miny)/(maxy-miny))*(yk-y0));
				}
				g.DrawLines(p,masPoint);
			}
			
		}
		void fNumberLines(double a, double b,  out double h,  out double y1,  out int n)
		{
			if(a>b)
			{
				double c=a;
				a=b;
				b=c;
			}
			double l= Math.Abs(b-a);
			double k = 1;
			h = 1;
			double y0 = 0;
			
			while(l<=4)
			{
				k=k/10;
				l*=10;
				
			}
			while(l>40)
			{
				k=k*10;
				l/=10;
				
			}
			if (l >= 4 && l <= 7)
			{
				h=1*k;
			}
			else if (l>7 && l<=16)
			{
				h=2*k;
			}
			else if (l>16 && l<=40)
			{
				h=5*k;
			}
			if(a>0)
			{
				while(y0<a)
				{
					y0=y0+h;
				}
			}
			else {
				while(y0>a)
				{
					y0=y0-h;
				}
				y0=y0+h;
			}
			y1=y0;
			n =1;
			while(y0<=b)
			{
				n++;
				y0=y0+h;
			}
			n--;
		}
	}
}
