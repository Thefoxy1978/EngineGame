
using OpenTK;
using System;
using System.Collections.Generic;

namespace Aiv.Fast2D.Component {
    public class Enemy : UserComponent {

        private const int numberOfPlayers = 2;

        private PlayerController[] players;

        private PlayerController bestPlayer;
        public PlayerController BestPlayer {
            get { return bestPlayer; }
        }

        private HealthModule healthModule;

        public Enemy (GameObject owner) : base (owner) {

        }

        public override void Awake() {
            healthModule = GetComponent<HealthModule>();
            players = new PlayerController[numberOfPlayers];
            for (int i = 0; i < players.Length; i++) {
                players[i] = GameObject.Find("Player_" + (i + 1)).GetComponent<PlayerController>();
            }
        }

        public void TakeDamage (float damage) {
            healthModule.TakeDamage(damage);
        }

        private PlayerController[] GetVisiblePlayer (float detectDistance, float detectAngle) {
            List<PlayerController> visiblePlayer = new List<PlayerController>();

            foreach (PlayerController player in players) {
                Vector2 distToPlayer = player.transform.Position - transform.Position;
                if (distToPlayer.LengthSquared > detectDistance) continue;
                float angle = Transform.RadiantsToDegrees((float)Math.Acos(Vector2.Dot(distToPlayer.Normalized(), transform.Forward)));
                if (angle > detectAngle * 0.5f) continue;
                visiblePlayer.Add(player);
            }

            return visiblePlayer.ToArray();
        }

        public PlayerController GetBestPlayer (float detectDistance, float detectAngle) {
            PlayerController[] visiblePlayers = GetVisiblePlayer(detectDistance, detectAngle);
            bestPlayer = null;
            if (visiblePlayers.Length > 0) bestPlayer = visiblePlayers[0]; //qui dovrete intervenire nel progetto a casa.
            return bestPlayer;
        }

    }
}
