using System;
using System.Drawing;
using System.Collections.Generic;
namespace application
{
	public class LineGraf
	{
		public List <PointF> spisok = new List<PointF>();	// список точек данного графика
		int tol;	// толщина линии
		Color cvet;	// цвет линии
		int tip_line;	// тип линии (1 - сплошная)
		string title;	// название графика (для легенды)
		public LineGraf()
		{
			tol = 3;
			cvet = Color.Blue;
			tip_line = 1;
			title = "График 1";
			//PointF []x=new PointF[1];
			//x[0].X=2;
			//x[0].Y=3;
			//spisok.AddRange(x);
			
		}
		//свойства полей
		public Color Cvet
		{
			set{
				cvet = value;
			}
			get{
				return cvet;
			}
		}
		public int Tol
		{
			set{
				tol=value;
			}
			get{
				return tol;
			}
		}
		public int KolPoint
		{
			get{
				return spisok.Count;
			}
		}
		//метод добавляет точку в линию
		public void Add(PointF p)
		{
			spisok.Add(p);
		}
		//печатает в консоль список точек
		public void print()
		{
			Console.WriteLine("\nтолщина линии="+tol+"\nцвет линии="+cvet+"\nтип линии="+tip_line+"\nназвание графика="+title);
			if(spisok.Count>0)
			{
				
				Console.WriteLine("список точек:");
				for(int i=0; i<spisok.Count; i++)
					Console.WriteLine(spisok[i].X+" \t"+spisok[i].Y);
			}
			else
				Console.WriteLine("список пуст");
		}

		public void writeToFile(){
			string pathname = @"C:\Users\leisa\Documents\";
			string filename = "points";
			string fileadd = ".txt";
			string fullpath;
			string result;
			
			fullpath = pathname + filename + fileadd;
			
			
			var file = System.IO.File.Create(fullpath);
			file.Close();
			
			using (System.IO.StreamWriter file1 = new System.IO.StreamWriter(fullpath))
			{
				if(spisok.Count>0){
					System.Threading.Thread.Sleep(1000);
					for(int i=0; i<spisok.Count; i++){
						result = spisok[i].X + " " +spisok[i].Y + "\r\n";
						file1.Write(result);
					}
					
				}
				else{
					file1.Write("ничего нет");
				}
				
				
			}
			
			
		}
		public void minmax(out float minx, out float miny,
		                   out float maxx, out float maxy)
		{
			PointF p = (PointF)spisok[0];
			minx = maxx = p.X;
			miny = maxy = p.Y;
			for(int i=1; i < spisok.Count; ++i)
			{
				p = spisok[i];
				if(p.X < minx) minx = p.X;
				if(p.X > maxx) maxx = p.X;
				if(p.Y < miny) miny = p.Y;
				if(p.Y > maxy) maxy = p.Y;
			}
		}
	}
}
