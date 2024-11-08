using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiv.Fast2D.Component.Tiled
{
    class ObjectRegistry
    {
        private Dictionary<string, ObjectFactory> instanceDictionary;

        public ObjectRegistry()
        {
            instanceDictionary = new Dictionary<string, ObjectFactory>();
        }

        public void Register(string _type, ObjectFactory _factory)
        {
            instanceDictionary.Add(_type, _factory);
        }

        public GameObject Create(Vector2 _position, List<Tileset> _tilesets, Object _object)
        {
            ObjectFactory factory;
            if (instanceDictionary.TryGetValue(_object.Type, out factory))
            {
                if(factory.IsValid(_object))
                {
                    GameObject go = new GameObject(_object.Name, _position);
                    factory.Initialize(go, _tilesets, _object);
                    return go;
                }
            }
            return null;
        }
    }
}
