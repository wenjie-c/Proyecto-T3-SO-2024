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
    public partial class Partidas : Form
    {
        internal int id_j;
        internal Socket server;
        public Partidas()
        {
            InitializeComponent();
            lista_partidas_lsbx.Items.Add("No hay partidas.");
        }

        private void Partidas_Load(object sender, EventArgs e)
        {
            string mensaje = $"3/{id_j.ToString()}";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];

            string[] total = mensaje.Split('/');
            if(total[0] != "0")
            {
                lista_partidas_lsbx.Items.Clear();
                for (int i = 0; i < int.Parse(total[0]); i++)
                {
                    lista_partidas_lsbx.Items.Add(total[i + 1]);
                }
            }

        }
    }
}
