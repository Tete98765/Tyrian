using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float _speed = 20.0f;
    private float _radius = 1.0f;
    private bool _movingRight = true;

    [SerializeField]
    private int collisionDamage;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = _movingRight ? _speed : -_speed;
        transform.position += new Vector3(moveX * Time.deltaTime, 0, 0);

        if (_movingRight && EnvironmentProps.Instance.EscapeBorderMax(transform.position, _radius))
        {
            _movingRight = false;
            transform.position = new Vector3(EnvironmentProps.Instance.maxX() - _radius, transform.position.y, transform.position.z - 1);
        }
        else if (!_movingRight && EnvironmentProps.Instance.EscapeBorderMin(transform.position, _radius))
        {
            _movingRight = true;
            transform.position = new Vector3(EnvironmentProps.Instance.minX() + _radius, transform.position.y, transform.position.z - 1);
        }

        if (EnvironmentProps.Instance.EscapedBelow(transform.position, _radius))
        {
            Destroy(this.gameObject);
            EnemiesFactory.enemiesNum--;
        }        

    }

    public void Set(float speed, float radius)
    {
        _speed = speed;
        _radius = radius;
        transform.localScale = new Vector3(_radius, _radius, _radius);
    }

    private void OnCollisionEnter(Collision other) {
        Health healthScript = other.gameObject.GetComponent<Health>();
        PlayExplosion.PlayDestroyAudio();

        if (healthScript != null)
        {
            // Access the health script and deal damage
            healthScript.DealDamage(collisionDamage);
        }
    }
}
