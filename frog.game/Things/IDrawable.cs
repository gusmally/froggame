using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace frog.game.Things
{
    internal interface IDrawable
    {
        Texture2D SmallSprite { get; }
        Texture2D LargeSprite { get; }
        public void Draw();
    }
}
