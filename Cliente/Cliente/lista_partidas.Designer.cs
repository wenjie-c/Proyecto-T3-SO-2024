
namespace Cliente
{
    partial class lista_partidas
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
            this.lista_partidas_lsbx = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_group_box = new System.Windows.Forms.GroupBox();
            this.join_btn = new System.Windows.Forms.Button();
            this.eliminar_partida_btn = new System.Windows.Forms.Button();
            this.crear_partida_btn = new System.Windows.Forms.Button();
            this.invitacion_btn = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.btn_group_box.SuspendLayout();
            this.SuspendLayout();
            // 
            // lista_partidas_lsbx
            // 
            this.lista_partidas_lsbx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lista_partidas_lsbx.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lista_partidas_lsbx.FormattingEnabled = true;
            this.lista_partidas_lsbx.ItemHeight = 16;
            this.lista_partidas_lsbx.Location = new System.Drawing.Point(3, 3);
            this.lista_partidas_lsbx.Name = "lista_partidas_lsbx";
            this.lista_partidas_lsbx.Size = new System.Drawing.Size(528, 751);
            this.lista_partidas_lsbx.TabIndex = 0;
            this.lista_partidas_lsbx.Enter += new System.EventHandler(this.lista_partidas_lsbx_Enter);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 92.18289F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.817109F));
            this.tableLayoutPanel1.Controls.Add(this.lista_partidas_lsbx, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_group_box, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(580, 757);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btn_group_box
            // 
            this.btn_group_box.Controls.Add(this.invitacion_btn);
            this.btn_group_box.Controls.Add(this.join_btn);
            this.btn_group_box.Controls.Add(this.eliminar_partida_btn);
            this.btn_group_box.Controls.Add(this.crear_partida_btn);
            this.btn_group_box.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_group_box.Location = new System.Drawing.Point(537, 3);
            this.btn_group_box.Name = "btn_group_box";
            this.btn_group_box.Size = new System.Drawing.Size(40, 751);
            this.btn_group_box.TabIndex = 1;
            this.btn_group_box.TabStop = false;
            // 
            // join_btn
            // 
            this.join_btn.BackColor = System.Drawing.Color.Silver;
            this.join_btn.Location = new System.Drawing.Point(1, 93);
            this.join_btn.Name = "join_btn";
            this.join_btn.Size = new System.Drawing.Size(40, 40);
            this.join_btn.TabIndex = 2;
            this.join_btn.Text = "Join";
            this.join_btn.UseVisualStyleBackColor = false;
            this.join_btn.Click += new System.EventHandler(this.join_btn_Click);
            // 
            // eliminar_partida_btn
            // 
            this.eliminar_partida_btn.BackColor = System.Drawing.Color.Silver;
            this.eliminar_partida_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.eliminar_partida_btn.Location = new System.Drawing.Point(0, 46);
            this.eliminar_partida_btn.Name = "eliminar_partida_btn";
            this.eliminar_partida_btn.Size = new System.Drawing.Size(40, 40);
            this.eliminar_partida_btn.TabIndex = 1;
            this.eliminar_partida_btn.Text = "-";
            this.eliminar_partida_btn.UseVisualStyleBackColor = false;
            // 
            // crear_partida_btn
            // 
            this.crear_partida_btn.BackColor = System.Drawing.Color.Silver;
            this.crear_partida_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.crear_partida_btn.ForeColor = System.Drawing.Color.Black;
            this.crear_partida_btn.Location = new System.Drawing.Point(0, 0);
            this.crear_partida_btn.Name = "crear_partida_btn";
            this.crear_partida_btn.Size = new System.Drawing.Size(40, 40);
            this.crear_partida_btn.TabIndex = 0;
            this.crear_partida_btn.Text = "+";
            this.crear_partida_btn.UseVisualStyleBackColor = false;
            this.crear_partida_btn.Click += new System.EventHandler(this.crear_partida_btn_Click);
            // 
            // invitacion_btn
            // 
            this.invitacion_btn.BackColor = System.Drawing.Color.Silver;
            this.invitacion_btn.Location = new System.Drawing.Point(0, 139);
            this.invitacion_btn.Name = "invitacion_btn";
            this.invitacion_btn.Size = new System.Drawing.Size(40, 40);
            this.invitacion_btn.TabIndex = 3;
            this.invitacion_btn.Text = "Join other";
            this.invitacion_btn.UseVisualStyleBackColor = false;
            this.invitacion_btn.Click += new System.EventHandler(this.invitacion_btn_Click);
            // 
            // lista_partidas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(580, 757);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "lista_partidas";
            this.Text = "Lista de partidas";
            this.Load += new System.EventHandler(this.lista_partidas_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.btn_group_box.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lista_partidas_lsbx;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox btn_group_box;
        private System.Windows.Forms.Button crear_partida_btn;
        private System.Windows.Forms.Button join_btn;
        private System.Windows.Forms.Button eliminar_partida_btn;
        private System.Windows.Forms.Button invitacion_btn;
    }
}