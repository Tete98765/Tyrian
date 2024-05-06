using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class CreditsMenu : AView
{
    [SerializeField] private Button backButton;
    public override void Initialize()
    {
      backButton.onClick.AddListener(() =>
        {
            UIManager.PlaySound();
            DoHide();
            UIManager.Show<MainMenuView>();
        });
    }

}
