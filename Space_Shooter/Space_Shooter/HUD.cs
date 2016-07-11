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
     public class HUD
    {
         public int playerScore,screenwidth,screenheight;
         public SpriteFont playerscorsefont;
         public Vector2 playerscorepos;
         public bool scorehud;

         public HUD()
         {
             playerScore = 0;
             scorehud = true;
             screenheight = 950;
             screenwidth = 700;
             playerscorsefont = null;
             playerscorepos = new Vector2(screenwidth / 2, 50);
         }
         public void LoadContent(ContentManager Content)
         {
             playerscorsefont = Content.Load<SpriteFont>("georgia");
         }
         public void update(GameTime gametime)
         {
             //key  to display
             KeyboardState keystate = Keyboard.GetState();
         }
         public void draw(SpriteBatch spritebatch)
         {
             if (scorehud)
                 spritebatch.DrawString(playerscorsefont, "Score= "+playerScore, playerscorepos, Color.White);
         }
    }
}
