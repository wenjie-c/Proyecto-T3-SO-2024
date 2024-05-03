
namespace Cliente
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.nombre_lb = new System.Windows.Forms.Label();
            this.password_lb = new System.Windows.Forms.Label();
            this.login_btn = new System.Windows.Forms.Button();
            this.nombre_tb = new System.Windows.Forms.TextBox();
            this.password_tb = new System.Windows.Forms.TextBox();
            this.conectar_desconectar_btn = new System.Windows.Forms.Button();
            this.sign_up_btn = new System.Windows.Forms.Button();
            this.play_btn = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.94382F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55.05618F));
            this.tableLayoutPanel1.Controls.Add(this.nombre_lb, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.password_lb, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.login_btn, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.nombre_tb, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.password_tb, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.conectar_desconectar_btn, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.sign_up_btn, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.play_btn, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(239, 136);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // nombre_lb
            // 
            this.nombre_lb.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nombre_lb.AutoSize = true;
            this.nombre_lb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.nombre_lb.Location = new System.Drawing.Point(3, 7);
            this.nombre_lb.Name = "nombre_lb";
            this.nombre_lb.Size = new System.Drawing.Size(69, 20);
            this.nombre_lb.TabIndex = 0;
            this.nombre_lb.Text = "Nombre:";
            // 
            // password_lb
            // 
            this.password_lb.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.password_lb.AutoSize = true;
            this.password_lb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.password_lb.Location = new System.Drawing.Point(3, 41);
            this.password_lb.Name = "password_lb";
            this.password_lb.Size = new System.Drawing.Size(96, 20);
            this.password_lb.TabIndex = 1;
            this.password_lb.Text = "Contraseña:";
            // 
            // login_btn
            // 
            this.login_btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.login_btn.Enabled = false;
            this.login_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.login_btn.Location = new System.Drawing.Point(110, 71);
            this.login_btn.Name = "login_btn";
            this.login_btn.Size = new System.Drawing.Size(126, 28);
            this.login_btn.TabIndex = 2;
            this.login_btn.Text = "Login";
            this.login_btn.UseVisualStyleBackColor = true;
            this.login_btn.Click += new System.EventHandler(this.login_btn_Click);
            // 
            // nombre_tb
            // 
            this.nombre_tb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nombre_tb.Location = new System.Drawing.Point(110, 3);
            this.nombre_tb.Name = "nombre_tb";
            this.nombre_tb.Size = new System.Drawing.Size(126, 20);
            this.nombre_tb.TabIndex = 3;
            // 
            // password_tb
            // 
            this.password_tb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.password_tb.Location = new System.Drawing.Point(110, 37);
            this.password_tb.Name = "password_tb";
            this.password_tb.Size = new System.Drawing.Size(126, 20);
            this.password_tb.TabIndex = 4;
            // 
            // conectar_desconectar_btn
            // 
            this.conectar_desconectar_btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.conectar_desconectar_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.conectar_desconectar_btn.Location = new System.Drawing.Point(3, 71);
            this.conectar_desconectar_btn.Name = "conectar_desconectar_btn";
            this.conectar_desconectar_btn.Size = new System.Drawing.Size(101, 28);
            this.conectar_desconectar_btn.TabIndex = 5;
            this.conectar_desconectar_btn.Text = "Conectar";
            this.conectar_desconectar_btn.UseVisualStyleBackColor = true;
            this.conectar_desconectar_btn.Click += new System.EventHandler(this.conectar_desconectar_btn_Click);
            // 
            // sign_up_btn
            // 
            this.sign_up_btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sign_up_btn.Enabled = false;
            this.sign_up_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.sign_up_btn.Location = new System.Drawing.Point(110, 105);
            this.sign_up_btn.Name = "sign_up_btn";
            this.sign_up_btn.Size = new System.Drawing.Size(126, 28);
            this.sign_up_btn.TabIndex = 6;
            this.sign_up_btn.Text = "Sign up";
            this.sign_up_btn.UseVisualStyleBackColor = true;
            this.sign_up_btn.Click += new System.EventHandler(this.sign_up_btn_Click);
            // 
            // play_btn
            // 
            this.play_btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.play_btn.Enabled = false;
            this.play_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.play_btn.Location = new System.Drawing.Point(3, 105);
            this.play_btn.Name = "play_btn";
            this.play_btn.Size = new System.Drawing.Size(101, 28);
            this.play_btn.TabIndex = 7;
            this.play_btn.Text = "Listar";
            this.play_btn.UseVisualStyleBackColor = true;
            this.play_btn.Click += new System.EventHandler(this.play_btn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(239, 136);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Form1";
            this.Text = "Cliente";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label nombre_lb;
        private System.Windows.Forms.Label password_lb;
        private System.Windows.Forms.Button login_btn;
        private System.Windows.Forms.TextBox nombre_tb;
        private System.Windows.Forms.TextBox password_tb;
        private System.Windows.Forms.Button conectar_desconectar_btn;
        private System.Windows.Forms.Button sign_up_btn;
        private System.Windows.Forms.Button play_btn;
    }
}

