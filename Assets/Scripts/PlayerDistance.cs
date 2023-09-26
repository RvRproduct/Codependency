using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDistance : MonoBehaviour
{
    public static PlayerDistance Instance;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject playerOne;
    public GameObject playerTwo;
    public float Distance;

    void Update()
    {
        var dir = playerOne.transform.position - playerTwo.transform.position;
        Distance = dir.magnitude;
        
    }
}
