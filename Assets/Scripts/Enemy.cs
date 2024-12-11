using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int lifes;
    [SerializeField] private int speed;
    [SerializeField] private float shootRatio;
    [SerializeField] GameObject bulletEnemyPrefab;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private SpriteRenderer skin;
    [SerializeField] private GameObject spawnPoint;

    private bool isAlive = true;

    public int Speed { get => speed; set => speed = value; }

    void Start()
    {
        StartCoroutine(ShootTimer());
    }

    
    void Update()
    {
        Movement();
    }

    protected IEnumerator ShootTimer() 
    {
        while (isAlive) 
        {
            yield return new WaitForSeconds(shootRatio);
            Shoot();
        }

    }

    protected void Movement()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    protected void PlayParticles()
    {
        particles.Play();
    }

    protected void Shoot() 
    {
        Instantiate(bulletEnemyPrefab, spawnPoint.transform.position, Quaternion.identity);
    }

    protected IEnumerator DestroyEnemy()
    {
        isAlive = false;
        PlayParticles();
        GetComponent<CircleCollider2D>().enabled = false;
        skin.enabled = false;
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("BulletPlayer")) 
        {
           Destroy(collision.gameObject);

           if (lifes <= 1)
           {
              StartCoroutine(DestroyEnemy());
           }
           else
           {
              lifes--;
           }
        }

        if (collision.CompareTag("Player"))
        {
            StartCoroutine(DestroyEnemy());
        }

        if (collision.CompareTag("Shield")) 
        {
            StartCoroutine(DestroyEnemy());
            collision.gameObject.SetActive(false);
        }


    }
}
