using Nettention.Proud;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LobbyManager : Singletone<LobbyManager>
{
    public Button btnCreate;
    public TMP_InputField inputPW;
    public TMP_InputField inputName;
    public RoomView originRoomViewer;
    public RectTransform roomViewersParent;

    private void Start()
    {
        btnCreate.onClick.AddListener(() => { User.proxy.CreateRoom(HostID.HostID_Server, RmiContext.ReliableSend, K.clientInfo.ID, inputName.text, inputPW.text); });

        User.proxy.GetRoomDatas(HostID.HostID_Server, RmiContext.ReliableSend, K.clientInfo.ID);
        User.proxy.GetClientDatas(HostID.HostID_Server, RmiContext.ReliableSend, K.clientInfo.ID);
        foreach (var room in K.rooms)
        {
            var obj = Instantiate(originRoomViewer, roomViewersParent);
            obj.roomId = room.id;
        }
    }

    private void FixedUpdate()
    {
        User.proxy.GetRoomDatas(HostID.HostID_Server, RmiContext.ReliableSend, K.clientInfo.ID);
        User.proxy.GetClientDatas(HostID.HostID_Server, RmiContext.ReliableSend, K.clientInfo.ID);
    }

    public void CreateRoom(string roomId)
    {
        var room = new Room(inputName.text, inputPW.text);
        room.id = System.Convert.ToInt32(roomId);
        K.rooms.Add(room);
        EnterRoom(roomId);
    }

    public void EnterRoom(string roomId)
    {
        var room = K.rooms.Find(x => x.id == System.Convert.ToInt32(roomId));
        var user = K.users.Find(x => x.ID == K.clientInfo.ID);
        room.clients.Add(user);
        // TODO : LobbyManager에서 방으로 옮겨줘야함
    }
}
