using System;
using System.Drawing;
using System.Windows.Forms;
namespace application
{	
	public partial class FormAbout : Form
	{
		public FormAbout()
		{			
			InitializeComponent();				
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
