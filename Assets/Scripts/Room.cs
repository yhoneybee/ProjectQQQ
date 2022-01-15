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
    public int num;
    public List<User> clients = new List<User>();
}