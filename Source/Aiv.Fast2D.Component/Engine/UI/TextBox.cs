using OpenTK;
using System;

namespace Aiv.Fast2D.Component.UI {
    public class TextBox : Component, IDrawable {
        public DrawLayer Layer => DrawLayer.GUI;

        private Font myFont;
        private Sprite sprite;
        private string currentText;
        private Vector2[] availableCharacters_offset;
        private Vector2[] availableCharacters_position;

        public int MaxCharacters {
            get { return availableCharacters_position.Length; }
        }

        public Camera Camera {
            get { return sprite.Camera; }
            set { sprite.Camera = value; }
        }

        public TextBox (GameObject owner, Font font, int maxCharacters, Vector2 fontScale) : base (owner) {
            currentText = string.Empty;
            myFont = font;
            availableCharacters_position = new Vector2[maxCharacters];
            availableCharacters_offset = new Vector2[maxCharacters];
            sprite = new Sprite(Game.PixelsToUnit(myFont.CharacterWidth * fontScale.X), 
                Game.PixelsToUnit (myFont.CharacterHeight * fontScale.Y));
            DrawMgr.AddItem(this);
        }

        public void SetText (string text) {
            if (currentText == text) return;
            currentText = text;
            float xPos = transform.Position.X;
            float yPos = transform.Position.Y;
            int maxIndex = GetMax();
            int xIndex = 0;
            for (int i = 0; i < maxIndex; i++) {
                if (currentText[i].Equals ('\n')) {
                    yPos += sprite.Height;
                    xIndex = 0;
                    continue;
                }
                availableCharacters_offset[i] = myFont.GetOffset(currentText[i]);
                availableCharacters_position[i].Y = yPos;
                availableCharacters_position[i].X = xPos + xIndex * sprite.Width;
                xIndex++;
            }
        }

        public void Draw() {
            int maxIndex = GetMax();
            for (int i = 0; i < maxIndex; i++) {
                sprite.position = availableCharacters_position[i];
                sprite.DrawTexture(myFont.Texture, (int)availableCharacters_offset[i].X,
                    (int)availableCharacters_offset[i].Y, myFont.CharacterWidth, myFont.CharacterHeight);
            }
        }
        
        public int GetMax () {
            return currentText.Length < MaxCharacters ? currentText.Length : MaxCharacters;
        }
    }
}
