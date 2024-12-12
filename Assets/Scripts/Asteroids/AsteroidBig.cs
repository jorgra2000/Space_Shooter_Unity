using UnityEngine;

public class AsteroidBig : Asteroid
{
    [SerializeField] private GameObject asteroidPrefab;

    void Update()
    {
        Movement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BulletPlayer") || collision.CompareTag("Player") || collision.CompareTag("Shield"))
        {
            if (collision.CompareTag("BulletPlayer"))
            {
                Instantiate(asteroidPrefab, this.transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
                Instantiate(asteroidPrefab, this.transform.position - new Vector3(0f, 0.5f, 0f), Quaternion.identity);
                collision.GetComponent<Bullet_Player>().Pool.Release(collision.GetComponent<Bullet_Player>());
            }

            if (collision.CompareTag("Shield"))
                collision.gameObject.SetActive(false);

            StartCoroutine(DestroyAsteroid());
        }
    }
}
