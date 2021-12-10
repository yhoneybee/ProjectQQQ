using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Cmd
{
    public readonly static ulong ArrowDown = 0x1;
    public readonly static ulong ArrowUp = 0x100;
    public readonly static ulong ArrowLeft = 0x10000;
    public readonly static ulong ArrowRight = 0x1000000;
    public readonly static ulong J = 0x100000000;
    public readonly static ulong K = 0x10000000000;
    public readonly static ulong U = 0x1000000000000;
    public readonly static ulong I = 0x100000000000000;
    public static Dictionary<string, ulong> dicCmdKey = new Dictionary<string, ulong>();
}

public class CommandManager : MonoBehaviour
{
    private ulong nowCmd;
    private Dictionary<ulong, System.Action> dicCmd;
    private Queue<ulong> cmdQueue;
    private WaitForSeconds cmdTimming;

    private void MakeCmd(System.Action action, params ulong[] keys)
    {
        ulong makeCmd = 0;
        for (int i = 0; i < keys.Length; i++) makeCmd |= keys[i] << i;
        dicCmd.Add(makeCmd, action);
    }

    private string GetKeyText(string txtKey) => txtKey switch
    {
        "ArrowDown" => "ก้",
        "ArrowUp" => "ก่",
        "ArrowLeft" => "ก็",
        "ArrowRight" => "กๆ",
        string s => s,
        _ => "",
    };

    private void Awake()
    {
        Cmd.dicCmdKey.Add("ArrowDown", Cmd.ArrowDown);
        Cmd.dicCmdKey.Add("ArrowUp", Cmd.ArrowUp);
        Cmd.dicCmdKey.Add("ArrowLeft", Cmd.ArrowLeft);
        Cmd.dicCmdKey.Add("ArrowRight", Cmd.ArrowRight);
        Cmd.dicCmdKey.Add("J", Cmd.J);
        Cmd.dicCmdKey.Add("K", Cmd.K);
        Cmd.dicCmdKey.Add("U", Cmd.U);
        Cmd.dicCmdKey.Add("I", Cmd.I);

        dicCmd = new Dictionary<ulong, System.Action>();
        cmdQueue = new Queue<ulong>();

        MakeCmd(A.player.LeftDash, Cmd.ArrowLeft, Cmd.ArrowLeft);
        MakeCmd(A.player.RightDash, Cmd.ArrowRight, Cmd.ArrowRight);

        cmdTimming = new WaitForSeconds(0.7f);
        StartCoroutine(ECmdClear());
        StartCoroutine(ECmdBlockDequeue());
    }

    private void Update()
    {
        System.Action action;
        if (dicCmd.TryGetValue(nowCmd, out action))
        {
            action();
            nowCmd = 0;
            cmdQueue.Clear();
            StartCoroutine(ECmdBlockDequeue(false));
        }

        foreach (var pair in Cmd.dicCmdKey)
        {
            if (Input.GetButtonDown(pair.Key))
            {
                nowCmd |= pair.Value << cmdQueue.Count;
                cmdQueue.Enqueue(pair.Value);
                UIManager.Instance.AddCmdBlock(GetKeyText(pair.Key));
                print($"<color=#33FF33>push</color> : {pair.Key}");
            }
        }
    }

    private IEnumerator ECmdClear()
    {
        while (true)
        {
            if (cmdQueue.Count > 0)
            {
                yield return cmdTimming;
                nowCmd = 0;
                cmdQueue.Clear();
            }

            yield return null;
        }
    }

    private IEnumerator ECmdBlockDequeue(bool isWait = true)
    {
        float waitTime = 3;
        while (isWait)
        {
            yield return new WaitForSeconds(waitTime);
            waitTime -= 1;
            waitTime = waitTime <= 0 ? 0 : waitTime;
            if (!UIManager.Instance.DequeueCmdBlock()) waitTime = 3;
            yield return null;
        }

        for (int i = 0; !isWait && i < UIManager.Instance.cmdBlocks.Count; i++)
            UIManager.Instance.DequeueCmdBlock();
    }
}
