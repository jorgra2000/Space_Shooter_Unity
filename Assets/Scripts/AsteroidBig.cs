using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBig : Asteroid
{
    [SerializeField] private GameObject asteroidPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BulletPlayer"))
        {
            Destroy(collision.gameObject);
            Instantiate(asteroidPrefab ,this.transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
            Instantiate(asteroidPrefab, this.transform.position - new Vector3(0f, 0.5f, 0f), Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
