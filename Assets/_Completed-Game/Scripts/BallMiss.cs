using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallMiss : MonoBehaviour
{
    private int count;
    public Text failtext;
    public Text loseText;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        loseText.text = "";
        failtext.text = "";
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
            other.attachedRigidbody.MovePosition(new Vector3(0.0f, 0.4f, 0.0f));
            
            count += 1;
            failtext.text = "Fail: " + count.ToString();
        }
    }

    ontrigger
}
