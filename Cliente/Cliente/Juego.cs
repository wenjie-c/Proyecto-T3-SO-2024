using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Web;


namespace Cliente
{
    public partial class Juego : Form
    {
        internal int id_partida;
        Socket server;
        string msgRx;
        public Juego()
        {
            InitializeComponent();
        }
        public void SetMensaje(string msgRecibido)
        {
            this.msgRx = msgRecibido;
        }
        string GetMensaje() 
        {
            string Mensaje = "5/"+Convert.ToString(id_partida)+"/"+LocalMsg.Text;
            return Mensaje ;
        }
        private void Juego_Load(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gameControl1_Click(object sender, EventArgs e)
        {

        }

      
        private void LocalBtn_Click(object sender, EventArgs e)
            {
                string mensaje = "5/" + LocalMsg.Text + "/";
                LocalMsg.Text = "";
                this.Hide();
            }

        private void LocalMsg_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
