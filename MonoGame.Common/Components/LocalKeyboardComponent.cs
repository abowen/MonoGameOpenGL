using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Common.Entities;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components
{
    public class LocalKeyboardComponent : IComponent, IKeyboardOuput
    {
        public LocalKeyboardComponent(GameObject owner)
        {
            Owner = owner;
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch gameTime)
        {
        }

        public GameObject Owner { get; set; }

        public IEnumerable<Keys> PressedKeys
        {
            get
            {
                return Keyboard.GetState().GetPressedKeys();
            }
        }
    }
}
