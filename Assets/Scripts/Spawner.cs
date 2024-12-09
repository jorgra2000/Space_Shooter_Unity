using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    
    void Update()
    {
        
    }

    IEnumerator SpawnEnemies() 
    {
        while (true)
        {
            Vector3 randomPosition = new Vector3(transform.position.x, Random.Range(-4.4f, 4.4f), 0f);
            Instantiate(enemy, randomPosition, Quaternion.identity);
            yield return new WaitForSeconds(1.5f);
        }

    }
}
