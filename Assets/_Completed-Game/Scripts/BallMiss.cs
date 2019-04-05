using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallMiss : MonoBehaviour
{
    public Text counttext;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            other.attachedRigidbody.MovePosition(new Vector3(0.0f, 0.4f, 0.0f));
            counttext.text = "Fail";
        }
    }
}
