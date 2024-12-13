using System.Collections;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private GameObject goldPrefab;
    [SerializeField] private bool isBig;

    private float timer = 0f;

    void Update()
    {
        Movement();

        timer += Time.deltaTime;

        if (timer > 10f)
        {
            Destroy(gameObject);
            timer = 0f;
        }
    }

    protected void Movement() 
    {
        transform.Translate(Vector3.left * movementSpeed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    protected IEnumerator DestroyAsteroid() 
    {
        PlayParticles();
        if (!isBig) 
        {
            Instantiate(goldPrefab, transform.position, Quaternion.identity);
        }
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }

    protected void PlayParticles() 
    {
        particles.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BulletPlayer") || collision.CompareTag("Player") || collision.CompareTag("Shield"))
        {
            if (collision.CompareTag("BulletPlayer"))
                collision.GetComponent<Bullet_Player>().Pool.Release(collision.GetComponent<Bullet_Player>());

            if(collision.CompareTag("Shield"))
                collision.gameObject.SetActive(false);

            StartCoroutine(DestroyAsteroid());
        }
    }
}
