

namespace Aiv.Fast2D.Component
{
   public class Check_Teleport : UserComponent
    {
        int Teleport_ID;
      public  Check_Teleport(GameObject owner, int Teleport_ID) : base(owner)
        {
            this.Teleport_ID = Teleport_ID;
        }
        public override void OnCollide(Collision collisionInfo)
        {
            EventHandlerManager.CastEvent((int)MyEvents.ChangeScene,new SceneUpdateArgs(Teleport_ID));
        }
    }
}
