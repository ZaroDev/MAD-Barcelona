using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextButton : MonoBehaviour
{
    TextMeshProUGUI buttonText;
    void Start()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = gameObject.name;
    }

    public virtual void OnPressed()
    {

    }
}
