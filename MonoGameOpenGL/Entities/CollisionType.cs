using System;

namespace MonoGameOpenGL.Entities
{
    public class CollisionType
    {
        public Type TypeA;
        public Type TypeB;
        public Action<Sprite, Sprite> Action;
    }
}