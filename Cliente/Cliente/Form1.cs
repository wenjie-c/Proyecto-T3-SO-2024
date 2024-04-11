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
    public partial class Form1 : Form
    {
        Socket server;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

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
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                MessageBox.Show("Tu id de jugador es: " + mensaje);

                if(mensaje != "-1")
                {
                    lista_partidas lista = new lista_partidas();
                    lista.server = this.server;
                    lista.id_j = int.Parse(mensaje);
                    lista.Show();
                }

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
                    IPAddress direc = IPAddress.Parse("192.168.56.102");
                    IPEndPoint ipep = new IPEndPoint(direc, 9060);
                    this.server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    this.server.Connect(ipep);

                    login_btn.Enabled = true;
                    sign_up_btn.Enabled = true;
                    this.conectar_desconectar_btn.Text = "Desconectar";
                }
                else
                {
                    string mensaje = "0/";
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    this.login_btn.Enabled = false;
                    this.sign_up_btn.Enabled = false;
                    this.server.Send(msg);
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
                MessageBox.Show("Bienvenido" + mensaje);

            }
            catch (SocketException err)
            {
                MessageBox.Show("Error: " + err.Message);
                //server.Shutdown(SocketShutdown.Both);
                //server.Close();
            }
        }
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