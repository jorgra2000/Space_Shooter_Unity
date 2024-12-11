using UnityEngine;

public class Enemy_Circle : Enemy
{
    [SerializeField] private float circleSpeed;
    public float circleRadius;

    private float elapsedTime;

    void Start()
    {
        elapsedTime = 0f;
        StartCoroutine(ShootTimer());
    }

    void Update()
    {
        MovementCircle();
    }

    void MovementCircle()
    {
        elapsedTime += Time.deltaTime;

        float xOffset = Mathf.Cos(elapsedTime * circleSpeed) * circleRadius;
        float yOffset = Mathf.Sin(elapsedTime * circleSpeed) * circleRadius;

        float leftMovement = -Speed;

        transform.Translate(new Vector3(leftMovement + xOffset , yOffset, 0f) * Time.deltaTime);
    }
}
