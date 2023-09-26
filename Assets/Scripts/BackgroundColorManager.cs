using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColorManager : MonoBehaviour
{
    [SerializeField] Controller controller;
    [SerializeField] Color[] nearColors = new Color[3];
    [SerializeField] Color[] farColors = new Color[3];

    Color[] colorDiffs = new Color[3];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < nearColors.Length; i++)
        {
            Color near = nearColors[i];
            Color far = farColors[i];
            colorDiffs[i] = near - far;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
