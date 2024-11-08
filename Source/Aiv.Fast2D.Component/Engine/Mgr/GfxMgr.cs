using System.Collections.Generic;

namespace Aiv.Fast2D.Component {
    static class GfxMgr {

        private static Dictionary<string, Texture> textures;

        static GfxMgr () {
            textures = new Dictionary<string, Texture>();
        }

        public static Texture AddTexture (string name, string texturePath ) {
            Texture temp = new Texture(texturePath);
            textures.Add(name, temp);
            return temp;
        }

        public static Texture GetTexture (string name) {
            if (!textures.ContainsKey(name)) return null;
            return textures[name];
        }

        public static void ClearAll () {
            textures.Clear();
        }

    }
}
