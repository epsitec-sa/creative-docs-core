﻿namespace App.Directories
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose ();
			}
			base.Dispose (disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.lst_result = new System.Windows.Forms.ListBox();
			this.cmd_search = new System.Windows.Forms.Button();
			this.txt_value = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(427, 314);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(169, 26);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// lst_result
			// 
			this.lst_result.FormattingEnabled = true;
			this.lst_result.Location = new System.Drawing.Point(12, 91);
			this.lst_result.Name = "lst_result";
			this.lst_result.Size = new System.Drawing.Size(583, 212);
			this.lst_result.TabIndex = 1;
			// 
			// cmd_search
			// 
			this.cmd_search.Location = new System.Drawing.Point(453, 26);
			this.cmd_search.Name = "cmd_search";
			this.cmd_search.Size = new System.Drawing.Size(142, 39);
			this.cmd_search.TabIndex = 2;
			this.cmd_search.Text = "Search";
			this.cmd_search.UseVisualStyleBackColor = true;
			this.cmd_search.Click += new System.EventHandler(this.cmd_search_Click);
			// 
			// txt_value
			// 
			this.txt_value.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txt_value.Location = new System.Drawing.Point(12, 26);
			this.txt_value.Name = "txt_value";
			this.txt_value.Size = new System.Drawing.Size(435, 39);
			this.txt_value.TabIndex = 3;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(608, 343);
			this.Controls.Add(this.txt_value);
			this.Controls.Add(this.cmd_search);
			this.Controls.Add(this.lst_result);
			this.Controls.Add(this.pictureBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "Directories";
			this.Load += new System.EventHandler(this.MainForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.ListBox lst_result;
		private System.Windows.Forms.Button cmd_search;
		private System.Windows.Forms.TextBox txt_value;
	}
}

