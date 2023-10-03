using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    public GameObject player;
    [SerializeField] SpriteRenderer[] backgroundOne = new SpriteRenderer[4];
    [SerializeField] SpriteRenderer[] backgroundTwo = new SpriteRenderer[4];
    public float width = 128;
    public float quarterWidth = 32;

    // Start is called before the first frame update
    void Start()
    {
        if(Controller.Instance != null)
        {
            player = Controller.Instance.activePlayer;
        }
        
        width = ((backgroundOne[0].size.x * 2) * gameObject.transform.localScale.x) / 2;       // the sprite renderer won't automatically account for the scale on the parent object
        quarterWidth = width / 4;
    }

    // Update is called once per frame
    void Update()
    {
        // if the player is in the quarter on either end of the background section, move the other section to cover the end
        float playerX = player.transform.position.x;

        // repeat for all layers of background
        for (int i = 0; i < backgroundOne.Length; i++)
        {
            if (playerX > backgroundOne[i].bounds.min.x && playerX < backgroundOne[i].bounds.max.x)
            {
                float backX = backgroundOne[i].transform.position.x;

                // left end
                if (playerX < backX - quarterWidth)
                {
                    backgroundTwo[i].transform.position = new Vector3(backX - width, backgroundTwo[i].transform.position.y, 0);
                }
                // right end
                else if (playerX > backX + quarterWidth)
                {
                    backgroundTwo[i].transform.position = new Vector3(backX + width, backgroundTwo[i].transform.position.y, 0);
                }
            }
            else
            {
                float backX = backgroundTwo[i].transform.position.x;

                // left end
                if (playerX < backX - quarterWidth)
                {
                    backgroundOne[i].transform.position = new Vector3(backX - width, backgroundOne[i].transform.position.y, 0);
                }
                // right end
                else if (playerX > backX + quarterWidth)
                {
                    backgroundOne[i].transform.position = new Vector3(backX + width, backgroundOne[i].transform.position.y, 0);
                }
            }
        }

    }
}