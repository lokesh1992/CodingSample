using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Audio;
namespace Space_Shooter
{
    public class SoundManager
    {
        public SoundEffect explodesound;
        public Song bgsong;

        public SoundManager()
        {
            bgsong = null;
            explodesound = null;
        }
        public void LoadContent(ContentManager Content)
        {
            bgsong = Content.Load<Song>("bgsound");
            explodesound = Content.Load<SoundEffect>("explode");
        }
    }
}
