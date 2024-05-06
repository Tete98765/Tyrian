using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilController : MonoBehaviour
{
    private float _speed = 20.0f;
    private float _radius = 0.1f;

    [SerializeField]
    private float collisionDamage;
    
    void Start()
    {
        transform.Rotate(new Vector3(90, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 0, _speed * Time.deltaTime);

        if (EnvironmentProps.Instance.EscapedAbove(transform.position, _radius))
        {
            Destroy(this.gameObject);
        }
    }

    public void Set(float speed, float radius)
    {
        _speed = speed;
        _radius = radius;
        transform.localScale = new Vector3(_radius, _radius, _radius);
    }

    //enemy + player projectile 
    //coef - score 5 credits 1
    void OnCollisionEnter(Collision other) {
        PlayExplosion.PlayDestroyAudio(); 
        Health healthScript = other.gameObject.GetComponent<Health>();

        if (healthScript != null)
        {
            // Access the health script and deal damage
            healthScript.DealDamage(collisionDamage);
            ShipController.score += (int)(5 * collisionDamage);
            ShipController.credits += (int)(1 * collisionDamage);

            //enemy is dead
            //coef score 10 credits 2
            if(healthScript._currentHealth <= 0) {
                ShipController.score += (int)(10 * collisionDamage);
                ShipController.credits += (int)(2 * collisionDamage);
            }
            // Destroy the projectile
            Destroy(gameObject);
        }
        else if(other.gameObject.tag == "Meteor") {
            ShipController.totalDestroyedMeteors++;
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }

}
