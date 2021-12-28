using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rainbow : MonoBehaviour
{
    public float changeSpeed;

    private MeshRenderer mr;

    private List<Color> colors = new List<Color>()
    {
        new Color(1,0,0),
        new Color(1,1,0),
        new Color(0,1,0),
        new Color(0,1,1),
        new Color(0,0,1),
        new Color(1,0,1),
        new Color(1,0,0),
    };

    private int index;

    void Start()
    {
        mr = GetComponent<MeshRenderer>();
    }
    void Update()
    {
        mr.material.color = Color.Lerp(mr.material.color, colors[index], Time.deltaTime * changeSpeed);
        if (Mathf.Abs(mr.material.color.r - colors[index].r) +
            Mathf.Abs(mr.material.color.g - colors[index].g) +
            Mathf.Abs(mr.material.color.b - colors[index].b) < 0.005)
        {
            index++;
            index %= colors.Count;
        }
    }
}
