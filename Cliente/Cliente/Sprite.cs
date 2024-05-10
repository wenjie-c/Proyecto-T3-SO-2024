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
    internal struct ray_casted
    {
        internal bool has_intersection;
        internal bool is_horizontal_the_nearest;
        internal double distance;
        internal int texX; // Coordenada x en la textura
        internal double offsetX;
        internal double offsetY;
    }
    internal class Sprite
    {
        public Rectangle hitbox;
        public Vector2 position;

        public Sprite(int x, int y, Rectangle hitbox)
        {
            this.hitbox = hitbox;
            this.position = new Vector2(x, y);
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
        public double player_angle = 0.5f * Math.PI; // Funciona mejor si se multiplica por el decimal directamente
        public double fov = 60 * Math.PI / 180;
        public int CELLSIZE = 64;
        internal SpriteBatch surface;
        public bool raycastflag = false;
        public bool space_btn_flag = false;
        public bool click_flag = false;

        private Rectangle[] slices;
        public Texture2D paredes;
        public Rectangle[] SourceTexture;
        public Rectangle[] HeightTexture;

        public string[] raycastinglogs;

        public Player(int x, int y, SpriteBatch spriteBatch) : base(x, y, new Rectangle(x, y, 64, 64))
        {

            this.surface = spriteBatch;
            GetSlices();


        }

        private void GetSlices()
        {
            slices = new Rectangle[CELLSIZE];

            // Crear las divisiones
            for (int x = 0; x < CELLSIZE; x++)
            {
                slices[x] = new Rectangle(x, 0, 1, CELLSIZE); // Columnas
            }
        }

        public void Update(GameTime gameTime, int[,] map)
        {
            base.Update(gameTime);
            double moveSpeed = 50f * gameTime.ElapsedGameTime.TotalSeconds;
            double rotSpeed = 3.0f * gameTime.ElapsedGameTime.TotalSeconds; // Radianes por segundos

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                /*
                position.X += (float)(-Math.Sin(player_angle) * moveSpeed);
                position.Y += (float)(Math.Cos(player_angle) * moveSpeed);
                */

                position.X += (float)Math.Cos(player_angle);
                position.Y += (float)Math.Sin(player_angle);

            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                /*
                position.X -= (float)(-Math.Sin(player_angle) * moveSpeed);
                position.Y -= (float)(Math.Cos(player_angle) * moveSpeed);
                */
                position.X -= (float)Math.Cos(player_angle);
                position.Y -= (float)Math.Sin(player_angle);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (player_angle > (359 * Math.PI / 180)) player_angle -= 2 * Math.PI; // Normalizar angulos

                player_angle -= rotSpeed;
                

            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (player_angle < 0) player_angle += 2 * Math.PI; // Normalizar angulos

                player_angle += rotSpeed;
                

            }



            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !space_btn_flag)
            {
                if (raycastflag)
                {
                    raycastflag = false;

                }
                else
                {
                    raycastflag = true;
                }
                space_btn_flag = true;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Space) && space_btn_flag) space_btn_flag = false;

            if (Mouse.GetState().LeftButton == ButtonState.Pressed && !click_flag && raycastflag)
            {
                var mouse_pos = Mouse.GetState().Position;
                if (mouse_pos.X > 0 && mouse_pos.Y > 0 && mouse_pos.X < 320 && mouse_pos.Y < 320)
                {
                    int mouse_cx = (int)Math.Floor((Double)mouse_pos.X / 32);
                    int mouse_cy = (int)Math.Floor((Double)mouse_pos.Y / 32);

                    if (map[mouse_cy, mouse_cx] > 0) map[mouse_cy, mouse_cx] = 0;
                    else map[mouse_cy, mouse_cx] = 1;
                }

                click_flag = true;
            }
            if (Mouse.GetState().LeftButton == ButtonState.Released && click_flag) click_flag = false;


        }


        double normalizar_angulos(double angulo)
        {
            if (angulo >= 0)
            {
                return angulo - Math.Floor(angulo / (2 * Math.PI)) * (2 * Math.PI);
            }
            else
            {
                return (Math.PI * 2) + (Math.Floor(Math.Abs(angulo) / (Math.PI * 2)) * (Math.PI * 2) + angulo);
            }
        }
        ray_casted cast_ray(double angulo, int[,] map, int[] screensize)
        {
            double direction = normalizar_angulos(angulo);
            bool looks_up = !(0 < direction && direction < Math.PI);
            bool looks_right = !(Math.PI / 2 < direction && direction < 3 * Math.PI / 2);

            var ROV = 10; // Profundidad

            double tan = Math.Tan(direction);

            ray_casted res = new ray_casted();

            int texX = 0;
            double offsetX = 0;
            double offsetY = 0;

            // Comprobar la interseccion horizontal.
            res.has_intersection = false;
            bool has_horizontal_intersection = false;
            double horizontal_distance = 0;

            if (tan != 0.000000000f)
            {
                // Proyeccion del vector  a la interseccion horizontal más cercano
                double yn = -(this.position.Y - (Math.Floor(this.position.Y / CELLSIZE)) * CELLSIZE);
                if (!looks_up) yn = CELLSIZE + yn;
                double xn = yn / tan;

                // Proyeccion al vector step
                double ys = -CELLSIZE;
                if (!looks_up) ys = -ys;
                double xs = ys / tan;

                double current_x = this.position.X + xn;
                double current_y = this.position.Y + yn;

                for (int i = 0; i < ROV + 1; i++)
                {
                    int ix = Convert.ToInt32(Math.Floor(current_x / CELLSIZE));
                    int iy = Convert.ToInt32(Math.Floor(current_y / CELLSIZE)) - 1;
                    if (!looks_up) iy += 1;

                    if (ix < 0 || iy < 0 || ix > map.GetLength(1) - 1 || iy > map.GetLength(0) - 1) //Limite del mapa
                    {
                        break;
                    }
                    if (map[iy, ix] > 0) // Interseccion con pared
                    {
                        has_horizontal_intersection = true;
                        horizontal_distance = Math.Sqrt(Math.Pow(current_x - this.position.X, 2) + Math.Pow(current_y - this.position.Y, 2)); // Teorema de Pitágoras
                        offsetX = current_x;

                        break;
                    }

                    current_x += xs;
                    current_y += ys;
                }

            }

            // Comrpobar la interseccion vertical
            double vertical_distance = 0;
            bool has_vertical_intersection = false;


            if (tan != 1.00000000000f)
            {
                double xn = -(this.position.X - Math.Floor(position.X / CELLSIZE) * CELLSIZE);
                if (looks_right) xn = CELLSIZE + xn;
                double yn = tan * xn;

                // Proyeccion del vector Step
                double xs = -CELLSIZE;
                if (looks_right) xs = -xs;

                double ys = tan * xs;

                double current_x = position.X + xn;
                double current_y = position.Y + yn;

                for (int i = 0; i < ROV + 1; i++)
                {
                    int ix = Convert.ToInt32(Math.Floor(current_x / CELLSIZE)) - 1;
                    int iy = Convert.ToInt32(Math.Floor(current_y / CELLSIZE));

                    if (looks_right) ix += 1;

                    if (ix < 0 || iy < 0 || ix > map.GetLength(1) - 1 || iy > map.GetLength(0) - 1)
                    {
                        break;
                    }

                    if (map[iy, ix] > 0)
                    {
                        has_vertical_intersection = true;
                        vertical_distance = Math.Sqrt(Math.Pow(current_x - position.X, 2) + Math.Pow(current_y - position.Y, 2));
                        offsetY = current_y;
                        break;
                    }
                    current_x += xs;
                    current_y += ys;
                }
            }

            res.distance = 0;
            res.is_horizontal_the_nearest = false;

            if (has_horizontal_intersection && !has_vertical_intersection)
            {
                res.distance = horizontal_distance;
                res.is_horizontal_the_nearest = true;
            }
            else
            {
                if (has_vertical_intersection && !has_horizontal_intersection)
                {
                    res.distance = vertical_distance;
                }
                else
                {
                    res.distance = Math.Min(horizontal_distance, vertical_distance);


                    if (horizontal_distance < vertical_distance)
                    {
                        res.is_horizontal_the_nearest = true;
                        texX = Convert.ToInt32(offsetX % CELLSIZE);

                    }
                    else
                    {
                        texX = Convert.ToInt32(offsetY % CELLSIZE);

                    }
                }
            }

            res.has_intersection = (has_horizontal_intersection || has_vertical_intersection);

            // Eliminando la distorsión
            double beta = Math.Abs(normalizar_angulos(player_angle) - direction);
            if (looks_up) beta = Math.PI * 2 - beta;
            res.distance *= Math.Cos(beta);



            if (texX >= 64) texX -= 1;
            res.texX = texX;

            res.offsetX = offsetX;
            res.offsetY = offsetY;
            return res;
        }

        public void Draw(int[,] map, int[] screensize) // Sin textura
        {
            int altura_media = screensize[1] / 2;
            double d = (CELLSIZE / 2) / Math.Tan(fov / 2);

            Color inicial = Color.White;
            Color final = Color.Gray;

            double current_angle = player_angle - fov / 2;
            double step = fov / (screensize[0] - 1);


            for (int i = 0; i < screensize[0]; i++)
            {

                ray_casted casted = cast_ray(current_angle, map, screensize);
                double h2 = (casted.distance / d) * (CELLSIZE / 2);
                if (h2 != 0)
                {
                    double line_ratio = (CELLSIZE / 2) / h2;
                    double half_line_legth = altura_media * line_ratio;
                    // Establecer el corte horizontal de la textura
                    SourceTexture[i] = slices[casted.texX];
                    // Establecer la altura del corte de pantalla
                    HeightTexture[i].Height = (int)(half_line_legth * 2);
                    // Establecer el punto en la se inicia el dibujo
                    HeightTexture[i].Y = (int)(altura_media - half_line_legth);

                    raycastinglogs[i] = $"Columna numero: {i.ToString()}, angulo actual: {current_angle.ToString()}, Tangente: {Math.Tan(current_angle)}; distancia de la colision: {casted.distance}, altura computado: {half_line_legth * 2}, posicion offset de la textura : {casted.texX}, offsetX : {casted.offsetX}, offsetY : {casted.offsetY}";

                    if (casted.has_intersection) // Dibujamos
                    {
                        Color color = casted.is_horizontal_the_nearest ? inicial : final;

                        //surface.DrawLine(i, (float)(altura_media + half_line_legth), i, (float)(altura_media - half_line_legth), color);
                        surface.Draw(paredes, HeightTexture[i], SourceTexture[i], color);
                        //Console.WriteLine($"Se ha dibujado la altura:");
                    }
                    current_angle += step;
                }

            }
        }

        /*
        public void DrawMap(int[,] map, int[] screensize)
        {
            surface.DrawRectangle(0, 0, 320, 320, Color.Black, thickness: 320);
            int indice = CELLSIZE / 2;
            for (int row = 0; row < map.GetLength(0); row++) // Rows
            {
                for (int column = 0; column < map.GetLength(1); column++) // Columns
                {
                    int square = row * 64 + column; // Calcular el square index
                    surface.DrawRectangle(new Rectangle(column * indice, row * indice, indice - 1, indice - 1), map[row, column] > 0 ? Color.White : Color.SkyBlue, thickness: indice - 2); // Se restan uno al ancho y el alto para que aparezcan las lineas de la cuadricula. El grosor hay que restarle 2.

                }

            }
            surface.DrawCircle(position.X / CELLSIZE * indice, position.Y / CELLSIZE * indice, 8, 100, Color.Red, thickness: 8);
            surface.DrawLine(position.X / CELLSIZE * indice, position.Y / CELLSIZE * indice, (float)((position.X / CELLSIZE * indice + Math.Cos(this.player_angle) * indice)), (float)((position.Y / CELLSIZE * indice + Math.Sin(player_angle) * indice)), Color.Yellow, thickness: 4);
        }
        */
    }
}
