using System;
using System.Drawing;
using System.Windows.Forms;
namespace application
{	
	public partial class FormTable : Form
	{
		public FormTable()
		{			
			InitializeComponent();		
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			Close();
		}
		
		void FormTableLoad(object sender, EventArgs e)
		{
			dataGridView1.RowCount=MainForm.table.GetLength(0);
			dataGridView1.ColumnCount=4;
			for(int i=0;i< dataGridView1.RowCount;i++)
				for(int j=0;j< dataGridView1.ColumnCount;j++)
			{
				dataGridView1[j,i].Value = MainForm.table[i,j];
			}
		}
	}
}
