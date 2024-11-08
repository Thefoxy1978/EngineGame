namespace Aiv.Fast2D.Component {
    public abstract class UserComponent : Component, IStartable,IUpdatable, ICollidable {

        public override bool Enabled {
            get { return base.Enabled; }
            set {
                if (value == enabled) return;
                base.Enabled = value;
                if (!gameObject.IsActive || !Game.CurrentScene.IsInitialize) return;
                if (value) {
                    OnEnable();
                } else {
                    OnDisable();
                }
            }
        }

        public UserComponent (GameObject gameObject) : base (gameObject) {

        }

        public virtual void Awake () {

        }

        public virtual void OnEnable () {

        }

        public virtual void OnDisable () {

        }

        public virtual void Start () {

        }

        public virtual void Update () {

        }

        public virtual void LateUpdate () {

        }

        public virtual void OnCollide (Collision collisionInfo) {

        }

        public virtual void OnDestroy() {

        }

    }
}
