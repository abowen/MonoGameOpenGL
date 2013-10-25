using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Game.Common.Entities;
using MonoGame.Game.Common.Events;
using MonoGame.Game.Common.Infrastructure;
using MonoGame.Game.Common.Interfaces;

namespace MonoGame.Game.Common.Components
{
    public class BulletComponent : IMonoGameComponent
    {
        private readonly Texture2D[] _texture2D;
        private readonly MovementComponent _movementComponent;


        public BulletComponent(GameObject gameObject, Texture2D[] texture, MovementComponent movementComponent)
        {
            _texture2D = texture;
            _movementComponent = movementComponent;
            Owner = gameObject;

            Owner.ActionEvent += OwnerOnActionEvent;
        }

        private void OwnerOnActionEvent(object sender, ActionEventArgs eventArgs)
        {
            if (eventArgs.Action == "Fire")
            {
                Fire();
            }
        }


        public void Fire()
        {
            // TODO: Allow different bullets
            var bullet = new Bullet(_texture2D.First(), new Vector2(Owner.Centre.X, Owner.Centre.Y), _movementComponent.FaceDirection, Owner.GameLayer);
            // TODO: Refactor
            Owner.GameLayer.GameEntities.Add(bullet);
        }

        public GameObject Owner { get; set; }

        public void Update(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }

        public void Draw(SpriteBatch gameTime)
        {
            throw new System.NotImplementedException();
        }
    }
}
