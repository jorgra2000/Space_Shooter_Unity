using UnityEngine;

public class AsteroidBig : Asteroid
{
    [SerializeField] private GameObject asteroidPrefab;

    void Start()
    {
        
    }

    void Update()
    {
        Movement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BulletPlayer") || collision.CompareTag("Player"))
        {

            DestroyAsteroid();
            if (collision.CompareTag("BulletPlayer"))
            {
                Instantiate(asteroidPrefab, this.transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
                Instantiate(asteroidPrefab, this.transform.position - new Vector3(0f, 0.5f, 0f), Quaternion.identity);
            }

            PlayParticles();
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            DestroyAsteroid();
        }
    }
}
