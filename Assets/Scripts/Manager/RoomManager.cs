using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Nettention.Proud;

public class RoomManager : Singletone<RoomManager>
{
    public Button btnStart;
    public Button btnReady;
    public Button btnLeave;

    private void Start()
    {
        btnLeave.onClick.AddListener(() => { User.proxy.ExitRoom(HostID.HostID_Server, RmiContext.ReliableSend, K.clientInfo.ID, K.clientInfo.roomID.ToString()); });
    }

    public void ExitRoom(string roomId)
    {
        var room = K.rooms.Find(x => x.id == System.Convert.ToInt32(roomId));
        var user = K.users.Find(x => x.ID == K.clientInfo.ID);
        room.clients.Remove(user);
        K.clientInfo.roomID = 0;
        K.SceneMove("Lobby");
    }
}
