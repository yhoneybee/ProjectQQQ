using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;

public class Popup : MonoBehaviour
{
    public TextMeshProUGUI txtHead;
    public TextMeshProUGUI txtBody;
    public event Action<bool> onActiveChanged;

    private void Awake()
    {
        K.popup = this;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                onActiveChanged?.Invoke(false);
                gameObject.SetActive(false);
            }
        }
    }

    public void PopupWindow(string head, string body, bool appear)
    {
        txtHead.text = head;
        txtBody.text = body;
        onActiveChanged?.Invoke(appear);
        gameObject.SetActive(appear);
    }
}
