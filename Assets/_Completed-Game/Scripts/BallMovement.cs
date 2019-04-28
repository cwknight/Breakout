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
    private enum STATE {Stuck, Unstuck};
    private STATE currentState;

    public GameObject Paddle;




    void Start()
    {
        Paddle = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();
        StickToPaddle();
        //Debug.Log("initial velocity" + rb.velocity.ToString());
    }

    private void StickToPaddle()
    {
        currentState = STATE.Stuck;        
        rb.transform.position = Paddle.transform.position + new Vector3(0, 0, 1.0f);
    }

    private void UnstickFromPaddle()
    {
        currentState = STATE.Unstuck;
        rb.velocity += initialVelocity;

    }

    private void Update()
    {
        lastFrameVelocity = rb.velocity;
        if(currentState == STATE.Stuck)
        {
            rb.position = Paddle.transform.position + new Vector3(0, 0, 1.0f);
        }

        //Debug.Log("current velocity" + rb.velocity.ToString());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(currentState == STATE.Stuck){
            return;
        }
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
