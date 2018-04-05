/*
 * Created by SharpDevelop.
 * User: A4
 * Date: 30.05.2017
 * Time: 11:18
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace application
{
	partial class FormTable
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
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.x = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Yt = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Ye = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Yr = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(351, 357);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(115, 54);
			this.button1.TabIndex = 0;
			this.button1.Text = "OK";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// dataGridView1
			// 
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
									this.x,
									this.Yt,
									this.Ye,
									this.Yr});
			this.dataGridView1.Location = new System.Drawing.Point(12, 12);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowTemplate.Height = 24;
			this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.dataGridView1.Size = new System.Drawing.Size(802, 325);
			this.dataGridView1.TabIndex = 1;
			// 
			// x
			// 
			this.x.HeaderText = "X";
			this.x.Name = "x";
			// 
			// Yt
			// 
			this.Yt.HeaderText = "Y (точное)";
			this.Yt.Name = "Yt";
			this.Yt.Width = 220;
			// 
			// Ye
			// 
			this.Ye.HeaderText = "Y (Эйлера)";
			this.Ye.Name = "Ye";
			this.Ye.Width = 220;
			// 
			// Yr
			// 
			this.Yr.HeaderText = "Y (Рунге-Кутты)";
			this.Yr.Name = "Yr";
			this.Yr.Width = 220;
			// 
			// FormTable
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(827, 437);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.button1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormTable";
			this.Text = "Сравнительная таблица для ОДУ";
			this.Load += new System.EventHandler(this.FormTableLoad);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.DataGridViewTextBoxColumn Yr;
		private System.Windows.Forms.DataGridViewTextBoxColumn Ye;
		private System.Windows.Forms.DataGridViewTextBoxColumn Yt;
		private System.Windows.Forms.DataGridViewTextBoxColumn x;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.Button button1;
	}
}
