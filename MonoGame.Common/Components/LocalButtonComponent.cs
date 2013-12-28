using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Common.Entities;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components
{
    public class LocalButtonComponent : ISimpleComponent, IButtonInput
    {
        public GameObject Owner { get; private set; }

        public void SetOwner(GameObject owner)
        {
            Owner = owner;
        }

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
