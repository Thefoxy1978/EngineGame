using OpenTK;
using System;

namespace Aiv.Fast2D.Component {
    public class CircleCollider : Collider {

        public float Radius;

        public CircleCollider (GameObject owner, float radius, Vector2 offset): base (owner, offset) {
            Radius = radius;
        }

        public override bool Collides(Collider collider, ref Collision collisionInfo) {
            return collider.Collides(this, ref collisionInfo);
        }

        public override bool Collides(BoxCollider box, ref Collision collisionInfo) {
            return box.Collides(this, ref collisionInfo);
        }

        public override bool Collides(CircleCollider circle, ref Collision collisionInfo) {
            Vector2 dist = circle.Position - Position;
            return dist.LengthSquared <= Math.Pow(Radius + circle.Radius, 2);
        }

        public override bool Contains(Vector2 point) {
            float distaFromCenter = (point - Position).LengthSquared;
            return distaFromCenter <= Radius * Radius;
        }

    }
}
