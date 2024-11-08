using OpenTK;

namespace Aiv.Fast2D.Component {
    public class TestScene : Scene {

        public override void InitializeScene() {
            base.InitializeScene();
            GameObject senderGO = new GameObject("SenderGO", Vector2.Zero);
            senderGO.AddComponent<TestSender>();
            GameObject listenerGO = new GameObject("ListenerGO", Vector2.Zero);
            listenerGO.AddComponent<TestListener>();
        }

    }
}
