using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float shootRatio;
    [SerializeField] private Bullet_Player bulletPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private TextMeshProUGUI lifesText;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject deathParticles;
    [SerializeField] private GameObject gameOverMenu;

    private float timer;
    private AudioSource audioSource;
    private int lifes;
    private int gold;
    private bool shieldActive;

    private ObjectPool<Bullet_Player> bulletPool;

    private System.Action shootToExecute;

    void Awake()
    {
        bulletPool = new ObjectPool<Bullet_Player> (CreateBullet, null, ReleaseBullet, DestroyBullet);
    }

    void Start()
    {
        if(PlayerPrefs.GetInt("x3") == 1) 
        {
            shootToExecute = TripleShoot;
        }
        else if(PlayerPrefs.GetInt("x2") == 1) 
        {
            shootToExecute = DoubleShoot;
        }
        else 
        {
            shootToExecute = SimpleShoot;
        }

        shieldActive = false;
        lifes = PlayerPrefs.GetInt("Lifes");
        gold = PlayerPrefs.GetInt("Gold");
        timer = 0.5f;
        audioSource = GetComponent<AudioSource>();
    }

    private Bullet_Player CreateBullet()
    {
        Bullet_Player bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.Pool = bulletPool;
        return bullet;
    }

    private void GetBullet(Bullet_Player bullet)
    {
        bullet.transform.position = spawnPoints[0].position;
        bullet.gameObject.SetActive(true);
    }

    private void ReleaseBullet(Bullet_Player bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void DestroyBullet(Bullet_Player bullet)
    {
        Destroy(bullet.gameObject);
    }


    void Update()
    {
        Movement();
        Clamp();
        Shoot();
    }

    void Movement() 
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0f).normalized * speed * Time.deltaTime);
    }

    void Clamp() 
    {
        float xClamped = Mathf.Clamp(transform.position.x, -8.5f, 8.45f);
        float yClamped = Mathf.Clamp(transform.position.y, -4.4f, 4.4f);
        transform.position = new Vector3(xClamped, yClamped, 0f);
    }

    void Shoot() 
    {
        timer += Time.deltaTime;

        if (Input.GetKey(KeyCode.Space) && timer > shootRatio) 
        {
            shootToExecute?.Invoke();
            audioSource.Play();
            timer = 0;
        }
    }

    void SimpleShoot() 
    {
        Bullet_Player bullet = bulletPool.Get();
        bullet.transform.position = spawnPoints[0].position;
        bullet.gameObject.SetActive(true);
    }

    void DoubleShoot() 
    {
        for (int i = 1; i <=2; i++) 
        {
            Bullet_Player bullet = bulletPool.Get();
            bullet.transform.position = spawnPoints[i].position;
            bullet.gameObject.SetActive(true);
        }

    }
    void TripleShoot()
    {
        for (int i = 0; i <= 2; i++)
        {
            Bullet_Player bullet = bulletPool.Get();
            bullet.transform.position = spawnPoints[i].position;
            bullet.gameObject.SetActive(true);
        }
    }

    void PowerUpFireSpeed() 
    {
        shootRatio = 0.3f;
    }

    void PowerUpSpeed() 
    {
        speed = 8f;
    }

    void UpdateLifes() 
    {
        lifesText.text = lifes.ToString();
    }

    void UpdateGold() 
    {
        goldText.text = gold.ToString();
    }

    IEnumerator TimerPowerUp(float time) 
    {
        yield return new WaitForSeconds(time);
        shootRatio = 0.5f;
        speed = 5f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("BulletEnemy"))
        {
            if (collision.CompareTag("BulletEnemy")) 
            {
                Destroy(collision.gameObject);
            }

            if (!shieldActive) 
            {
                lifes--;
                UpdateLifes();
                if (lifes <= 0) 
                {
                    Instantiate(deathParticles, transform.position, Quaternion.identity);
                    gameOverMenu.SetActive(true);
                    Destroy(this.gameObject);
                }
            }
            else 
            {
                shieldActive = false;
            }
        }

        if (collision.CompareTag("Gold")) 
        {
            Destroy(collision.gameObject);
            gold++;
            UpdateGold();
        }

        if (collision.CompareTag("PU_FireSpeed")) 
        {
            PowerUpFireSpeed();
            Destroy(collision.gameObject);
            StartCoroutine(TimerPowerUp(5f));
        }

        if (collision.CompareTag("PU_Shield"))
        {
            shield.SetActive(true);
            shieldActive = true;
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("PU_Speed")) 
        {
            PowerUpSpeed();
            Destroy(collision.gameObject);
            StartCoroutine(TimerPowerUp(5f));
        }
    }
}
