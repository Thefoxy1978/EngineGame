using Aiv.Fast2D.Component.UI;
using System;
using System.Collections.Generic;

namespace Aiv.Fast2D.Component {
    public abstract class Scene {

        protected List<GameObject> sceneObjects = new List<GameObject>();
        protected bool isInitialize;
        public bool IsInitialize {
            get { return isInitialize; }
            set { isInitialize = value; }
        }

        public virtual void InitializeScene () {
            LoadAssets();
        }

        public void Awake () {
            foreach (GameObject gameObject in sceneObjects) {
                gameObject.Awake();
            }
        }

        public void Start () {
            foreach (GameObject gameObject in sceneObjects) {
                if (!gameObject.IsActive) continue;
                gameObject.Start();
            }
        }

        public void Update () {
            foreach (GameObject gameObject in sceneObjects) {
                if (!gameObject.IsActive) continue;
                gameObject.Update();
            }
        }

        public void LateUpdate() {
            foreach (GameObject gameObject in sceneObjects) {
                if (!gameObject.IsActive) continue;
                gameObject.LateUpdate();
            }
        }

        public GameObject FindGameObject (string name) {
            foreach (GameObject obj in sceneObjects) {
                if (obj.Name == name) return obj;
            }
            return null; //non esiste neanche un gameobject nella scena che si chiama così
        }

        public GameObject[] FindGameObjectsWithName (string name) {
            List<GameObject> list = new List<GameObject>();
            foreach (GameObject obj in sceneObjects) {
                if (obj.Name == name) list.Add(obj);
            }
            return list.ToArray();
        }

        public void RegisterGameObject (GameObject obj) {
            sceneObjects.Add(obj);
        }

        protected virtual void LoadAssets () {

        }

        public virtual void DestroyScene () {
            foreach (GameObject go in sceneObjects) {
                go.OnDestroy();
            }
            sceneObjects.Clear();
            PhysicsMgr.ClearAll();
            DrawMgr.ClearAll();
            GfxMgr.ClearAll();
            FontMgr.ClearAll();
            CameraMgr.ClearAll();
            AudioManager.ClearAll();
            GC.Collect();
        }

    }
}
