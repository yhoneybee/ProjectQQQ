using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nettention.Proud;
using System;
using DG.Tweening;
using Newtonsoft.Json;
using System.Linq;

public struct UserInfo
{
    public float pX, pY, pZ;
    public float rX, rY, rZ;
    public bool isReady;
    public bool isHost;
    public string ID, PW;
    public HostID hostID;
    public int roomID;
}

public class User : MonoBehaviour
{
    public static NetClient netClient;
    public static C2S.Proxy proxy;
    public static S2C.Stub stub;
    private NetConnectionParam param;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SetParam();
        netClient = new NetClient();
        proxy = new C2S.Proxy();
        stub = new S2C.Stub();

        stub.SignUpResult = OnSignUpResult;
        stub.LoginResult = OnLoginResult;
        stub.EchoToAll = OnEchoToAll;
        stub.EchoToRoom = OnEchoToRoom;
        stub.EchoToPerson = OnEchoPerson;
        stub.CreateRoomResult = OnCreateRoomResult;
        stub.EnterRoomResult = OnEnterRoomResult;
        stub.ExitRoomResult = OnExitRoomResult;
        stub.GameReadyReflection = OnGameReadyReflection;
        stub.GameStartReflection = OnGameStartReflection;
        stub.GetRoomDatas = OnGetRoomDatas;
        stub.GetClientDatas = OnGetClientDatas;
        stub.NotifyPosition = OnNotifyPosition;
        stub.NotifyRotation = OnNotifyRotation;

        netClient.JoinServerCompleteHandler = OnJoinServer;

        netClient.AttachProxy(proxy);
        netClient.AttachStub(stub);

        Connect();
    }

    private void Update()
    {
        netClient.FrameMove();
    }

    private bool OnSignUpResult(HostID remote, RmiContext rmiContext, string id, bool isSuccess)
    {
        if (isSuccess)
        {
            print("Success");
            K.popup.PopupWindow("", "Sign Successful", true);
            K.popup.onActiveChanged = (b) =>
            {
                if (!b)
                    TitleManager.Instance.SwitchLoginSign();
            };
        }
        else
        {
            print("Fail");
            K.clientInfo = new UserInfo();
        }
        return true;
    }

    private bool OnLoginResult(HostID remote, RmiContext rmiContext, string id, bool isSuccess)
    {
        if (isSuccess)
        {
            print("Success");
            K.popup.PopupWindow("", "Login Successful", true);
            K.popup.onActiveChanged = (b) =>
            {
                proxy.GetRoomDatas(HostID.HostID_Server, RmiContext.ReliableSend, K.clientInfo.ID);
                K.SceneMove("Lobby");
            };
        }
        else
        {
            print("Fail");
            K.clientInfo = new UserInfo();
            TitleManager.Instance.rtrnBtnsParent.DOAnchorPosY(-100, 0.5f);
        }
        return true;
    }

    private bool OnEchoToAll(HostID remote, RmiContext rmiContext, string id, string chat)
    {
        K.chat.EchoChat(chat, Color.black);
        return true;
    }

    private bool OnEchoToRoom(HostID remote, RmiContext rmiContext, string id, string roomId, string chat)
    {
        K.chat.EchoChat(chat, Color.cyan);
        return true;
    }

    private bool OnEchoPerson(HostID remote, RmiContext rmiContext, string id, string targetId, string chat)
    {
        K.chat.EchoChat(chat, Color.green);
        return true;
    }

    private bool OnCreateRoomResult(HostID remote, RmiContext rmiContext, string id, string roomId, bool isSuccess)
    {
        if (isSuccess) LobbyManager.Instance.CreateRoom(roomId);
        return isSuccess;
    }

    private bool OnEnterRoomResult(HostID remote, RmiContext rmiContext, string id, string roomId, bool isSuccess)
    {
        if (isSuccess) LobbyManager.Instance.EnterRoom(roomId);
        return isSuccess;
    }

    private bool OnExitRoomResult(HostID remote, RmiContext rmiContext, string id, string roomId, bool isSuccess)
    {
        return true;
    }

    private bool OnGameReadyReflection(HostID remote, RmiContext rmiContext, string id)
    {
        return true;
    }

    private bool OnGameStartReflection(HostID remote, RmiContext rmiContext, string id)
    {
        return true;
    }

    private bool OnGetRoomDatas(HostID remote, RmiContext rmiContext, string json)
    {
        var users = K.rooms.Select(x => x.clients);
        K.rooms = JsonConvert.DeserializeObject<Serialization<Room>>(json).target;
        for (int i = 0; i < K.rooms.Count; i++)
        {
            K.rooms[i].clients = users.ElementAt(i);
        }
        return true;
    }

    private bool OnGetClientDatas(HostID remote, RmiContext rmiContext, string json)
    {
        K.users = JsonConvert.DeserializeObject<Serialization<UserInfo>>(json).target;
        return true;
    }

    private bool OnNotifyPosition(HostID remote, RmiContext rmiContext, string id, float x, float y, float z)
    {
        return true;
    }

    private bool OnNotifyRotation(HostID remote, RmiContext rmiContext, string id, float x, float y, float z)
    {
        return true;
    }

    private void OnJoinServer(ErrorInfo info, ByteArray replyFromServer)
    {
        print("JOIN");
        K.clientInfo = new UserInfo();
        return;
    }

    private void SetParam()
    {
        param = new NetConnectionParam();
        param.protocolVersion = new Nettention.Proud.Guid("{2256FFC1-99F9-48DA-8A27-E18D61954A00}");
        //param.serverIP = "127.0.0.1";
        //param.serverIP = "192.168.30.25";
        param.serverIP = "119.196.245.41";
        param.serverPort = 6475;
    }

    public bool Connect() => netClient.Connect(param);

    private void OnApplicationQuit()
    {
        netClient.Disconnect();
    }
}
