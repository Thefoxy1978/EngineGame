using OpenTK;

namespace Aiv.Fast2D.Component.UI {
    public class Font {

        protected int numCol;
        protected int firstVal; // in ASCII
        public Texture Texture { get; protected set; }
        public string TextureName { get; protected set; }
        public int CharacterWidth { get; protected set; }
        public int CharacterHeight { get; protected set; }


        public Font (string textureName, string texturePath, int numCol, int firstCharacterASCIIValue, int charWidht,
            int charHeight) {
            TextureName = textureName;
            Texture = GfxMgr.AddTexture(textureName, texturePath);
            firstVal = firstCharacterASCIIValue;
            CharacterWidth = charWidht;
            CharacterHeight = charHeight;
            this.numCol = numCol;
        }

        public virtual Vector2 GetOffset (char c) {
            int cVal = c; //trovo il suo codice ascii
            int delta = cVal - firstVal;
            int x = delta % numCol; //così trovo la colonna che appartiene a c, ed è la coordinata x
            int y = delta / numCol; //così trovo la riga
            return new Vector2(x * CharacterWidth, y * CharacterHeight);
        }
    }
}
