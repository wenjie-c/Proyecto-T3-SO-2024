using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Controls;
using MonoGame;

using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Color = Microsoft.Xna.Framework.Color;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace Cliente
{
    internal class Sprite
    {
        public Rectangle hitbox;
        public Vector2 position;

        public Sprite(Rectangle hitbox, Vector2 position)
        {
            this.hitbox = hitbox;
            this.position = position;
        }

        public void Update_hitbox()
        {
            hitbox.X = Convert.ToInt32(position.X);
            hitbox.Y = Convert.ToInt32(position.Y);
        }
        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }
    }

    internal class Player : Sprite
    {
        public Player(Vector2 position) : base(new Rectangle( (int)position.X, (int)position.Y, 150,25), position)
        {
            


        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            float changeX = 0;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                changeX += 5;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                changeX -= 5;
            }

            position.X += changeX;

            if (position.X + hitbox.Width > 794 || position.X < 0) position.X -= changeX;

            base.Update_hitbox();

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
