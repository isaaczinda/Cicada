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
    class DrawTextruredBlock
    {
        SpriteBatch spriteBatch;
        VertexPositionNormalTexture[] vertices;
        Vector3[] BlockLocations;
        Matrix projectionMatrix;
        Matrix viewMatrix;
        Matrix worldMatrix;
        Effect effect;
        Vector3 ArbitraryVector = new Vector3(1, 0, 0);
        Texture2D PassedTexture;
        ContentManager Content;
        GraphicsDevice GraphicsD;
        Texture2D CrossHairTexture;
        MouseState mousestate;
        Vector3 vector;
        Vector3 TranslationTotal;

 
        public void Begin(Texture2D Texture, ContentManager content, GraphicsDevice GraphicsDevice, Vector3[] BlockPoints)
        {
            Content = content;
            GraphicsD = GraphicsDevice;
            PassedTexture = Texture;
            CrossHairTexture = Content.Load<Texture2D>("Cross-hair");
            effect = Content.Load<Effect>("effects");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            BlockLocations = BlockPoints;
            DrawVertices();
            CalculateNormals();
        }

        public void UpdateVertices(Vector3[] BlockPoints)
        {
            BlockLocations = BlockPoints;
            DrawVertices();
            CalculateNormals();
            Translate(TranslationTotal);
        }
        public Vector3[] ReturnVertices()
        {
            Vector3[] VerticesToReturn;
            VerticesToReturn = new Vector3[vertices.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                VerticesToReturn[i] = vertices[i].Position;
            }
            return VerticesToReturn;
        }

        public void ManipulateWorld(Matrix PMatrix, Matrix WMatrix, Matrix VMatrix, Vector3 Translation)
        {
            worldMatrix = WMatrix;
            viewMatrix = VMatrix;
            projectionMatrix = PMatrix;
            Translate(Translation);
            TranslationTotal = TranslationTotal + Translation;
            
        }
        void Translate(Vector3 TranslationValue)
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i].Position = vertices[i].Position + TranslationValue;
            }
            
        }
        public void DrawBlocks()
        {
            //GraphicsD.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.White, 1.0f, 0);
            RasterizerState rs = new RasterizerState();
            rs.CullMode = CullMode.None;
            GraphicsD.RasterizerState = rs;

            effect.CurrentTechnique = effect.Techniques["Textured"];
            effect.Parameters["xView"].SetValue(viewMatrix);
            effect.Parameters["xProjection"].SetValue(projectionMatrix);
            effect.Parameters["xWorld"].SetValue(worldMatrix);
            
            Vector3 lightDirection = new Vector3(0f, 0f, .35f);
            effect.Parameters["xAmbient"].SetValue(.3f);
            effect.Parameters["xLightDirection"].SetValue(Vector3.Transform(lightDirection, worldMatrix));
            effect.Parameters["xEnableLighting"].SetValue(true);
            effect.Parameters["xTexture"].SetValue(PassedTexture);
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();

            GraphicsD.DrawUserPrimitives(PrimitiveType.TriangleList, vertices, 0, BlockLocations.Length * 12, VertexPositionNormalTexture.VertexDeclaration);
            }
            mousestate = Mouse.GetState();
            if (mousestate.RightButton == ButtonState.Pressed)
            {
            }

            
        }

        void CalculateNormals()
        {
            for (int p = 0; p < vertices.Length / 36; p++)
            {
                for (int s = 0; s < 6; s++)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        if (s == 0)
                        {
                            vertices[s * 6 + p * 36].Normal = new Vector3(0, 0, -1);
                            vertices[s * 6 + 1 + p * 36].Normal = new Vector3(0, 0, -1);
                            vertices[s * 6 + 2 + p * 36].Normal = new Vector3(0, 0, -1);
                            vertices[s * 6 + 3 + p * 36].Normal = new Vector3(0, 0, -1);
                            vertices[s * 6 + 4 + p * 36].Normal = new Vector3(0, 0, -1);
                            vertices[s * 6 + 5 + p * 36].Normal = new Vector3(0, 0, -1);
                        }
                        if (s == 1)
                        {
                            vertices[s * 6 + p * 36].Normal = new Vector3(0, 0, 1);
                            vertices[s * 6 + 1 + p * 36].Normal = new Vector3(0, 0, 1);
                            vertices[s * 6 + 2 + p * 36].Normal = new Vector3(0, 0, 1);
                            vertices[s * 6 + 3 + p * 36].Normal = new Vector3(0, 0, 1);
                            vertices[s * 6 + 4 + p * 36].Normal = new Vector3(0, 0, 1);
                            vertices[s * 6 + 5 + p * 36].Normal = new Vector3(0, 0, 1);
                        }
                        if (s == 2)
                        {
                            vertices[s * 6 + p * 36].Normal = new Vector3(-1, 0, 0);
                            vertices[s * 6 + 1 + p * 36].Normal = new Vector3(-1, 0, 0);
                            vertices[s * 6 + 2 + p * 36].Normal = new Vector3(-1, 0, 0);
                            vertices[s * 6 + 3 + p * 36].Normal = new Vector3(-1, 0, 0);
                            vertices[s * 6 + 4 + p * 36].Normal = new Vector3(-1, 0, 0);
                            vertices[s * 6 + 5 + p * 36].Normal = new Vector3(-1, 0, 0);
                        }
                        if (s == 3)
                        {
                            vertices[s * 6 + p * 36].Normal = new Vector3(1, 0, 0);
                            vertices[s * 6 + 1 + p * 36].Normal = new Vector3(1, 0, 0);
                            vertices[s * 6 + 2 + p * 36].Normal = new Vector3(1, 0, 0);
                            vertices[s * 6 + 3 + p * 36].Normal = new Vector3(1, 0, 0);
                            vertices[s * 6 + 4 + p * 36].Normal = new Vector3(1, 0, 0);
                            vertices[s * 6 + 5 + p * 36].Normal = new Vector3(1, 0, 0);
                        }
                        if (s == 4)
                        {
                            vertices[s * 6 + p * 36].Normal = new Vector3(0, 1, 0);
                            vertices[s * 6 + 1 + p * 36].Normal = new Vector3(0, 1, 0);
                            vertices[s * 6 + 2 + p * 36].Normal = new Vector3(0, 1, 0);
                            vertices[s * 6 + 3 + p * 36].Normal = new Vector3(0, 1, 0);
                            vertices[s * 6 + 4 + p * 36].Normal = new Vector3(0, 1, 0);
                            vertices[s * 6 + 5 + p * 36].Normal = new Vector3(0, 1, 0);
                        }
                        if (s == 5)
                        {
                            vertices[s * 6 + p * 36].Normal = new Vector3(0, -1, 0);
                            vertices[s * 6 + 1 + p * 36].Normal = new Vector3(0, -1, 0);
                            vertices[s * 6 + 2 + p * 36].Normal = new Vector3(0, -1, 0);
                            vertices[s * 6 + 3 + p * 36].Normal = new Vector3(0, -1, 0);
                            vertices[s * 6 + 4 + p * 36].Normal = new Vector3(0, -1, 0);
                            vertices[s * 6 + 5 + p * 36].Normal = new Vector3(0, -1, 0);
                        }
                    }
                }
            }
            for (int i = 0; i < vertices.Length; i++)
                vertices[i].Normal.Normalize();
        }
        void DrawVertices()
        {
            vertices = new VertexPositionNormalTexture[BlockLocations.Length * 36];
            for (int i = 0; i < BlockLocations.Length; i++)
            {
                vertices[0 + 36 * i].Position = new Vector3(0, 0, 0);
                vertices[0 + 36 * i].TextureCoordinate.X = 0;
                vertices[0 + 36 * i].TextureCoordinate.Y = 1;
                vertices[1 + 36 * i].Position = new Vector3(0, 10, 0);
                vertices[1 + 36 * i].TextureCoordinate.X = 0;
                vertices[1 + 36 * i].TextureCoordinate.Y = 0;
                vertices[2 + 36 * i].Position = new Vector3(10, 10, 0);
                vertices[2 + 36 * i].TextureCoordinate.X = 1;
                vertices[2 + 36 * i].TextureCoordinate.Y = 0;
                vertices[3 + 36 * i].Position = new Vector3(0, 0, 0);
                vertices[3 + 36 * i].TextureCoordinate.X = 0;
                vertices[3 + 36 * i].TextureCoordinate.Y = 1;
                vertices[4 + 36 * i].Position = new Vector3(10, 10, 0);
                vertices[4 + 36 * i].TextureCoordinate.X = 1;
                vertices[4 + 36 * i].TextureCoordinate.Y = 0;
                vertices[5 + 36 * i].Position = new Vector3(10, 0, 0);
                vertices[5 + 36 * i].TextureCoordinate.X = 1;
                vertices[5 + 36 * i].TextureCoordinate.Y = 1;

                vertices[6 + 36 * i].Position = new Vector3(0, 0, 10);
                vertices[6 + 36 * i].TextureCoordinate.X = 0;
                vertices[6 + 36 * i].TextureCoordinate.Y = 1;
                vertices[7 + 36 * i].Position = new Vector3(0, 10, 10);
                vertices[7 + 36 * i].TextureCoordinate.X = 0;
                vertices[7 + 36 * i].TextureCoordinate.Y = 0;
                vertices[8 + 36 * i].Position = new Vector3(10, 10, 10);
                vertices[8 + 36 * i].TextureCoordinate.X = 1;
                vertices[8 + 36 * i].TextureCoordinate.Y = 0;
                vertices[9 + 36 * i].Position = new Vector3(0, 0, 10);
                vertices[9 + 36 * i].TextureCoordinate.X = 0;
                vertices[9 + 36 * i].TextureCoordinate.Y = 1;
                vertices[10 + 36 * i].Position = new Vector3(10, 10, 10);
                vertices[10 + 36 * i].TextureCoordinate.X = 1;
                vertices[10 + 36 * i].TextureCoordinate.Y = 0;
                vertices[11 + 36 * i].Position = new Vector3(10, 0, 10);
                vertices[11 + 36 * i].TextureCoordinate.X = 1;
                vertices[11 + 36 * i].TextureCoordinate.Y = 1;

                vertices[12 + 36 * i].Position = new Vector3(0, 10, 0);
                vertices[12 + 36 * i].TextureCoordinate.X = 1;
                vertices[12 + 36 * i].TextureCoordinate.Y = 0;
                vertices[13 + 36 * i].Position = new Vector3(0, 0, 0);
                vertices[13 + 36 * i].TextureCoordinate.X = 1;
                vertices[13 + 36 * i].TextureCoordinate.Y = 1;
                vertices[14 + 36 * i].Position = new Vector3(0, 0, 10);
                vertices[14 + 36 * i].TextureCoordinate.X = 0;
                vertices[14 + 36 * i].TextureCoordinate.Y = 1;
                vertices[15 + 36 * i].Position = new Vector3(0, 0, 10);
                vertices[15 + 36 * i].TextureCoordinate.X = 0;
                vertices[15 + 36 * i].TextureCoordinate.Y = 1;
                vertices[16 + 36 * i].Position = new Vector3(0, 10, 0);
                vertices[16 + 36 * i].TextureCoordinate.X = 1;
                vertices[16 + 36 * i].TextureCoordinate.Y = 0;
                vertices[17 + 36 * i].Position = new Vector3(0, 10, 10);
                vertices[17 + 36 * i].TextureCoordinate.X = 0;
                vertices[17 + 36 * i].TextureCoordinate.Y = 0;

                vertices[18 + 36 * i].Position = new Vector3(10, 10, 0);
                vertices[18 + 36 * i].TextureCoordinate.X = 1;
                vertices[18 + 36 * i].TextureCoordinate.Y = 0;
                vertices[19 + 36 * i].Position = new Vector3(10, 0, 0);
                vertices[19 + 36 * i].TextureCoordinate.X = 1;
                vertices[19 + 36 * i].TextureCoordinate.Y = 1;
                vertices[20 + 36 * i].Position = new Vector3(10, 0, 10);
                vertices[20 + 36 * i].TextureCoordinate.X = 0;
                vertices[20 + 36 * i].TextureCoordinate.Y = 1;
                vertices[21 + 36 * i].Position = new Vector3(10, 0, 10);
                vertices[21 + 36 * i].TextureCoordinate.X = 0;
                vertices[21 + 36 * i].TextureCoordinate.Y = 1;
                vertices[22 + 36 * i].Position = new Vector3(10, 10, 0);
                vertices[22 + 36 * i].TextureCoordinate.X = 1;
                vertices[22 + 36 * i].TextureCoordinate.Y = 0;
                vertices[23 + 36 * i].Position = new Vector3(10, 10, 10);
                vertices[23 + 36 * i].TextureCoordinate.X = 0;
                vertices[23 + 36 * i].TextureCoordinate.Y = 0;

                vertices[24 + 36 * i].Position = new Vector3(0, 10, 0);
                vertices[24 + 36 * i].TextureCoordinate.X = 0;
                vertices[24 + 36 * i].TextureCoordinate.Y = 1;
                vertices[25 + 36 * i].Position = new Vector3(0, 10, 10);
                vertices[25 + 36 * i].TextureCoordinate.X = 0;
                vertices[25 + 36 * i].TextureCoordinate.Y = 0;
                vertices[26 + 36 * i].Position = new Vector3(10, 10, 10);
                vertices[26 + 36 * i].TextureCoordinate.X = 1;
                vertices[26 + 36 * i].TextureCoordinate.Y = 0;
                vertices[27 + 36 * i].Position = new Vector3(0, 10, 0);
                vertices[27 + 36 * i].TextureCoordinate.X = 0;
                vertices[27 + 36 * i].TextureCoordinate.Y = 1;
                vertices[28 + 36 * i].Position = new Vector3(10, 10, 10);
                vertices[28 + 36 * i].TextureCoordinate.X = 1;
                vertices[28 + 36 * i].TextureCoordinate.Y = 0;
                vertices[29 + 36 * i].Position = new Vector3(10, 10, 0);
                vertices[29 + 36 * i].TextureCoordinate.X = 1;
                vertices[29 + 36 * i].TextureCoordinate.Y = 1;

                vertices[30 + 36 * i].Position = new Vector3(0, 0, 0);
                vertices[30 + 36 * i].TextureCoordinate.X = 0;
                vertices[30 + 36 * i].TextureCoordinate.Y = 1;
                vertices[31 + 36 * i].Position = new Vector3(0, 0, 10);
                vertices[31 + 36 * i].TextureCoordinate.X = 0;
                vertices[31 + 36 * i].TextureCoordinate.Y = 0;
                vertices[32 + 36 * i].Position = new Vector3(10, 0, 10);
                vertices[32 + 36 * i].TextureCoordinate.X = 1;
                vertices[32 + 36 * i].TextureCoordinate.Y = 0;
                vertices[33 + 36 * i].Position = new Vector3(0, 0, 0);
                vertices[33 + 36 * i].TextureCoordinate.X = 0;
                vertices[33 + 36 * i].TextureCoordinate.Y = 1;
                vertices[34 + 36 * i].Position = new Vector3(10, 0, 10);
                vertices[34 + 36 * i].TextureCoordinate.X = 1;
                vertices[34 + 36 * i].TextureCoordinate.Y = 0;
                vertices[35 + 36 * i].Position = new Vector3(10, 0, 0);
                vertices[35 + 36 * i].TextureCoordinate.X = 1;
                vertices[35 + 36 * i].TextureCoordinate.Y = 1;

                for (int p = 0; p < 36; p++)
                {
                    vertices[p + i * 36].Position = vertices[p + i * 36].Position + BlockLocations[i];
                }
            }


        }
    }
}
