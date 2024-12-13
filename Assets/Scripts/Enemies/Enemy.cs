using System.Collections;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [SerializeField] private int lifes;
    [SerializeField] private float speed;
    [SerializeField] private float shootRatio;
    [SerializeField] GameObject bulletEnemyPrefab;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private SpriteRenderer skin;
    [SerializeField] private GameObject[] spawnPoint;
    [SerializeField] private GameObject[] powerUps;

    private bool isAlive = true;
    private float timer = 0f;

    public float Speed { get => speed; set => speed = value; }

    void Start()
    {
        StartCoroutine(ShootTimer());
    }

    
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

    protected IEnumerator ShootTimer() 
    {
        while (isAlive) 
        {
            Shoot();
            yield return new WaitForSeconds(shootRatio);
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
        for (int i = 0; i < spawnPoint.Length; i++) 
        {
            Instantiate(bulletEnemyPrefab, spawnPoint[i].transform.position, spawnPoint[i].transform.rotation);
        }
        
    }

    protected IEnumerator DestroyEnemy()
    {
        float loot = Random.Range(0, 1f);
        if (loot > 0.8f) 
        {
            int randomPowerUp = Random.Range(0, 3);
            Instantiate(powerUps[randomPowerUp], transform.position, Quaternion.identity);
        }
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

           collision.GetComponent<Bullet_Player>().Pool.Release(collision.GetComponent<Bullet_Player>());

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
