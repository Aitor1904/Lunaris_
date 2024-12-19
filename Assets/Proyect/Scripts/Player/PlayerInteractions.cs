using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("ammoBox"))
        {
            GameManager.Instance.gunAmmo += other.gameObject.GetComponent<AmmoBox>().ammo;
            Destroy(other.gameObject);
        }
        if(other.CompareTag("HealthBox"))
        {
            GameManager.Instance.GainHP(45);
        }
        if(other.CompareTag("Sierra"))
        {
            GameManager.Instance.LoseHP(25);
        }
    }
}
