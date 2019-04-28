using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallMiss : MonoBehaviour
{
    private Scoreboard scoreboard;

    // Start is called before the first frame update
    void Start()
    {
        scoreboard = GameObject.FindWithTag("Scoreboard").GetComponent<Scoreboard>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball")) {
            Destroy(other.gameObject);
            scoreboard.DecrementLives();
        }
        if (other.CompareTag("Brick")) {
            GameObject.FindGameObjectWithTag("Scoreboard").GetComponent<Scoreboard>().LoseGame();
        }
    }

}
