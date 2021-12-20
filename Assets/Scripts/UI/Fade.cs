using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Fade : MonoBehaviour
{
    public Image imgFade;

    private void Awake()
    {
        K.fade = this;
        imgFade.DOFade(0, 1.5f);
    }
}
