using Aiv.Fast2D.Component.UI;
using OpenTK;

namespace Aiv.Fast2D.Component
{
    public class Start_Scene : Scene
    {

        protected override void LoadAssets()
        {
            FontMgr.AddFont("stdFont", "Assets/Fonts/textSheet.png", 15, 32, 20, 20);
        }
        public override void InitializeScene()
        {
            base.InitializeScene();
            GameObject titleText = new GameObject("TitleTest", new Vector2(Game.Win.OrthoWidth * 0.80f - Game.PixelsToUnit(FontMgr.GetFont("stdFont").CharacterWidth) * 3 * 3, 1));
            titleText.AddComponent<TextBox>(FontMgr.GetFont("stdFont"), 6, Vector2.One).SetText("Alive");
            GameObject feedbackText = new GameObject("FeedbackText", new Vector2(Game.Win.OrthoWidth * 0.59f - Game.PixelsToUnit(FontMgr.GetFont("stdFont").CharacterWidth) * 3 * 4, Game.Win.OrthoHeight * 0.5f));
            feedbackText.AddComponent<TextBox>(FontMgr.GetFont("stdFont"), 20, Vector2.One ).SetText("Press Enter To Start");
            GameObject menuController = new GameObject("MenuController", Vector2.Zero);
            menuController.AddComponent<MenuController>();

        }


    }
}
