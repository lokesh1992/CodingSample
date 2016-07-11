using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;


namespace Space_Shooter
{
    public class Asteriod
    {
        public Rectangle asboundingbox;
        public Texture2D astexture;
        public Vector2 asposition, asorigin;
        public float rotationangle;
        public int asspeed;

        public bool isvisbile;
        Random randow = new Random();
        public float randx, randy;

        public Asteriod(Texture2D newtexture,Vector2 newposition)
        {
            asposition = newposition;
            astexture = newtexture;
            asspeed = 4;
            randx = randow.Next(0, 750);
            randy = randow.Next(-600, -50);
            isvisbile = true;
        }
        public void loadContent(ContentManager Content)
        {
            
            asorigin.X = astexture.Width / 2;
            asorigin.Y = astexture.Height / 2;
        }
        public void Update(GameTime gametime)
        {
            asboundingbox = new Rectangle((int)asposition.X, (int)asposition.Y, 45, 45);

            asposition.Y = asposition.Y + asspeed;
            if (asposition.Y >= 950)
                asposition.Y = -50;
            //Rotation
            //float elapsed = (float)gametime.ElapsedGameTime.TotalSeconds;
            //rotationangle += elapsed;
            //float circle = MathHelper.Pi * 2;
            //rotationangle = rotationangle % circle;

        }
        public void Draw(SpriteBatch spritebatch)
        {
            if (isvisbile)
            {
                spritebatch.Draw(astexture, asposition, Color.White);
                //spritebatch.Draw(astexture, asposition,null, Color.White, rotationangle, asorigin, 1.0f, SpriteEffects.None,0f);
                
            }
        }
    }
}
