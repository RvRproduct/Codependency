using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeShooter : MonoBehaviour
{
    private Transform fireplace;
    public GameObject bullet;
    public int bulletspeed;
    public float cooldown;
    public int lifespan;
    private float index;
    // Start is called before the first frame update
    void Start()
    {
        fireplace = this.transform.Find("FiringPlace");
        index = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        CountingDown();
    }

    private void CountingDown()
    {
        index -= Time.deltaTime;
        if(index <= 0 )
        {
            Shootbullet();
            index = cooldown;
        }
    }
    private void Shootbullet()
    {
        GameObject bulletInstance = Instantiate(bullet, fireplace.position, transform.rotation);
        bulletInstance.GetComponent<Spike>().bulletspeed = bulletspeed;
        bulletInstance.GetComponent<Spike>().lifespan = lifespan;
    }
}
