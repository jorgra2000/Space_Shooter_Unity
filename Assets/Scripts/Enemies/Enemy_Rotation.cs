using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Rotation : Enemy
{
    [SerializeField] private float rotationSpeed; 

    void Start()
    {
        StartCoroutine(ShootTimer());
    }

    void Update()
    {
        MovementRotation();
    }

    void MovementRotation() 
    {
        transform.Translate(Vector3.left * Speed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
