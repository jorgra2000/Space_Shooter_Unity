using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private GameObject goldPrefab;

    void Update()
    {
        Movement();
    }

    protected void Movement() 
    {
        transform.Translate(Vector3.left * movementSpeed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    protected IEnumerator DestroyAsteroid() 
    {
        PlayParticles();
        Instantiate(goldPrefab, transform.position, Quaternion.identity);
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
            if(collision.CompareTag("BulletPlayer"))
                Destroy(collision.gameObject);

            if(collision.CompareTag("Shield"))
                collision.gameObject.SetActive(false);

            StartCoroutine(DestroyAsteroid());
        }
    }
}
