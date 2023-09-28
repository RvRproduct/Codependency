using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextBubble : MonoBehaviour
{
    public TMP_Text bubbleText;
    public GameObject bubble;
    public string text;
    private float bubbleDelay = 3f;
    private bool triggered;

    void ChangeText()
    {
        bubbleText.text = text;
    }

    void TextBubbleEnable()
    {
        ChangeText();
        bubble.SetActive(true);
        Invoke(nameof(TextBubbleDisable), bubbleDelay);

    }

    void TextBubbleDisable()
    {
        bubble.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "cubeA")
        {
            if (!triggered)
            {
                triggered = true;
                TextBubbleEnable();
            }
        }
    }

}
