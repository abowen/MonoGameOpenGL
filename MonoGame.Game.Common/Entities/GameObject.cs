using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Game.Common.Enums;
using MonoGame.Game.Common.Events;
using MonoGame.Game.Common.Infrastructure;
using MonoGame.Game.Common.Interfaces;

namespace MonoGame.Game.Common.Entities
{
    public class GameObject
    {
        private readonly List<IMonoGameComponent> InputComponents = new List<IMonoGameComponent>();
        private readonly List<IMonoGameComponent> PhysicsComponents = new List<IMonoGameComponent>();
        private readonly List<IMonoGameComponent> GraphicsComponents = new List<IMonoGameComponent>();
        public readonly GameLayer GameLayer;

        public void AddInputComponent(IMonoGameComponent component)
        {
            InputComponents.Add(component);
            component.Owner = this;
        }

        public void AddPhysicsComponent(IMonoGameComponent component)
        {
            PhysicsComponents.Add(component);
            component.Owner = this;
        }

        public void AddGraphicsComponent(IMonoGameComponent component)
        {
            GraphicsComponents.Add(component);
            component.Owner = this;
        }

        public event EventHandler<ObjectEventArgs> ObjectEvent;

        public void Event(ObjectEvent action)
        {
            if (ObjectEvent != null)
            {
                ObjectEvent(this, new ObjectEventArgs() { Action = action });
            }
        }

        public GameObject(GameLayer gameLayer, Vector2 startLocation)
        {
            GameLayer = gameLayer;
            Centre = startLocation;
        }

        public void RemoveGameObject()
        {
            GameLayer.GameObjects.Remove(this);
        }

        /// <summary>
        /// Centre co-ordinators for the object
        /// </summary>
        public Vector2 Centre { get; set; }

        public int Width;
        public int Height;

        public bool HasCollision = false;


        // Not sure if should make this event based instead
        //protected virtual void Collision()
        //{
        //    GameLayer.GameObjects.Remove(this);
        //}

        //public Action OnCollision;

        //private bool? _hasCollision;

        //public bool HasCollision
        //{
        //    get
        //    {
        //        if (_hasCollision == null)
        //        {
        //            _hasCollision = PhysicsComponents.Any(c => c.GetType() == typeof (CollisionComponent));
        //        }
        //        return _hasCollision.Value;
        //    }
        //}
        
        public Rectangle BoundingRectangle
        {
            get
            {
                var left = (int) (Centre.X - Width/2);
                var top = (int) (Centre.Y - Height/2);
                return new Rectangle(left, top, Width, Height);
            }
        }


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
