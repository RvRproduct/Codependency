using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goalpoint : MonoBehaviour
{
    bool cubeA;
    bool cubeB;

    private float counter = 3f;
    private bool goals;
    public Animator animator;

    private GameManager manager;
    private SoundEffects soundEffects;

    private bool wonLevel;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.instance;
        soundEffects = SoundEffects.Instance;
        goals = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (cubeA && cubeB)
        {
            if (!wonLevel)
            {
                wonLevel = true;
                soundEffects.controller.GetComponent<AudioSource>().clip = soundEffects.audioClips[2];
                soundEffects.controller.GetComponent<AudioSource>().Play();
            }
            

            goals = true; 
            animator.SetTrigger("goal");
        }
        if (goals)
        {
            counter -= Time.deltaTime;
        }
        if (counter < 0)
        {
            manager.WinLevel();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "cubeA")
        {
            cubeA = true;
        }
        if (collision.gameObject.tag == "cubeB")
        {
            cubeB = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "cubeA")
        {
            cubeA = false;
        }
        if (collision.gameObject.tag == "cubeB")
        {
            cubeB = false;
        }
    }
}
