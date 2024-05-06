using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI scoreText; 
    [SerializeField] private TextMeshProUGUI creditsText; 

    [SerializeField] private GameObject dieText; 
    [SerializeField] private GameObject winText; 
    [SerializeField] private GameObject FinalwinText; 
    [SerializeField] private TextMeshProUGUI FinalscoreText; 

    [SerializeField] private GameObject counterText; 
    [SerializeField] private GameObject counterTextText; 


    bool win = false;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        

        //DontDestroyOnLoad(gameObject);
        //DestroyOnLoad(gameObject);
    }

    public void SetUp() {
        dieText.SetActive(false);
        winText.SetActive(false);
        FinalwinText.SetActive(false);
        counterText.SetActive(true);
        counterTextText.SetActive(true);
    }

    void Start() {
        SetUp();
    }
    public void UpdateScoreText(int newScore)
    {
        if (scoreText != null)
            scoreText.text = newScore.ToString();
    }

    public void UpdateCreditsText(int newCredits)
    {
        if (scoreText != null)
            creditsText.text = newCredits.ToString();
    }

    public void YouDead() {
        dieText.SetActive(true);
    }

    public void YouWin() {
        win = !win;
        winText.SetActive(win);
    }

    public void FinalWin() {
        FinalwinText.SetActive(true);
        FinalscoreText.text = scoreText.text;
        counterText.SetActive(false);
        counterTextText.SetActive(false);
         
    }
    void Update()
    {
        UpdateScoreText(ShipController.score);
        UpdateCreditsText(ShipController.credits);
    }
}
