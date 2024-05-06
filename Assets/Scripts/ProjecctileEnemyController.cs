using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjecctileEnemyController : MonoBehaviour
{
    private float _radius = 1.0f;

    [SerializeField]
    private float Damage;

    void Start()
    {
        //transform.Rotate(new Vector3(0, -90, 90));
        transform.rotation = Quaternion.Euler(0, -90, 90);
    }

    // Update is called once per frame
    void Update()
    {
       transform.position += Vector3.back * EnvironmentProps.Instance.ProjectileSpeed * Time.deltaTime;
       if (EnvironmentProps.Instance.EscapedBelow(transform.position, _radius))
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter(Collision other) {
        PlayExplosion.PlayDestroyAudio();
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
