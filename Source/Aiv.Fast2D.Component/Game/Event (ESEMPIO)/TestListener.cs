using System;

namespace Aiv.Fast2D.Component {
    public class TestListener : UserComponent {

        public TestListener (GameObject owner) : base (owner) {

        }

        public override void OnEnable() {
            EventHandlerManager.AddListener((int)MyEvents.PremutoA, OnPremutoA);
            EventHandlerManager.AddListener((int)MyEvents.PremutoD, OnPremutoD);
            EventHandlerManager.AddListener((int)MyEvents.PremutoS, OnPremutoS);
            EventHandlerManager.AddListener((int)MyEvents.PremutoW, OnPremutoW);
        }

        public override void OnDisable() {
            EventHandlerManager.RemoveListener((int)MyEvents.PremutoA, OnPremutoA);
            EventHandlerManager.RemoveListener((int)MyEvents.PremutoD, OnPremutoD);
            EventHandlerManager.RemoveListener((int)MyEvents.PremutoS, OnPremutoS);
            EventHandlerManager.RemoveListener((int)MyEvents.PremutoW, OnPremutoW);
        }


        private void OnPremutoA (EventArgs message) {
            DateTimeEventArg realMessage = (DateTimeEventArg)message;
            Console.WriteLine("Premuto A con DateTime: " + realMessage.CastTime.ToString());
        }

        private void OnPremutoS (EventArgs message) {
            Console.WriteLine("Premuto S");
        }

        private void OnPremutoW (EventArgs message) {
            Console.WriteLine("Premuto W");
        }

        private void OnPremutoD (EventArgs message) {
            Console.WriteLine("Premuto D");
        }

    }
}
