using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliente
{
    public partial class Codigo_invitacion : Form
    {
        public string codigo;
        public Codigo_invitacion()
        {
            InitializeComponent();
            codigo = String.Empty;
        }

        private void cancelar_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void accept_btn_Click(object sender, EventArgs e)
        {
            if(codigo_bx.Text != string.Empty)
            {
                codigo = codigo_bx.Text;
                this.Close();
            }
            else
            {
                MessageBox.Show("No puedes dejar el campo vacio!");
            }
        }
    }
}
