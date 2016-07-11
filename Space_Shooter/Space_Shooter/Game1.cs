using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Design;


namespace Space_Shooter
{
   //main
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public enum states
        {
            intialvideo,
            Menu,
            play,
            gameover,
            exit
        }
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public Texture2D menutexture;
        public Texture2D gameovertexture;
        public int enemybulletDamage;
        List<Asteriod> asteroidlist = new List<Asteriod>();
        List<Enemy> enemylist = new List<Enemy>();
        List<Explosion> explosionlist=new List<Explosion>();
        Random rand = new Random();
        HUD hud = new HUD();
        Player p = new Player();
        BackGround bg = new BackGround();
        SoundManager sm = new SoundManager();
        Video vid;
        VideoPlayer vidplayer;
        Texture2D vidtexture;
        Rectangle vidrectangle;

       

        //enumstates
        states gamestate = states.intialvideo;

        //constructor
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 700;
            graphics.PreferredBackBufferHeight = 950;
            this.Window.Title = "Space Shooter";
            enemybulletDamage = 10;
            menutexture = null;
            gameovertexture = null;

        }
        //Init
        protected override void Initialize()
        {
            vidplayer = new VideoPlayer();
            // TODO: Add your initialization logic here

            base.Initialize();
        }
        //Load
           protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            vid = Content.Load<Video>("video");
            vidrectangle = new Rectangle(GraphicsDevice.Viewport.X, GraphicsDevice.Viewport.Y,
                         GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            vidplayer.Play(vid);
            hud.LoadContent(Content);
            p.LoadContent(Content);
            bg.LoadContent(Content);
            sm.LoadContent(Content);
            menutexture = Content.Load<Texture2D>("Menu");
            gameovertexture = Content.Load<Texture2D>("gover");
            
            // TODO: use this.Content to load your game content here
        }
        //Unload
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        //Upload
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            switch (gamestate)
            {
                case states.intialvideo:
                    {
                        KeyboardState key = Keyboard.GetState();
                        if (key.IsKeyDown(Keys.Escape))
                        {
                            gamestate = states.Menu;
                        }
                        break;
                    }
                case states.Menu:
                 {
                     vidplayer.Stop();
                     KeyboardState keystate = Keyboard.GetState();
                     if (keystate.IsKeyDown(Keys.Enter))
                     {
                         gamestate = states.play;
                         MediaPlayer.Play(sm.bgsong);
                         MediaPlayer.IsRepeating = true;
                     }
                     p.health = 200;
                     hud.playerScore = 0;
                     break;
                 }
                case states.play:
                    {                      

                        foreach (Enemy en in enemylist)
                        {
                            if (en.enboundingbox.Intersects(p.boundingBox))
                            {
                                p.health -= 40;
                                en.isenvisible = false;
                            }
                            //if (en.enposition.Y >= 950)// everytime come with different angle
                            //{
                                
                            //}

                            //enemy bullets collision
                            for (int i = 0; i < en.enbulletlist.Count(); i++)
                            {
                                if (p.boundingBox.Intersects(en.enbulletlist[i].boundingbullet))
                                {
                                    p.health -= enemybulletDamage;
                                    en.enbulletlist[i].isvisible = false;
                                }
                            }
                            //check player bullet
                            for (int i = 0; i < p.bulletlist.Count(); i++)
                            {
                                if (p.bulletlist[i].boundingbullet.Intersects(en.enboundingbox))
                                {
                                    sm.explodesound.Play();
                                    explosionlist.Add(new Explosion(Content.Load<Texture2D>("explosion3"), new Vector2(en.enposition.X, en.enposition.Y)));
                                    en.isenvisible = false;
                                    p.bulletlist[i].isvisible = false;
                                    hud.playerScore += 10;
                                }
                            }
                            en.update(gameTime);
                        }

                        foreach (Asteriod a in asteroidlist)
                        {
                            if (a.asboundingbox.Intersects(p.boundingBox))
                            {
                                a.isvisbile = false;
                                p.health -= 20;
                            }
                            // bullet collision
                            for (int i = 0; i < p.bulletlist.Count; i++)
                            {
                                if (a.asboundingbox.Intersects(p.bulletlist[i].boundingbullet))
                                {
                                    sm.explodesound.Play();
                                    explosionlist.Add(new Explosion(Content.Load<Texture2D>("explosion3"), new Vector2(a.asposition.X, a.asposition.Y)));
                                    a.isvisbile = false;
                                    p.bulletlist.ElementAt(i).isvisible = false;
                                    hud.playerScore += 5;
                                }
                            }
                            a.Update(gameTime);
                        }
                        foreach (Explosion ex in explosionlist)
                        {
                            ex.update(gameTime);
                        }
                        if (p.health <= 0)
                             gamestate = states.gameover;
                        
                        LoadAsteriod();
                        LoadEnemy();
                        manageExplosion();
                        p.update(gameTime);
                        bg.Update(gameTime);
                        //hud.update(gameTime);
                       
                        break;
                    }
                case states.gameover:
                    {
                        KeyboardState keystate = Keyboard.GetState();
                        if (keystate.IsKeyDown(Keys.Escape))
                        {
                            gamestate = states.Menu;
                            enemylist.Clear();
                            asteroidlist.Clear();
                        }
                        MediaPlayer.Stop();
                        break;
                    }
            }
            // TODO: Add your update logic here
            // ateriod and check collision

            base.Update(gameTime);
            
           
        }
        //Draw
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            switch (gamestate)
            {
                case states.play:
                    {
                        bg.Draw(spriteBatch);
                        p.Draw(spriteBatch);
                        hud.draw(spriteBatch);
                        foreach (Asteriod a in asteroidlist)
                        {
                            a.Draw(spriteBatch);
                        }
                        foreach (Enemy e in enemylist)
                        {
                            e.draw(spriteBatch);
                        }
                        foreach (Explosion ex in explosionlist)
                        {
                            ex.Draw(spriteBatch);
                        }
                        break;
                    }
                case states.Menu:
                    {
                        spriteBatch.Draw(menutexture, new Vector2(0, 0), Color.White); 
                        break;
                    }
                case states.intialvideo:
                    {
                        vidtexture = vidplayer.GetTexture();
                        spriteBatch.Draw(vidtexture, vidrectangle, Color.White);
                                          
                        break; }
                case states.gameover:
                    {
                        spriteBatch.Draw(gameovertexture, new Vector2(0, 0), Color.White);
                        spriteBatch.DrawString(hud.playerscorsefont,"your score is  "+hud.playerScore.ToString(),new Vector2(235,100),Color.Green);
                        break; }
            }
            spriteBatch.End();
            
            base.Draw(gameTime);
        }
        public void LoadAsteriod()
        {
            int randY = rand.Next(-600, -50);
            int randX = rand.Next(0, 700);
            //only 5 asteroid at a moment
            if (asteroidlist.Count() < 5)
            {
                asteroidlist.Add(new Asteriod(Content.Load<Texture2D>("asteroid"),new Vector2(randX,randY)));
            }
            for (int i =0;i<asteroidlist.Count();i++)
            {
                if(!asteroidlist[i].isvisbile)
                {
                    asteroidlist.RemoveAt(i);
                    i--;
                }
            }
        }
        public void LoadEnemy()
        {
            int randY = rand.Next(-600, -50);
            int randX = rand.Next(0, 700);
            //only 5 asteroid at a moment
            if (enemylist.Count() < 3)
            {
                enemylist.Add(new Enemy(Content.Load<Texture2D>("enemyship"), new Vector2(randX, randY), Content.Load<Texture2D>("EnemyBullet")));
            }

            for (int i = 0; i < enemylist.Count(); i++)
            {
                if (!enemylist[i].isenvisible)
                {
                    enemylist.RemoveAt(i);
                    i--;
                }
            }
        }
        public void manageExplosion()
        {
            for (int i = 0; i < explosionlist.Count(); i++)
            {
                if (!explosionlist[i].isvisible)
                {
                    explosionlist.RemoveAt(i);
                    i--;
                }
            }
        }
        }
    }

