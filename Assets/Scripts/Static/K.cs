using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;


public static class K
{
    public static UserInfo clientInfo;
    public static Popup popup;
    public static Fade fade;
    public static Chat chat;
    public static Trie command;
    public static List<Room> rooms = new List<Room>();
    public static List<UserInfo> users = new List<UserInfo>();
    public static void SceneMove(string name)
    {
        fade.imgFade.DOFade(1, 1.5f).onComplete = () =>
        {
            SceneManager.LoadScene(name);
        };
    }
}
