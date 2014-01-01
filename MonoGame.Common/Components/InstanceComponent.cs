using Microsoft.Xna.Framework;
using MonoGame.Common.Entities;
using MonoGame.Common.Enums;
using MonoGame.Common.Events;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components
{
    public class InstanceComponent : SimpleComponent, ISimpleUpdateable
    {
        public InstanceComponent()
        {

        }

        private void OwnerOnObjectEvent(object sender, ObjectEventArgs objectEventArgs)
        {
            if (objectEventArgs.Action == ObjectEvent.Collision)
            {
                _hasCollided = true;
            }
        }

        private bool _hasCollided = false;

        public override void SetOwner(GameObject owner)
        {
            Owner = owner;
            owner.ObjectEvent += OwnerOnObjectEvent;
        }

        public void Update(GameTime gameTime)
        {
            if (_hasCollided)
            {
                Owner.RemoveGameObject();
            }
        }
    }
}
