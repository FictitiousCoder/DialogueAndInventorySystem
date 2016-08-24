using UnityEngine;
using System.Collections;


public class SitDown : MonoBehaviour {

    public GameObject player;
    public GameObject sittingPlayer;

    private bool inRange = false;
    private bool sitting = false;


    void Update()
    {
        if (inRange == true && sitting == false && Input.GetKeyDown(KeyCode.R))
        {
            Sit();  
        }
        else if (sitting == true && Input.GetKeyDown(KeyCode.R))
        {
            StandUp();
        }
    }

    void Sit()
    {
        player.SetActive(false);
        sittingPlayer.SetActive(true);
        sitting = true;
    }

    void StandUp()
    {
        sittingPlayer.SetActive(false);
        player.SetActive(true);
        sitting = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = false;
        }
    }
}
