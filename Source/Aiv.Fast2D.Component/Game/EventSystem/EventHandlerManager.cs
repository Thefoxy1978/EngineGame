using OpenTK;
using System;

namespace Aiv.Fast2D.Component
{

    public delegate void EventTemplate(EventArgs message);

    public static class EventHandlerManager
    {

        private static EventTemplate[] gameEvents;

        public static void Init(int numberOfEvents)
        {
            gameEvents = new EventTemplate[numberOfEvents];
        }

        public static void AddListener(int eventID, EventTemplate listener)
        {
            gameEvents[eventID] += listener;
        }

        public static void RemoveListener(int eventID, EventTemplate listener)
        {
            gameEvents[eventID] -= listener;
        }

        public static void CastEvent(int eventID, EventArgs message)
        {
            gameEvents[eventID]?.Invoke(message);
        }

        public static void ClearListeners()
        {
            for (int i = 0; i < gameEvents.Length; i++)
            {
                gameEvents[i] = null;
            }
        }

    }
    public class PositionEventArgs : EventArgs
    {
        public Vector2 Position;
        public PositionEventArgs(Vector2 Position)
        {
            this.Position = Position;
        }
    }
    public class GameObjectArg : EventArgs
    {
        public GameObject GameObject;

        public GameObjectArg(GameObject go)
        {
            GameObject = go;
        }
    }

    public class SceneUpdateArgs : EventArgs
    {
        public int Teleport_ID;
        public SceneUpdateArgs (int Teleport_ID)
        {
            this.Teleport_ID = Teleport_ID;
        }

    }
    public class PlayerHealthUpdatedArgs : EventArgs
    {
        public int PlayerID;
        public float HealthPercentage;

        public PlayerHealthUpdatedArgs(int playerID, float healtPercentage)
        {
            this.HealthPercentage = healtPercentage;
            this.PlayerID = playerID;
        }
    }
}
