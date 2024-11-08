using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Aiv.Fast2D.Component
{
    public class ArenaGrid
    {
        public struct Cell
        {
            public int x;
            public int y;
        }

        class SearchState : IAStarState<SearchState>
        {
            class SearchActionMove : IAStarAction<SearchState>
            {
                public SearchState NextState { get; }

                public float Cost { get; }

                public SearchActionMove(Cell _dove, int[,] _grid)
                {
                    NextState = new SearchState(_dove, _grid);
                    Cost = 1;
                }
            }

            public Cell Position;

            public static List<int> _Go = new List<int>
             {30,72,74,78, // lv1
             168,  // lv2
             257,262,251,252, // lv4 
             0,40,41,80,101,114,99,104,138,63 //lv5      
             };

            public int[,] Grid { get; }

            public SearchState(Cell _dove, int[,] _grid)
            {
                Position = _dove;
                Grid = _grid;
            }

            public List<IAStarAction<SearchState>> Actions { get { return GetActions(); } }

            public bool Equals(SearchState _other)
            {
                return Position.x == _other.Position.x && Position.y == _other.Position.y;
            }

            public float Heuristic(SearchState _other)
            {
                return Math.Abs(Position.x - _other.Position.x) + Math.Abs(Position.y - _other.Position.y);
            }

            private List<IAStarAction<SearchState>> GetActions()
            {
                List<IAStarAction<SearchState>> result = new List<IAStarAction<SearchState>>();

                if (Position.x > 0)
                {
                    Cell c = new Cell { x = Position.x - 1, y = Position.y };
                    if (_Go.Contains(Grid[c.x, c.y] - 1))
                    {
                        result.Add(new SearchActionMove(c, Grid));
                    }
                }
                if (Position.y > 0)
                {
                    Cell c = new Cell { x = Position.x, y = Position.y - 1 };
                    if (_Go.Contains(Grid[c.x, c.y] - 1))
                    {
                        result.Add(new SearchActionMove(c, Grid));
                    }
                }

                if (Position.x < Grid.GetUpperBound(0))
                {
                    Cell c = new Cell { x = Position.x + 1, y = Position.y };
                    if (_Go.Contains(Grid[c.x, c.y] - 1))
                    {
                        result.Add(new SearchActionMove(c, Grid));
                    }
                }
                if (Position.y < Grid.GetLength(1) - 1)
                {
                    Cell c = new Cell { x = Position.x, y = Position.y + 1 };
                    if (_Go.Contains(Grid[c.x, c.y] - 1))
                    {
                        result.Add(new SearchActionMove(c, Grid));
                    }
                }

                return result;
            }
        }

        public int[,] Grid { get; private set; }
        public float CellWidth { get; private set; }
        public float CellHeight { get; private set; }

        public ArenaGrid(int[,] _grid, float _cellWidth, float _cellHeight)
        {
            Grid = _grid;

            CellWidth = _cellWidth;
            CellHeight = _cellHeight;
        }

        public Vector2 GetCellCenter(Cell _c)
        {
            return new Vector2((_c.x + 0.5f) * CellWidth,
                (_c.y + 0.5f) * CellHeight);
        }

        public Cell GetCellAt(Vector2 _pos)
        {
            return new Cell { x = (int)(_pos.X / CellWidth),
                y = (int)(_pos.Y / CellHeight) };
        }

        public List<Cell> FindPath(Cell _from, Cell _to)
        {
            List<Cell> result = new List<Cell>();
            var path = AStar<SearchState>.Find(new SearchState(_from, Grid), new SearchState(_to, Grid));
            while (path != null)
            {
                result.Add(path.State.Position);
                path = path.Parent;
            }
            result.Reverse();
            return result;
        }
    }
}
