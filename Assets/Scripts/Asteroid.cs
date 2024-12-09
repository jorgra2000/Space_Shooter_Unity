using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;

    void Update()
    {
        Movement();
    }

    protected void Movement() 
    {
        transform.Translate(Vector3.left * movementSpeed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BulletPlayer")) 
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
