using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Nettention.Proud;
using System.Text.RegularExpressions;

public class TitleManager : Singletone<TitleManager>
{
    [SerializeField] private TMP_InputField tmpInputID;
    [SerializeField] private TMP_InputField tmpInputPW;
    [SerializeField] private Button btnLogin;

    private void Start()
    {
        btnLogin.onClick.AddListener(() => 
        {
            if (isPassCheck(tmpInputPW.text))
            {
                print("<color=#00FF22>is Matched!</color>");
            }
            else
            {
                print("<color=red>is UnMatched!</color>");
            }
            Client.proxy.LogIn(HostID.HostID_Server, RmiContext.ReliableSend, tmpInputID.text, tmpInputPW.text);
            K.clientInfo = new ClientInfo { hostID = Client.netClient.GetLocalHostID(), ID = tmpInputID.text, nickName = "", PW = tmpInputPW.text, roomNum = -1 };
        });
    }

    public bool isPassCheck(string pass)
    {
        if (pass != null && pass.Length < 8) return false;

        Regex regexPass = new Regex(@"^(?=.*?[a-z])(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{9,}$", RegexOptions.IgnorePatternWhitespace);
        return regexPass.IsMatch(pass);
    }
}
