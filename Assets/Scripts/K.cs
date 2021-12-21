using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public static class K
{
    public static ClientInfo clientInfo;
    public static Popup popup;
    public static Fade fade;
    public static Chat chat;

    public static void SceneMove(string name)
    {
        fade.imgFade.DOFade(1, 1.5f).onComplete = () =>
        {
            SceneManager.LoadScene(name);
        };
    }
}
