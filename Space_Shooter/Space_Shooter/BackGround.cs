using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Shooter
{
    public class BackGround
    {
        public Texture2D bgtexture;
        public Vector2 bgposition1, bgposition2;
        public int bgspeed;

        public BackGround()
        {
            bgtexture = null;
            bgposition1 = new Vector2(0, 0);
            bgposition2 = new Vector2(0, -950);
            bgspeed = 5;

        }
        public void LoadContent(ContentManager Content)
        {
            bgtexture = Content.Load<Texture2D>("space");
        }
        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(bgtexture, bgposition1, Color.White);
            spritebatch.Draw(bgtexture, bgposition2, Color.White);
        }
        public void Update(GameTime gametime)
        {
            bgposition1.Y += bgspeed;
            bgposition2.Y += bgspeed;

            if (bgposition1.Y >= 950)
            {
                bgposition1.Y = 0;
                bgposition2.Y = -950;
            }
        }
    }
}
