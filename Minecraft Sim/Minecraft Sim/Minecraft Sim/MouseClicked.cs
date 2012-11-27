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
    
    class MouseClicked
    {
        bool currentright;
        bool previousright;
        bool currentleft;
        bool previousleft;

        MouseState Left;
        MouseState Right;

        public bool RightButtonClicked()
        {

            Right = Mouse.GetState();
            if (Right.RightButton == ButtonState.Pressed)
                currentright = true;
            else
                currentright = false;

            if (previousright == false && currentright == true)
            {
                Right = Mouse.GetState();

                if (Right.RightButton == ButtonState.Pressed)
                    previousright = true;
                else
                    previousright = false;
                return true;
            }
            else
            {
                Right = Mouse.GetState();

                if (Right.RightButton == ButtonState.Pressed)
                    previousright = true;
                else
                    previousright = false;
                return false;
            }
        }

        public bool LeftButtonClicked()
        {

            Left = Mouse.GetState();
            if (Left.LeftButton == ButtonState.Pressed)
                currentleft = true;
            else
                currentleft = false;

            if (previousleft == false && currentleft == true)
            {
                Left = Mouse.GetState();

                if (Left.LeftButton == ButtonState.Pressed)
                    previousleft = true;
                else
                    previousleft = false;
                return true;
            }
            else
            {
                Left = Mouse.GetState();

                if (Left.LeftButton == ButtonState.Pressed)
                    previousleft = true;
                else
                    previousleft = false;
                return false;
            }
        }
    }
}
