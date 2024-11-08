using System.Collections.Generic;

namespace Aiv.Fast2D.Component.UI {
    public static class FontMgr {

        private static Dictionary<string, Font> fonts;
        private static Font DefaultFont;

        static FontMgr () {
            fonts = new Dictionary<string, Font>();
        }

        public static Font AddFont (string fontName, string texturePath, int numCol, int firstCharacterASCIIValue,
            int charWidth, int charHeight) {
            Font f = new Font(fontName, texturePath, numCol, firstCharacterASCIIValue, charWidth, charHeight);
            fonts.Add(fontName, f);
            if (DefaultFont != null) return f;
            DefaultFont = f;
            return f;
        }

        public static Font GetFont (string fontName) {
            if (!fonts.ContainsKey(fontName)) return DefaultFont;
            return fonts[fontName];
        }

        public static void ClearAll () {
            fonts.Clear();
            DefaultFont = null;
        }

    }
}
