using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Nettention.Proud;

public class RoomView : MonoBehaviour
{
    public Button btn;
    public TextMeshProUGUI tmpInfo;
    public int roomId;
    
    private void Start()
    {
        btn.onClick.AddListener(() => { User.proxy.EnterRoom(HostID.HostID_Server, RmiContext.ReliableSend, K.clientInfo.ID, roomId.ToString(), LobbyManager.Instance.inputPW.text); });
    }

    private void FixedUpdate()
    {
        var room = K.rooms.Find(x => x.id == roomId);
        tmpInfo.text = $" {room.name} ( {room.id} )\n ( {room.clients.Count} / 20 ) {(room.isPlaying ? "Playing" : "Waiting")}";
        if (K.rooms.Find(x => x.id == room.id) == null) Destroy(gameObject);
    }
}
