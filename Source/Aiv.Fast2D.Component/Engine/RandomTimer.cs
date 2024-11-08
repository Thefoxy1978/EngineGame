using System;

namespace Aiv.Fast2D.Component {
    public class RandomTimer {

        private int timeMin;
        private int timeMax;
        private Random rand;
        private float remainingSeconds;

        public RandomTimer (int timeMin, int timeMax) {
            this.timeMax = timeMax;
            this.timeMin = timeMin;
            this.rand = new Random();

            Reset();
        }

        public void Tick (float deltaTime) {
            remainingSeconds -= deltaTime;
            if (remainingSeconds <= 0) remainingSeconds = 0;
        }

        public bool IsOver () {
            return remainingSeconds == 0;
        }

        public void Reset () {
            remainingSeconds = rand.Next(timeMin, timeMax);
        }
    }
}
