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
    private int count;

    public Text countText;
    public Text winText;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = initialVelocity;
        lastFrameVelocity = rb.velocity;
        count = 0;
        countText.text = "Count: ";
        winText.text = "";
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
        if (collision.collider.CompareTag("Brick"))
        {
            count += 1;

            SetCountText();
        }
    }

    private void Bounce(Vector3 collisionNormal)
    {
        var speed = lastFrameVelocity.magnitude;
        var direction = Vector3.Reflect(lastFrameVelocity.normalized, collisionNormal);

        Debug.Log("Out Direction: " + direction);
        rb.velocity = direction * Mathf.Max(speed, minVelocity);
    }

    // Create a standalone function that can update the 'countText' UI and check if the required amount to win has been achieved
    void SetCountText()
    {
        // Update the text field of our 'countText' variable
        countText.text = "Count: " + count.ToString();

        // Check if our 'count' is equal to or exceeded 12
        if (count >= 25)
        {
            // Set the text value of our 'winText'
            winText.text = "You Win!";
        }
    }
}
