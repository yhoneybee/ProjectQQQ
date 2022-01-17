using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Room
{
    public Room(string name, string pw)
    {
        this.name = name;
        this.pw = pw;
    }
    public string name, pw;
    public int id;
    public bool isPlaying;
    public List<UserInfo> clients = new List<UserInfo>();
}