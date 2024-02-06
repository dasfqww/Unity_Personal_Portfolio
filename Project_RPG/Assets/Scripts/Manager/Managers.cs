using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Manager
{
    public class Managers : MonoBehaviour
    {
        static Managers s_Instance;
        static Managers Instance { get { return s_Instance; } }

        GameManager game = new GameManager();
        public static GameManager Game { get { return Instance.game; } }

        ResourceManager resource = new ResourceManager();
        SceneMng scene = new SceneMng();
        SoundManager sound = new SoundManager();
        PoolManager pool = new PoolManager();

        public static ResourceManager Resource { get { return Instance.resource; } }
        public static SceneMng Scene { get { return Instance.scene; } }
        public static SoundManager Sound { get { return Instance.sound; } }
        public static PoolManager Pool { get { return Instance.pool; } }

        // Start is called before the first frame update
        void Start()
        {
            Init();
        }

        private static void Init()
        {
            if (s_Instance == null)
            {
                GameObject go = GameObject.Find("@Managers");
                if (go == null)
                {
                    go = new GameObject { name = "@Managers" };
                    go.AddComponent<Managers>();
                }

                DontDestroyOnLoad(go);
                s_Instance = go.GetComponent<Managers>();

                s_Instance.sound.Init();
                s_Instance.pool.Init();
            }
        }

        public static void Clear()
        {
            Sound.Clear();
            Pool.Clear();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

