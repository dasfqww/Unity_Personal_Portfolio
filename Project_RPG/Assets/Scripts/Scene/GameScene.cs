using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
   
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();

        SceneType = Type.Scene.Game2;
    }

    public override void Clear()
    {
        throw new System.NotImplementedException();
    }

}
