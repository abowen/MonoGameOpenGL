using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Common.Entities;
using MonoGame.Common.Enums;
using MonoGame.Common.Events;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components
{
    public class InstanceComponent : ISimpleComponent, ISimpleUpdateable
    {
        public InstanceComponent(GameObject owner)
        {
            Owner = owner;
            owner.ObjectEvent += OwnerOnObjectEvent;
        }

        private void OwnerOnObjectEvent(object sender, ObjectEventArgs objectEventArgs)
        {
            if (objectEventArgs.Action == ObjectEvent.Collision)
            {
                _hasCollided = true;
            }
        }

        private bool _hasCollided = false;

        public GameObject Owner { get; set; }

        public void Update(GameTime gameTime)
        {
            if (_hasCollided)
            {
                Owner.RemoveGameObject();
            }
        }
    }
}
