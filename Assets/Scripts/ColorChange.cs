using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ColorChange : MonoBehaviour
{
    [SerializeField] SpriteRenderer square;
    [SerializeField] Light2D light;
    [SerializeField] Color SHAME = new Color(0.3686275f, 0, 0.5960785f);
    Color diff;

    // Start is called before the first frame update
    void Start()
    {
        diff = Color.white - SHAME;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // takes in the proportion of shame the player has taken to calculate the new color
    public void UpdateColor(float shameProp)
    {
        Color shadeDelta = diff * shameProp;
        Color shade = Color.white - shadeDelta;
        square.color = shade;
        light.color = shade;
    }
}
