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

using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Color = Microsoft.Xna.Framework.Color;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace Cliente
{
    class GameControl : MonoGame.Forms.Controls.MonoGameControl
    {
        // El juego en si

        const string test = "Esto es no deberia salir en producción!";
        Player player;

        protected override void Initialize() {

            base.Initialize();
            Editor.BackgroundColor = Color.Aqua;
            player = new Player(new Vector2(300,500));
        }
        
        protected override void Update(GameTime gameTime) {

            player.Update(gameTime);
        
        }
        protected override void Draw() {
            base.Draw();
            Editor.spriteBatch.Begin();
            Editor.spriteBatch.DrawString(Editor.Font, test, new Vector2(120,0), Color.Black);
            //DrawRectangle(new Rectangle(400, 300, 100, 100), Color.Green);
            DrawRectangle(player.hitbox, Color.Red);
            Editor.spriteBatch.End();
        }

        // --- Funciones de soporte para monogame ---

        private Texture2D CreateRectangle(Color color)
        {
            Texture2D res = new Texture2D(this.GraphicsDevice, 1, 1);
            res.SetData(new[] { color});
            return res;

        }

        private void DrawRectangle(Rectangle rectangulo,Color color) // Crear rectangulo rellenado
        {
            var buffer = this.CreateRectangle(color);
            Editor.spriteBatch.Draw(buffer, rectangulo, Color.White);
        }
        // --- Fin de funciones de soporte para monogame ---

    }
}
