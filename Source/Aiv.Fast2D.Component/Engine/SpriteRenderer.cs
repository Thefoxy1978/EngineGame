using OpenTK;


namespace Aiv.Fast2D.Component {
    public class SpriteRenderer : Component, IDrawable {

        public Camera Camera {
            get { return mySprite.Camera; }
            set { mySprite.Camera = value; }
        }

        private Texture myTexture;
        public Texture MyTexture {
            get { return myTexture; }
            internal set { myTexture = value; }
        }
        private Sprite mySprite;
        public Sprite Sprite {
            get { return mySprite; }
        }
        private DrawLayer layer;
        public DrawLayer Layer {
            get { return layer; }
        }
        private Vector2 pivot;
        public Vector2 Pivot {
            get { return pivot; }
            set {
                pivot = value;
                mySprite.pivot = new Vector2(mySprite.Width * pivot.X, mySprite.Height * pivot.Y);
            }
        }

        private Vector2 textureOffset;
        public Vector2 TextureOffset {
            get { return textureOffset; }
            set { textureOffset = value; }
        }
        private float height;
        public float Height {
            get { return height * transform.Scale.Y; }
        }
        private float width;
        public float Width {
            get { return width * transform.Scale.X; }
            set { width = value; }
        }

        public SpriteRenderer (GameObject owner) :base (owner) {
            DrawMgr.AddItem(this);
        }

        public SpriteRenderer(GameObject gameObject, string textureName,
            Vector2 pivot, DrawLayer layer) : base(gameObject) {
            textureOffset = Vector2.Zero;
            myTexture = GfxMgr.GetTexture(textureName);
            this.width = Game.PixelsToUnit(myTexture.Width);
            this.height = Game.PixelsToUnit(myTexture.Height);
            mySprite = new Sprite(this.width, this.height);
            Pivot = pivot;
            this.layer = layer;
            DrawMgr.AddItem(this);
        }

        public SpriteRenderer (GameObject gameObject, string textureName, Vector2 pivot, float width, float height,
            DrawLayer layer = DrawLayer.Background) : base (gameObject) {
            myTexture = GfxMgr.GetTexture(textureName);
            textureOffset = Vector2.Zero;
            this.width = Game.PixelsToUnit(width);
            this.height = Game.PixelsToUnit(height);
            mySprite = new Sprite(this.width, this.height);
            this.layer = layer;
            Pivot = pivot;
            DrawMgr.AddItem(this);
        }

        public void Draw () {
            mySprite.position = transform.Position;
            mySprite.scale = transform.Scale;
            mySprite.EulerRotation = transform.Rotation;
            mySprite.DrawTexture(myTexture, (int) textureOffset.X, (int) textureOffset.Y, 
                (int) Game.UnitToPixel(width), (int) Game.UnitToPixel(height));
        }

        public static SpriteRenderer Factory (GameObject owner, string textureName, Vector2 pivot, DrawLayer layer) {
            return new SpriteRenderer(owner, textureName, pivot, layer);
        }

        public static SpriteRenderer Factory (GameObject owner, string textureName, Vector2 pivot, DrawLayer layer,
            Vector2 textureOffset, float width, float height) {
            SpriteRenderer sr = new SpriteRenderer(owner, textureName, pivot, width, height, layer);
            sr.TextureOffset = textureOffset;
            return sr;
        }

        public override Component Clone(GameObject owner) {
            SpriteRenderer clone = new SpriteRenderer(owner);
            clone.myTexture = myTexture;
            clone.mySprite = new Sprite(mySprite.Width, mySprite.Height);
            clone.width = width;
            clone.height = height;
            clone.Pivot = Pivot;
            clone.textureOffset = textureOffset;
            clone.Camera = Camera;
            return clone;
        }

    }
}
