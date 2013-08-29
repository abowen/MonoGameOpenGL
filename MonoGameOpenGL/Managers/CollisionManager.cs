using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGameOpenGL.Entities;

namespace MonoGameOpenGL.Managers
{
    public class CollisionManager
    {
        public CollisionManager(GameLayer gameLayer)
        {
            _gameLayer = gameLayer;
        }

        private readonly GameLayer _gameLayer;
        public readonly List<CollisionType> CollisionTypes = new List<CollisionType>();
        
        public void Update(GameTime gameTime)
        {
            Parallel.ForEach(CollisionTypes, collisionType =>
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
            });
        }
    }
}
