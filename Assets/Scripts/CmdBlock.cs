using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CmdBlock : MonoBehaviour
{
    public Text txtKey;
    public string strKey;
    public Animator anim;

    public void ApplyText() => txtKey.text = strKey;
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
