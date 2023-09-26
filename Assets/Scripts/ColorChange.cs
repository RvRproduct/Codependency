using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ColorChange : MonoBehaviour
{
    [SerializeField] SpriteRenderer square;
    [SerializeField] Light2D light;
    [SerializeField] Vector3 SHAME = new Vector3(94, 0, 152);
    Vector3 WHITE = new Vector3(255, 255, 255);
    Vector3 diff;

    // Start is called before the first frame update
    void Start()
    {
        diff = WHITE - SHAME;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // takes in the proportion of shame the player has taken to calculate the new color
    public void UpdateColor(float shameProp)
    {
        Vector3 shadeDelta = diff * shameProp;
        Vector3 shade = WHITE - shadeDelta;
        square.color = new Color(shade.x, shade.y, shade.z);
        light.color = new Color(shade.x, shade.y, shade.z);
    }
}
