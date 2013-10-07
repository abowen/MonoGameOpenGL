using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using MonoGameOpenGL.Entities;

namespace MonoGameOpenGL.Interfaces
{
    public interface IGameContent
    {
        void LoadGraphics(ContentManager content);
        void LoadBackground(GameLayer gameLayer);
        void LoadGame(GameLayer gameLayer);

    }
}
