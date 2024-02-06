using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Manager
{
    public class ResourceManager
    {
        public T Load<T>(string path)where T:Object
        {
            if (typeof(T)==typeof(GameObject))
            {
                string name = path;
                int idx = name.LastIndexOf('/');
                if (idx >= 0)
                    name = name.Substring(idx + 1);

                GameObject go = Managers.Pool.GetOriginal(name);
                if (go)
                    return go as T;
            }

            return Resources.Load<T>(path);
        }

        public GameObject Instantiate(string path, Transform parent=null)
        {
            GameObject original = Load<GameObject>($"Game/{path}");
            if (!original)
            {
                Debug.Log($"Fail to load prefab: {path}");
                return null;
            }

            if (original.GetComponent<Poolable>())
                return Managers.Pool.Pop(original, parent).gameObject;

            GameObject go = Object.Instantiate(original, parent);
            go.name = original.name;

            return go;
        }

        public void Destroy(GameObject go)
        {
            if (!go)
                return;

            Poolable poolable = go.GetComponent<Poolable>();
            if (poolable)
            {
                Managers.Pool.Push(poolable);
                return;
            }

            Object.Destroy(go);
        }
    }

}