using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet_Player : Bullet
{
    private ObjectPool<Bullet_Player> pool;
    private float timerRelease = 0f;

    public ObjectPool<Bullet_Player> Pool { get => pool; set => pool = value; }

    // Update is called once per frame
    void Update()
    {
        Movement();

        timerRelease += Time.deltaTime;

        if (timerRelease > 4f)
        {
            pool.Release(this);
            timerRelease = 0f;
        }
    }
}
