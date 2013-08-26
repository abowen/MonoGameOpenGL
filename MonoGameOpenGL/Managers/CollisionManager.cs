using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGameOpenGL.Entities;

namespace MonoGameOpenGL.Managers
{
    public class CollisionManager
    {
        public CollisionManager(GameState gameState)
        {
            _gameState = gameState;
        }

        private readonly GameState _gameState;
        public readonly List<CollisionType> CollisionTypes = new List<CollisionType>();
        
        public void Update(GameTime gameTime)
        {
            Parallel.ForEach(CollisionTypes, collisionType =>
            {
                var sourceCollisions = _gameState.GameEntities.Where(s => s.GetType() == collisionType.TypeA).ToList();
                var destinationCollision = _gameState.GameEntities.Where(s => s.GetType() == collisionType.TypeB).ToList();
                
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
