using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClickText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textObject;

    void Start()
    {
        textObject.text = "";
    }

    void OnMouseDown()
    {
        textObject.text = "Clicked";
    }
}
