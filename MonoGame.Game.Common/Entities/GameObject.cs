using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Game.Common.Infrastructure;
using MonoGame.Game.Common.Interfaces;

namespace MonoGame.Game.Common.Entities
{
    public class GameObject
    {
        public readonly List<IMonoGameComponent> InputComponents = new List<IMonoGameComponent>();
        public readonly List<IMonoGameComponent> PhysicsComponents = new List<IMonoGameComponent>();
        public readonly List<IMonoGameComponent> GraphicsComponents = new List<IMonoGameComponent>();
        public readonly GameLayer GameLayer;


        public GameObject(GameLayer gameLayer, Vector2 startLocation)
        {
            GameLayer = gameLayer;
            Centre = startLocation;
        }

        /// <summary>
        /// Centre co-ordinators for the object
        /// </summary>
        public Vector2 Centre { get; set; }        


        public virtual void Update(GameTime gameTime)
        {
            InputComponents.ForEach(c => c.Update(gameTime));
            PhysicsComponents.ForEach(c => c.Update(gameTime));
            GraphicsComponents.ForEach(c => c.Update(gameTime));
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {            
            GraphicsComponents.ForEach(c => c.Draw(spriteBatch));            
        }
    }
}
