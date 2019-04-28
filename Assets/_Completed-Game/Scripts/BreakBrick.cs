using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBrick : MonoBehaviour
{
    private Scoreboard scoreboard;
    private int health;
    SetColor setcolor;
    // Start is called before the first frame update
    void Start()
    {
        setcolor = gameObject.GetComponentInParent<SetColor>();
        scoreboard = GameObject.FindWithTag("Scoreboard").GetComponent<Scoreboard>();
        health = setcolor.health;



    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Ball")) {
            setcolor.health--;
            setcolor.UpdateColorAndHealth();
            scoreboard.IncrementScore();
        }

    }

}
