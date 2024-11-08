using Aiv.Fast2D.Component.Tiled;
using System.Collections.Generic;
using OpenTK;

namespace Aiv.Fast2D.Component
{
    class KeyFactory : Tiled.ObjectFactory
    {

        public void Initialize(GameObject GObj,List<Tileset> tilesets, Tiled.Object obj)
        {
            GObj.AddComponent(SpriteRenderer.Factory(GObj, "key", new Vector2(0.45f, 0.45f), DrawLayer.Playground));
            GObj.AddComponent<Rigidbody>();
            GObj.AddComponent(ColliderFactory.CreateBoxFor(GObj));
            GObj.GetComponent<Rigidbody>().AddCollisionType((uint)RigidbodyTypes.Player);
            GObj.GetComponent<Rigidbody>().Type = (uint)RigidbodyTypes.Key;
            GObj.AddComponent<Key>();
        }
        


        public bool IsValid(Tiled.Object _object)
        {
            return true;
        }
    }
}
