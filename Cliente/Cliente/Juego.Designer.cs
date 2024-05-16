
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
            this.ChatLocalBox = new System.Windows.Forms.GroupBox();
            this.LocalMsg = new System.Windows.Forms.TextBox();
            this.LocalBtn = new System.Windows.Forms.Button();
            this.LocalScreen = new System.Windows.Forms.TextBox();
            this.gameControl1 = new Cliente.GameControl();
            this.barra_estado = new System.Windows.Forms.StatusStrip();
            this.tableLayoutPanel1.SuspendLayout();
            this.ChatLocalBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.ChatLocalBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.gameControl1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 746F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1075, 964);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // ChatLocalBox
            // 
            this.ChatLocalBox.Controls.Add(this.LocalMsg);
            this.ChatLocalBox.Controls.Add(this.LocalBtn);
            this.ChatLocalBox.Controls.Add(this.LocalScreen);
            this.ChatLocalBox.Location = new System.Drawing.Point(3, 748);
            this.ChatLocalBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChatLocalBox.Name = "ChatLocalBox";
            this.ChatLocalBox.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChatLocalBox.Size = new System.Drawing.Size(1063, 216);
            this.ChatLocalBox.TabIndex = 58;
            this.ChatLocalBox.TabStop = false;
            this.ChatLocalBox.Text = "Chat Local";
            // 
            // LocalMsg
            // 
            this.LocalMsg.Location = new System.Drawing.Point(4, 160);
            this.LocalMsg.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LocalMsg.Multiline = true;
            this.LocalMsg.Name = "LocalMsg";
            this.LocalMsg.Size = new System.Drawing.Size(696, 25);
            this.LocalMsg.TabIndex = 46;
            this.LocalMsg.TextChanged += new System.EventHandler(this.LocalMsg_TextChanged);
            // 
            // LocalBtn
            // 
            this.LocalBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LocalBtn.Location = new System.Drawing.Point(706, 150);
            this.LocalBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LocalBtn.Name = "LocalBtn";
            this.LocalBtn.Size = new System.Drawing.Size(50, 42);
            this.LocalBtn.TabIndex = 48;
            this.LocalBtn.Text = "Enviar";
            this.LocalBtn.UseVisualStyleBackColor = true;
            this.LocalBtn.Click += new System.EventHandler(this.LocalBtn_Click);
            // 
            // LocalScreen
            // 
            this.LocalScreen.Location = new System.Drawing.Point(0, 16);
            this.LocalScreen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LocalScreen.Multiline = true;
            this.LocalScreen.Name = "LocalScreen";
            this.LocalScreen.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LocalScreen.Size = new System.Drawing.Size(1063, 123);
            this.LocalScreen.TabIndex = 47;
            // 
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
            // barra_estado
            // 
            this.barra_estado.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.barra_estado.Location = new System.Drawing.Point(0, 942);
            this.barra_estado.Name = "barra_estado";
            this.barra_estado.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.barra_estado.Size = new System.Drawing.Size(1075, 22);
            this.barra_estado.TabIndex = 1;
            this.barra_estado.Text = "Barra de estado";
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
            this.ChatLocalBox.ResumeLayout(false);
            this.ChatLocalBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.StatusStrip barra_estado;
        private GameControl gameControl1;
        private System.Windows.Forms.GroupBox ChatLocalBox;
        private System.Windows.Forms.TextBox LocalMsg;
        private System.Windows.Forms.Button LocalBtn;
        private System.Windows.Forms.TextBox LocalScreen;
    }
}