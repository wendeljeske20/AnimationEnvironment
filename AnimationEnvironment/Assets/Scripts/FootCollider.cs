using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootCollider : MonoBehaviour
{
    public Player player;
    bool onGround;
    private void Update()
    {

        player.onGround = onGround;
        onGround = false;
    }
    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Bomb"))
            onGround = true;
    }

    // void OnTriggerExit(Collider other)
    // {
    //     onGround = false;
    // }
}
