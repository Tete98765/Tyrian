using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentProps : MonoBehaviour
{

    public static EnvironmentProps Instance { get; private set; }
    //delay between two shots from of geometry gun
    [SerializeField]
    private float shootingInterval;
    public float ShootingInterval { get { return shootingInterval; } }
    public float sizeX;
    public float sizeZ;
    public float minX() { return -sizeX / 2.0f; }
    public float maxX() { return sizeX / 2.0f; }
    public float minZ() { return -sizeZ / 2.0f; }
    public float maxZ() { return sizeZ / 2.0f; }

    // speed of all projectiles in game
    [SerializeField]
    private float projectileSpeed = 5.0f;
    public float ProjectileSpeed { get { return projectileSpeed; } }
    public void Awake()
    {
        // Check, if we do not have any instance yet.
        if (Instance == null)
        {
            // 'this' is the first instance created => save it.
            Instance = this;
        }
        else if (Instance != this)
        {
            // Destroy 'this' object as there exist another instance
            Destroy(this.gameObject);
        }
    }

    /*public Vector3 IntoArea(Vector3 pos, float dx)
    {
        Vector3 result = pos;
        result.x = result.x - dx < minX() ? minX() + dx : result.x;
        result.x = result.x + dx > maxX() ? maxX() - dx : result.x;

        result.z = result.z - dx < minZ() ? minZ() + dx : result.z;
        result.z = result.z + dx > maxZ() ? maxZ() - dx : result.z;
        return result;
    }*/

    public Vector3 IntoArea(Vector3 pos, float dx, float dz)
    {
        Vector3 result = pos;
        result.x = result.x - dx < minX() ? minX() + dx : result.x;
        result.x = result.x + dx > maxX() ? maxX() - dx : result.x;

        result.z = result.z - dz < minZ() ? minZ() + dx : result.z;
        result.z = result.z + dz > maxZ() ? maxZ() - dx : result.z;

        return result;
    }


    public bool EscapedBelow(Vector3 pos, float dz)
    {
        return pos.z + dz < minZ();
    }

    public bool EscapedAbove(Vector3 pos, float dz)
    {
        return pos.z + dz > maxZ();
    }

    public bool EscapeBorderMin(Vector3 pos, float dx) {
        return pos.x + dx < minX();
    }

    public bool EscapeBorderMax(Vector3 pos, float dx) {
        return pos.x + dx > maxX();
    }

    public bool IsOutsideArea(Vector3 pos)
    {
        return pos.x < minX() || pos.x > maxX() || pos.z < minZ() || pos.z > maxX();
    }

}
