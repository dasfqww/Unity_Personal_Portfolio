using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMng
{
    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }

    public void LoadScene(Type.Scene type)
    {
        CurrentScene.Clear();
        SceneManager.LoadScene(GetSceneName(type));        
    }

    string GetSceneName(Type.Scene type)
    {
        string name = System.Enum.GetName(typeof(Type.Scene), type);
        return name;
    }

    public string GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
}
