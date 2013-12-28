using Microsoft.Xna.Framework;
using MonoGame.Common.Entities;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components
{
    public class GravityComponent : ISimpleComponent, ISimpleUpdateable
    {

        public GameObject Owner { get; private set; }

        public void SetOwner(GameObject owner)
        {
            Owner = owner;
        }

        public GravityComponent()
        {
            
        }

        public void Update(GameTime gameTime)
        {
            Owner.TopLeft += new Vector2(0,0.5f);
        }
    }
}
