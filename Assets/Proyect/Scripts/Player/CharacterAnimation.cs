using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimation : MonoBehaviour
{[SerializeField]
    private Animator animator;

    [SerializeField]
    private NavMeshAgent agent;

   
    private void Update()
    {
        animator.SetFloat("Velocity", agent.velocity.magnitude);
    }

    void OnAttack()
    {
        animator.SetTrigger("Attack");
    }

    void OnDie()
    {
        animator.SetTrigger("Die");
    }

    void OnHit()
    {
        animator.SetTrigger("Hit");
    }
}
