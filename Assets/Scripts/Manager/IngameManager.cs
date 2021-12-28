using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nettention.Proud;

public class IngameManager : Singletone<IngameManager>
{
    public Player player1;
    public Dictionary<string, Player> playerPairs;

    void Start()
    {
        playerPairs = new Dictionary<string, Player>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //Client.proxy.RecordPosition(HostID.HostID_Server, RmiContext.ReliableSend, K.clientInfo.ID, player1.transform.position.x, player1.transform.position.y, player1.transform.position.z);
    }
}
