

namespace Aiv.Fast2D.Component
{
    public class MenuController : UserComponent
    {

        public MenuController(GameObject owner) : base(owner)
        {

        }

        public override void Update()
        {
            if (Input.GetButtonDown("UI_Confirm"))
            {
                Game.TriggerChangeScene(new Map1_Island());
            }
        }

    }
}
