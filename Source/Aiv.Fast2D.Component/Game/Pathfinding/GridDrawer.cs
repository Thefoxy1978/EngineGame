using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiv.Fast2D.Component
{
    class GridDrawer : Component, IDrawable
    {
        public ArenaGrid Grid;

        private Sprite mySprite;
        private Texture myTexture;

        public GridDrawer(GameObject gameobject, ArenaGrid grid, string textureName)
            : base(gameobject)
        {
            Grid = grid;

            myTexture = GfxMgr.GetTexture(textureName);
            mySprite = new Sprite(grid.CellWidth, grid.CellHeight);

            DrawMgr.AddItem(this);
        }

        public DrawLayer Layer { get { return DrawLayer.GUI; } }

        public void Draw()
        {
            mySprite.scale = transform.Scale;
            mySprite.EulerRotation = transform.Rotation;

            for (int x = 0; x < Grid.Grid.GetLength(0); ++x)
            {
                for (int y = 0; y < Grid.Grid.GetLength(1); ++y)
                {
                    if (Grid.Grid[x, y] != 1)
                    {
                        mySprite.position = transform.Position + new OpenTK.Vector2(x * Grid.CellWidth, y * Grid.CellHeight);
                        mySprite.DrawTexture(myTexture);
                    }
                }
            }
        }
    }
}
