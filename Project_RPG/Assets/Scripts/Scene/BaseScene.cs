using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{
   

    public Type.Scene SceneType { get; protected set; } = Type.Scene.Unknown;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    protected virtual void Init()
    {

    }

    public abstract void Clear();
}
