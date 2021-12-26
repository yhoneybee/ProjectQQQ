using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : Singletone<LobbyManager>
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            K.SceneMove("Ingame");
        }
    }
}
