using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Manager;

public class LobbyScene:BaseScene
{

    protected override void Init()
    {
        base.Init();

        SceneType = Type.Scene.Lobby;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Managers.Scene.LoadScene(Type.Scene.Game2);
        }
    }

    public override void Clear()
    {
        Debug.Log("lobby Clear..");
    }
}
