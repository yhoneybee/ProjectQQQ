using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    public TMP_InputField inputChat;
    public TextMeshProUGUI txtChatLevel;

    private eCHAT_LEVEL chatLevel;

    private void Awake()
    {
        K.chat = this;
        ChatLevel = eCHAT_LEVEL.All;

        var trie = Trie.Get("apple", "appee");

        inputChat.onValueChanged.AddListener((s) =>
        {
            if (s.StartsWith("/"))
            {
                s = s.Substring(1);
                if (trie.Find(s))
                {
                    print("FIND!");
                }
            }
        });
    }
}
