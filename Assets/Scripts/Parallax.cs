using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 direction;
    [SerializeField] private float widthImage;

    private Vector3 startPosition;
    
    void Start()
    {
        startPosition = transform.position;
    }

    
    void Update()
    {
        float module = (speed * Time.time) % widthImage;

        transform.position = startPosition + module * direction;
    }
}
