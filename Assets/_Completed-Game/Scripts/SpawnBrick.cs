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

    public void SpawnBrickAt(Vector3 position)
    {
        GameObject brick = Instantiate(Brick);
        brick.transform.position = position;
        brickCount += brick.GetComponent<SetColor>().health;
    }

    public void SpawnBrickRowAt(int height, int length)
    {
        
        height = Mathf.Min(maxHeight, height);
        height = maxHeight - height;
        float heightF = 1.5f * height;
        length = Mathf.Min(maxLength, length);
        for (int i = 0; i < length; i++)
        {
            float lengthF = 3.5f * i;
            Vector3 startPos = new Vector3(-7.0f + lengthF, 0.5f, heightF);
            SpawnBrickAt(startPos);
        }
        
    }

    public void DestroyBricks()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Brick");
        foreach (var item in gameObjects)
        {
            Destroy(item);
        }
        brickCount = 0;
    }
}
