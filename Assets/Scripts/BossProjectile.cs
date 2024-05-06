using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float speed = 10.0f;
    private Vector3 velocity = Vector3.zero;

    public static GameObject projectilePrefab;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = GameUtils.Instance.ComputeEulerStep(
        transform.position, velocity, Time.deltaTime);
        if (EnvironmentProps.Instance.IsOutsideArea(transform.position))
            Destroy(gameObject);
    }

    /*public static BossProjectile Instantiate(
        Vector3 pos, Vector3 gunVelocity, Vector3 gunUnitAimingDir)
    {
        // First we create a default sphere GO.
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        // We are not concerned with collisions in this tutorial.
        Destroy(sphere.GetComponent<SphereCollider>());
        // Next we add the Projectile component.
        BossProjectile self = sphere.AddComponent<BossProjectile>();
        // And we set the position and velocity
        self.transform.position = pos;
        self.velocity = gunVelocity + self.speed * gunUnitAimingDir;
        return self;
    }*/


    public static BossProjectile Instantiate(
        Vector3 pos, Vector3 gunVelocity, Vector3 gunUnitAimingDir)
    {
        if (projectilePrefab == null)
        {
            Debug.LogError("Projectile prefab is not assigned!");
            return null;
        }

        GameObject projectileGO = GameObject.Instantiate(projectilePrefab, pos, Quaternion.identity);
        projectileGO.transform.Rotate(new Vector3(-90, 0, 0));
        BossProjectile self = projectileGO.AddComponent<BossProjectile>();

        PlayExplosion.PlayDestroyAudio();
        self.velocity = gunVelocity + self.speed * gunUnitAimingDir;
        return self;
    }

}
