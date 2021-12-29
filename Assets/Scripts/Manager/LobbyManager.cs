using Nettention.Proud;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : Singletone<LobbyManager>
{
    private void Start()
    {
        Client.proxy.GetRoomDatas(HostID.HostID_Server, RmiContext.ReliableSend, K.clientInfo.ID);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            K.SceneMove("Ingame");
        }
    }
}
