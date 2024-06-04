using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace Cliente
{
    public partial class Form1 : Form
    {

        delegate void EntreHilos(string argumento); //Delegado para todas las funciones.
        delegate int devolverID(); // Delegado para funciones que devuelvan enteros
        delegate DialogResult dialogosentrehilos();
        bool invitadoflag = false;
        Thread atender;
        Juego partida;
        bool closedjuego = false;

        delegate bool getflag();
        
        private void AtenderServidor()
        {
            while (true)
            {
                byte[] incoming_msg = new byte[128];
                server.Receive(incoming_msg);
                string msg = Encoding.ASCII.GetString(incoming_msg).Split('\0')[0];

                string[] trozos = msg.Split('/');
                int n;
                if (int.TryParse(trozos[0],out n)){
                    switch (Convert.ToInt32(trozos[0]))
                    {
                        case 2:
                            this.id_jugador = Convert.ToInt32(trozos[1]);
                            MessageBox.Show("Tu id de jugador es: " + trozos[1]);
                            if (trozos[1] != "-1")
                            {
                                //play_btn.Enabled = true;
                                play_btn.Invoke(new Action(enable_play_btn));
                            }
                            break;
                        case 3:
                            string[] mensaje = new string[trozos.Length - 1];
                            for (int i = 1; i < trozos.Length; i++)
                            {
                                mensaje[i - 1] = trozos[i];
                            }
                            //lista.parsing_server(string.Join("/",mensaje));

                            lista.Invoke(new EntreHilos(lista.parsing_server), string.Join("/", mensaje));
                            break;
                        case 4:
                            //lista.add_partida(Convert.ToInt32(trozos[1]));
                            lista.Invoke(new Action<int>(lista.add_partida), new object[] { Convert.ToInt32(trozos[1]) });
                            break;
                        case 5:
                            partida.Invoke(new Action<string>(partida.ReceiveMessage), new object[] { trozos[1] });
                            break;
                        case 6:
                            if (Convert.ToInt32(trozos[1]) == -1)
                            {
                                MessageBox.Show("No se ha podido unirse a la partida deseada!");
                            }
                            else
                            {
                                MessageBox.Show($"Se ha podido unirse en la partida seleccionado.\nPartida.id = {lista.GetIdPartida()}");
                                //lista.Close();
                                lista.Invoke(new Action(lista.Close));
                            }
                            break;
                        case 7:
                            if (Convert.ToInt32(trozos[1]) == 0)
                            {
                                // int id_partida = (int)lista.Invoke(new devolverID(lista.GetIdPartida)); // Esto da error
                                //MessageBox.Show($"Te has unido en la partida: {id_partida}, invitacion con exito.");

                                lista.Invoke(new Action(lista.aceptarInvitacion)); // Te han aceptado la invitacion
                                this.Invoke(new Action(setInvitado));
                                MessageBox.Show($"Invitacion con exito.");
                                lista.Invoke(new Action(lista.Close));
                                //this.Invoke(new Action<int>(Jugar), id_partida);


                            }
                            else
                            {
                                MessageBox.Show($"No has podido unirte en la prtida.");
                                //this.Jugar(lista.GetIdPartida());
                            }

                            break;
                        case 8:
                            DialogResult result = MessageBox.Show($"Hay otro jugador con id:{trozos[1]} que quiere unirse a tu partida.", "Invitando a otro jugador.", MessageBoxButtons.YesNo);
                            int decision = (result == DialogResult.Yes) ? 0 : -1;
                            byte[] msg2 = System.Text.Encoding.ASCII.GetBytes($"8/{decision.ToString()}");
                            server.Send(msg2);

                            if (decision == 0)
                            {
                                this.Invoke(new Action(setInvitado)); // Hemos aceptado la invitacion
                            }
                            break;
                        case 9:
                            if(partida != null)
                            if (!partida.IsDisposed)
                                partida.Invoke(new Action<string>(this.UpdateJugadorB), new object[] { trozos[1] });
                            break;
                    }

                }
            }
        }
        Socket server;
        private int id_jugador;
        lista_partidas lista;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //CheckForIllegalCrossThreadCalls = false;
        }

        private void login_btn_Click(object sender, EventArgs e)
        {
            if(nombre_tb.Text == String.Empty || password_tb.Text == String.Empty)
            {
                MessageBox.Show("El campo de nombre y de la contraseña no pueden estar vacias");
                return;
            }

            

            try
            {
                
                string mensaje = $"2/{nombre_tb.Text}/{password_tb.Text}";
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                

            }catch(SocketException err)
            {
                MessageBox.Show("Error: " + err.Message);
                //server.Shutdown(SocketShutdown.Both);
                //server.Close();
            }
   
        }

        private void conectar_desconectar_btn_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (this.conectar_desconectar_btn.Text == "Conectar")
                {
                    IPAddress direc = IPAddress.Parse("10.4.119.5");
                    IPEndPoint ipep = new IPEndPoint(direc, 50073);
                    this.server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    this.server.Connect(ipep);

                    login_btn.Enabled = true;
                    sign_up_btn.Enabled = true;
                    this.conectar_desconectar_btn.Text = "Desconectar";

                    ThreadStart ts = delegate { AtenderServidor(); };
                    atender = new Thread(ts);
                    atender.Start();
                }
                else
                {
                    string mensaje = "0/";
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    this.login_btn.Enabled = false;
                    this.sign_up_btn.Enabled = false;
                    this.play_btn.Enabled = false;
                    this.server.Send(msg);
                    atender.Abort();
                    this.server.Shutdown(SocketShutdown.Both);
                    this.conectar_desconectar_btn.Text = "Conectar";
                    //this.server.Close();
                }
            }
            catch(Exception err)
            {
                MessageBox.Show("Error: " + err.Message);
                //server.Shutdown(SocketShutdown.Both);
                //server.Close();
            }
        }

        private void sign_up_btn_Click(object sender, EventArgs e)
        {
            if (nombre_tb.Text == String.Empty || password_tb.Text == String.Empty)
            {
                MessageBox.Show("El campo de nombre y de la contraseña no pueden estar vacias");
                return;
            }



            try
            {

                string mensaje = $"1/{nombre_tb.Text}/{password_tb.Text}";
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                MessageBox.Show("Bienvenido al servidor" + mensaje);

            }
            catch (SocketException err)
            {
                MessageBox.Show("Error: " + err.Message);
                //server.Shutdown(SocketShutdown.Both);
                //server.Close();
            }
        }

        public void enable_play_btn()
        {
            this.play_btn.Enabled = true;
        }

        private void change_play_btn(object sender, FormClosedEventArgs e)
        {
            if (lista.GetIdPartida() == -1 && !lista.invitacionflag) return; // No se ha seleccionado ninguna partida.
            if (play_btn.Text == "Listar") play_btn.Text = "Play";
            else play_btn.Text = "Listar";
        }

        private void play_btn_Click(object sender, EventArgs e)
        {
            if (play_btn.Text == "Listar") {
                lista = new lista_partidas();
                lista.server = this.server;
                lista.id_j = int.Parse(this.id_jugador.ToString());
                lista.FormClosed += new FormClosedEventHandler(change_play_btn);
                lista.ShowDialog(); 
            }
            else
            {
                if (lista.invitacionflag)
                {
                    Jugar(Convert.ToInt32(lista.invitacion.codigo));
                    return;
                }
                Jugar(lista.GetIdPartida());
            }
        }

        private void Jugar(int id_partida)
        {
            partida = new Juego(invitadoflag);
            partida.id_partida = id_partida;
            partida.FormClosing += Partida_FormClosing; 
            //partida.ShowDialog();
            partida.Show();
            partida.id_jugador = id_jugador;
            //partida.InitializeGameControl();
            if (lista.invitacionflag || invitadoflag)
            {
                partida.CrearJugadorB();
                partida.SetSocket(this.server);
                partida.enable_chat();
            }
            //partida.ShowDialog();
        }

        private void Partida_FormClosing(object sender, FormClosingEventArgs e)
        {
            closedjuego = true;
        }

        internal void setInvitado() {
            invitadoflag = true;
        }
        internal void UpdateJugadorB(string trozo)
        {
            partida.UpdateControl(trozo);
        }

        private bool getclosedjuego() { return closedjuego; }
        
    }
}


/*
Recordar que hemos instalado estas bibliotecas desde NuGet y abria que atribuirlos al final: 
  
SharpDX.4.0.1
SharpDX.Direct3D9.4.0.1
SharpDX.DXGI.4.0.1
SharpDX.Direct2D1.4.0.1
SharpDX.Direct3D11.4.0.1
SharpDX.Mathematics.4.0.1
SharpDX.MediaFoundation.4.0.1
SharpDX.XAudio2.4.0.1
SharpDX.XInput.4.0.1
MonoGame.Framework.WindowsDX.9000.3.8.9102
MonoGame.Forms.DX.3.2.0
 */