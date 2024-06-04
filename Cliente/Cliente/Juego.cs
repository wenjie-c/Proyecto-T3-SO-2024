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
using System.Timers;
using System.Net.Sockets;



namespace Cliente
{
    public partial class Juego : Form
    {
        internal int id_partida;
        internal int id_jugador;
        internal bool invitadoflag;
        internal Socket server; //Fijese que el socket lo utilizamos si hemos aceptado al otro jugador.
        System.Timers.Timer timer = new System.Timers.Timer(500);
        public Juego(bool invitadof)
        {
            InitializeComponent();
            this.chat_rtb.Enabled = false;
            this.send_tb.Enabled = false;
            this.send_btn.Enabled = false;
            this.FormClosing += new FormClosingEventHandler(CloseGameControl);
            //gameControl1.OnUpdate += new GameControl.UpdateEventHandler(SetStatusBar);
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = true;
            invitadoflag = invitadof;
            if (invitadoflag) timer.Start();
            
        }

        

        private void Juego_Load(object sender, EventArgs e)
        {
            
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
            lock (server) // Acceso exclusivo a este recurso compartido (https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/lock)
            {
                string outcoming = $"{id_jugador.ToString()}:" + send_tb.Text;
                this.chat_rtb.Text += "\n" + outcoming;
                byte[] msg = Encoding.ASCII.GetBytes($"5/{outcoming}");
                server.Send(msg);
                this.send_tb.Text = string.Empty; // Limpiar el textbox
            }
        }
        internal void ReceiveMessage(string msg)
        {
            this.chat_rtb.Text += "\n" + msg;
        }
        // --- Funciones para GameControl ---
        internal void CrearJugadorB()
        {
            gameControl1.CrearJugadorB();
            this.chat_rtb.Enabled = true;
            this.send_tb.Enabled = true;
            this.send_btn.Enabled = true;
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
            float posX, posY;
            //posX = Convert.ToFloat(posicion[0]);
            float.TryParse(posicion[0], out posX);
            float.TryParse(posicion[1], out posY);
            //posY = Convert.ToDouble(posicion[1]);
            gameControl1.UpdateJugadorB(posX, posY);
        }

        private void CloseGameControl(object sender, FormClosingEventArgs e)
        {
            gameControl1.Dispose();
            timer.Stop();
            timer.Dispose();
        }

        internal void SetStatusBar(object sender ,string text)
        {
            estado.Text = text;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e) //Temporizador para enviar coordenadas
        {

            if (gameControl1.players != null && server != null)
            {
                string outcoming = $"9/0:{gameControl1.players[0].position.X.ToString()};{gameControl1.players[0].position.Y.ToString()}";
                SetStatusBar(this, $"Se ha enviado : {outcoming} con periodo {timer.Interval.ToString()}");
                server.Send(System.Text.Encoding.ASCII.GetBytes(outcoming));
            }


        }
        // --- Fin de funciones para GameControl ---

    }
}
