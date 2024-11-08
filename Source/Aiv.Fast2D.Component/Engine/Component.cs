using System;

namespace Aiv.Fast2D.Component {
    public abstract class Component {

        protected bool enabled;
        public virtual bool Enabled {
            get { return enabled && gameObject.IsActive; }
            set {
                if (enabled == value) return;
                enabled = value;
            }
        }
        public bool EnabledSelf {
            get { return enabled; }
        }
        public GameObject gameObject{
            get;
            private set;
        }
        public Transform transform {
            get { return gameObject.transform; }
        }

        public Component (GameObject gameobject) {
            this.gameObject = gameobject;
            this.enabled = true;
        }

        //wrapper del GetComponent
        public T GetComponent<T> () where T : Component {
            return gameObject.GetComponent<T>();
        }

        public virtual Component Clone (GameObject owner) { //l'owner sarà il gameobjcet su cui sarà attaccato il clone
            throw new NotImplementedException();
        }
    }
}
