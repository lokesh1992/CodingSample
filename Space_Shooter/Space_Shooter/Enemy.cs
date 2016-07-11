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
    class Enemy
    {
        public Texture2D entexture,enbullettexture;
        public Rectangle enboundingbox;
        public Vector2 enposition;
        public int enspeed, enbulletdelay, health,currentlevel,enbulletspeed;
        public bool isenvisible;
        public List<Bullets> enbulletlist;


        //construct
        public Enemy(Texture2D newtexture, Vector2 newposition, Texture2D newbullettexture)
        {
            enbulletlist=new List<Bullets>();
            entexture=newtexture;
            enbullettexture = newbullettexture;
            health = 3;
            enposition = newposition;
            currentlevel = 1;
            enbulletdelay = 50;
            enspeed = 3;
            isenvisible = true;
            enbulletspeed = 5;

        }
        public void update(GameTime gametime)
        {
            //collision
            enboundingbox = new Rectangle((int)enposition.X, (int)enposition.Y, entexture.Width, entexture.Height);

            enposition.Y += enspeed;
            //
            if (enposition.Y >= 950)
            {
                enposition.Y = -75;

            }
            enshoot();
            updatebullet();
        }
        public void draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(entexture, enposition, Color.White);
            //bullet
            foreach (Bullets b in enbulletlist)
            {
                b.Draw(spritebatch);
            }
        }
        public void updatebullet()
        {
            
            //remove if hit top
            foreach (Bullets b in enbulletlist)
            {
                b.boundingbullet = new Rectangle((int)b.position.X, (int)b.position.Y, b.texture.Width, b.texture.Height);
                b.position.Y = b.position.Y + enbulletspeed;
                if (b.position.Y >= 950)
                {
                    b.isvisible = false;
                }

            }
            //visibility of bullet
            for (int i = 0; i < enbulletlist.Count; i++)
            {
                if (!enbulletlist[i].isvisible)
                {
                    enbulletlist.RemoveAt(i);
                    i--;
                }
            }
        
        }
        public void enshoot()
        {
            if (enbulletdelay >= 0)
            {
                enbulletdelay--;
            }
            if (enbulletdelay <= 0)
            {
                Bullets newbullet = new Bullets(enbullettexture);
                newbullet.position = new Vector2(enposition.X + entexture.Width /2-newbullet.texture.Width/2, enposition.Y+30);
                newbullet.isvisible = true;
                if (enbulletlist.Count < 20)
                {
                    enbulletlist.Add(newbullet);
                }
            }
            if (enbulletdelay == 0)
                enbulletdelay = 50;
        }
    }
}
