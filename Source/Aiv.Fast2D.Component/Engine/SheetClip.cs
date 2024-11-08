using OpenTK;

namespace Aiv.Fast2D.Component {
    public class SheetClip {

        public Sheet Sheet {
            get;
            private set;
        }

        public string AnimationName {
            get;
            private set;
        }

        public int[] Frames {
            get;
            private set;
        }

        public bool Loop {
            get;
            private set;
        }

        public string NextAnimation {
            get;
            private set;
        }

        public int FPS {
            get;
            private set;
        }

        public Texture Texture {
            get { return Sheet.Texture; }
        }

        public int NumberOfRow {
            get { return Sheet.NumberOfRow; }
        }

        public int NumberOfColumn {
            get { return Sheet.NumberOfColumn; }
        }

        public float FrameWidth {
            get { return Sheet.FrameWidth; }
        }
        public float FrameHeight {
            get { return Sheet.FrameHeight; }
        }

        public SheetClip (Sheet sheet, string animationName, int[] frames, bool loop
            , int fps, string nextAnimation = "") {
            Sheet = sheet;
            AnimationName = animationName;
            Frames = new int[frames.Length];
            for (int i = 0; i < frames.Length; i++) {
                Frames[i] = frames[i];
            }
            Loop = loop;
            FPS = fps;
            NextAnimation = nextAnimation;
        }

    }
}
