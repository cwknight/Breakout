using UnityEngine;

// Include the namespace required to use Unity UI
using UnityEngine.UI;

using System.Collections;

public class PlayerController : MonoBehaviour {
	
	// Create public variables for player speed, and for the Text UI game objects
	public float speed;
    [SerializeField]
    private float moveHorizontal;


    // Create private references to the rigidbody component on the player, and the count of pick up objects picked up so far
    private Rigidbody rb;

	// At the start of the game..
	void Start ()
	{
        // Assign the Rigidbody component to our private rb variable
        rb = GetComponent<Rigidbody>();


	}

	// Each physics step..
	void FixedUpdate ()
	{
        // Set some local float variables equal to the value of our Horizontal and Vertical Inputs
        moveHorizontal = Input.GetAxis ("Horizontal");
        //float moveVertical = Input.GetAxis ("Vertical");
        Vector3 movement;

        if (moveHorizontal == 0.0f)
        {
            movement = new Vector3(-rb.velocity.x, 0.0f, 0.0f);
            rb.AddForce(movement * speed, ForceMode.Impulse);
            
        }
        else
        {
            // Create a Vector3 variable, and assign X and Z to feature our horizontal and vertical float variables above
            movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
            // Add a physical force to our Player rigidbody using our 'movement' Vector3 above, 
            // multiplying it by 'speed' - our public player speed that appears in the inspector
            rb.AddForce(movement * speed, ForceMode.Impulse);
        }
		

        

	

	}



	
}