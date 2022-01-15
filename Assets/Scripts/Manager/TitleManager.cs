using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Nettention.Proud;
using System.Text.RegularExpressions;

[System.Serializable]
public struct LoginSignUI
{
    public TMP_InputField tmpInputID;
    public TMP_InputField tmpInputPW;
    public TMP_InputField tmpInputConfirmPW;
    public Button btnTry;
    public Button btnGoBack;
}

public class TitleManager : Singletone<TitleManager>
{
    public RectTransform rtrnLoginParent;
    public RectTransform rtrnSignupParent;
    public RectTransform rtrnBtnsParent;

    [SerializeField] private LoginSignUI login;
    [SerializeField] private LoginSignUI sign;

    private void Start()
    {
        login.btnGoBack.onClick.AddListener(() =>
        {
            SwitchLoginSign();
        });
        sign.btnGoBack.onClick.AddListener(() =>
        {
            SwitchLoginSign();
        });

        login.btnTry.onClick.AddListener(() =>
        {
            //isPassCheck(login.tmpInputPW.text);

            User.proxy.LogIn(HostID.HostID_Server, RmiContext.ReliableSend, login.tmpInputID.text, login.tmpInputPW.text);
            K.clientInfo = new ClientInfo { hostID = User.netClient.GetLocalHostID(), ID = login.tmpInputID.text, PW = login.tmpInputPW.text, roomNum = -1 };
        });

        sign.btnTry.onClick.AddListener(() =>
        {
            if (sign.tmpInputPW.text != sign.tmpInputConfirmPW.text) return;

            User.proxy.SignUp(HostID.HostID_Server, RmiContext.ReliableSend, sign.tmpInputID.text, sign.tmpInputPW.text, sign.tmpInputConfirmPW.text);
            K.clientInfo = new ClientInfo { hostID = User.netClient.GetLocalHostID(), ID = sign.tmpInputID.text, PW = sign.tmpInputPW.text, roomNum = -1 };
        });
    }

    public void SwitchLoginSign()
    {
        print("Switch");
        bool login = rtrnLoginParent.gameObject.activeSelf;
        bool sign = rtrnSignupParent.gameObject.activeSelf;
        rtrnLoginParent.gameObject.SetActive(sign);
        rtrnSignupParent.gameObject.SetActive(login);
    }

    public bool isPassCheck(string pass)
    {
        if (pass != null && pass.Length < 8) return false;

        Regex regexPass = new Regex(@"^(?=.*?[a-z])(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{9,}$", RegexOptions.IgnorePatternWhitespace);
        bool result = regexPass.IsMatch(pass);
        if (result)
        {
            print("<color=#00FF22>is Matched!</color>");
        }
        else
        {
            print("<color=red>is UnMatched!</color>");
        }
        return result;
    }
}
