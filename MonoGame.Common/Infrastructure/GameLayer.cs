using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Common.Entities;
using MonoGame.Common.Enums;
using MonoGame.Common.Interfaces;
using MonoGame.Common.Networking;

namespace MonoGame.Common.Infrastructure
{
    /// <summary>
    /// GameLayer represents a single environment
    /// e.g. Shop, world map, fighting arena
    /// </summary>
    public class GameLayer : ISimpleDrawable, ISimpleUpdateable, ISimpleNetworking
    {
        public readonly GameLayerDepth GameLayerDepth;
        public readonly List<IManager> Managers = new List<IManager>();
        private readonly List<GameObject> _gameObjects = new List<GameObject>();

        public IEnumerable<GameObject> GameObjects
        {
            get
            {
                return _gameObjects;
            }
        }

        public GameLayer(GameLayerDepth gameLayerDepth)
        {
            GameLayerDepth = gameLayerDepth;
        }

        public void Update(GameTime gameTime)
        {
            _gameObjects.ForEach(s => s.Update(gameTime));
            Managers.ForEach(s => s.Update(gameTime));

            // Collision Manager
            var sourceComponents = GameObjects.Where(co => co.HasCollision).ToList();
            var destinationComponents = sourceComponents.ToList();

            foreach (var source in sourceComponents)
            {
                destinationComponents.Remove(source);
                foreach (var destination in destinationComponents)
                {
                    if (source.BoundingRectangle.Intersects(destination.BoundingRectangle))
                    {
                        source.Event(ObjectEvent.Collision);
                        destination.Event(ObjectEvent.Collision);
                    }
                }
            }
        }

        public void AddGameObject(GameObject gameObject)
        {
            _gameObjects.Add(gameObject);
            gameObject.GameLayer = this;
        }

        public void RemoveGameObject(GameObject gameObject)
        {
            _gameObjects.Remove(gameObject);
            gameObject.GameLayer = null;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _gameObjects.ForEach(s => s.Draw(spriteBatch));
        }

        public void Update(NetworkMessage message)
        {
            _gameObjects.ForEach(s => s.Update(message));
        }
    }

}
