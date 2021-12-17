using System;
using System.Collections.Generic;
using Nettention.Proud;
using System.Linq;

class Program
{
    public static NetServer netServer = new NetServer();
    public static S2C.Proxy proxy = new S2C.Proxy();
    public static C2S.Stub stub = new C2S.Stub();

    private static void Main(string[] _)
    {
        var param = new StartServerParameter();
        param.tcpPorts.Add(6475);
        param.udpPorts.Add(6475);
        param.protocolVersion = new Nettention.Proud.Guid("{2256FFC1-99F9-48DA-8A27-E18D61954A00}");

        stub.SignUp = OnSignUp;
        stub.LogIn = OnLogIn;
        stub.ChatToAll = OnChatToAll;
        stub.ChatToRoom = OnChatToRoom;
        stub.ChatToPerson = OnChatToPerson;
        stub.CreateRoom = OnCreateRoom;
        stub.GameReady = OnGameReady;
        stub.GameStart = OnGameStart;
        stub.RecordPosition = OnRecordPosition;

        netServer.ClientJoinHandler = OnJoinServer;
        netServer.ClientLeaveHandler = OnLeaveServer;

        netServer.AttachProxy(proxy);
        netServer.AttachStub(stub);
        netServer.Start(param);

        while (true)
        {

        }
    }

    private static bool OnSignUp(HostID remote, RmiContext rmiContext, string id, string nickName, string pw, string confirmPw)
    {
        return false;
    }

    private static bool OnLogIn(HostID remote, RmiContext rmiContext, string id, string pw)
    {
        return false;
    }

    private static bool OnChatToAll(HostID remote, RmiContext rmiContext, string id, string chat)
    {
        return false;
    }

    private static bool OnChatToRoom(HostID remote, RmiContext rmiContext, string id, string roomId, string chat)
    {
        return false;
    }

    private static bool OnChatToPerson(HostID remote, RmiContext rmiContext, string id, string targetId, string chat)
    {
        return false;
    }

    private static bool OnCreateRoom(HostID remote, RmiContext rmiContext, string id, string roomName, string pw)
    {
        return false;
    }

    private static bool OnGameReady(HostID remote, RmiContext rmiContext, string id, string roomName, bool isReady)
    {
        return false;
    }

    private static bool OnGameStart(HostID remote, RmiContext rmiContext, string id, string roomName)
    {
        return false;
    }

    private static bool OnRecordPosition(HostID remote, RmiContext rmiContext, string id, float x, float y, float z)
    {
        return false;
    }

    private static void OnJoinServer(NetClientInfo clientInfo)
    {
        Console.WriteLine("JOIN");
        return;
    }

    private static void OnLeaveServer(NetClientInfo clientInfo, ErrorInfo errorinfo, ByteArray comment)
    {
        Console.WriteLine("LEAVE");
        return;
    }
}