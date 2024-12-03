using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;

    void Start()
    {
        
    }


    void Update()
    {
        Movement();
        Clamp();
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
}
