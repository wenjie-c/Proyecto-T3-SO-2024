﻿
namespace Cliente
{
    partial class Juego
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
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
<<<<<<< Updated upstream
            this.gameControl1 = new Cliente.GameControl();
=======
>>>>>>> Stashed changes
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.chat_rtb = new System.Windows.Forms.RichTextBox();
            this.send_tb = new System.Windows.Forms.TextBox();
            this.send_btn = new System.Windows.Forms.Button();
            this.barra_estado = new System.Windows.Forms.StatusStrip();
<<<<<<< Updated upstream
=======
            this.gameControl1 = new Cliente.GameControl();
>>>>>>> Stashed changes
            this.estado = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.barra_estado.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.gameControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 746F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1075, 933);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
<<<<<<< Updated upstream
            // gameControl1
            // 
            this.gameControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gameControl1.Location = new System.Drawing.Point(4, 4);
            this.gameControl1.Margin = new System.Windows.Forms.Padding(4);
            this.gameControl1.MouseHoverUpdatesOnly = false;
            this.gameControl1.Name = "gameControl1";
            this.gameControl1.Size = new System.Drawing.Size(1067, 738);
            this.gameControl1.TabIndex = 0;
            this.gameControl1.Text = "gameControl1";
            this.gameControl1.Click += new System.EventHandler(this.gameControl1_Click);
            // 
=======
>>>>>>> Stashed changes
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 91.25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.75F));
            this.tableLayoutPanel2.Controls.Add(this.chat_rtb, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.send_tb, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.send_btn, 1, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 750);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 82.87671F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17.12329F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1067, 180);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // chat_rtb
            // 
            this.chat_rtb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chat_rtb.Location = new System.Drawing.Point(4, 4);
            this.chat_rtb.Margin = new System.Windows.Forms.Padding(4);
            this.chat_rtb.Name = "chat_rtb";
            this.chat_rtb.ReadOnly = true;
            this.chat_rtb.Size = new System.Drawing.Size(965, 141);
            this.chat_rtb.TabIndex = 0;
            this.chat_rtb.Text = "";
            // 
            // send_tb
            // 
            this.send_tb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.send_tb.Location = new System.Drawing.Point(4, 153);
            this.send_tb.Margin = new System.Windows.Forms.Padding(4);
            this.send_tb.Name = "send_tb";
            this.send_tb.Size = new System.Drawing.Size(965, 22);
            this.send_tb.TabIndex = 1;
            // 
            // send_btn
            // 
            this.send_btn.Location = new System.Drawing.Point(977, 153);
            this.send_btn.Margin = new System.Windows.Forms.Padding(4);
            this.send_btn.Name = "send_btn";
            this.send_btn.Size = new System.Drawing.Size(85, 23);
            this.send_btn.TabIndex = 2;
            this.send_btn.Text = "Enviar";
            this.send_btn.UseVisualStyleBackColor = true;
            this.send_btn.Click += new System.EventHandler(this.send_btn_Click);
            // 
            // barra_estado
            // 
<<<<<<< Updated upstream
            this.barra_estado.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.barra_estado.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.estado});
            this.barra_estado.Location = new System.Drawing.Point(0, 942);
            this.barra_estado.Name = "barra_estado";
            this.barra_estado.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.barra_estado.Size = new System.Drawing.Size(1075, 22);
            this.barra_estado.TabIndex = 1;
            this.barra_estado.Text = "Barra de estado";
            // 
            // estado
            // 
            this.estado.Name = "estado";
            this.estado.Size = new System.Drawing.Size(0, 16);
=======
            this.barra_estado.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.estado});
            this.barra_estado.Location = new System.Drawing.Point(0, 761);
            this.barra_estado.Name = "barra_estado";
            this.barra_estado.Size = new System.Drawing.Size(806, 22);
            this.barra_estado.TabIndex = 1;
            this.barra_estado.Text = "Barra de estado";
            // 
            // gameControl1
            // 
            this.gameControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gameControl1.Location = new System.Drawing.Point(3, 3);
            this.gameControl1.MouseHoverUpdatesOnly = false;
            this.gameControl1.Name = "gameControl1";
            this.gameControl1.Size = new System.Drawing.Size(800, 600);
            this.gameControl1.TabIndex = 0;
            this.gameControl1.Text = "gameControl1";
            this.gameControl1.Click += new System.EventHandler(this.gameControl1_Click);
            // 
            // estado
            // 
            this.estado.Name = "estado";
            this.estado.Size = new System.Drawing.Size(0, 17);
>>>>>>> Stashed changes
            // 
            // Juego
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1075, 964);
            this.Controls.Add(this.barra_estado);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Juego";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Juego_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.barra_estado.ResumeLayout(false);
            this.barra_estado.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.StatusStrip barra_estado;
        private GameControl gameControl1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.RichTextBox chat_rtb;
        private System.Windows.Forms.TextBox send_tb;
        private System.Windows.Forms.Button send_btn;
        private System.Windows.Forms.ToolStripStatusLabel estado;
    }
}