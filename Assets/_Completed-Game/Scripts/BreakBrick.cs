using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBrick : MonoBehaviour
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Ball"))
        {
            Destroy(gameObject);
            scoreboard.IncrementScore();
        }
        
    }
    
}
