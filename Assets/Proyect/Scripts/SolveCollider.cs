using UnityEngine;

public class SolveCollider : MonoBehaviour
{
    [SerializeField]
    Collider solveCollider;
    void ActivateSolveCollider()
    {
        solveCollider.enabled = true;
    }
    void DesactivateSolveCollider()
    {
        solveCollider.enabled = false;
    }
}
