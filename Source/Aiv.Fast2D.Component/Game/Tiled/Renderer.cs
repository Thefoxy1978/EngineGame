using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiv.Fast2D.Component.Tiled
{
    class Renderer 
    {
        private List<TileRenderer[,]> tilesLayers;

        public Renderer(Map _map, float _scaling)
        {
            tilesLayers = new List<TileRenderer[,]>();

            foreach (var layer in _map.Layers)
            {
                var tiles = new TileRenderer[_map.Height, _map.Width];
                Vector2 layerOffset = new Vector2((float)layer.OffsetX / Game.UnitSize, (float)layer.OffsetY / Game.UnitSize);
                for (int row = 0; row < _map.Height; ++row)
                {
                    for (int col = 0; col < _map.Width; ++col)
                    {
                        var tile = new TileRenderer(_map.Tilesets, layer.Tiles[row, col], _scaling);
                        tile.Position = (layerOffset + new Vector2(Game.PixelsToUnit(col * tile.Width), Game.PixelsToUnit(row * tile.Height))) * _scaling;
                        tiles[row, col] = tile;
                    }
                }
                tilesLayers.Add(tiles);
            }
        }

        public void Draw(Camera _camera, Window _window)
        {
            foreach (var tiles in tilesLayers)
            {
                /*int startRow = 1;
                int startCol = 1;
                while (startRow < tiles.GetLength(0) && tiles[startRow, 0].Position.Y 
                    < (_camera.position.Y - Game.PixelsToUnit(_window.Height * 0.5f))) 
                    ++startRow;
                while (startCol < tiles.GetLength(1) && tiles[0, startCol].Position.X 
                    < (_camera.position.X - Game.PixelsToUnit(_window.Width * 0.5f))) 
                    ++startCol;*/

                int newStartCol = (int)Math.Floor((_camera.position.X - Game.PixelsToUnit(_window.Width * .5f)) / 
                    Game.PixelsToUnit(tiles[0, 0].Width * tiles[0,0].Scale));
                int newStartRow = (int)Math.Floor((_camera.position.Y - Game.PixelsToUnit(_window.Height * .5f)) / 
                    Game.PixelsToUnit(tiles[0, 0].Height * tiles[0, 0].Scale));

                newStartCol = newStartCol < 0 ? 0 : newStartCol;
                newStartRow = newStartRow < 0 ? 0 : newStartRow;




                /*if (newStartCol != startCol - 1)
                    Console.WriteLine("new Start Col: " + (newStartCol) + " Old value: " + startCol);
                if (newStartRow != startRow - 1)
                    Console.WriteLine("new Start Row: " + (newStartRow) + " Old value: " + startRow);

                int maxRow = startRow;
                int maxCol = startCol;
                while (maxRow < tiles.GetLength(0) && tiles[maxRow, 0].Position.Y 
                    < (_camera.position.Y + Game.PixelsToUnit(_window.Height * 0.5f))) 
                    ++maxRow;
                while (maxCol < tiles.GetLength(1) && tiles[0, maxCol].Position.X 
                    < (_camera.position.X + Game.PixelsToUnit(_window.Width * 0.5f))) 
                    ++maxCol;*/

                int newMaxCol = newStartCol + 
                   (int)Math.Ceiling(_window.Width / (tiles[0, 0].Width * tiles[0, 0].Scale)) + 1;
                int newMaxRow = newStartRow + (int)Math.Ceiling(_window.Height / (tiles[0, 0].Height * tiles[0, 0].Scale)) + 1;  

                newMaxCol = newMaxCol > tiles.GetLength(1) ? tiles.GetLength(1) : newMaxCol;
                newMaxRow = newMaxRow > tiles.GetLength(0) ? tiles.GetLength(0) : newMaxRow;


                for (int row = newStartRow; row < newMaxRow; ++row)
                {
                    for (int col = newStartCol; col < newMaxCol; ++col)
                    {
                        if (tiles[row, col] != null)
                        {
                            tiles[row, col].Draw();
                        }
                    }
                }

                /*
                if (newMaxCol != maxCol)
                    Console.WriteLine("new Max Col: " + newMaxCol + " Old value: " + maxCol);
                if (newMaxRow != maxRow)
                    Console.WriteLine("new Max Col: " + newMaxRow + " Old value: " + maxRow);*/

            }
        }
    }
}
