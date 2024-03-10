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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

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