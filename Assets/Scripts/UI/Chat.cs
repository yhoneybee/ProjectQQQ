using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Nettention.Proud;
using DG.Tweening;

public enum eCHAT_LEVEL
{
    All,
    Room,
    Person,
}

public struct Command
{
    public string cmd;
    public string desc;
    public event Action onExcution;
}

public class Chat : MonoBehaviour
{
    public eCHAT_LEVEL ChatLevel
    {
        get => chatLevel;
        set
        {
            chatLevel = value;
            txtChatLevel.text = chatLevel switch
            {
                eCHAT_LEVEL.All => "( All )",
                eCHAT_LEVEL.Room => "( Room )",
                eCHAT_LEVEL.Person => "( Person )",
                _ => "",
            };
        }
    }
    public bool IsChatLog
    {
        get => isChatLog;
        set
        {
            isChatLog = value;
            inputChat.text = "";
            if (isChatLog) srtChat.GetComponent<RectTransform>().DOSizeDelta(Vector2.up * SHOW_LOG_HEIGHT, 1);
            else srtChat.GetComponent<RectTransform>().DOSizeDelta(Vector2.zero, 1);
        }
    }

    public TMP_InputField inputChat;
    public TextMeshProUGUI txtChatLevel;
    public RectTransform rtrnContent;
    public TextMeshProUGUI originTxtChat;
    public ScrollRect srtChat;

    private eCHAT_LEVEL chatLevel;
    private bool isChatLog;
    private readonly float SHOW_LOG_HEIGHT = 1007.6f;

    private void Awake()
    {
        K.chat = this;
        ChatLevel = eCHAT_LEVEL.All;

        K.command = Trie.Get("apple", "appee");

        inputChat.onSubmit.AddListener((s) =>
        {
            Chatting();
        });
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.C))
            IsChatLog = !IsChatLog;
    }

    public void EchoChat(string chat, Color color)
    {
        var obj = Instantiate(originTxtChat, rtrnContent, false);
        obj.text = chat;
        obj.color = color;
        srtChat.verticalScrollbar.value = 0;
    }

    public void Chatting()
    {
        if (inputChat.text == "" || inputChat.text.StartsWith("/"))
        {
            inputChat.text = "";
            return;
        }
        if (inputChat.text.StartsWith("@"))
        {

        }
        string chat = $"{txtChatLevel.text}[ {K.clientInfo.ID} ] : {inputChat.text}";
        inputChat.text = "";
        inputChat.Select();

        List<string> strs = new List<string>();
        int oneLineWordCount = 45;
        int loopCount = (chat.Length - 1) / oneLineWordCount;

        for (int i = 0; i < loopCount; i++)
            strs.Add(chat.Substring(i * oneLineWordCount, oneLineWordCount - 1));
        strs.Add(chat.Substring(loopCount * oneLineWordCount));

        foreach (var str in strs)
        {
            switch (ChatLevel)
            {
                case eCHAT_LEVEL.All:
                    Client.proxy.ChatToAll(HostID.HostID_Server, RmiContext.ReliableSend, K.clientInfo.ID, str);
                    break;
                case eCHAT_LEVEL.Room:
                    Client.proxy.ChatToRoom(HostID.HostID_Server, RmiContext.ReliableSend, K.clientInfo.ID, K.clientInfo.roomID, str);
                    break;
                case eCHAT_LEVEL.Person:
                    Client.proxy.ChatToAll(HostID.HostID_Server, RmiContext.ReliableSend, K.clientInfo.ID, str);
                    break;
            }
        }
    }
}
