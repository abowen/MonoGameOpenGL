using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Game.Common.Components;
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

        public string GameType { get; set; }

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
                ObjectEvent(this, new ObjectEventArgs { Action = action });
            }
        }

        public GameObject(GameLayer gameLayer, Vector2 topLeftLocation)
        {
            GameLayer = gameLayer;
            TopLeft = topLeftLocation;
        }

        public void RemoveGameObject()
        {
            GameLayer.GameObjects.Remove(this);
        }

        /// <summary>
        /// Top left co-ordinates for the object
        /// </summary>
        public Vector2 TopLeft { get; set; }

        /// <summary>
        /// Centre co-ordinates of the object
        /// </summary>
        public Vector2 Centre
        {
            get
            {
                var x = (TopLeft.X + Width/2);
                var y = (TopLeft.Y + Height/2);
                return new Vector2(x, y);
            }
        }

        public int Width { get; private set; }
        public int Height { get; private set; }


        public Rectangle BoundingRectangle
        {
            get
            {
                return new Rectangle((int)TopLeft.X, (int)TopLeft.Y, Width, Height);
            }
        }


        private bool? _hasCollision;

        public bool HasCollision
        {
            get
            {
                if (_hasCollision == null)
                {
                    var component = PhysicsComponents.FirstOrDefault(c => c.GetType() == typeof(BoundaryComponent)) as BoundaryComponent;
                    if (component != null)
                    {
                        Width = component.Width;
                        Height = component.Height;
                        _hasCollision = true;
                    }
                    else
                    {
                        _hasCollision = false;
                    }
                }
                return _hasCollision.Value;
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
            if (GameConstants.ShowObjectBoundary)
            {
                PhysicsComponents.ForEach(c => c.Draw(spriteBatch));
            }
        }

        public override string ToString()
        {
            if (!string.IsNullOrWhiteSpace(GameType))
            {
                return GameType;
            }
            return base.ToString();
        }
    }
}
