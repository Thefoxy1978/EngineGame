using OpenTK;
using System;

namespace Aiv.Fast2D.Component
{
    public class Map2_Cass : Scene
    {
        Tiled.ObjectRegistry objectRegistry = new Tiled.ObjectRegistry();
        public ArenaGrid _Grid;
        float scaling = 1f;


        public override void InitializeScene()
        {
            EventHandlerManager.AddListener((int)MyEvents.ChangeScene, ChangeScene);
            var map1 = new Tiled.Map("Assets/Level/2.tmx");
            base.InitializeScene();
            SetCamera();
            int[,] grid = new int[map1.Width, map1.Height];
            for (int i = 0; i < map1.Height; i++)
            {
                for (int j = 0; j < map1.Width; j++)
                {
                    grid[j, i] = map1.Layers[0].Tiles[i, j].Gid;
                }
            }
            foreach (var obj in map1.ObjectGroups[0].Objects)
            {
                var idProperty = obj.Properties.Find(p => { return p.Name == "Teleport_ID" && p.Type == EType.EInt; });
                if (idProperty != null)
                {
                    GameObject teleport = new GameObject("Teleport", new Vector2(Game.PixelsToUnit((float)obj.X), Game.PixelsToUnit((float)obj.Y)));
                    teleport.AddComponent(SpriteRenderer.Factory(teleport, "key", Vector2.Zero, DrawLayer.Background, Vector2.Zero, 32, 16));
                    Rigidbody rb = teleport.AddComponent<Rigidbody>();
                    teleport.AddComponent(ColliderFactory.CreateBoxFor(teleport));
                    rb.Type = (uint)RigidbodyTypes.Teleport;
                    rb.AddCollisionType((uint)RigidbodyTypes.Player);
                    int Tdi = idProperty != null ? idProperty.AsInt() : 0;
                    teleport.AddComponent<Check_Teleport>(Tdi);
                }
            }
            _Grid = new ArenaGrid(grid, Game.PixelsToUnit(map1.TileWidth * scaling), Game.PixelsToUnit(map1.TileHeight * scaling));
            objectRegistry.Register("Player", new PlayerFactory(_Grid));
            objectRegistry.Register("key", new KeyFactory());
            CreateBackgroundCamera();
            CreateBackground(map1);
        }

        protected override void LoadAssets()
        {
            GfxMgr.AddTexture("cell", "Assets/Cell/Cell.png");
            GfxMgr.AddTexture("Player", "Assets/Player/Movment/provae.png");
            GfxMgr.AddTexture("key", "Assets/Key/item8BIT_key.png");
            AudioManager.Init((int)AudioLayer.last);
            AudioManager.AddClip("Background", "Assets/Audio/Untitled.wav");

        }

        private void SetCamera()
        {
            CameraMgr.Init(new Vector2(Game.Win.OrthoWidth * 0.5f, Game.Win.OrthoHeight * 0.5f), new Vector2(Game.Win.OrthoWidth * 0.5f, Game.Win.OrthoHeight * 0.5f));
            CameraMgr.SetCameraLimits(Game.Win.OrthoWidth * 0.5f,
                (Game.Win.OrthoWidth * 1.3f) * 0.5f,
                Game.Win.OrthoHeight * 0.5f,
                Game.Win.OrthoHeight * 0.5f);
        }
        private void ChangeScene(EventArgs eventArgs)
        {
            SceneUpdateArgs SUA = (SceneUpdateArgs)eventArgs;

            switch (SUA.Teleport_ID)
            {
                case 2:
                    Game.TriggerChangeScene(new Map1_Island());
                    break;

            }
        }

        private void CreateBackground(Tiled.Map map)
        {
            float scaling = 1f;

            foreach (var objGroup in map.ObjectGroups)
            {
                foreach (var obj in objGroup.Objects)
                {
                    objectRegistry.Create(new Vector2(Game.PixelsToUnit((float)obj.X * scaling),
                        Game.PixelsToUnit((float)obj.Y * scaling)), map.Tilesets, obj);
                }
            }

            GameObject tileMap = new GameObject("map", Vector2.Zero);
            tileMap.AddComponent<TiledMap>(new Tiled.Renderer(map, scaling), DrawLayer.Background);
            tileMap.AddComponent<GridDrawer>(_Grid, "cell");
            AudioSourceComponent asc = tileMap.AddComponent<AudioSourceComponent>();
            asc.MyType = (int)AudioLayer.music;
            asc.Loop = true;
            asc.SetClip(AudioManager.GetClip("Background"));
            asc.Play();
        }

        private void CreateBackgroundCamera()
        {
            CameraMgr.AddCameras("GUI", new Camera());
        }
    }
}
