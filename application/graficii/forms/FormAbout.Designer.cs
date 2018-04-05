/*
 * Created by SharpDevelop.
 * User: stud
 * Date: 01.06.2017
 * Time: 11:47
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace application
{
	partial class FormAbout
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.button1 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(172, 406);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(155, 46);
			this.button1.TabIndex = 0;
			this.button1.Text = "OK";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(35, 151);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(131, 36);
			this.label1.TabIndex = 1;
			this.label1.Text = "Автор:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(35, 235);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(224, 38);
			this.label2.TabIndex = 2;
			this.label2.Text = "Дата разработки:";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(265, 151);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(205, 74);
			this.label4.TabIndex = 4;
			this.label4.Text = "студент гр.31404 Кокорина Л.Р.";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(265, 235);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(149, 38);
			this.label5.TabIndex = 5;
			this.label5.Text = "май 2017 г.";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(35, 294);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(421, 90);
			this.label6.TabIndex = 6;
			this.label6.Text = "Статус программы: бесплатно для некоммерческого использования";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(35, 31);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(402, 93);
			this.label3.TabIndex = 7;
			this.label3.Text = "Программа предназначена демонстрации и сравнения численных методов.";
			// 
			// FormAbout
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(494, 459);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormAbout";
			this.Text = "О программе";
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button1;
	}
}
