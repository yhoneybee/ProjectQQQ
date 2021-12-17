using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nettention.Proud;
using System;

public class Client : MonoBehaviour
{
    public NetClient netClient;
    public C2S.Proxy proxy;
    public S2C.Stub stub;
    private NetConnectionParam param;

    private void Awake()
    {
        K.client = this;
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

        netClient.JoinServerCompleteHandler = OnJoinServer;

        netClient.AttachProxy(proxy);
        netClient.AttachStub(stub);

        Connect();
    }

    private bool OnSignUpResult(HostID remote, RmiContext rmiContext, string id, bool isSuccess)
    {
        return false;
    }

    private bool OnLoginResult(HostID remote, RmiContext rmiContext, string id, bool isSuccess)
    {
        return false;
    }

    private bool OnEchoToAll(HostID remote, RmiContext rmiContext, string id, string chat)
    {
        return false;
    }

    private bool OnEchoToRoom(HostID remote, RmiContext rmiContext, string id, string roomId, string chat)
    {
        return false;
    }

    private bool OnEchoPerson(HostID remote, RmiContext rmiContext, string id, string targetId, string chat)
    {
        return false;
    }

    private bool OnCreateRoomResult(HostID remote, RmiContext rmiContext, string id, string roomName, bool isSuccess)
    {
        return false;
    }

    private bool OnGameReadyReflection(HostID remote, RmiContext rmiContext, string id, string roomName, bool isReady)
    {
        return false;
    }

    private bool OnGameStartReflection(HostID remote, RmiContext rmiContext, string id, string roomName, bool isSuccess)
    {
        return false;
    }

    private bool OnPositionReflection(HostID remote, RmiContext rmiContext, string id, string roomName, float x, float y, float z)
    {
        return false;
    }

    private void OnJoinServer(ErrorInfo info, ByteArray replyFromServer)
    {
        return;
    }

    private void SetParam()
    {
        param = new NetConnectionParam();
        param.protocolVersion = new Nettention.Proud.Guid("{2256FFC1-99F9-48DA-8A27-E18D61954A00}");
        param.serverIP = "127.0.0.1";
        param.serverPort = 6475;
    }

    public void Connect() => netClient.Connect(param);

    private void OnApplicationQuit()
    {
        netClient.Disconnect();
    }
}
