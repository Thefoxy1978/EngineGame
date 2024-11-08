using System;

namespace Aiv.Fast2D.Component {
    public class TestSender : UserComponent {

        public TestSender (GameObject owner) : base (owner) {

        }

        public override void Update() {
            if (Input.GetKeyDown(KeyCode.A)) {
                EventHandlerManager.CastEvent((int)MyEvents.PremutoA, new DateTimeEventArg (DateTime.Now));
            } else if (Input.GetKeyDown(KeyCode.S)) {
                EventHandlerManager.CastEvent((int)MyEvents.PremutoS, EventArgs.Empty);
            } else if (Input.GetKeyDown(KeyCode.W)) {
                EventHandlerManager.CastEvent((int)MyEvents.PremutoW, EventArgs.Empty);
            } else if (Input.GetKeyDown(KeyCode.D)) {
                EventHandlerManager.CastEvent((int)MyEvents.PremutoD, EventArgs.Empty);
            }
        }

    }
    
}
