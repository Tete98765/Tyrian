using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour
{
    /*void Update()
    {
        if (Input.GetKeyUp(KeyCode.N))
        {
            Debug.Log(SceneManager.GetActiveScene().buildIndex + "index" + SceneManager.sceneCountInBuildSettings + "total");
            if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1) {
                Debug.Log("No more levels");
            }
            else {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            }
        }

    }*/
    [SerializeField] private List<string> allLevels = new List<string>();

    private int currentLevelIndex = -1;

    private void Start()
    {
        
        int totalScenes = SceneManager.sceneCountInBuildSettings;
        for (int i = 0; i < totalScenes; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            if (sceneName.StartsWith("Level"))
            {
                allLevels.Add(sceneName);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            LoadNextLevel();
        }
    }

    private void LoadNextLevel()
    {
        currentLevelIndex++;
        if (currentLevelIndex >= allLevels.Count)
        {
            currentLevelIndex = 0; // Loop back to the first level
        }
        SceneManager.LoadScene(allLevels[currentLevelIndex]);
    }

}