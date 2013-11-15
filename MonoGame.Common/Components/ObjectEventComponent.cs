using System;
using MonoGame.Game.Common.Entities;
using MonoGame.Game.Common.Enums;
using MonoGame.Game.Common.Events;
using MonoGame.Game.Common.Interfaces;

namespace MonoGame.Game.Common.Components
{
    public class ObjectEventComponent : IComponent
    {
        private readonly ObjectEvent _subscribeEvent;
        private readonly Action<GameObject> _action;
        public GameObject Owner { get; set; }

        public ObjectEventComponent(GameObject owner, ObjectEvent subscribeEvent, Action<GameObject> action)
        {
            _subscribeEvent = subscribeEvent;
            _action = action;
            owner.ObjectEvent += OwnerOnObjectEvent;
        }

        private void OwnerOnObjectEvent(object sender, ObjectEventArgs objectEventArgs)
        {
            if (objectEventArgs.Action == _subscribeEvent)
            {
                _action.Invoke(Owner);
            }
        }

        public void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            //     throw new NotImplementedException();
        }

        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch gameTime)
        {
            //      throw new NotImplementedException();
        }
    }
}
