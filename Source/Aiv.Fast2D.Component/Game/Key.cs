
namespace Aiv.Fast2D.Component
{
    class Key : UserComponent
    {
        private SpriteRenderer key;
        public Key (GameObject owner) : base(owner)
        {
           
        }

        public override void Awake()
        {
            key = GetComponent<SpriteRenderer>();
            gameObject.IsActive = !PickKey.AssKey;
        }

        public override void OnCollide(Collision collisionInfo)
        {
            key.Enabled = false;
            PickKey.AssKey = true;

        }

    }
}
