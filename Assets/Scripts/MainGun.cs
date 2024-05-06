using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGun : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;
    
    private Transform firePoint;
    public float projectileSpeed = 20f;

    public float initialProjectileDelay = 0.25f;
    private float projectileDelay;
    private float lastShotTime; 

    private AudioSource _audioSource;

    void Start()
    {
        firePoint = GetComponent<Transform>();
        projectileDelay = initialProjectileDelay;
        lastShotTime = 0f;
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (Time.time - lastShotTime >= projectileDelay)
            {
                Fire();
                lastShotTime = Time.time;
            }
        }

        #if UNITY_IOS || UNITY_ANDROID
        if (Time.time - lastShotTime >= projectileDelay)
        {
            Fire();
            lastShotTime = Time.time;
        }
        #endif
    }

    void Fire()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        Transform projectileTransform = projectile.transform;

        Vector3 direction = projectileTransform.up;

        float deltaTime = Time.deltaTime;
        projectileTransform.position += direction * projectileSpeed * deltaTime;
        _audioSource.PlayOneShot(_audioSource.clip); 
    }
}
