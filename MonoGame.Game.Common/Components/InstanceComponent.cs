using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Game.Common.Entities;
using MonoGame.Game.Common.Enums;
using MonoGame.Game.Common.Events;
using MonoGame.Game.Common.Interfaces;

namespace MonoGame.Game.Common.Components
{
    public class InstanceComponent : IMonoGameComponent
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

        public void Draw(SpriteBatch gameTime)
        {
        }
    }
}
