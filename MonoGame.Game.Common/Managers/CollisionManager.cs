using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoGameOpenGL.Entities;
using MonoGameOpenGL.Infrastructure;
using MonoGameOpenGL.Interfaces;

namespace MonoGameOpenGL.Managers
{
    public class CollisionManager : IManager
    {
        public CollisionManager(GameLayer gameLayer)
        {
            _gameLayer = gameLayer;
        }

        private readonly GameLayer _gameLayer;
        public readonly List<CollisionType> CollisionTypes = new List<CollisionType>();
        
        public void Update(GameTime gameTime)
        {
            foreach (var collisionType in CollisionTypes)
            {
                var sourceCollisions = _gameLayer.GameEntities.Where(s => s.GetType() == collisionType.TypeA).ToList();
                var destinationCollision = _gameLayer.GameEntities.Where(s => s.GetType() == collisionType.TypeB).ToList();
                
                foreach (var source in sourceCollisions)
                {
                    foreach (var destination in destinationCollision)
                    {
                        if (source.BoundingBox.Intersects(destination.BoundingBox))
                        {
                            collisionType.Action(source, destination);
                        }
                    }
                }
            }
        }
    }
}
