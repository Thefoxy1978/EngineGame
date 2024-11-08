using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiv.Fast2D.Component.Tiled
{
    interface ObjectFactory
    {
        void Initialize(GameObject _gameObject, List<Tileset> _tilesets, Object _object);
        bool IsValid(Object _object);
    }
}
