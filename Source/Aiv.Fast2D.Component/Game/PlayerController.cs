using OpenTK;
using System.Collections.Generic;

namespace Aiv.Fast2D.Component{
    public class PlayerController : UserComponent {

        private float speed;

        private List<ArenaGrid.Cell> path;
        private Vector2 SolC;
        private Rigidbody rigidbody;
        private ArenaGrid grid;


        public PlayerController (GameObject owner, float speed, ArenaGrid grid) : base (owner) {
            this.speed = speed;
            this.grid = grid;

        }

        public override void Awake() {
            rigidbody = GetComponent<Rigidbody>();
            path = new List<ArenaGrid.Cell>();
        }

        public override void Update() 
        {
            
                if (Input.GetMouseButtonDown(MouseButton.LeftMouse)
                && Game.Win.HasFocus)
                {
                SolC = new Vector2();
                SolC.X = MathHelper.Clamp(Game.Win.MousePosition.X + CameraMgr.MainCamera.position.X -CameraMgr.MainCamera.pivot.X, 0, float.MaxValue);
                SolC.Y = MathHelper.Clamp(Game.Win.MousePosition.Y + CameraMgr.MainCamera.position.Y - CameraMgr.MainCamera.pivot.Y, 0, float.MaxValue);
                path = grid.FindPath(grid.GetCellAt(transform.Position ), grid.GetCellAt(SolC)) ;

            }

            if (path.Count > 1)
            {
                Vector2 distCell = grid.GetCellCenter(path[1]) - transform.Position;
                if (distCell.LengthSquared < 0.01f)
                {
                    path.RemoveAt(0);
                }
                rigidbody.Velocity = distCell.Normalized() * speed;
            }
            else if (path.Count == 1)
            {
                rigidbody.Velocity = (SolC
                    - transform.Position).Normalized() * speed;
                path.RemoveAt(0);
            }
            else
            {
                rigidbody.Velocity = Vector2.Zero;
            }
        }


    }
}
