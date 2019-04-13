using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallMovement : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Just for debugging, adds some velocity during OnEnable")]
    private Vector3 initialVelocity;

    [SerializeField]
    private float minVelocity = 10f;

    private Vector3 lastFrameVelocity;
    private Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = initialVelocity;
        lastFrameVelocity = rb.velocity;
        

        //Debug.Log("initial velocity" + rb.velocity.ToString());
    }

    private void Update()
    {
        lastFrameVelocity = rb.velocity;

        //Debug.Log("current velocity" + rb.velocity.ToString());
    }

    private void OnCollisionEnter(Collision collision)
    {
        Bounce(collision.contacts[0].normal);
    }

    private void Bounce(Vector3 collisionNormal)
    {
        var speed = lastFrameVelocity.magnitude;
        var direction = Vector3.Reflect(lastFrameVelocity.normalized, collisionNormal);
        rb.velocity = direction * Mathf.Max(speed, minVelocity);
        //StartCoroutine(minSpeed());
    }

    IEnumerator minSpeed()
    {
        
        while (rb.velocity.magnitude < minVelocity)
        {
            
            rb.velocity = rb.velocity * minVelocity;
            
            yield return new WaitWhile(()=>rb.velocity.magnitude > minVelocity);
        }
    }

    
}
