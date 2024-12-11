using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Sin : Enemy
{

    [SerializeField] private float amplitude;
    [SerializeField] private float verticalSpeed;

    private Vector3 direction;
    private float elapsedTime;

    void Start()
    {
        elapsedTime = 0f;
        direction = Vector3.up;
        StartCoroutine(ShootTimer());
    }

    void Update()
    {
        MovementSin();
    }

    void MovementSin()
    {
        elapsedTime += Time.deltaTime;

        float yOffset = Mathf.Sin(elapsedTime * verticalSpeed) * amplitude;

        Vector3 leftMovement = Vector3.left * Speed * Time.deltaTime;

        Vector3 sinMovement = new Vector3(0f, yOffset, 0f) * Time.deltaTime;

        transform.Translate(leftMovement + sinMovement);
    }
}
