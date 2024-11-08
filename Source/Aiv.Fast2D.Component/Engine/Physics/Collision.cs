using OpenTK;

namespace Aiv.Fast2D.Component {

    public enum CollisionType { None, RectIntersection}

    public struct Collision {
        public Vector2 Delta;
        public Collider Collider;
        public CollisionType Type;
    }
}
