using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firework : Explosive
{
    // Start is called before the first frame update
  public ParticleSystem explosionPrefab;

    private void Update()
    {
        if (transform.position.y > 22)
        {
            ParticleSystem explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(explosion.gameObject, explosion.main.duration);
            Destroy(gameObject);
        }

    }
}
