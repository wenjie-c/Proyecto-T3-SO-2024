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
        
        public Juego()
        {
            InitializeComponent();
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
