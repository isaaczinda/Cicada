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
using System.Diagnostics;

namespace Minecraft_Sim
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        Vector3[] BarkBlockLocations;
        Vector3 Translation = new Vector3(0, 0, 0);
        DrawTextruredBlock Bark;
        Texture2D BarkTexture;
        Texture2D CrossHairTexture;


        GraphicsDeviceManager graphics;
        Matrix projectionMatrix;
        Matrix viewMatrix;
        MouseState mousestate;
        MotionLogic Motion;
        SpriteBatch spriteBatch;
        SpriteFont Font;
        IntersectionAlgorithm Intersection;
        bool previous = false;
        bool current = false;
        
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";    
        }
        bool RightButtonClicked()
        {
            mousestate = Mouse.GetState();
            if (mousestate.RightButton == ButtonState.Pressed)
                current = true;
            else
                current = false;

            if (previous == false && current == true)
            {
                mousestate = Mouse.GetState();

                if (mousestate.RightButton == ButtonState.Pressed)
                    previous = true;
                else
                    previous = false;
                return true;
            }
            else
            {
                mousestate = Mouse.GetState();

                if (mousestate.RightButton == ButtonState.Pressed)
                    previous = true;
                else
                    previous = false;
                return false;
            }
        }
       
        protected override void Initialize()
        {
            //graphics.ToggleFullScreen();
            base.Initialize();
        }

        void PlaceClickBlocks()
        {
            
            if (RightButtonClicked() == true)
            {
                int BlockNumber = (int)CheckCollision().Y;
                int SideNumber = (int)CheckCollision().X;
                if (SideNumber != 0)
                {
                    Array.Resize<Vector3>(ref BarkBlockLocations, BarkBlockLocations.Length + 1);
                    if (SideNumber == 1)
                    {
                        BarkBlockLocations[BarkBlockLocations.Length - 1] = new Vector3(BarkBlockLocations[BlockNumber].X, BarkBlockLocations[BlockNumber].Y, BarkBlockLocations[BlockNumber].Z - 10);
                    }
                    if (SideNumber == 2)
                    {
                        BarkBlockLocations[BarkBlockLocations.Length - 1] = new Vector3(BarkBlockLocations[BlockNumber].X, BarkBlockLocations[BlockNumber].Y, BarkBlockLocations[BlockNumber].Z + 10);
                    }
                    if (SideNumber == 3)
                    {
                        BarkBlockLocations[BarkBlockLocations.Length - 1] = new Vector3(BarkBlockLocations[BlockNumber].X - 10, BarkBlockLocations[BlockNumber].Y, BarkBlockLocations[BlockNumber].Z);
                    }
                    if (SideNumber == 4)
                    {
                        BarkBlockLocations[BarkBlockLocations.Length - 1] = new Vector3(BarkBlockLocations[BlockNumber].X + 10, BarkBlockLocations[BlockNumber].Y, BarkBlockLocations[BlockNumber].Z);
                    }
                    if (SideNumber == 5)
                    {
                        BarkBlockLocations[BarkBlockLocations.Length - 1] = new Vector3(BarkBlockLocations[BlockNumber].X, BarkBlockLocations[BlockNumber].Y + 10, BarkBlockLocations[BlockNumber].Z);
                    }
                    if (SideNumber == 6)
                    {
                        BarkBlockLocations[BarkBlockLocations.Length - 1] = new Vector3(BarkBlockLocations[BlockNumber].X, BarkBlockLocations[BlockNumber].Y - 10, BarkBlockLocations[BlockNumber].Z);
                    }
                    Bark.UpdateVertices(BarkBlockLocations);
                }
            }
        }
        Vector2 CheckCollision()
        {
            Vector3[] Vertices = Bark.ReturnVertices();


            int Side = 0;
            int Cube = 0;
            double MinDistance = -1000;
           

            for (int i = 0; i < Vertices.Length; i++)
            {
                Vertices[i] = Vector3.Transform(Vertices[i], Motion.WorldMatrixValue());
            }
            if (mousestate.ScrollWheelValue > 0)
            {
            }
            for (int s = 0; s < Vertices.Length; s = s + 36)
            {
                for (int i = 0; i <= 30; i += 6)
                {
                    if (Intersection.CheckForCollisionSquare(Vertices[5 + s + i], Vertices[0 + s + i], Vertices[1 + s + i], Vertices[2 + s + i]) &&
                        Intersection.GetDistanceFromCollision(Vertices[5 + s + i], Vertices[0 + s + i], Vertices[1 + s + i]) > MinDistance && Intersection.GetDistanceFromCollision(Vertices[5 + s + i], Vertices[0 + s + i], Vertices[1 + s + i]) < 0)
                    {
                        
                        MinDistance = Intersection.GetDistanceFromCollision(Vertices[5 + s + i], Vertices[0 + s + i], Vertices[1 + s + i]);
                        Side = i / 6 + 1;
                        Cube = s / 36;
                    }
                  
                }
            }

            return new Vector2(Side, Cube);
        }
        
        protected override void LoadContent()
        {
            
            BarkTexture = Content.Load<Texture2D>("Bark Texture");
            CrossHairTexture = Content.Load<Texture2D>("Cross-hair");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Font = Content.Load<SpriteFont>("myFont");

            // Draws Bark Blocks
            Bark = new DrawTextruredBlock();
            BarkBlockLocations = new Vector3[1];
            BarkBlockLocations[0] = new Vector3(0, 0, 0);
            Bark.Begin(BarkTexture, Content, GraphicsDevice, BarkBlockLocations);


            // MotionLogic
            Motion = new MotionLogic();
            viewMatrix = Matrix.CreateLookAt(new Vector3(0, 0, 0), new Vector3(0, 0, 50), new Vector3(0, 1, 0));
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 1.0f, 300.0f);

            Intersection = new IntersectionAlgorithm();
        }
        
        protected override void Update(GameTime gameTime)
        {
            PlaceClickBlocks();
            Bark.ManipulateWorld(projectionMatrix, Motion.WorldMatrixValue(), viewMatrix, Motion.TranslationValue());
            
            
            mousestate = Mouse.GetState();
            if (mousestate.LeftButton == ButtonState.Pressed)
                this.Exit();
            

            base.Update(gameTime);

            
        }
        //string ReturnLocationOfCollisionString(Vector3 TrianglePointOne, Vector3 TrianglePointTwo, Vector3 TrianglePointThree)
        //{
        //    Matrix totalMatrix = Motion.WorldMatrixValue() * Matrix.CreateTranslation(Motion.TranslationValue());
        //    TrianglePointOne = Vector3.Transform(TrianglePointOne, totalMatrix);
        //    TrianglePointTwo = Vector3.Transform(TrianglePointTwo, totalMatrix);
        //    TrianglePointThree = Vector3.Transform(TrianglePointThree, totalMatrix);

        //    return GetLocationOfCollision(TrianglePointOne, TrianglePointTwo, TrianglePointThree).ToString();
        //}

        void ResetFor3d()
        {
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            GraphicsDevice.DepthStencilState = DepthStencilState.None;
            GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
            GraphicsDevice.SamplerStates[0] = SamplerState.LinearClamp;
            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            //GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
        }
        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.White, 1.0f, 0);
            ResetFor3d();
            Bark.DrawBlocks();


            spriteBatch.Begin();
            spriteBatch.DrawString(Font, CheckCollision().ToString(), new Vector2(0, 0), Color.Black);
            spriteBatch.DrawString(Font, RightButtonClicked().ToString(), new Vector2(0, 50), Color.Black);
            //spriteBatch.Draw(CrossHairTexture, new Vector2(330, 160), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
