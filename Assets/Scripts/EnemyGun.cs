using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;

    private Transform firePoint;
    public float projectileSpeed = 20f;

    public float minProjectileDelay = 3.0f;
    public float maxProjectileDelay = 5.0f;
    private float projectileDelay;
    private float lastShotTime;
    private AudioSource _audioSource;

    void Start()
    {
        firePoint = GetComponent<Transform>();
        _audioSource = GetComponent<AudioSource>();
        ResetProjectileDelay();
    }

    void Update()
    {
        if (Time.time - lastShotTime >= projectileDelay)
        {
            Fire();
            ResetProjectileDelay();
            lastShotTime = Time.time;
        }
    }

    void Fire()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        Transform projectileTransform = projectile.transform;

        Vector3 direction = -projectileTransform.up; 

        float deltaTime = Time.deltaTime;
        projectileTransform.position += direction * projectileSpeed * deltaTime;
        _audioSource.PlayOneShot(_audioSource.clip); 
    }

    void ResetProjectileDelay()
    {
        projectileDelay = Random.Range(minProjectileDelay, maxProjectileDelay);
    }
}
