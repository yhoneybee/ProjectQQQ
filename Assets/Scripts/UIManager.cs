using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singletone<UIManager>
{
    [SerializeField] CmdBlock originCmdBlock;
    public RectTransform rtrnCmdBlockParent;
    public Queue<CmdBlock> cmdBlocks;

    private void Start()
    {
        cmdBlocks = new Queue<CmdBlock>();
    }

    public void AddCmdBlock(string txtKey)
    {
        var obj = Instantiate(originCmdBlock, rtrnCmdBlockParent, false);
        obj.strKey = txtKey;
        obj.anim.Play("Appear");
        cmdBlocks.Enqueue(obj);
    }
    public bool DequeueCmdBlock()
    {
        if (cmdBlocks.Count <= 0) return false;
        var block = cmdBlocks.Dequeue();
        block.strKey = "";
        block.anim.Play("Disappear");
        return true;
    }
}
