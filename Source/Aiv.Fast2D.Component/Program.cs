
namespace Aiv.Fast2D.Component {

    public enum RigidbodyTypes { Player = 1, Teleport = 2, Key = 4}

    public enum MyEvents {
        ChangeScene,
        Last
    }
     public enum AudioLayer
    {
        music,
        last
    }


    class Program {
        static void Main(string[] args) {
            EventHandlerManager.Init((int)MyEvents.Last);

            Input.AddUserButton("Movment", new ButtonMatch[] { new MouseButtonMatch(MouseButton.RightMouse) });
            Input.AddUserButton("UI_Confirm", new ButtonMatch[] { new KeyButtonMatch (KeyCode.Return) });

            Game.Init(1280 , 980, "Alive2007", new Start_Scene(), 320, 20);
            Game.Play();
        }
    }
}
