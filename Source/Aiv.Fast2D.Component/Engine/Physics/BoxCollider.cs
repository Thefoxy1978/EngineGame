using OpenTK;
using System;

namespace Aiv.Fast2D.Component {
    public class BoxCollider : Collider {

        protected float halfWidth;
        protected float HalfWidth {
            get { return halfWidth * transform.Scale.X; }
        }
        protected float halfHeight;
        protected float HalfHeight {
            get { return halfHeight * transform.Scale.Y; }
        }
        public float Height {
            get {return HalfHeight * 2; }
        }
        public float Width {
            get { return HalfWidth * 2; }
        }

        public BoxCollider (GameObject owner, float w, float h, Vector2 offset) : base (owner, offset) {
            halfWidth = w * 0.5f;
            halfHeight = h * 0.5f;
        }

        public override bool Collides (Collider collider, ref Collision collisionInfo) {
            return collider.Collides(this, ref collisionInfo);
        }

        public override bool Collides(CircleCollider circle, ref Collision collisionInfo) {
            float deltaX = circle.Position.X -
                Math.Max(Position.X - HalfWidth, Math.Min(circle.Position.X, Position.X + HalfWidth));
            float deltaY = circle.Position.Y -
                Math.Max(Position.Y - HalfHeight, Math.Min(circle.Position.Y, Position.Y + HalfHeight));
            return (deltaX * deltaX + deltaY * deltaY) < circle.Radius * circle.Radius;
        }

        public override bool Collides(BoxCollider box, ref Collision collisionInfo) {
            collisionInfo.Type = CollisionType.RectIntersection;
            float deltaX = Math.Abs(box.Position.X - Position.X) - (HalfWidth + box.HalfWidth);
            if (deltaX > 0) return false;
            float deltaY = Math.Abs(box.Position.Y - Position.Y) - (HalfHeight + box.HalfHeight);
            if (deltaY > 0) return false;
            collisionInfo.Delta = new Vector2(-deltaX, -deltaY);
            return true;
        }

        public override bool Contains(Vector2 point) {
            return
                point.X >= Position.X - HalfWidth &&
                point.X <= Position.X + HalfWidth &&
                point.Y >= Position.Y - HalfHeight &&
                point.Y <= Position.Y + HalfHeight;
        }

        public override Component Clone(GameObject owner) {
            return new BoxCollider(owner, Width, Height, Offset);
        }

    }
}
