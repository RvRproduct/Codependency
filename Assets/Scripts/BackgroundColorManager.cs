using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColorManager : MonoBehaviour
{
    [SerializeField] Controller controller;
    [SerializeField] float minDist = 5f;        // minimum distance before background starts to change
    [SerializeField] float distLimit = 10f;       // range where background stops changing
    [SerializeField] Color[] nearColors = new Color[3];
    [SerializeField] Color[] farColors = new Color[3];
    [SerializeField] SpriteRenderer baseLayer;
    [SerializeField] SpriteRenderer[] colorOne = new SpriteRenderer[3];
    [SerializeField] SpriteRenderer[] colorTwo = new SpriteRenderer[3];
    
    Color[] colorDiffs = new Color[3];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < nearColors.Length; i++)
        {
            Color near = nearColors[i];
            Color far = farColors[i];
            colorDiffs[i] = far - near;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // get player's distance from partner
        float dist = controller.playerDist;

        // find amount over the limit for color shift
        float diff = 0;
        
        if(dist > minDist + distLimit) {
            diff = distLimit;
        }
        else if (dist > minDist)
        {
            diff = dist - minDist;
        }

        // find proportion that color should change
        float changeMult = diff / distLimit;

        // find new shades
        Color[] newShades = new Color[3];
        for (int i = 0;i < colorDiffs.Length; i++)
        {
            Color shadeDelta = colorDiffs[i] * changeMult;
            newShades[i] = nearColors[i] + shadeDelta;

        }

        baseLayer.color = newShades[0];
        foreach(SpriteRenderer renderer in colorOne)
        {
            renderer.color = newShades[1];
        }
        foreach (SpriteRenderer renderer in colorTwo)
        {
            renderer.color = newShades[2];
        }






    }
}
