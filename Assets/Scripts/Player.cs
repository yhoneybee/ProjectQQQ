using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Animator anim;

    private void Awake()
    {
        A.player = this;
    }

    private void Start()
    {
        if (!anim) anim = GetComponent<Animator>();
    }

    private void Update()
    {
    }

    public void LeftDash()
    {
        print("<color=red>L DASH</color>");
    }
    public void RightDash()
    {
        print("<color=red>R DASH</color>");
    }
}
