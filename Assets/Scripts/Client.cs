using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nettention.Proud;
using System;
using DG.Tweening;

public struct ClientInfo
{
    public string ID, PW;
    public HostID hostID;
    public int roomNum;
}

public class Client : MonoBehaviour
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
        stub.GameReadyReflection = OnGameReadyReflection;
        stub.GameStartReflection = OnGameStartReflection;
        stub.PositionReflection = OnPositionReflection;
        stub.GetRoomDatas = OnGetRoomDatas;

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
            K.clientInfo = new ClientInfo();
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
                K.SceneMove("Lobby");
            };
        }
        else
        {
            print("Fail");
            K.clientInfo = new ClientInfo();
            TitleManager.Instance.rtrnBtnsParent.DOAnchorPosY(-100, 0.5f);
        }
        return true;
    }

    private bool OnEchoToAll(HostID remote, RmiContext rmiContext, string id, string chat)
    {
        K.chat.EchoChat(chat);
        return true;
    }

    private bool OnEchoToRoom(HostID remote, RmiContext rmiContext, string id, string roomId, string chat)
    {
        return true;
    }

    private bool OnEchoPerson(HostID remote, RmiContext rmiContext, string id, string targetId, string chat)
    {
        return true;
    }

    private bool OnCreateRoomResult(HostID remote, RmiContext rmiContext, string id, string roomName, bool isSuccess)
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

    private bool OnPositionReflection(HostID remote, RmiContext rmiContext, string id, float x, float y, float z)
    {
        return true;
    }

    private bool OnGetRoomDatas(HostID remote, RmiContext rmiContext, string json)
    {
        throw new NotImplementedException();
    }

    private void OnJoinServer(ErrorInfo info, ByteArray replyFromServer)
    {
        print("JOIN");
        K.clientInfo = new ClientInfo();
        return;
    }

    private void SetParam()
    {
        param = new NetConnectionParam();
        param.protocolVersion = new Nettention.Proud.Guid("{2256FFC1-99F9-48DA-8A27-E18D61954A00}");
        param.serverIP = "127.0.0.1";
        //param.serverIP = "192.168.30.25";
        param.serverPort = 6475;
    }

    public bool Connect() => netClient.Connect(param);

    private void OnApplicationQuit()
    {
        netClient.Disconnect();
    }
}
