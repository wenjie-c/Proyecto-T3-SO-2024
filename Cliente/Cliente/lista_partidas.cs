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
    public partial class lista_partidas : Form
    {
        internal Socket server;
        internal int id_j;
        public Codigo_invitacion invitacion;
        public lista_partidas()
        {
            InitializeComponent();
            lista_partidas_lsbx.Items.Add("No has creado ninguna partida.");
        }

        private void lista_partidas_Load(object sender, EventArgs e)
        {
            string mensaje = $"3/";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            
        }
        public void parsing_server(string[] total)
        {
            
            if (int.Parse(total[0]) != 0)
            {
                lista_partidas_lsbx.Items.Clear();
                for (int i = 0; i < int.Parse(total[0]); i++)
                {
                    lista_partidas_lsbx.Items.Add(total[i+1]);
                }
            }
        }

        private void crear_partida_btn_Click(object sender, EventArgs e)
        {
            string mensaje = "4/0";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        public void add_partida(int id_partida)
        {
            if(id_partida == -1)
            {
                MessageBox.Show("Error al crear una nueva partida!");
                return;
            }
            lista_partidas_lsbx.Items.Add(id_partida);
        }

        private void lista_partidas_lsbx_Enter(object sender, EventArgs e)
        {
            
        }

        private void join_btn_Click(object sender, EventArgs e)
        {
            if ((lista_partidas_lsbx.Items[0].ToString() == "No has creado ninguna partida."))
            {
                MessageBox.Show("Primero crea una partida!");
            }
            else
            {
                var id_partida = lista_partidas_lsbx.SelectedItem.ToString();
                string mensaje = $"6/{id_partida}";
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
        }
        public int GetIdPartida()
        {
            return Convert.ToInt32(this.lista_partidas_lsbx.SelectedItem);
        }

        private void invitacion_btn_Click(object sender, EventArgs e)
        {
            invitacion = new Codigo_invitacion();
            invitacion.ShowDialog();
            if(invitacion.codigo != String.Empty)
            {
                int id_partida = Convert.ToInt32(invitacion.codigo);
                byte[] msg = Encoding.ASCII.GetBytes($"7/{invitacion.codigo}");
                server.Send(msg);
            }
        }
    }
}
