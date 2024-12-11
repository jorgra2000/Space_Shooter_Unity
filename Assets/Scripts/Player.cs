using System.Collections;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float shootRatio;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform spawnPoint;
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

    void Start()
    {
        shieldActive = false;
        lifes = PlayerPrefs.GetInt("Lifes");
        gold = PlayerPrefs.GetInt("Gold");
        timer = 0.5f;
        audioSource = GetComponent<AudioSource>();
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
            Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
            audioSource.Play();
            timer = 0;
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
