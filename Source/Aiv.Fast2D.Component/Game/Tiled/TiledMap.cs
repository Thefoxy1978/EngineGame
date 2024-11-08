using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiv.Fast2D.Component
{
    class TiledMap : UserComponent, IDrawable
    {
        private Tiled.Renderer renderer;

        private DrawLayer layer;
        public DrawLayer Layer
        {
            get { return layer; }
            set { layer = value; }
        }

        public TiledMap(GameObject owner, Tiled.Renderer _renderer, DrawLayer layer) : base(owner)
        {
            Layer = layer;
            renderer = _renderer;
            DrawMgr.AddItem(this);
        }

        public void Draw()
        {
            renderer.Draw(CameraMgr.MainCamera, Game.Win);
        }

        public override Component Clone(GameObject owner)
        {
            TiledMap sr = new TiledMap(owner, renderer, Layer);
            return sr;
        }

    }
}
