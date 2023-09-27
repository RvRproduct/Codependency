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
    Color diff;
    bool recovering = false;
    float elapsed = 0;

    // Start is called before the first frame update
    void Start()
    {
        diff = flash - Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        if (recovering && elapsed <= duration)
        {
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
        square.color = flash;
        auraLight.color = flash;
        recovering = true;
        elapsed = 0;
    }
}
