using System;

namespace Aiv.Fast2D.Component.AI {
    public abstract class State {

        protected FSM machine;


        //GameLoop wrapper
        public virtual void Awake () {

        }

        public virtual void Start () {

        }

        public virtual void Update () {

        }

        public virtual void LateUpdate () {

        }

        public void OnCollide (Collision collisionInfo) {

        }
 

        //FSM events
        public virtual void Enter () {

        }

        public virtual void Exit () {

        }

        public void AssignStateMachine (FSM stateMachine) {
            machine = stateMachine;
        }

    }
}
