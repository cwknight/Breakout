using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallMiss : MonoBehaviour
{
    private int count;
    public Text failtext;
    public Text loseText;
    private SpawnBall spawner;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        loseText.text = "";
        failtext.text = "";
        spawner = GetComponent<SpawnBall>();
    }

    // Update is called once per frame
    void Update()
    {
        if (count >= 3)
        {
            loseText.text = "You lose";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Destroy(other.gameObject);
            spawner.Spawn();
            
            count += 1;
            failtext.text = "Fail: " + count.ToString();
        }
    }
    
}
