using System.Collections.Generic;

namespace Aiv.Fast2D.Component {

    public enum DrawLayer { Background, Middleground, Playground, Foreground, GUI, Last}

    static class DrawMgr {

        private static List<IDrawable>[] items;

        static DrawMgr() {
            items = new List<IDrawable>[(int) DrawLayer.Last];

            for (int i = 0; i< items.Length; i++) {
                items[i] = new List<IDrawable>();
            }
        }

        public static void AddItem (IDrawable item) {
            if (items[(uint)item.Layer].Contains(item)) return;
            items[(uint)item.Layer].Add(item);
        }

        public static void RemoveItem (IDrawable item) {
            if (!items[(uint)item.Layer].Contains(item)) return;
            items[(uint)item.Layer].Remove(item);
        }

        public static void ClearAll () {
            for (int i = 0; i < items.Length; i++) {
                items[i].Clear();
            }
        }

        public static void Draw () {
            for (int i = 0; i < items.Length; i++) {
                for (int j = 0; j< items[i].Count; j++) {
                    if (!items[i][j].Enabled) continue;
                    items[i][j].Draw();
                }
            }
        }

    }
}
