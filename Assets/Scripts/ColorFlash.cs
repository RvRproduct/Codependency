using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class ColorFlash : MonoBehaviour
{
    [SerializeField] float duration = 0.125f;
    [SerializeField] Color flash;
    [SerializeField] SpriteRenderer square;
    [SerializeField] Light2D auraLight;
    Color baseColor = Color.white;
    Color diff;
    bool recovering = false;
    float elapsed = 0;

    // Update is called once per frame
    void Update()
    {
        if (recovering && elapsed <= duration)
        {
            diff = flash - baseColor;

            Color shadeDelta = diff * (elapsed / duration);
            Color shade = flash - shadeDelta;
            square.color = shade;
            auraLight.color = shade;
            elapsed += Time.deltaTime;
        }
        else
        {
            recovering = false;
            elapsed = 0;
        }
    }

    public void FlashColor()
    {
        baseColor = square.color;
        square.color = flash;
        auraLight.color = flash;
        recovering = true;
        elapsed = 0;
    }
}
