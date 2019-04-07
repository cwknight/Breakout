using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColor : MonoBehaviour
{
    public int health;

    Color[] colors;

    public Renderer thisRend; //Renderer of our Cube

    float transitionTime = 5f; // Amount of time it takes to fade between colors

    private void Awake()
    {
        thisRend = GetComponent<Renderer>(); // grab the renderer component on our cube

        colors = new Color[6]; // We will randomize through this array

        //initialize our array indexes with colors

        colors[0] = Color.white;

        colors[1] = Color.blue;

        colors[2] = Color.green;

        colors[3] = Color.yellow;

        colors[4] = Color.red;

        colors[5] = Color.black;

        health = (Random.Range(1, 5));
        UpdateColorAndHealth();

        //start our coroutine when the game starts

        //StartCoroutine(ColorChange());
    }
    void Start()

    {

        

    }

    void Update()

    {

    }

    public void UpdateColorAndHealth()
    {
        if(health > 0)
        {
            Color newColor = colors[health];
            thisRend.material.SetColor("_Color", newColor);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    IEnumerator ColorChange()

    {

        //Infinite loop will ensure our coroutine runs all game

        while (true)

        {

            Color newColor = colors[(Random.Range(0, 5))]; // Assign newColor to a random color from our array

            float transitionRate = 0; //Create and set transitionRate to 0. This is necessary for our next while loop to function

            /* 1 is the highest value that the Color.Lerp function uses for

             * transitioning between two colors. This while loop will execute

             * until transitionRate is incremented to 1 or higher

             */

            while (transitionRate < 1)

            {

                //this next line is how we change our material color property. We Lerp between the current color and newColor

                thisRend.material.SetColor("_Color", Color.Lerp(thisRend.material.color, newColor, Time.deltaTime * transitionRate));

                transitionRate += Time.deltaTime / transitionTime; // Increment transitionRate over the length of transitionTime

                yield return null; // wait for a frame then loop again

            }

            yield return null; // wait for a frame then loop again

        }

    }

}
