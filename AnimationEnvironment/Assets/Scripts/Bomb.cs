using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Explosive
{
    public ParticleSystem explosionPrefab;


    private void Update()
    {
        if (transform.position.y < -10)
            Destroy(gameObject);
    }
    void OnCollisionEnter(Collision other)
    {
        ParticleSystem explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion.gameObject, explosion.main.duration);

        Destroy(gameObject);
    }


}
