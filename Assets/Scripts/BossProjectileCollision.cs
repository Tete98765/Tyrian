using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectileCollision : MonoBehaviour
{

    public static float Damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other) {
        Health healthScript = other.gameObject.GetComponent<Health>();

        if (healthScript != null)
        {
            // Access the health script and deal damage
            healthScript.DealDamage(Damage);
            // Destroy the projectile
            Destroy(gameObject);
        }
    }
}
