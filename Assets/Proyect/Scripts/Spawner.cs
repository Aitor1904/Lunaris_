using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject barrilPrefab;
    [SerializeField]
    Transform spawner;
    [SerializeField]
    float barrilForce;
    float lastSpawn = 0f;
    [SerializeField]
    float spawnFrecuence = 0.6f;
    private void Update()
    {
        if ((lastSpawn + spawnFrecuence) <= Time.time)
        {
            lastSpawn = Time.time;
            GameObject newBarrilPrefab;
            newBarrilPrefab = Instantiate(barrilPrefab, spawner.position, Quaternion.identity);
            barrilPrefab.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, barrilForce));
        }
    }
    
}
