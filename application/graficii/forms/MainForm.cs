using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
namespace application
{
	public delegate double del(double x);
	public delegate double del2(del f, double x);
	public delegate double del3(double x, double y);
	public delegate void dele(double x, double []y, double []y1);
	
	public partial class MainForm : Form
	{
		public static float [,] table;
		public static int x0MainForm, xkMainForm, y0MainForm, ykMainForm;
		public static float minxMainForm, minyMainForm, maxxMainForm, maxyMainForm;
		int x0_rect, xk_rect, y0_rect, yk_rect;
		Rectangle r_rect;
		public static string OPZ;
		double a, b, h;
		bool priznak_ris;
		bool priznak_ris_dif_M;
		bool priznak_ris_Extrem;
		bool priznak_ris_dif;
		bool priznak_ris_int;
		bool firstPoint;
		Grafic graf;
		Grafic graf0;
		double []masX;
		double []masY;
		int nomer_metod;
		int M;
		del d;
		del dd;
		del2 d2;
		del3 d3;
		dele d3e;
		public MainForm()
		{			
			InitializeComponent();
			System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
			priznak_ris=false;
			priznak_ris_Extrem=false;
			priznak_ris_dif=false;
			priznak_ris_int = false;
			priznak_ris_dif_M = false;
			graf = new Grafic();
			graf0 = new Grafic();
			nomer_metod=0;
			d = new del(Func.f);
			dd = new del(Func.ff);
			d2 = new del2 (Differential.Proizvodnaya1);
			d3 = new del3 (Runge.f);
			d3e = new dele(Runge.f_M);
			OPZ="";
			firstPoint=false;
			x0_rect=  xk_rect =  y0_rect =  yk_rect = 0;
			M = 2;			
		}		
		
		void PictureBox1Paint(object sender, PaintEventArgs e)
		{
			if(priznak_ris)
			{
				Graphics g = e.Graphics;
				Rectangle r = pictureBox1.ClientRectangle;
				graf.Draw(g, r);
			}
		}
		
		//кнопка построить график в первой вкладке
		void Button1Click(object sender, EventArgs e) 
		{
			priznak_ris=true;
			graf.Clear();
			listBox1.Items.Clear();
			label5.Text=graf.title_graf;
			LineGraf graf1=new LineGraf();
			LineGraf graf2=new LineGraf();
			string z=(textBox1.Text);
			a=double.Parse(textBox2.Text);
			b=double.Parse(textBox3.Text);
			h=double.Parse(textBox4.Text);
			if(a>b){
				double c=a;
				a=b;
				b=c;
			}
			if(h<0){
				h=Math.Abs(h);
			}
			if(h==0){
				h=0.001;
			}
			int n=(int)((b-a)/h);
			h=(b-a)/n;
			float x,y;
			OPZ = Calculation.ConvertToReversePolishNotation(z.Replace("x", Const.INTERVAL_VARIABLE));
			for(int i=0; i<n+1; i++)
			{
				x=(float)(a+h*i);				
				y=(float)d(x);				
				PointF t=new PointF(x,y);				
				graf1.Add(t);
			}
			graf1.Cvet=Color.Red;
			graf1.Tol=5;
			graf1.print();
			graf1.writeToFile();
			int k = graf1.spisok.Count;
			masX = new double[k];
			masY = new double[k];
			for (int i=0; i<k;i++)
			{
				masX[i] = graf1.spisok[i].X;
				masY[i] = graf1.spisok[i].Y;
			}		
			graf.Add(graf1);	
			priznak_ris= true;
			firstPoint=false;
			button2.Enabled = true;
			pictureBox1.Invalidate();
		}
		
		void ExitToolStripMenuItemClick(object sender, EventArgs e)
		{
			Close();
		}
		private List<double?> CalculateExpressionOnInterval(Variables v, PartsOPZ pOPZ,
            double left, double right, double step, string varName)
        {
            List<double?> results = new List<double?>(); //в этом списке будут хранится результаты

            string expression = textBox1.Text; //получение выражения для вычисления

            //удаляем пробелы
            expression = expression.Replace(" ", "");

            if (expression.Length > 0) //если введена не пустая строка 
            {
                //т.к. log(a, b) = ln(a):ln(b)
                //заменяем log в выражении на ln
                //это позволит правильно парсить и вычислять log(выражение, основание) 
                expression = expression.Replace("log", "ln");
                //заменяем запятую в выражении на скобки ):ln(
                //это позволит правильно парсить и вычислять log(выражение, основание) 
                expression = expression.Replace(",", "):ln(");

                string OPZ; //выражение в обратной польской записи

                //добавляем переменную c именем varName и значением INTERVAL_VARIABLE
                v.AddVar(varName, Const.INTERVAL_VARIABLE);

                //в условии подставляем переменные в выражение и заодно проверяются ошибки в переменных
                if (v.SetVar(ref expression) && Errors.CheckError(expression, textBox1)) //проверяем ошибки в выражении
                {
                    //удаляем пробелы
                    expression = expression.Replace(" ", "");
                    //получаем обратную польскую запись
                    OPZ = ReversePolishNotation.ConvertToReversePolishNotation(expression.
                        Replace("(" + Const.INTERVAL_VARIABLE + ")", Const.INTERVAL_VARIABLE));
                    //удаляем переменную с именем varName
                    v.DeleteVar(varName);
                }
              
                for (double i = left; i <= right; i += step) //проходим весь отрезок [left; right] с шагом step
                {
                    double? result;
                    {
                        //разделяем ОПЗ-выражение на части в список структур
                       // ReversePolishNotation.MakeParts(OPZ.Replace(Const.INTERVAL_VARIABLE, i.ToString()), pOPZ);
                        //вычисляем выражение в ОПЗ
                        result = pOPZ.Calculate();
                    }          

                    if (result != null)
                    {
						pOPZ = new PartsOPZ();
                    }
                    results.Add(result); //добавляем результат в список со всеми результатами
                }
            }
            return results;
        }
		
		// вычисление корней уравнения
		void Button2Click(object sender, EventArgs e)
		{
			double x;
			string s;
			if(radioButton1.Checked) nomer_metod=0;
			if(radioButton2.Checked) nomer_metod=1;
			for (int i = 0; i<masX.Length-1;i++)
			{
				if(masY[i]*masY[i+1]<0 | masY[i]==0 )
				{
					switch(nomer_metod)
					{
						case 0 :
							x=Root.Newton_meth(d,d2,masX[i],0.0000001);
							s= String.Format("{0:f5}", x);
							listBox1.Items.Add(s);
							break;
						case 1:
							Console.WriteLine("xleft={0}  xright={1}", masX[i],masX[i+1]);
							x=Root.Polov_Del(d,masX[i],masX[i+1],0.0000001);
							Console.WriteLine("x={0}", x);
							s= String.Format("{0:f5}", x);
							listBox1.Items.Add(s);
							break;
					}
				}
			}
		}
		
		//кнопка "построить график" на второй вкладке
		void Button3Click(object sender, EventArgs e)
		{
			priznak_ris_Extrem=true;
			graf.Clear();			
			LineGraf graf1=new LineGraf();		
			string z=(textBox8.Text);
			a=double.Parse(textBox7.Text);
			b=double.Parse(textBox6.Text);
			h=double.Parse(textBox5.Text);
			if(a>b){
				double c=a;
				a=b;
				b=c;
			}
			if(h<0){
				h=Math.Abs(h);
			}
			if(h==0){
				h=0.001;
			}
			int n=(int)((b-a)/h);
			h=(b-a)/n;
			float x,y;
			OPZ = Calculation.ConvertToReversePolishNotation(z.Replace("x", Const.INTERVAL_VARIABLE));
			for(int i=0; i<n+1; i++)
			{
				x=(float)(a+h*i);
				y=(float)d(x);				
				PointF t=new PointF(x,y);				
				graf1.Add(t);
			}
			graf1.Cvet=Color.Red;
			graf1.Tol=5;
			graf1.print();
			int k = graf1.spisok.Count;
			masX = new double[k];
			masY = new double[k];
			for (int i=0; i<k;i++)
			{
				masX[i] = graf1.spisok[i].X;
				masY[i] = graf1.spisok[i].Y;
			}		
			graf.Add(graf1);
			
			priznak_ris= true;
			firstPoint=false;
			button4.Enabled = true;
			pictureBox2.Invalidate();
		}
		
		//кнопка "вычислить" на второй вкладке
		void Button4Click(object sender, EventArgs e)
		{
			double x, y, h;
			string s;
			bool prMax=true;
			int k=0;
			if(radioButton3.Checked) nomer_metod=0;
			if(radioButton4.Checked) nomer_metod=1;
			dataGridView1.ColumnCount=2;
			dataGridView1.RowCount=20;
			for(int i=0;i< dataGridView1.RowCount;i++)
				for(int j=0;j< dataGridView1.ColumnCount;j++)
			{
				dataGridView1[j,i].Value = " ";
			}
			if(masY[0]>masY[1]) prMax = false;
			for (int i = 1; i<masX.Length-1;i++)
			{
				// ищем Максимумы
				if(masY[i]>masY[i-1] && masY[i]>masY[i+1] && prMax) 
				{
					switch(nomer_metod)
					{
						case 0 :
							x=Extremum.FindMax(d,masX[i],masX[i+1]);
							s= String.Format("{0:f5}", x);
							dataGridView1[0,k].Value = s;
							y=d(x);
							s= String.Format("{0:f5}", y);							
							dataGridView1[1,k].Value = s;
							k++;
							break;
						case 1:
							h=(masX[i+1] - masX[i])/3;
							x=Extremum.MethDrobMax(d,masX[i], h);
							s= String.Format("{0:f5}", x);
							dataGridView1[0,k].Value = s;
							y=d(x);
							s= String.Format("{0:f5}", y);							
							dataGridView1[1,k].Value = s;							
							k++;
							break;
					}
					prMax=false;
				}
				else if(masY[i]<masY[i-1] && masY[i]<masY[i+1] && !prMax) 
				{
					//ищем минимумы
					switch(nomer_metod)
					{
						case 0 :
							x=Extremum.FindMin(d,masX[i],masX[i+1]);
							s= String.Format("{0:f5}", x);
							dataGridView1[0,k].Value = s;
							y=d(x);
							s= String.Format("{0:f5}", y);							
							dataGridView1[1,k].Value = s;							
							k++;
							break;
						case 1:
							h=(masX[i+1] - masX[i])/3;
							x=Extremum.MethDrobMin(d,masX[i],h);
							s= String.Format("{0:f5}", x);
							dataGridView1[0,k].Value = s;
							y=d(x);
							s= String.Format("{0:f5}", y);							
							dataGridView1[1,k].Value = s;
							k++;							
							break;
					}
					prMax = true;
				}
			}
		}
		
		void PictureBox2Paint(object sender, PaintEventArgs e)
		{
			if(priznak_ris_Extrem)
			{
				Graphics g = e.Graphics;
				Rectangle r = pictureBox2.ClientRectangle;
				graf.Draw(g, r);
			}
		}
		
		void Button6Click(object sender, EventArgs e)
		{
			double [] xx;
			double [] yy;
			int k;
			priznak_ris_dif=true;
			graf.Clear();
			graf0.Clear();
			listBox1.Items.Clear();
			label5.Text=graf.title_graf;
			LineGraf graf1=new LineGraf();
			LineGraf graf2=new LineGraf();
			LineGraf graf3=new LineGraf();			
			a=double.Parse(textBox11.Text);
			b=double.Parse(textBox10.Text);
			h=double.Parse(textBox9.Text);
			if(a>b){
				double c=a;
				a=b;
				b=c;
			}
			if(h<0){
				h=Math.Abs(h);
			}
			if(h==0){
				h=0.001;
			}
			int n=(int)((b-a)/h);
			h=(b-a)/n;
			float x,y;
			xx = new double[n+1];
			yy = new double[n+1];
			table = new float[n+1,4];
			
			// точное решение
			for(int i=0; i<n+1; i++)
			{
				x=(float)(a+h*i);				
				y=(float)(Math.Exp(1-Math.Cos(x)));
				table[i,1]=y;
				PointF t=new PointF(x,y);				
				graf1.Add(t);
			}
			graf1.Cvet=Color.Red;
			graf1.Tol=5;
			graf1.print();		
			graf.Add(graf1);
			graf0.Add(graf1);

			// метод Эйлера			 
			 k= Euler.EulerMeth(d3, xx, yy, n+1, a, b, 1, h);
			for(int i=0; i<k; i++)
	    	{
    			x = (float)xx[i];
    			table[i,0]=x;
    			y = (float)yy[i];
    			table[i,2]=y;
    			PointF t=new PointF(x,y);				
				graf2.Add(t);
    		}			
			
			graf2.Cvet=Color.Blue;
			graf2.print();
			graf.Add(graf2);
			graf0.Add(graf2);
			
			// метод Рунге-Кутты			
			 k= RungeKutta.rk4(d3, xx, yy, n+1, a, b, 1, h);			
			for(int i=0; i<k; i++)
	    	{
    			x = (float)xx[i];
    			y = (float)yy[i];
    			table[i,3]=y;
    			PointF t=new PointF(x,y);				
				graf3.Add(t);
    		}			
			
			graf3.Cvet=Color.Green;
			graf3.print();
			graf.Add(graf3);
			graf0.Add(graf3);
			
			priznak_ris= true;
			firstPoint=false;
			button5.Enabled = true;
			button9.Enabled = true;
			pictureBox3.Invalidate();
		}
		
		void PictureBox3Paint(object sender, PaintEventArgs e)
		{
			if(priznak_ris_dif)
			{
				Graphics g = e.Graphics;
				Rectangle r = pictureBox3.ClientRectangle;
				graf.Draw(g, r);
				DrawRect(g);
			}
		}
		
		public void DrawRect(Graphics g)
		{
			int x, y, w, h;
			if(x0_rect<=xk_rect)
			{
				x=x0_rect;
				w=xk_rect-x0_rect;
			}
			else
			{
				x=xk_rect;
				w=x0_rect-xk_rect;
			}
			if(y0_rect<=yk_rect)
			{
				y=y0_rect;
				h=yk_rect-y0_rect;
			}
			else
			{
				y=yk_rect;
				h=y0_rect-yk_rect;
			}
			r_rect = new Rectangle(x, y, w, h);
			g.DrawRectangle(new Pen(Color.Green,3),x, y, w, h);
		}
		
		void PictureBox3MouseClick(object sender, MouseEventArgs e)
		{
			if(firstPoint==false)
			{
				x0_rect = e.X;
				y0_rect = e.Y;
				xk_rect = x0_rect;
				yk_rect = y0_rect;
				firstPoint = true;		
			}
			else{
				firstPoint = false;
				xk_rect = e.X;
				yk_rect = e.Y;
				button5.Visible=true;
				pictureBox3.Invalidate();
			}
		}
		
		void PictureBox3MouseMove(object sender, MouseEventArgs e)
		{
			if(firstPoint)
			{
				xk_rect = e.X;
				yk_rect = e.Y;
				pictureBox3.Invalidate();
			} 
		}
		
		// Увеличение графика
		void Button5Click(object sender, EventArgs e)
		{
			int k=0;
			Grafic gr = new Grafic();
			for(int j=0; j<graf.spisokLines.Count; j++)
			{
				LineGraf lin = new LineGraf();
				lin.Cvet=graf.spisokLines[j].Cvet;
				lin.Tol=graf.spisokLines[j].Tol;				
				for(int i = 0; i<graf.spisokLines[j].spisok.Count; i++)
				{
					PointF t=new PointF(graf.spisokLines[j].spisok[i].X, graf.spisokLines[j].spisok[i].Y);
					Console.WriteLine("|||| x={0}  y={1}", t.X, t.Y);
					Point p = new Point();
					p.X = (int) (MainForm.x0MainForm + (t.X-MainForm.minxMainForm)/(MainForm.maxxMainForm-MainForm.minxMainForm)*(MainForm.xkMainForm-MainForm.x0MainForm));
					p.Y = (int) (MainForm.y0MainForm + (1-(t.Y-MainForm.minyMainForm)/(MainForm.maxyMainForm-MainForm.minyMainForm))*(MainForm.ykMainForm-MainForm.y0MainForm));
					if(r_rect.Contains(p))
						lin.Add(t);
				}
				if(lin.spisok.Count>=2) 
				{
					gr.Add(lin);
					k++;
				}				
			}
			if(k>=1)
			{
				graf.Clear();
				for(int j=0; j<gr.spisokLines.Count; j++)
				{
					LineGraf lin = new LineGraf();
					for(int i = 0; i<gr.spisokLines[j].spisok.Count; i++)
					{
						PointF t=new PointF(gr.spisokLines[j].spisok[i].X, gr.spisokLines[j].spisok[i].Y);
						lin.Add(t);
					}					 
					lin.Cvet=gr.spisokLines[j].Cvet;
					lin.Tol=gr.spisokLines[j].Tol;					
					graf.Add(lin);				
				}
				gr.Clear();				
				pictureBox3.Invalidate();
			}
			x0_rect=  xk_rect =  y0_rect =  yk_rect = 0;
		}
		
		//вернуть исходный график
		void Button7Click(object sender, EventArgs e)
		{
			graf.Clear();
			for(int j=0; j<graf0.spisokLines.Count; j++)
				{
					LineGraf lin = new LineGraf();
					for(int i = 0; i<graf0.spisokLines[j].spisok.Count; i++)
					{
						PointF t=new PointF(graf0.spisokLines[j].spisok[i].X, graf0.spisokLines[j].spisok[i].Y);
						lin.Add(t);
					}
					lin.Cvet=graf0.spisokLines[j].Cvet;
					lin.Tol=graf0.spisokLines[j].Tol;					
					graf.Add(lin);				
				}
			pictureBox3.Invalidate();
		}		
	
		//Вычисление интеграла
		void Button8Click(object sender, EventArgs e)
		{			
			int n;
			priznak_ris_int=true;
			graf.Clear();			
			label5.Text=graf.title_graf;
			LineGraf graf1=new LineGraf();
			LineGraf graf2=new LineGraf();
			a=0;
			b=2;
			n=int.Parse(textBox13.Text);
			if(a>b){
				double c=a;
				a=b;
				b=c;
			}						
			float x,y;
			
			// метод трапеции
			for(int i=4; i<=n; i*=2)
			{
				x=i;				
				y=(float)Math.Log10(Integral.Opr_Integral(dd,a,b,i)-32);				
				PointF t=new PointF(x,y);				
				graf1.Add(t);
			}
			graf1.Cvet=Color.Red;
			graf1.Tol=5;
			graf1.print();		
			graf.Add(graf1);
			
			// метод Симпсона
			for(int i=4; i<=n; i*=2)
			{
				x=i;
				y=(float)Math.Log10(Integral.Simpson_Integral(dd,a,b,i)-32);				
				PointF t=new PointF(x,y);				
				graf2.Add(t);
			}
			graf2.Cvet=Color.Blue;
			graf2.print();
			graf.Add(graf2);			
			priznak_ris_int= true;			
			pictureBox4.Invalidate();
		}
		
		void PictureBox4Paint(object sender, PaintEventArgs e)
		{
			if(priznak_ris_int){
				Graphics g = e.Graphics;
				Rectangle r = pictureBox4.ClientRectangle;
				graf.Draw(g, r);
			}
		}
		
		void Button9Click(object sender, EventArgs e)
		{
			FormTable f= new FormTable();
			f.ShowDialog();
		}
		
		void ToolStripTextBox1Click(object sender, EventArgs e)
		{
			FormAbout f= new FormAbout();
			f.ShowDialog();
		}		
		
		//вычисление для построения графиков системы ОДУ
		void Button10Click(object sender, EventArgs e)
		{
			double [] xx;
			double [,] yy;			
			int k;
			priznak_ris_dif_M=true;
			graf.Clear();
			graf0.Clear();
			listBox1.Items.Clear();
			label5.Text=graf.title_graf;
			LineGraf graf1=new LineGraf();
			LineGraf graf2=new LineGraf();
			LineGraf graf3=new LineGraf();
			LineGraf graf4=new LineGraf();
			LineGraf graf5=new LineGraf();
			LineGraf graf6=new LineGraf();
			a=double.Parse(textBox19.Text);
			b=double.Parse(textBox20.Text);
			h=double.Parse(textBox21.Text);
			if(a>b){
				double c=a;
				a=b;
				b=c;
			}
			if(h<0){
				h=Math.Abs(h);
			}
			if(h==0){
				h=0.001;
			}
			int n=(int)((b-a)/h);
			h=(b-a)/n;
			float x,y;
			xx = new double[n+1];
			yy = new double[n+1,M];
			table = new float[n+1,7];
			
			// точное решение
			for(int i=0; i<n+1; i++)
			{
				x=(float)(a+h*i);				
				y=(float)(-Math.Exp(x)+1);
				table[i,1]=y;
				PointF t=new PointF(x,y);				
				graf1.Add(t);
				
				y=(float)(Math.Exp(x)-Math.Exp(2*x)+1);
				table[i,2]=y;
				PointF tt=new PointF(x,y);				
				graf2.Add(tt);
			}
			graf1.Cvet=Color.HotPink;
			graf1.Tol=5;
			graf2.Cvet=Color.Red;
			graf2.Tol=5;
			graf1.print();		
			graf.Add(graf1);
			graf0.Add(graf1);
			graf.Add(graf2);
			graf0.Add(graf2);

			// метод Эйлера			 
			double []y0 = {0,1};
			k= Euler.Ejler_M(d3e, xx, yy, n+1, M, a, b, y0,  h);
			for(int i=0; i<k; i++)
	    	{
    			x = (float)xx[i];
    			table[i,0]=x;
    			y = (float)yy[i,0];
    			table[i,3]=y;
    			PointF t=new PointF(x,y);				
				graf3.Add(t);				
				
    			y = (float)yy[i,1];
    			table[i,4]=y;
    			PointF tt=new PointF(x,y);				
				graf4.Add(tt);
    		}			
			
			graf3.Cvet=Color.BlueViolet;
			graf3.print();
			graf.Add(graf3);
			graf0.Add(graf3);
			graf4.Cvet=Color.Blue;
			graf4.print();
			graf.Add(graf4);
			graf0.Add(graf4);
			
			// метод Рунге-Кутты		 
			k = RungeKutta.rk4_M(d3e, xx, yy, n+1, M, a, b, y0, h);
			for(int i=0; i<k; i++)
	    	{
    			x = (float)xx[i];
    			y = (float)yy[i,0];
    			table[i,5]=y;
    			PointF t=new PointF(x,y);				
				graf5.Add(t);
				
				y = (float)yy[i,1];
    			table[i,6]=y;
    			PointF tt=new PointF(x,y);				
				graf6.Add(tt);
    		}			
			
			graf5.Cvet=Color.Lime;
			graf5.print();
			graf.Add(graf5);
			graf0.Add(graf5);
			
			graf6.Cvet=Color.Green;
			graf6.print();
			graf.Add(graf6);
			graf0.Add(graf6);
			priznak_ris= true;
			firstPoint=false;
			button11.Enabled = true;
			button12.Enabled = true;
			button13.Enabled = true;
			pictureBox5.Invalidate();
		}
		
		void PictureBox5Paint(object sender, PaintEventArgs e)
		{
			if(priznak_ris_dif_M)
			{
				Graphics g = e.Graphics;
				Rectangle r = pictureBox5.ClientRectangle;
				graf.Draw(g, r);
				DrawRect(g);
			}
		}
		
		void PictureBox5MouseClick(object sender, MouseEventArgs e)
		{
			if(firstPoint==false)
			{
				x0_rect = e.X;
				y0_rect = e.Y;
				xk_rect = x0_rect;
				yk_rect = y0_rect;
				firstPoint = true;		
			}
			else{
				firstPoint = false;
				xk_rect = e.X;
				yk_rect = e.Y;
				button5.Visible=true;
				pictureBox5.Invalidate();
			}
		}
		
		void PictureBox5MouseMove(object sender, MouseEventArgs e)
		{
			if(firstPoint)
			{
				xk_rect = e.X;
				yk_rect = e.Y;
				pictureBox5.Invalidate();
			} 
		}
		
		// увеличить графики функций для системы ОДУ
		void Button11Click(object sender, EventArgs e)
		{
			int k=0;
			Grafic gr = new Grafic();
			for(int j=0; j<graf.spisokLines.Count; j++)
			{
				LineGraf lin = new LineGraf();
				lin.Cvet=graf.spisokLines[j].Cvet;
				lin.Tol=graf.spisokLines[j].Tol;
				
				for(int i = 0; i<graf.spisokLines[j].spisok.Count; i++)
				{
					PointF t=new PointF(graf.spisokLines[j].spisok[i].X, graf.spisokLines[j].spisok[i].Y);					
					Point p = new Point();
					p.X = (int) (MainForm.x0MainForm + (t.X-MainForm.minxMainForm)/(MainForm.maxxMainForm-MainForm.minxMainForm)*(MainForm.xkMainForm-MainForm.x0MainForm));
					p.Y = (int) (MainForm.y0MainForm + (1-(t.Y-MainForm.minyMainForm)/(MainForm.maxyMainForm-MainForm.minyMainForm))*(MainForm.ykMainForm-MainForm.y0MainForm));
					if(r_rect.Contains(p))
						lin.Add(t);
				}
				if(lin.spisok.Count>=2) 
				{
					gr.Add(lin);
					k++;
				}				
			}
			if(k>=1)
			{
				graf.Clear();
				for(int j=0; j<gr.spisokLines.Count; j++)
				{
					LineGraf lin = new LineGraf();
					for(int i = 0; i<gr.spisokLines[j].spisok.Count; i++)
					{
						PointF t=new PointF(gr.spisokLines[j].spisok[i].X, gr.spisokLines[j].spisok[i].Y);
						lin.Add(t);
					}					 
					lin.Cvet=gr.spisokLines[j].Cvet;
					lin.Tol=gr.spisokLines[j].Tol;					
					graf.Add(lin);
				}
				gr.Clear();				
				pictureBox5.Invalidate();
			}
			x0_rect=  xk_rect =  y0_rect =  yk_rect = 0;
		}
		
		// востановить графики функций для системы ОДУ
		void Button12Click(object sender, EventArgs e)
		{
			graf.Clear();
			for(int j=0; j<graf0.spisokLines.Count; j++)
				{
					LineGraf lin = new LineGraf();
					for(int i = 0; i<graf0.spisokLines[j].spisok.Count; i++)
					{
						PointF t=new PointF(graf0.spisokLines[j].spisok[i].X, graf0.spisokLines[j].spisok[i].Y);
						lin.Add(t);
					}
					lin.Cvet=graf0.spisokLines[j].Cvet;
					lin.Tol=graf0.spisokLines[j].Tol;					
					graf.Add(lin);				
				}
			pictureBox5.Invalidate();
		}
		
		// вывод таблицы значений для системы ОДУ
		void Button13Click(object sender, EventArgs e)
		{
			FormSystemODU f = new FormSystemODU();
			f.ShowDialog();
		}
	}
}