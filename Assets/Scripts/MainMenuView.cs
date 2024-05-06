using UnityEngine.UI;
using UnityEngine;
public class MainMenuView : AView
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button exitButton;


    public override void Initialize()
    {
        startButton.onClick.AddListener(() =>
        {
            //startButton.GetComponent<AudioSource>().Play();
            UIManager.PlaySound();
            DoHide();
            UIManager.Show<LevelsMenu>();
        });

        creditsButton.onClick.AddListener(() =>
        {
            UIManager.PlaySound();
            DoHide();
            UIManager.Show<CreditsMenu>();
        });

        exitButton.onClick.AddListener(() =>
        {
            UIManager.PlaySound();
            Application.Quit();
        });
    }
}