using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.GamerServices;

namespace Space_Shooter
{
   public class Explosion
    {
       public Texture2D extexture;
       public Vector2 exposition;
       public float timer;
       public float interval;
       public Vector2 orgin;
       public int currentframe, spritewidth, spriteheight;
       public Rectangle sourceRect;
       public bool isvisible;
       public Explosion(Texture2D newtexture, Vector2 newposition)
       {
           exposition = newposition;
           extexture=newtexture;
           timer=0f;
           interval=20f;
           currentframe = 1;
           spritewidth = 120;
           spriteheight = 120;
           isvisible = true;
       }
       public void loadcontent(ContentManager content)
       {
 
       }

       public void update(GameTime gametime)
       {
           //increase timer goolge it
           timer += (float)gametime.ElapsedGameTime.TotalMilliseconds;

           if (timer > interval)
           {
               currentframe++;
               timer = 0f;
           }
           //last frame 
           if (currentframe == 17)
           {
               isvisible = false;
               currentframe = 0; 
           }
           sourceRect = new Rectangle(currentframe * spritewidth, 0, spritewidth, spriteheight);
           orgin = new Vector2(sourceRect.Width /2, sourceRect.Height/2);
       }
       public void Draw(SpriteBatch spritebatch)
       {
           if (isvisible)
           {
               spritebatch.Draw(extexture, exposition, sourceRect, Color.White, 0f, orgin,1.0f, SpriteEffects.None, 0);
           }
       }
    }
}
