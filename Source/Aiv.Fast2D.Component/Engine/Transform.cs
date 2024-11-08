using OpenTK;
using System;

namespace Aiv.Fast2D.Component {
    public class Transform {

        private Vector2 position;
        public Vector2 Position {
            get { return position; }
            set { position = value; }
        }
        private Vector2 scale;
        public Vector2 Scale {
            get { return scale; }
            set { scale = value; }
        }
        private float rotation;
        public float Rotation {
            get { return rotation; }
            set { rotation = value; }
        }

        public Vector2 Forward {
            get {
                return new Vector2(
                    (float)Math.Cos(DegreesToRadiants(rotation)), //componente X, che è il coseno dell'angolo
                    (float)Math.Sin(DegreesToRadiants(rotation))  //componente Y, che è il seno dell'angolo
                ); }
            set {
                rotation = RadiantsToDegrees((float)Math.Atan2(value.Y, value.X));
            }
        }

        public Transform (Vector2 position, Vector2 scale, float rotation = 0) {
            Position = position;
            Scale = scale;
            Rotation = rotation;
        }

        public static float RadiantsToDegrees (float radiants) {
            return (180 / (float)Math.PI) * radiants;
        }

        public static float DegreesToRadiants (float degrees) {
            return (float)Math.PI * degrees / 180f;
        }

    }
}
