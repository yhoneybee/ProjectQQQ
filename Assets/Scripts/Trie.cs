using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trie
{
    public bool isEnd;
    public char spell;
    public string strParent;

    public List<Trie> childs = new List<Trie>();

    public void Add(string word)
    {
        Trie level = this;
        for (int i = 0; i < word.Length; i++)
        {
            var child = new Trie();
            child.spell = word[i];
            child.isEnd = i == word.Length - 1;
            child.strParent = $"{level.strParent}{word[i]}";
            var find = level.childs.Find(x => x.spell == word[i]);

            if (find != null) child = find;
            else level.childs.Add(child);

            level = child;
        }
    }

    public bool Find(string word)
    {
        Trie level = this;
        for (int i = 0; i < word.Length; i++)
        {
            var find = level.childs.Find(x => x.spell == word[i]);
            if (find != null) level = find;
            else
            {
                return false;
            }
        }

        return level.isEnd;
    }

    public static Trie Get(params string[] arrStr)
    {
        Trie root = new Trie();
        foreach (var str in arrStr) root.Add(str);
        return root;
    }
}
