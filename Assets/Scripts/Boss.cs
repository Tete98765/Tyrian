using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    enum State
    {
        ENTER_GAME_ZONE,
        ATTACK,
        RETREAT
    }
    private State activeState = State.ENTER_GAME_ZONE;

    public float minReactionDelay = 0.1f;
    public float maxReactionDelay = 0.2f;
    private float reactionDelay = 0.0f;

    private bool gameZoneEntered = false;
    public int NumShotsToCooldown = 7;
    private int numShots = 0;

    public float firePointShiftZ = 5.0f;
    private Vector3 enemyPosition = Vector3.zero;

    public float maxSpeed = 8;
    public float maxAccel = 25;
    public Vector3 velocity = Vector3.zero;
    private BoxCollider collider;
    private Transform targetTransform;

    public float ReloadSeconds = 0.3f;
    private float reload = 0.0f;
    public float CooldownSeconds = 5.0f;
    private float cooldown;
    private Transform gun;
    private bool shotIsReady = true;

    public float PowerShotCooldown = 10.0f;
    private float powerShotCooldownTimer = 0.0f;
    public int PowerShotProjectiles = 5;
    private bool powerShotReady = true;

    [SerializeField]
    private GameObject rotatingObject; 
    private float rotationSpeed;

    [SerializeField]
    private int collisionDamage;

    public GameObject projectilePrefab;

    void Awake()
    {
        collider = GetComponent<BoxCollider>();
        BossProjectile.projectilePrefab = projectilePrefab;
    }

    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = Random.Range(-180, 180);
        gun = transform.Find("Gun");
        cooldown = CooldownSeconds;
        GameObject targetObject = GameObject.FindGameObjectWithTag("Player");
        targetTransform = targetObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        rotatingObject.transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);

        reactionDelay -= Time.deltaTime;
        if (reactionDelay <= 0.0f)
        {
            reactionDelay = Random.Range(minReactionDelay, maxReactionDelay);
            Perception();
            SelectState();
        }

        ProcessGunTimers();

        switch (activeState)
        {
            case State.ENTER_GAME_ZONE:
                Process_ENTER_GAME_ZONE();
                break;
            case State.ATTACK:
                Process_ATTACK();
                break;
            case State.RETREAT:
                Process_RETREAT();
                break;
            default: Debug.Assert(false); break;
        }
    }

    private void Perception()
    {
        enemyPosition = targetTransform.position;
    }
    private void SelectState()
    {
        switch (activeState)
        {
            case State.ENTER_GAME_ZONE:
                if (gameZoneEntered)
                    activeState = numShots < NumShotsToCooldown ?
                    State.ATTACK : State.RETREAT;
                break;
            case State.ATTACK:
                //activeState = shotIsReady == true ? State.ATTACK : State.RETREAT;
                activeState = !shotIsReady || !powerShotReady ? State.RETREAT : State.ATTACK;
                break;
            case State.RETREAT:
                //activeState = shotIsReady == true ? State.ATTACK : State.RETREAT;
                activeState = !shotIsReady || !powerShotReady ? State.RETREAT : State.ATTACK;
                break;
            default: Debug.Assert(false); break;
        }
    }
    private void Process_ENTER_GAME_ZONE()
    {
        EnvironmentProps env = EnvironmentProps.Instance;
        Vector3 target = new Vector3(
            0.5f * (env.minX() + env.maxX()),
            0.0f,
            env.minZ() + 0.75f * (env.maxZ() - env.minZ())
            );

        velocity = GameUtils.Instance.ComputeSeekVelocity(
            transform.position, velocity,
            maxSpeed, maxAccel,
            target, Time.deltaTime);
        transform.position = GameUtils.Instance.ComputeEulerStep(
            transform.position, velocity, Time.deltaTime);

        if ((target - transform.position).magnitude < 1.0f)
            gameZoneEntered = true;

    }
    private void Process_ATTACK()
    {
        velocity = GameUtils.Instance.ComputeSeekVelocity(
            transform.position, velocity,
            maxSpeed, maxAccel,
            targetTransform.position + new Vector3(0, 0, firePointShiftZ),
            Time.deltaTime
            );

        Vector3 pos = GameUtils.Instance.ComputeEulerStep(
            transform.position, velocity, Time.deltaTime);

        transform.position = EnvironmentProps.Instance.IntoArea(
            pos, 0.5f * collider.size.x, 0.5f * collider.size.z);

        Shoot();
    }
    private void Process_RETREAT()
    {
        EnvironmentProps env = EnvironmentProps.Instance;
        Vector3 target = new Vector3(
            env.minX() +
                (enemyPosition.x < transform.position.x ? 0.8f : 0.2f) *
                (env.maxX() - env.minX()),
            0,
            env.minZ() + 0.8f * (env.maxZ() - env.minZ())
            );

        velocity = GameUtils.Instance.ComputeSeekVelocity(
            transform.position, velocity,
            maxSpeed, maxAccel,
            target, Time.deltaTime);

        Vector3 pos = GameUtils.Instance.ComputeEulerStep(
            transform.position, velocity, Time.deltaTime);

        transform.position = EnvironmentProps.Instance.IntoArea(
            pos, 0.5f * collider.size.x, 0.5f * collider.size.z);
    }
    private void ProcessGunTimers()
    {
        ////////
        if (powerShotCooldownTimer > 0.0f)
        {
            powerShotCooldownTimer -= Time.deltaTime;
            powerShotReady = false;
            if (powerShotCooldownTimer <= 0.0f)
            {
                powerShotCooldownTimer = 0.0f;
                powerShotReady = true;
            }
        }
    //////
        //if (numShots == NumShotsToCooldown)
        if(shotIsReady == false)
        {
            cooldown -= Time.deltaTime;
            if (cooldown <= 0.0f)
            {
                cooldown = CooldownSeconds;
                reload = 0.0f;
                numShots = 0;
                shotIsReady = true;
            }
        }
        else if (reload > 0.0f)
            reload -= Time.deltaTime;
    }
    private void Shoot()
    {
        float distance = Vector3.Distance (targetTransform.position, this.transform.position);
        if (powerShotReady) {
            if(distance < 5 && distance > 4) {
                float angleStep = 360f / PowerShotProjectiles;
                float angle = 0f;
                for (int i = 0; i < PowerShotProjectiles; i++)
                {
                    Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward;
                    BossProjectile.Instantiate(
                        gun.position,
                        velocity,
                        direction
                    );
                    angle += angleStep;
                    BossProjectileCollision.Damage = 10;
                }
                powerShotCooldownTimer = PowerShotCooldown;
                powerShotReady = false;
            } 
        }
        //short attack - proste striela ked je blizko
        if(distance < 3 && shotIsReady) {
            if (reload <= 0.0f)
            {
                Vector3 horizontalVelocity =
                    Vector3.Dot(velocity, Vector3.right) * Vector3.right;
                BossProjectile.Instantiate(
                    gun.position,
                    horizontalVelocity,
                    Matrix4x4.Rotate(gun.rotation).MultiplyVector(
                        new Vector3(0, 0, 1)
                    )
                );

                //++numShots;
                BossProjectileCollision.Damage = 3;
                reload = ReloadSeconds;
            } 
        }
        //long attack - vystrelim jednu silnu strelu a prestanem strielat
        else {
            if (reload <= 0.0f)
            {
                Vector3 horizontalVelocity =
                    Vector3.Dot(velocity, Vector3.right) * Vector3.right;
                BossProjectile.Instantiate(
                    gun.position,
                    horizontalVelocity,
                    Matrix4x4.Rotate(gun.rotation).MultiplyVector(
                        new Vector3(0, 0, 1)
                    )
                );

                ++numShots;
                BossProjectileCollision.Damage = 5;
                shotIsReady = false;
                reload = ReloadSeconds;
            } 
        }
             
    }

    private void OnCollisionEnter(Collision other) {
        PlayExplosion.PlayDestroyAudio();
        Health healthScript = other.gameObject.GetComponent<Health>();

        if (healthScript != null)
        {
            // Access the health script and deal damage
            healthScript.DealDamage(collisionDamage);
        }
    }

}
