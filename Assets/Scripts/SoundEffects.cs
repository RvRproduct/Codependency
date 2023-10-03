using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public static SoundEffects Instance;
    public GameObject controller; 

    private void Awake()
    {
        Instance = this;
    }

    public List<AudioClip> audioClips; 
    
}
