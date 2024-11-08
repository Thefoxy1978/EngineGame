using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiv.Fast2D.Component.Tiled
{
    class TileRenderer
    {
        public static int CorrectionFactor = 1;

        private Sprite sprite;
        public int Width { get; private set; }
        public int Height { get; private set; }

        private int horizontalOffset;
        private int verticalOffset;

        
        private Texture texture;

        private Vector2 fakePosition;
        public Vector2 Position { get { return sprite != null ?
                    sprite.position : fakePosition; }
            set { if (sprite != null)
                { sprite.position = value
                        + (sprite.EulerRotation != 0 ? Vector2.UnitX * Game.PixelsToUnit( Width) : Vector2.Zero); }
                else { fakePosition = value; } } }

        public float Scale { get; private set; }

        public TileRenderer(List<Tileset> _tilesets, Tile _tile, float _scaling)
        {
            if (_tile.Gid > 0)
            {
                Tileset tileset = null;
                foreach (var e in _tilesets)
                {
                    if (e.FirstGid <= _tile.Gid)
                    {
                        if (tileset != null)
                        {
                            if (e.FirstGid > tileset.FirstGid)
                            {
                                tileset = e;
                            }
                        }
                        else
                        {
                            tileset = e;
                        }
                    }
                }
                sprite = new Sprite(Game.PixelsToUnit(tileset.TileWidth), Game.PixelsToUnit(tileset.TileHeight));
                sprite.scale = new Vector2(_scaling*((float)(tileset.TileWidth+ CorrectionFactor) / tileset.TileWidth), _scaling * ((float)(tileset.TileHeight + CorrectionFactor) / tileset.TileHeight));
                sprite.position = new Vector2(0.0f, 0.0f);
                
                horizontalOffset = tileset.HorizontalOffset(_tile.Gid);
                verticalOffset = tileset.VerticalOffset(_tile.Gid);
                sprite.FlipX = _tile.HorizontalFlip;
                sprite.FlipY = _tile.VerticalFlip;

                if (_tile.DiagonalFlip) { 
                    sprite.EulerRotation = 90;
                }
                
                
                Width = tileset.TileWidth;
                Height = tileset.TileHeight;
                texture = tileset.Source;

                //Salviamoci lo scale dell Rendere
                Scale = _scaling;
            }
            else
            {
                Width = _tilesets[0].TileWidth;
                Height = _tilesets[0].TileHeight;
            }
        }

        public void Draw()
        {

            if (sprite != null)
            {

                sprite.DrawTexture(texture, horizontalOffset+ CorrectionFactor,
                    verticalOffset+ CorrectionFactor, Width- CorrectionFactor,
                    Height- CorrectionFactor);
            }
        }
    }
}
