using OpenTK;
using System;

namespace Aiv.Fast2D.Component {

    public delegate void EventTemplate(EventArgs message);

    public static class EventHandlerManager {

        private static EventTemplate[] gameEvents;

        public static void Init(int numberOfEvents) {
            gameEvents = new EventTemplate[numberOfEvents];
        }

        public static void AddListener(int eventID, EventTemplate listener) {
            gameEvents[eventID] += listener;
        }

        public static void RemoveListener(int eventID, EventTemplate listener) {
            gameEvents[eventID] -= listener;
        }

        public static void CastEvent(int eventID, EventArgs message) {
            gameEvents[eventID]?.Invoke(message);
        }

        public static void ClearListeners() {
            for (int i = 0; i < gameEvents.Length; i++) {
                gameEvents[i] = null;
            }
        }

    }
    
    public class DateTimeEventArg : EventArgs {
        public DateTime CastTime;

        public DateTimeEventArg (DateTime time) {
            CastTime = time;
        }
    }

}
