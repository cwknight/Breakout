using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBall : MonoBehaviour
{
    public GameObject ballPrefab;
    public Vector3 startPosition;
    public GameObject Ball;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Spawn()
    {
        Ball = Instantiate(ballPrefab);
        Ball.transform.position = startPosition;

    }

    public void DestroyBall()
    {
        Destroy(Ball);
    }
}
