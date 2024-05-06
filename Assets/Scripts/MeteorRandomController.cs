using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorRandomController : MonoBehaviour
{
    [SerializeField] private GameObject meteorPrefab;
    [SerializeField] private float delayMin;
    [SerializeField] private float delayMax;

    private float _delay;

    void Start()
    {
        _delay = 0;
    }

    void Update()
    {
        _delay -= Time.deltaTime;
        if (_delay > 0.0f)
            return;

        float x = Random.Range(
            EnvironmentProps.Instance.minX(),
            EnvironmentProps.Instance.maxX()
        );

        float z = EnvironmentProps.Instance.maxZ();

        float meteorRadius = Random.Range(0.5f, 2.0f);
        float meteorSpeed = Random.Range(10.0f, 30.0f);
        _delay = Random.Range(delayMin, delayMax);
        var meteorGO = Instantiate(meteorPrefab, new Vector3(x, 0, z), Quaternion.identity);
        var meteorContr = meteorGO.GetComponent<MeteorController>();

        if (meteorContr != null)
        {
            meteorContr.Set(meteorSpeed, meteorRadius);
        }
        else
        {
            Debug.LogError("Missing MeteorController component");
        }

        
    }
}
