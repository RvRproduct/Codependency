using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColorManager : MonoBehaviour
{
    [SerializeField] Controller controller;
    [SerializeField] float warningShiftBegin = 5f;        // minimum distance before background starts to change
    [SerializeField] float warningLength = 10f;       // range where background stops changing
    [SerializeField] float blackShiftStart = 5f;        // minimum distance before background starts to change
    [SerializeField] float blackShiftLength = 10f;       // range where background stops changing
    [SerializeField] Color[] nearColors = new Color[3];
    [SerializeField] Color[] farColors = new Color[3];
    [SerializeField] SpriteRenderer baseLayer;
    [SerializeField] SpriteRenderer[] colorOne = new SpriteRenderer[3];
    [SerializeField] SpriteRenderer[] colorTwo = new SpriteRenderer[3];
    
    Color[] colorDiffs = new Color[3];
    Color[] blackDiffs = new Color[3];
    float warningShiftEnd;
    float blackShiftBegin;
    float blackShiftEnd;

    // Start is called before the first frame update
    void Start()
    {
        warningShiftEnd = warningShiftBegin + warningLength;
        blackShiftBegin = warningShiftEnd + blackShiftStart;
        blackShiftEnd = blackShiftEnd + blackShiftStart;

        for (int i = 0; i < nearColors.Length; i++)
        {
            Color near = nearColors[i];
            Color far = farColors[i];
            colorDiffs[i] = far - near;

            blackDiffs[i] = Color.black - far;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // get player's distance from partner
        float dist = controller.playerDist;

        // find amount over the limit for color shift
        float diff = 0;
        
        // warning shift
        if(dist <= warningShiftEnd)
        {
            if (dist > warningShiftBegin)
            {
                diff = dist - warningShiftBegin;
            }

            // find proportion that color should change
            float changeMult = diff / warningLength;

            // find new shades
            Color[] newShades = new Color[3];
            for (int i = 0; i < colorDiffs.Length; i++)
            {
                Color shadeDelta = colorDiffs[i] * changeMult;
                newShades[i] = nearColors[i] + shadeDelta;

            }

            baseLayer.color = newShades[0];
            foreach (SpriteRenderer renderer in colorOne)
            {
                renderer.color = newShades[1];
            }
            foreach (SpriteRenderer renderer in colorTwo)
            {
                renderer.color = newShades[2];
            }
        }
        else if(dist > blackShiftBegin)
        {
            // fade to black
            diff = dist - blackShiftBegin;
            diff = Mathf.Min(diff, blackShiftLength);

            // find proportion that color should change
            float changeMult = diff / blackShiftLength;

            // find new shades
            Color[] newShades = new Color[3];
            for (int i = 0; i < colorDiffs.Length; i++)
            {
                Color shadeDelta = blackDiffs[i] * changeMult;
                newShades[i] = farColors[i] + shadeDelta;
            }

            baseLayer.color = newShades[0];
            foreach (SpriteRenderer renderer in colorOne)
            {
                renderer.color = newShades[1];
            }
            foreach (SpriteRenderer renderer in colorTwo)
            {
                renderer.color = newShades[2];
            }
        }


    }
}
