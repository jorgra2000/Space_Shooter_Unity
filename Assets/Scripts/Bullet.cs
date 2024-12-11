using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 direction;

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
