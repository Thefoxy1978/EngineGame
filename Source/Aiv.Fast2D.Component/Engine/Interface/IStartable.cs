namespace Aiv.Fast2D.Component {
    interface IStartable {

        bool Enabled { get; }
        void Start();
        void Awake();
        void OnEnable();
        void OnDisable();
        void OnDestroy();

    }
}
