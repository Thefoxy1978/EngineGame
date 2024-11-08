using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiv.Fast2D.Component
{
    class TiledObject : UserComponent, IDrawable
    {
        private Tiled.TileRenderer renderer;

        private DrawLayer layer;
        public DrawLayer Layer
        {
            get { return layer; }
            set { layer = value; }
        }

        public TiledObject(GameObject owner, Tiled.TileRenderer _renderer, DrawLayer layer) : base(owner)
        {
            Layer = layer;
            renderer = _renderer;
            DrawMgr.AddItem(this);
        }

        public void Draw()
        {
            renderer.Position = transform.Position;
            renderer.Draw();
        }

        public override Component Clone(GameObject owner)
        {
            TiledObject sr = new TiledObject(owner, renderer, Layer);
            return sr;
        }

    }
}
