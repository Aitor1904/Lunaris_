using UnityEngine;
using Unity.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int gunAmmo = 300;
    public TextMeshProUGUI ammoText;
    private void Update()
    {
        ammoText.text = gunAmmo.ToString();
    }
    private void Awake()
    {
        Instance = this;
    }
}
