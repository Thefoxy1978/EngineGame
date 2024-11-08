using Aiv.Fast2D.Component.Tiled;
using System.Collections.Generic;
using OpenTK;

namespace Aiv.Fast2D.Component
{
    class PlayerFactory : Tiled.ObjectFactory
    {
        private ArenaGrid grid;

        public PlayerFactory(ArenaGrid grid)
        {
            this.grid = grid;
        }

        public void Initialize(GameObject GObj, List<Tileset> tileset, Tiled.Object obj)
        {
            GObj.AddComponent(SpriteRenderer.Factory(GObj, "Player", new Vector2(0.50f, 0.45f), DrawLayer.Playground, Vector2.Zero, 16, 16));
            Sheet playerSheet = new Sheet(GfxMgr.GetTexture("Player"),2,20 );
            SheetClip Move = new SheetClip(playerSheet, "Move", new[] { 0 , 1 ,2 }, true, 9);
            Animator playerAnimator = GObj.AddComponent<Animator>(GObj.GetComponent<SpriteRenderer>());
            playerAnimator.AddClip(Move);

            GObj.AddComponent<Rigidbody>();
            GObj.AddComponent(ColliderFactory.CreateBoxFor(GObj));
            GObj.GetComponent<Rigidbody>().AddCollisionType((uint)RigidbodyTypes.Teleport);
            GObj.GetComponent<Rigidbody>().Type = (uint)RigidbodyTypes.Player;
            float speed = 10f;
            GObj.AddComponent<PlayerController>(speed, grid);
            CameraMgr.target = GObj;
        }


        public bool IsValid(Tiled.Object _object)
        {
            return true;
        }
    }
}
