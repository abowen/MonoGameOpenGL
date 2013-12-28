using Microsoft.Xna.Framework;
using MonoGame.Common.Entities;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components
{
    public class GravityComponent : ISimpleComponent, ISimpleUpdateable
    {
        
        public GameObject Owner { get; set; }

        public GravityComponent()
        {
            
        }

        public void Update(GameTime gameTime)
        {
            Owner.TopLeft += new Vector2(0,0.5f);
        }
    }
}
