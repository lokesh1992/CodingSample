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
    public class Bullets
    {
        public Rectangle boundingbullet;
        public Texture2D texture;
        public float speed;
        public Vector2 origin, position;
        public bool isvisible;
    
    public Bullets(Texture2D newtexture)
    {
        speed=10;
        texture=newtexture;
        isvisible=false;
    }
    public void Draw(SpriteBatch spritebatch)
    {
        spritebatch.Draw(texture, position, Color.White);
    }
   
}
}