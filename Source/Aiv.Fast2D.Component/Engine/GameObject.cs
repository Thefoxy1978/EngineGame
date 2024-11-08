using System;
using System.Collections.Generic;
using OpenTK;

namespace Aiv.Fast2D.Component {
    public sealed class GameObject {

        public string Tag { get; set; }

        private string name;
        public string Name {
            get { return name; }
            set { name = value == string.Empty ? "GameObject" : name = value; }
        }
        private bool isActive;
        public bool IsActive {
            get { return isActive; }
            set {
                if (isActive == value) return;
                if (value) {
                    isActive = value;
                    OnEnable();
                    if (isStarted || !Game.CurrentScene.IsInitialize) return;
                    Start();
                } else {
                    OnDisable();
                    isActive = value;
                }
            }
        }
        public Transform transform {
            get;
            private set;
        }

        private List<Component> components;

        private bool isStarted;

        public GameObject (string name, Vector2 position) {
            Name = name;
            transform = new Transform(position, Vector2.One);
            components = new List<Component>();
            IsActive = true;
            EndConstructor();
        }

        public GameObject (string name, Vector2 position, Vector2 scale, float rotation = 0) {
            Name = name;
            transform = new Transform(position, scale, rotation);
            components = new List<Component>();
            IsActive = true;
            EndConstructor();
        }

        private void EndConstructor () {
            Game.CurrentScene.RegisterGameObject(this);
            if (Game.CurrentScene.IsInitialize) {
                Awake();
            }
        }

        public void AddComponent (Component component) {
            components.Add(component);
        }

        public void Awake () {
            foreach (Component component in components) {
                IStartable temp = component as IStartable;
                if (temp == null) continue;
                temp.Awake();
                if (!IsActive || !temp.Enabled) return;
                temp.OnEnable();
            }
        }

        public void OnEnable () {
            foreach (Component component in components) {
                if (!component.Enabled) continue;
                IStartable temp = component as IStartable;
                if (temp == null) continue; //non è un IStartable
                temp.OnEnable();
            }
        }

        public void OnDisable () {
            foreach (Component component in components) {
                if (!component.Enabled) continue;
                IStartable temp = component as IStartable;
                if (temp == null) continue; //non è un IStartable
                temp.OnDisable();
            }
        }

        public void Start () {
            isStarted = true;
            foreach (Component component in components) {
                if (!component.Enabled) continue;
                IStartable temp = component as IStartable;
                if (temp == null) continue; //non è un IStartable
                temp.Start();
            }
        }

        public void Update () {
            foreach (Component component in components) {
                if (!component.Enabled) continue;
                IUpdatable temp = component as IUpdatable;
                if (temp == null) continue; //non è un IStartable
                temp.Update();
            }
        }

        public void LateUpdate() {
            foreach (Component component in components) {
                if (!component.Enabled) continue;
                IUpdatable temp = component as IUpdatable;
                if (temp == null) continue; //non è un IStartable
                temp.LateUpdate();
            }
        }

        public void OnDestroy() {
            foreach (Component component in components) {
                IStartable temp = component as IStartable;
                if (temp == null) continue; //non è un IStartable
                temp.OnDestroy();
            }
        }

        public void OnCollide (Collision collisionInfo) {
            foreach (Component component in components) {
                if (!component.Enabled) continue;
                ICollidable temp = component as ICollidable;
                if (temp == null) continue; //non è un IStartable
                temp.OnCollide(collisionInfo);
            }
        }

        public Component GetComponent (Type type) {
            for (int i = 0; i < components.Count; i++) {
                if (type == components[i].GetType()) {
                    return components[i];
                }
            }
            Type currentType;
            foreach (Component component in components) {
                currentType = component.GetType().BaseType;
                while (currentType != typeof(object)) {
                    if (currentType == type) {
                        return component;
                    }
                    currentType = currentType.BaseType;
                }
            }
            return null;
        }

        public Component AddComponent (Type type, params object[] initialization) {
            object[] parameters = new object[initialization.Length + 1];
            parameters[0] = this;
            for (int i = 1; i < parameters.Length; i++) {
                parameters[i] = initialization[i - 1];
            }
            Component component = Activator.CreateInstance(type, parameters) as Component;
            components.Add(component);
            return component;
        }

        public T GetComponent<T> () where T : Component {
            foreach (Component component in components) {
                if (component is T) {
                    return (T)component;
                }
            }
            return null;
        }

        public T AddComponent<T>(params object[] initialization) where T : Component {
            object[] parameters = new object[initialization.Length + 1];
            parameters[0] = this;
            for (int i = 1; i < parameters.Length; i++) {
                parameters[i] = initialization[i - 1];
            }
            T component = (T)Activator.CreateInstance(typeof(T), parameters);
            components.Add(component);
            return component;
        }


        public static GameObject Find (string name) {
            return Game.CurrentScene.FindGameObject(name);
        }

        public static GameObject Clone (GameObject toClone) {
            GameObject clone = new GameObject(toClone.Name + "_Clone", toClone.transform.Position);
            foreach (Component componentToClone in toClone.components) {
                clone.AddComponent(componentToClone.Clone(clone));
            }
            return clone;
        }

    }
}
