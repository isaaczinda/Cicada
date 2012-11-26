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
    class MotionLogic
    {
        MouseState mousestate;
        KeyboardState keystate;
        double YRotation = 0;
        int LastX = 0;
        int CurrentX = 0;
        int LastY = 0;
        int CurrentY = 0;
        Vector3 TranslateBy;
        float ChangeX;
        Matrix NewMatrix;
        public float YRotationReturn()
        {
            return (float)YRotation;
        }

        public float ChangeXReturn()
        {
            return (float)ChangeX;
        }
        public Matrix WorldMatrixValue()
        {
            Motion();
            Rotation();
            NewMatrix = Matrix.CreateRotationY(MathHelper.ToRadians((float)YRotation)) * Matrix.CreateFromAxisAngle(new Vector3(1, 0, 0), ChangeX);
            return NewMatrix;
        }
        public Vector3 TranslationValue()
        {
            Motion();
            Rotation();
            return TranslateBy;
        }
        void Motion()
        {
            keystate = Keyboard.GetState();
            mousestate = Mouse.GetState();

            if (YRotation > 360)
                YRotation = YRotation - 360;
            if (YRotation <= 0)
                YRotation = YRotation + 360;

            TranslateBy = new Vector3(0, 0, 0);

            if (keystate.IsKeyDown(Keys.W))
            {
                TranslateBy = TranslateBy + new Vector3((float)Math.Sin(MathHelper.ToRadians((float)YRotation)), 0, -(float)Math.Cos(MathHelper.ToRadians((float)YRotation)));
            }

            if (keystate.IsKeyDown(Keys.S))
            {
                TranslateBy = TranslateBy + new Vector3(-(float)Math.Sin(MathHelper.ToRadians((float)YRotation)), 0, (float)Math.Cos(MathHelper.ToRadians((float)YRotation)));
            }
            if (keystate.IsKeyDown(Keys.Up))
            {
                TranslateBy = TranslateBy + new Vector3(0, -1f, 0);
            }
            if (keystate.IsKeyDown(Keys.Down))
            {
                TranslateBy = TranslateBy + new Vector3(0, 1f, 0);
            }

        }

        void Rotation()
        {
            // X Rotation Logic
            CurrentX = mousestate.X;

            if (mousestate.X > 500)
            {
                Mouse.SetPosition(mousestate.X - 498, mousestate.Y);
                mousestate = Mouse.GetState();
                LastX = mousestate.X;
                CurrentX = mousestate.X;
            }
            if (mousestate.X <= 1)
            {
                Mouse.SetPosition(mousestate.X + 498, mousestate.Y);
                mousestate = Mouse.GetState();
                LastX = mousestate.X;
                CurrentX = mousestate.X;
            }

            YRotation = YRotation + (double)(CurrentX - LastX) / 10;

            LastX = mousestate.X;

            // Y Rotation Logic

            CurrentY = mousestate.Y;

            if (mousestate.Y > 500)
            {
                Mouse.SetPosition(mousestate.X, mousestate.Y - 498);
                mousestate = Mouse.GetState();
                LastY = mousestate.Y;
                CurrentY = mousestate.Y;
            }

            if (mousestate.Y <= 1)
            {
                Mouse.SetPosition(mousestate.X, mousestate.Y + 498);
                mousestate = Mouse.GetState();
                LastY = mousestate.Y;
                CurrentY = mousestate.Y;
            }

            ChangeX = ChangeX + ((float)(LastY - CurrentY) / 400);

            LastY = mousestate.Y;
        }
    }
}
