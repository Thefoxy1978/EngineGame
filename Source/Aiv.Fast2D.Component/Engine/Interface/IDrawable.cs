namespace Aiv.Fast2D.Component {
    interface IDrawable {

        bool Enabled { get; }
        DrawLayer Layer { get; }
        void Draw();

    }
}
