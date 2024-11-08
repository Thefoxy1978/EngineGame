using OpenTK;
using System.Collections.Generic;

namespace Aiv.Fast2D.Component {
    public class Animator : UserComponent {

        //Class fields
        private SpriteRenderer spriteRenderer;
        private List<SheetClip> myClip;
        private SheetClip currentClip;

        //Support fields
        private float sliceTime;
        private float currentSliceTime;
        private int currentIndexFrame;

        public Animator (GameObject owner, SpriteRenderer spriteRenderer) : base (owner) {
            this.spriteRenderer = spriteRenderer;
            myClip = new List<SheetClip>();
        }

        public void AddClip (SheetClip clip) {
            myClip.Add(clip);
        }

        public override void Awake() {
            ChangeClip(myClip[0].AnimationName);
        }

        public void ChangeClip (string name) {
            for (int i = 0; i < myClip.Count; i++) {
                if (myClip[i].AnimationName != name) continue;
                currentClip = myClip[i];
                sliceTime = 1f / currentClip.FPS;
                currentSliceTime = sliceTime;
                currentIndexFrame = 0;
                spriteRenderer.MyTexture = currentClip.Texture;
                SetNewFrame(currentClip.Frames[0]);
                break;
            }
        }


        public override void LateUpdate() {
            currentSliceTime -= Game.DeltaTime;
            if (currentSliceTime <= 0) {
                currentSliceTime = sliceTime;
                currentIndexFrame++;
                if (currentIndexFrame < currentClip.Frames.Length) {
                    SetNewFrame(currentClip.Frames[currentIndexFrame]);
                } else {
                    if (currentClip.Loop) {
                        currentIndexFrame = currentIndexFrame % currentClip.Frames.Length;
                    } else {
                        if (!string.IsNullOrEmpty (currentClip.NextAnimation)) {
                            ChangeClip(currentClip.NextAnimation);
                        }
                    }
                }
            }
        }

        public void SetNewFrame (int index) {
            int rowIndex = index / currentClip.NumberOfColumn;
            int columnIndex = index % currentClip.NumberOfColumn;
            spriteRenderer.TextureOffset = new Vector2(currentClip.FrameWidth * columnIndex,
                currentClip.FrameHeight * rowIndex);
        }


        public string GetCurrentAnimationName () {
            if (currentClip == null) return string.Empty;
            return currentClip.AnimationName;
        }

        public override Component Clone(GameObject owner) {
            Animator clone = new Animator(owner, owner.GetComponent<SpriteRenderer>());
            foreach (SheetClip clip in myClip) {
                clone.myClip.Add(clip);
            }
            return clone;
        }

    }

}
