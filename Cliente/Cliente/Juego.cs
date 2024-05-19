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


namespace Cliente
{
    public partial class Juego : Form
    {
        internal int id_partida;
        internal int id_jugador;
        internal Socket server; //Fijese que el socket lo utilizamos si hemos aceptado al otro jugador.
        
        public Juego()
        {
            InitializeComponent();
            this.chat_rtb.Enabled = false;
            this.send_tb.Enabled = false;
            this.send_btn.Enabled = false;

        }

        private void Juego_Load(object sender, EventArgs e)
        {
            this.chat_rtb.Enabled = true;
            this.send_tb.Enabled = true;
            this.send_btn.Enabled = true;
        }

        internal void enable_chat() //Ejecutar cuando se esta en multijugador
        {
            
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gameControl1_Click(object sender, EventArgs e)
        {

        }
        private void send_btn_Click(object sender, EventArgs e)
        {
            string outcoming = $"{id_jugador.ToString()}:" + send_tb.Text;
            this.chat_rtb.Text += "\n" + outcoming;
            byte[] msg = Encoding.ASCII.GetBytes($"5/{outcoming}");
            server.Send(msg);
            this.send_tb.Text = string.Empty; // Limpiar el textbox

        }
        internal void ReceiveMessage(string msg)
        {
            this.chat_rtb.Text += "\n" + msg;
        }
        // --- Funciones para GameControl ---
        internal void CrearJugadorB()
        {
            gameControl1.CrearJugadorB();
        }

        internal void DisableGameControl()
        {
            gameControl1.Enabled = false;
        }
        internal void SetSocket(Socket server)
        {
            gameControl1.server = server;
            this.server = server;
        }
        internal void UpdateControl(string coordenadas)
        {
            string[] partes = coordenadas.Split(':');
            string[] posicion = partes[1].Split(';');
            double posX, posY;
            posX = Convert.ToDouble(posicion[0]);
            posY = Convert.ToDouble(posicion[1]);
            gameControl1.UpdateJugadorB(posX, posY);
        }

        
        // --- Fin de funciones para GameControl ---

    }
}
