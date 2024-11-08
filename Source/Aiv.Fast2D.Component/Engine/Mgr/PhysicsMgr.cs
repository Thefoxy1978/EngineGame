using System.Collections.Generic;

namespace Aiv.Fast2D.Component{
    static class PhysicsMgr {


        static Collision collisionInfo;

        static List<Rigidbody> items;
        
        static PhysicsMgr () {
            items = new List<Rigidbody>();
        }

        public static void AddItem(Rigidbody item) {
            items.Add(item);
        }

        public static void RemoveItem (Rigidbody item) {
            items.Remove(item);
        }

        public static void ClearAll () {
            items.Clear();
        }

        public static void FixedUpdate () {
            for (int i = 0; i < items.Count; i++) {
                if (!items[i].Enabled) continue;
                items[i].FixedUpdate();
            }
        }

        public static void CheckCollisions () {
            for (int i = 0; i < items.Count - 1; i++) {
                if (!(items[i].IsCollisionAffected && items[i].Enabled)) continue;
                for (int j = i + 1; j < items.Count; j++) {
                    if (!(items[j].IsCollisionAffected && items[j].Enabled)) continue;
                    bool firstCheck = items[i].CollisionMatches(items[j].Type);
                    bool secondCheck = items[j].CollisionMatches(items[i].Type);
                    if (!firstCheck && !secondCheck) continue;
                    if (!items[i].Collides(items[j], ref collisionInfo)) continue;
                    if (firstCheck) {
                        collisionInfo.Collider = items[j].Collider;
                        items[i].gameObject.OnCollide(collisionInfo);
                    }
                    if (secondCheck) {
                        collisionInfo.Collider = items[i].Collider;
                        items[j].gameObject.OnCollide(collisionInfo);
                    }
                }
            }
        }

    }
}
