using OpenTK;

namespace Aiv.Fast2D.Component {
    public abstract class Collider : Component {

        public Vector2 Offset;
        public Vector2 Position {
            get { return gameObject.transform.Position + Offset; }
        }

        public Collider (GameObject owner, Vector2 Offset) : base (owner) {
            this.Offset = Offset;
            Rigidbody tempRigidbody = gameObject.GetComponent(typeof(Rigidbody)) as Rigidbody;
            if (tempRigidbody != null) {
                tempRigidbody.Collider = this;
            }
        }

        public abstract bool Collides(Collider collider, ref Collision collisionInfo);
        public abstract bool Collides(CircleCollider circle, ref Collision collisionInfo);
        public abstract bool Collides(BoxCollider box, ref Collision collisionInfo);
        public abstract bool Contains(Vector2 point);

    }
}
