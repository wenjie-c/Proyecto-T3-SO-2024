using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Forms.Controls;
using MonoGame;
using System.IO;
using System.Net;
using System.Net.Sockets;

using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Color = Microsoft.Xna.Framework.Color;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Microsoft.Xna.Framework.Media;

namespace Cliente
{
    class GameControl : MonoGame.Forms.Controls.MonoGameControl
    {
        // El juego en si
        

        const string test = "Esto es no deberia salir en producción!";
        /*
        public delegate void UpdateEventHandler(object sender,string args);
        public event UpdateEventHandler OnUpdate;
        */


        int[] ScreenSize = { 800, 600 };

        public Socket server;
        //public int counter = 0; // Contador para no desbonrdar el buffer
        Song background;

        public List<Player> players;
        internal int[,] map = {                  // La izquierda es el norte, abajo es el este
            { 1,1,1,1,1,1,1,1,1,1},
        { 1,0,0,0,1,0,0,0,0,1},
        { 1,0,0,0,1,0,0,0,0,1},
        { 1,0,0,1,1,0,0,0,0,1},
        { 1,0,0,0,0,0,0,0,0,1},
        { 1,0,0,0,0,0,0,0,0,1},
        { 1,0,0,0,0,0,0,0,0,1},
        { 1,0,0,0,0,0,0,0,0,1},
        { 1,0,0,0,0,0,0,0,0,1},
        { 1,1,1,1,1,1,1,1,1,1}
        };

        Other JugadorB;

        protected override void Initialize() {

            base.Initialize();
            Editor.BackgroundColor = Color.Aqua;
            players = new List<Player>();
            players.Add(new Player(320, 320, Editor.spriteBatch));
            //player = new Player(new Vector2(300,500));
            players[0].SourceTexture = new Rectangle[ScreenSize[0]];
            players[0].paredes = Editor.Content.Load<Texture2D>("madera3");
            players[0].HeightTexture = SliceView();
            players[0].raycastinglogs = new string[ScreenSize[0]];
            players[0].graphics = this.GraphicsDevice;
            background = Editor.Content.Load<Song>("Littleroot_Town");
            MediaPlayer.Play(background);
            MediaPlayer.IsRepeating = true;
            this.Disposed += GameControl_Disposed;
            
        }

        

        protected override void Update(GameTime gameTime) {

            // Primero comprobamos si hay otro jugador para enviarle lnuestras cordenadas
            /*
            counter += (int)(gameTime.ElapsedGameTime.TotalSeconds);
            if(server != null || counter >= 100000)
            {
                string outcoming = $"9/0:{players[0].position.X.ToString()};{players[0].position.Y.ToString()}";
                if (OnUpdate != null) OnUpdate(this, $"Se ha enviado : {outcoming} con periodo {gameTime.ElapsedGameTime.TotalSeconds.ToString()} con el contador {counter.ToString()}");
                server.Send(System.Text.Encoding.ASCII.GetBytes(outcoming));
                
                counter = 0;
            }
            // ----------------------------------------------------------------------
            */
            

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                using (var file = new StreamWriter("player.log", false))
                {
                    file.Write(string.Join("\n", players[0].raycastinglogs));
                }
                
            }
            players[0].Update(gameTime,map);
        
        }
        protected override void Draw() {
            base.Draw();
            Editor.spriteBatch.Begin();
            //Editor.spriteBatch.DrawString(Editor.Font, test, new Vector2(120,0), Color.Black);
            //DrawRectangle(new Rectangle(400, 300, 100, 100), Color.Green);
            //DrawRectangle(player.hitbox, Color.Red);
            DrawRectangle(new Rectangle(0, ScreenSize[1] / 2, ScreenSize[0], ScreenSize[1]), Color.DarkKhaki);
            players[0].Draw(map,ScreenSize,JugadorB);
            if (players[0].raycastflag) players[0].DrawMap(map, ScreenSize);
            
            Editor.spriteBatch.End();
        }

        // --- Funciones de soporte para monogame ---

        private Texture2D CreateRectangle(Color color)
        {
            Texture2D res = new Texture2D(this.GraphicsDevice, 1, 1);
            res.SetData(new[] { color});
            return res;

        }
        private Texture2D CreateRectangle(Color color, Microsoft.Xna.Framework.Graphics.GraphicsDevice graphicsDevice)
        {
            Texture2D res = new Texture2D(graphicsDevice, 1, 1);
            res.SetData(new[] { color });
            return res;

        }



        internal void DrawRectangle(Rectangle rectangulo,Color color) // Crear rectangulo rellenado
        {
            var buffer = this.CreateRectangle(color);
            Editor.spriteBatch.Draw(buffer, rectangulo, Color.White);
        }
        internal void DrawRectangle(Rectangle rectangulo, Color color, Microsoft.Xna.Framework.Graphics.GraphicsDevice graphicsDevice) // Crear rectangulo rellenado
        {
            var buffer = this.CreateRectangle(color, graphicsDevice);
            Editor.spriteBatch.Draw(buffer, rectangulo, Color.White);
        }

        public delegate void Para_crear_rectangulos(Rectangle rectangle, Color color, Microsoft.Xna.Framework.Graphics.GraphicsDevice graphicsDevice);


        public Rectangle[] SliceView()
        {
            Rectangle[] res = new Rectangle[ScreenSize[0]];
            for (int x = 0; x < ScreenSize[0]; x++)
            {
                res[x] = new Rectangle(x, 0, 1, ScreenSize[1]);
            }
            return res;
        }

        private void GameControl_Disposed(object sender, EventArgs e)
        {
            MediaPlayer.Stop();
        }
        // --- Fin de funciones de soporte para monogame ---

        // --- Funciones para interactual con otros hilos ---
        public void CrearJugadorB()
        {
            JugadorB = new Other(320, 320,Editor.Content.Load<Texture2D>("fran"));
        }
        public void UpdateJugadorB(float X, float Y)
        {
            JugadorB.position = new Vector2(X, Y);
        }
        // --- Fin de las Funciones para interactual con otros hilos ---

        
         
    }
}
