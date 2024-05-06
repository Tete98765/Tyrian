using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelsMenu : AView
{
    [SerializeField] private Button level1;
    [SerializeField] private Button level2;
    [SerializeField] private Button level3;

    [SerializeField] private Button backButton;

    
    public override void Initialize()
    {
        level1.onClick.AddListener(() =>
        {
            UIManager.PlaySound();
            DoHide();
            SceneManager.LoadScene("Level1");
        });

        level2.onClick.AddListener(() =>
        {
            UIManager.PlaySound();
            DoHide();
            SceneManager.LoadScene("Level2");
        });

        level3.onClick.AddListener(() =>
        {
            UIManager.PlaySound();
            DoHide();
            SceneManager.LoadScene("Level3");
        });
        backButton.onClick.AddListener(() =>
        {
            UIManager.PlaySound();
            DoHide();
            UIManager.Show<MainMenuView>();
        });


    }
}
