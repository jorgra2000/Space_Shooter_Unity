using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 direction;

    private float timer = 0f;

    void Update()
    {
        Movement();

        timer += Time.deltaTime;

        if(timer > 4f) 
        {
            Destroy(gameObject);
            timer = 0f;
        }
    }

    protected void Movement() 
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
