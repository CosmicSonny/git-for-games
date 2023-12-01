using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject ballprefab;
    public float spawnTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnBalls", 0f, spawnTime);
    }

    void SpawnBalls()
    {
        Instantiate(ballprefab, transform.position, Quaternion.identity);
    }
}
