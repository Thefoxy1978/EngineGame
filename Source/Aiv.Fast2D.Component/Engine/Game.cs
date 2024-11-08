
namespace Aiv.Fast2D.Component {
    public static class Game {

        public static float WorkingHeight { get; private set; }
        public static float UnitSize { get; private set; }
        public static float Gravity { get; set; }

        public static Window Win;
        public static bool IsRunning;
        public static float DeltaTime {
            get {
                if (firstFrame) return 0;
                return Win.DeltaTime * timeScale;
            }
        }
        public static float UnscaledDeltaTime {
            get {
                if (firstFrame) return 0;
                return Win.DeltaTime;
            }
        }
        private static float timeScale = 1;
        public static float TimeScale {
            set { timeScale = value; }
        }

        private static bool firstFrame;

        private static Scene currentScene;
        public static Scene CurrentScene {
            get { return currentScene; }
        }

        private static bool changeScene;
        private static Scene nextScene;

        public static void Init (int windowWidth, int windowHeight, string windowName, Scene startScene
            , float workingHeight, float ortographicSize) {
            Win = new Window(windowWidth, windowHeight, windowName);
            Win.Position = new OpenTK.Vector2(0, 0);
            Win.SetVSync(false);
            WorkingHeight = workingHeight;
            Win.SetDefaultViewportOrthographicSize(ortographicSize);
            UnitSize = WorkingHeight / Win.OrthoHeight;
            Game.nextScene = startScene;
        }

        public static float PixelsToUnit (float pixelsSize) {
            return pixelsSize / UnitSize;
        }

        public static float UnitToPixel (float unit) {
            return unit * UnitSize;
        }

        public static void Play () {
            IsRunning = true;
            ChangeScene(); //inizializzo la prima scena
            while (Win.IsOpened && IsRunning) {

                PhysicsMgr.FixedUpdate();
                PhysicsMgr.CheckCollisions();

                currentScene.Update();
                currentScene.LateUpdate();
                CameraMgr.MoveCameras();
                DrawMgr.Draw();
                firstFrame = false;
                Input.PerformLastKey();
                Win.Update();
                if (changeScene) {
                    ChangeScene();
                    firstFrame = true;
                    changeScene = false;
                }
            }
        }

        public static void TriggerChangeScene (Scene nextScene) {
            changeScene = true;
            Game.nextScene = nextScene;
        }

        private static void ChangeScene () {
            if (currentScene != null) {
                currentScene.DestroyScene();
            }
            if (nextScene == null) {
                IsRunning = false;
                return;
            }
            currentScene = nextScene;
            currentScene.InitializeScene();
            currentScene.Awake();
            currentScene.Start();
            currentScene.IsInitialize = true;
        }
    }
}
