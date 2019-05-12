using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBrick : MonoBehaviour
{
    public GameObject Brick;
    public int maxLength = 5; //set to the max width of the field in bricks
    public int maxHeight = 6;
    [SerializeField]
    public int brickCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        brickCount = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ContinuousModeRandom()
    {
        StartCoroutine(MoveAndSpawn());
    }

    public void ContinuousMode(int health)
    {
        StartCoroutine(MoveAndSpawn(health));
    }

    public void StopContinuousMode()
    {
        CancelInvoke();
    }

    public void SpawnBrickAt(Vector3 position)
    {
        GameObject brick = Instantiate(Brick);
        brick.transform.position = position;
        brickCount += brick.GetComponent<SetColor>().health;
    }

    public void SpawnBrickAt(Vector3 position, int health)
    {
        GameObject brick = Instantiate(Brick);
        brick.transform.position = position;
        brick.GetComponent<SetColor>().health = health;
        brick.GetComponent<SetColor>().UpdateColorAndHealth();
        brickCount += brick.GetComponent<SetColor>().health;
    }

    public void SpawnBrickAt(int height, int width)
    {
        height = Mathf.Min(maxHeight, height);
        height = maxHeight - height;
        float heightF = 1.5f * height;
        width = Mathf.Min(maxLength, width);
        float lengthF = -7.0f + (3.5f * width);
        Vector3 position = new Vector3(lengthF, 0.5f, heightF);
        SpawnBrickAt(position);
    }

    public void SpawnBrickAt(int height, int width, int health)
    {
        height = Mathf.Min(maxHeight, height);
        height = maxHeight - height;
        float heightF = 1.5f * height;
        width = Mathf.Min(maxLength, width);
        float lengthF = -7.0f + (3.5f * width);
        Vector3 position = new Vector3(lengthF, 0.5f, heightF);
        SpawnBrickAt(position, health);
    }

    public void SpawnBrickRowAt(int height, int length)
    {

        height = Mathf.Min(maxHeight, height);
        height = maxHeight - height;
        float heightF = 1.5f * height;
        length = Mathf.Min(maxLength, length);
        for (int i = 0; i < length; i++) {
            float lengthF = 3.5f * i;
            Vector3 startPos = new Vector3(-7.0f + lengthF, 0.5f, heightF);
            SpawnBrickAt(startPos);
        }

    }

    public void SpawnBrickRowAt(int height, int length, int health)
    {

        height = Mathf.Min(maxHeight, height);
        height = maxHeight - height;
        float heightF = 1.5f * height;
        length = Mathf.Min(maxLength, length);
        for (int i = 0; i < length; i++) {
            float lengthF = 3.5f * i;
            Vector3 startPos = new Vector3(-7.0f + lengthF, 0.5f, heightF);
            SpawnBrickAt(startPos, health);
        }

    }

    public void DestroyBricks()
    {
        StopAllCoroutines();
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Brick");
        foreach (var item in gameObjects) {
            Destroy(item);
        }
        brickCount = 0;
    }

    public void SpawnEveryOtherRow(int startingHeight)
    {
        startingHeight = maxHeight - Mathf.Min(maxHeight, startingHeight);
        for (int i = startingHeight; i >= 0; i -= 2) {
            SpawnBrickRowAt(i, maxLength);
        }

    }

    public void SpawnEveryOtherRow(int startingHeight, int health)
    {
        startingHeight = maxHeight - Mathf.Min(maxHeight, startingHeight);
        for (int i = startingHeight; i >= 0; i -= 2) {
            SpawnBrickRowAt(i, maxLength, health);
        }

    }

    public void MoveAllBricksDown()
    {
        GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");
        foreach (var brick in bricks) {
            brick.transform.Translate(0, 0, -1.5f);
        }
    }

    IEnumerator MoveAndSpawn(int health)
    {
        while (true) {
            MoveAllBricksDown();
            SpawnBrickRowAt(0, 5, health);
            yield return new WaitForSeconds(6.0f);
        }

    }

    IEnumerator MoveAndSpawn()
    {
        while (true) {
            MoveAllBricksDown();
            SpawnBrickRowAt(0, 5);
            yield return new WaitForSeconds(10.0f);
        }

    }

    public void SpawnLevel(int levelSelector)
    {
        //brick addressing goes from top to bottom
        switch (levelSelector) {
            case 0: 
                ContinuousModeRandom();
                break;
            case 1: 
                SpawnBrickRowAt(6, 5);
                SpawnBrickRowAt(2, 5);
                SpawnBrickAt(0, 2);
                break;
            case 2:
                SpawnEveryOtherRow(1);
                break;
            case 3:
                SpawnEveryOtherRow(0);
                SpawnBrickAt(1, 1);
                SpawnBrickAt(1, 3);
                break;


        }
    }

    public void SpawnLevel(int levelSelector, int health)
    {
        //brick addressing goes from top to bottom
        switch (levelSelector) {
            case 0: //every other row starting at 0
                ContinuousMode(health);
                break;
            case 1: //ever other row starting at 1
                SpawnBrickRowAt(6, 5, health);
                SpawnBrickRowAt(2, 5, health);
                SpawnBrickAt(0, 2, health);
                break;
            case 2:
                SpawnEveryOtherRow(1, health);
                break;
            case 3:
                SpawnEveryOtherRow(0, health);
                SpawnBrickAt(1, 1, health);
                SpawnBrickAt(1, 3, health);
                break;


        }
    }
}
