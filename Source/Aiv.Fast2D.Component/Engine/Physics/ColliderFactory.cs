using OpenTK;
using System;

namespace Aiv.Fast2D.Component {
    public static class ColliderFactory {

        public static Collider CreateCircleFor (GameObject obj) {
            SpriteRenderer spriteRenderer = obj.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
            float halfDiagonal = (float)(Math.Sqrt(spriteRenderer.Width * spriteRenderer.Width +
                spriteRenderer.Height * spriteRenderer.Height)) * 0.5f;
            return new CircleCollider(obj, halfDiagonal, 
                new Vector2 ((0.5f - spriteRenderer.Pivot.X) * spriteRenderer.Width,
                (0.5f - spriteRenderer.Pivot.Y) * spriteRenderer.Height));
        }

        public static Collider CreateBoxFor (GameObject obj) {
            SpriteRenderer spriteRenderer = obj.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
            return new BoxCollider(obj, spriteRenderer.Width / obj.transform.Scale.X, spriteRenderer.Height / obj.transform.Scale.Y,
                new Vector2((0.5f - spriteRenderer.Pivot.X) * spriteRenderer.Width,
                (0.5f - spriteRenderer.Pivot.Y) * spriteRenderer.Height));
        }

    }
}
