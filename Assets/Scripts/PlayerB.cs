using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerB : MonoBehaviour
{
    public Controller controller;
    public Transform cameraAim;
    private GameManager manager;
    ColorChange colorShifter;
    ColorFlash colorFlasher;

    // shame stuff
    public float SHAME_LIMIT = 20;
    public float shameDamage = 1;
    public float healDist = 3;
    public float healRate = 2;
    float shameLevel = 0;

    // instances 
    private SoundEffects soundEffects;
    

    void Start()
    {
        colorShifter = gameObject.GetComponent<ColorChange>();
        colorFlasher = gameObject.GetComponent <ColorFlash>();
        manager = GameManager.instance;
        soundEffects = SoundEffects.Instance;
    }

    void Update()
    {
        if(controller.playerDist <= healDist)
        {
            shameLevel -= healRate * Time.deltaTime;
            colorShifter.UpdateColor(shameLevel / SHAME_LIMIT);

        }
        shameLevel = Mathf.Max(0, shameLevel);
    }

    public void ShamePlayer()
    {
        // adjust health
        shameLevel += shameDamage;

        // adjust color
        colorShifter.UpdateColor(shameLevel / SHAME_LIMIT);

        // color flash
        colorFlasher.FlashColor(colorShifter.currentColor);

        // if too much shame, gameOver
        if (shameLevel >= SHAME_LIMIT)
        {
            manager.LoseGame();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "spike")
        {
            soundEffects.controller.GetComponent<AudioSource>().clip = soundEffects.audioClips[0];
            soundEffects.controller.GetComponent<AudioSource>().Play();
            manager.LoseGame();
        }
    }

}
