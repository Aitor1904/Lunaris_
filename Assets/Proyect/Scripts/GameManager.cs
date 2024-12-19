using UnityEngine;
using Unity.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Ammo")]
    public int gunAmmo = 300;
    public TextMeshProUGUI ammoText;

    [Header("Score")]
    public int score = 0;
    public TextMeshProUGUI scoreText;

    [Header("Health")]
    public int health = 100;
    public TextMeshProUGUI healthText;
    private void Start()
    {
        
    }
    private void Update()
    {
        ammoText.text = gunAmmo.ToString();
        healthText.text = health.ToString();
    }
    private void Awake()
    {
        Instance = this;
    }
    public void GainPoints(int pointsToGain)
    {
        score += pointsToGain;
        //Debug.Log("Suma");
        SetCountText();
    }
    void SetCountText()
    {
        scoreText.text = score.ToString();
    }
    public void LoseHP(int healthToLose)
    {
        health = health - healthToLose;
    }
    public void GainHP(int healthToGain)
    {
        health = health + healthToGain;
    }
}
