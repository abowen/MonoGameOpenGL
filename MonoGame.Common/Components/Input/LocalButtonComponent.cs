using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components.Input
{
    public class LocalButtonComponent : SimpleComponent, IButtonInput
    {

        public IEnumerable<Buttons> ButtonsPressed
        {
            get
            {
                var buttons = new List<Buttons>();

                var state = GamePad.GetState(PlayerIndex.One);
                if (state.IsConnected)
                {
                    if (state.DPad.Down == ButtonState.Pressed)
                    {
                        buttons.Add(Buttons.DPadDown);
                    }
                    if (state.DPad.Up == ButtonState.Pressed)
                    {
                        buttons.Add(Buttons.DPadUp);
                    }
                    if (state.DPad.Left == ButtonState.Pressed)
                    {
                        buttons.Add(Buttons.DPadLeft);
                    }
                    if (state.DPad.Right == ButtonState.Pressed)
                    {
                        buttons.Add(Buttons.DPadRight);
                    }
                    if (state.Buttons.A == ButtonState.Pressed)
                    {
                        buttons.Add(Buttons.A);
                    }
                }
                else
                {
                    Console.WriteLine("NOT CONNNECTED");
                }
                return buttons;
            }
        }
    }
}
