using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Avatar;
using Microsoft.Xna.Framework.GamerServices;
//using Microsoft.Xna.Framework.Net;
//using Microsoft.Xna.Framework.Storage;
//using Microsoft.Xna.Framework.Video;

namespace Space_Shooter
{
    public class Player
    {
        public Texture2D texture,bullettexture,healthtexture;
        public Vector2 position,healthbarposition;
        public int speed,health;
        public float bulletdelay;
        public List<Bullets> bulletlist;
        SoundManager sm = new SoundManager();
        
        //collision
        public Rectangle boundingBox,healthRectangle;
        public bool iscolliding;

        //Constructor
        public Player()
        {
            texture = null;
            position = new Vector2(300, 800);
            speed = 10;
            iscolliding = false;
            bulletdelay = 2;
            bulletlist = new List<Bullets>();
            health = 200;
            healthbarposition = new Vector2(50, 50);
        }

        //Load  
        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("Plane1");
            bullettexture = Content.Load<Texture2D>("playerbullet");
            healthtexture = Content.Load<Texture2D>("healthbar");
            sm.LoadContent(Content);
        }

        //Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
            foreach (Bullets b in bulletlist)
            {
                b.Draw(spriteBatch);
            }
            spriteBatch.Draw(healthtexture, healthRectangle, Color.White);
        }

        //Update
        public void update(GameTime gametime)
        {
            KeyboardState keyState = Keyboard.GetState();
            //bounding box for player  and health
            healthRectangle = new Rectangle((int)healthbarposition.X, (int)healthbarposition.Y, health, 25);
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            if(keyState.IsKeyDown(Keys.W))
                position.Y-= speed;
            
            if(keyState.IsKeyDown(Keys.S))
                position.Y+= speed;
            
            if(keyState.IsKeyDown(Keys.A))
                position.X-=speed;
            
            if(keyState.IsKeyDown(Keys.D))
                position.X +=speed;

            if(keyState.IsKeyDown(Keys.Space))
            {
                shoot();
            }
            if (keyState.IsKeyDown(Keys.Escape))
            {
                
            }
            updatelists();


            //bounding 
            if(position.X<=0)position.X=0;
            if (position.X >= 700-texture.Width) position.X=700-texture.Width;
            if (position.Y <= 0) position.Y = 0;
            if (position.Y >= 950-texture.Height) position.Y = 950-texture.Height;
        }
        public void shoot()
        {
            if (bulletdelay >= 0)
                bulletdelay--;

            //display and add to list
            if (bulletdelay <= 0)
            {
                
                Bullets nbullet = new Bullets(bullettexture);
                
                nbullet.position = new Vector2(position.X + 32-nbullet.texture.Width/2, position.Y+30);
                
                nbullet.isvisible = true;
                if (bulletlist.Count() < 20)
                {
                    bulletlist.Add(nbullet);
 
                }
            }
            if (bulletdelay == 0)
            {
                bulletdelay = 20;
            }
        }
        public void updatelists()
        {
            
            //remove if hit top
            foreach (Bullets b in bulletlist)
            {
                b.boundingbullet = new Rectangle((int)b.position.X, (int)b.position.Y, b.texture.Width, b.texture.Height);
                b.position.Y = b.position.Y - b.speed;
                if (b.position.Y <= 0)
                {
                    b.isvisible = false;
                }

            }
            //visibility of bullet
            for (int i = 0; i < bulletlist.Count; i++)
            {
                if (!bulletlist[i].isvisible)
                {
                    bulletlist.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
