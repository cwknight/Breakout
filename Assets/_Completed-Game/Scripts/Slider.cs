using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider : MonoBehaviour
{
    public float lowerSpeedBound;
    public float upperSpeedBound;
    float speed = new float();
    Vector3 randomVector;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        int startingPosition = (int) (transform.position[0] + transform.position[1] + transform.position[2]);
        Random.InitState(startingPosition);
        speed = Random.Range(lowerSpeedBound, upperSpeedBound);
        randomVector = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        // Rotate the game object that this script is attached to by 15 in the X axis,
        // 30 in the Y axis and 45 in the Z axis, multiplied by deltaTime in order to make it per second
        // rather than per frame.
        rb.AddForce(randomVector * (speed));

        //transform.Translate(Vector3.right * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall")) {
            randomVector *= -1;
        }


    }
}
