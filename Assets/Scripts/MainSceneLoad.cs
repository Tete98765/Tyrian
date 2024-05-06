using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneLoad : MonoBehaviour
{
    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadMainMenu()
    {
        _audioSource.PlayOneShot(_audioSource.clip);
        //ScoreManager.Instance.SetUp();
        ShipController.score = 0;
        ShipController.credits = 0;
        ShipController.totalKillEnemies = 0;
        SceneManager.LoadScene("Menu");
    }
}
