using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesFactory : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> enemyPrefab;
    [SerializeField]
    private float delayMin;
    [SerializeField]
    private float delayMax;
    [SerializeField]
    private float _enemyRadius;
    [SerializeField]
    private float _enemySpeed;
    // delay from last spawn
    private float _delay;

    [SerializeField]
    private int enemyMax;
    public static int enemiesNum;

    void Start()
    {
        _delay = 0;
        enemiesNum = 0;
    }
    

    void Update()
    {
        // time elapsed from previous frame
        _delay -= Time.deltaTime;
        int prefabIndex = Random.Range(0, 3);
        if (_delay > 0.0f)
        return;

        if(enemiesNum < enemyMax) {
            enemiesNum++;
            //horizontal
            float x = Random.Range(
                EnvironmentProps.Instance.minX() + _enemyRadius,
                EnvironmentProps.Instance.maxX() - _enemyRadius
            );
            //vertical
            float z = EnvironmentProps.Instance.maxZ() + _enemyRadius;
            _delay = Random.Range(delayMin, delayMax);

            var enemyGO = Instantiate(enemyPrefab[prefabIndex], new Vector3(x, 0, z),
            Quaternion.identity);
            
            var enemyContr = enemyGO.GetComponent<EnemyController>();
            if (enemyContr != null)
            {
                enemyContr.Set(_enemySpeed, _enemyRadius);
            }
            else
            {
                Debug.LogError("Missing EnemyController component");
            }
        }
    }
}
