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
    }
}
