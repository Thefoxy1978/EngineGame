using System.Collections.Generic;

namespace Aiv.Fast2D.Component.AI {
    public class FSM : UserComponent {

        private Dictionary<int, State> states;
        private State currentState;

        private int startState = int.MinValue;

        public void SetStartState (int startState) {
            this.startState = startState;
        }

        public FSM (GameObject owner) : base (owner) {
            states = new Dictionary<int, State>();
        }

        public void RegisterState (int id, State state) {
            states.Add(id, state);
            state.AssignStateMachine(this);
        }

        public void Switch (int id) {
            if (currentState != null) {
                currentState.Exit();
            }
            currentState = states[id];
            currentState.Enter();
        }

        public override void Awake() {
            foreach (State state in states.Values) {
                state.Awake();
            }
        }

        public override void OnEnable() {
            if (startState == int.MinValue) return;
            Switch(startState);
        }

        public override void Start() {
            foreach (State state in states.Values) {
                state.Start();
            }
        }

        public override void Update() {
            if (currentState == null) return;
            currentState.Update();
        }

        public override void LateUpdate() {
            if (currentState == null) return;
            currentState.LateUpdate();
        }

        public override void OnCollide(Collision collisionInfo) {
            if (currentState == null) return;
            currentState.OnCollide(collisionInfo);
        }

    }
}
