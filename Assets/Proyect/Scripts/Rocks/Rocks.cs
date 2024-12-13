using UnityEngine;

public class Rocks : MonoBehaviour
{
    [SerializeField]
    GameObject entireRock;
    [SerializeField]
    GameObject crackedRock;
    [SerializeField]
    GameObject rockParticles;
    bool firstCracked;
    private void Start()
    {
        firstCracked = false;
        crackedRock.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerBullet") && firstCracked == false)
        {
            Destroy(other.gameObject);
            entireRock.SetActive(false);
            crackedRock.SetActive(true);
            firstCracked = true;
            Debug.Log("Collision");
        }
        else if (other.gameObject.CompareTag("PlayerBullet") && firstCracked == true)
        {
            Destroy(other.gameObject);
            crackedRock.SetActive(false);
            Instantiate(rockParticles, transform.position, Quaternion.identity);
            //Invoke("DestroyParticles", 0.5f);
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Pala") && firstCracked == false)
        {
            entireRock.SetActive(false);
            crackedRock.SetActive(true);
            firstCracked = true;
            Debug.Log("Collision");
        }
        else if (other.gameObject.CompareTag("Pala") && firstCracked == true)
        {
            crackedRock.SetActive(false);
            Instantiate(rockParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}