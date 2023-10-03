using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class ColorFlash : MonoBehaviour
{
    [SerializeField] float duration = 0.125f;
    [SerializeField] Color shameColor;
    [SerializeField] Color followColor;
    [SerializeField] Color unfollowColor;
    [SerializeField] SpriteRenderer square;
    [SerializeField] Light2D auraLight;
    Color flashColor;
    Color diff;
    bool recovering = false;
    float elapsed = 0;

    // Update is called once per frame
    void Update()
    {
        if (recovering && elapsed <= duration)
        {
            Color shadeDelta = diff * (elapsed / duration);
            Color shade = flashColor - shadeDelta;
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

    public void FlashColor(Color baseline)
    {
        flashColor = shameColor;
        diff = shameColor - baseline;
        square.color = shameColor;
        auraLight.color = shameColor;
        recovering = true;
        elapsed = 0;
    }

    // only for player two
    public void FlashFollow()
    {
        flashColor = followColor;
        diff = followColor - Color.white;
        square.color = followColor;
        auraLight.color = followColor;
        recovering = true;
        elapsed = 0;
    }

    // only for player two
    public void FlashUnfollow()
    {
        flashColor = unfollowColor;
        diff = unfollowColor - Color.white;
        square.color = unfollowColor;
        auraLight.color = unfollowColor;
        recovering = true;
        elapsed = 0;
    }
}
