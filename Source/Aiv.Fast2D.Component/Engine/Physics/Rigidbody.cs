using OpenTK;

namespace Aiv.Fast2D.Component {
    public class Rigidbody : Component, IFixedUpdatable {

        protected uint collisionMask;

        public uint Type;
        private Vector2 velocity;
        public Vector2 Velocity {
            get { return velocity; }
            set { velocity = value; }
        }

        public bool IsGravity;
        public bool IsCollisionAffected;

        protected float friction;
        public float Friction {
            get { return friction; }
            set {
                if (value >= 0) friction = value;
            }
        }

        public Collider Collider { get; set; }

        public Rigidbody (GameObject owner) : base (owner) {
            PhysicsMgr.AddItem(this);
            IsCollisionAffected = true;
            Collider = gameObject.GetComponent(typeof(Collider)) as Collider;
        }

        public virtual void FixedUpdate () {
            if (IsGravity) {
                velocity.Y += Game.Gravity * Game.DeltaTime;
            }
            if (velocity.LengthSquared > 0 && friction > 0) {
                float fAmount = Friction * Game.DeltaTime;
                float newVelocityLength = velocity.Length - fAmount;
                if (newVelocityLength < 0) newVelocityLength = 0;
                velocity = velocity.Normalized() * newVelocityLength;
            }
            transform.Position += Velocity * Game.DeltaTime;
        }

        public virtual bool Collides(Rigidbody other, ref Collision collisionInfo) {
            return Collider.Collides(other.Collider, ref collisionInfo);
        }

        public void AddCollisionType (uint add) {
            collisionMask |= add;
        }

        public bool CollisionMatches (uint type) {
            return (type & collisionMask) != 0;
        }

    }
}
