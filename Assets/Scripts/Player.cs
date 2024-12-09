using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float shootRatio;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private TextMeshProUGUI lifesText;

    private float timer;
    private AudioSource audioSource;
    private int lifes;


    void Start()
    {
        lifes = 3;
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

    void UpdateLifes() 
    {
        lifesText.text = lifes.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            lifes--;
            UpdateLifes();
        }
    }
}
