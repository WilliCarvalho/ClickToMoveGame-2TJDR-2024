using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.HID;

public class EnemyBehavior : MonoBehaviour
{
    public float moveSpeed;
    public ParticleSystem deathParticle;

    NavMeshAgent agent;
    private Transform playerPosition;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        playerPosition = GameManager.Instance.GetPlayer().transform;
    }

    private void Update()
    {
        agent.SetDestination(playerPosition.position);
    }

    private void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("death");
            agent.isStopped = true;
            ParticleSystem particle = Instantiate(deathParticle, transform.position, deathParticle.transform.rotation);
            Destroy(particle.gameObject, 0.5f);
            Destroy(this.gameObject, 2f);
        }       
    }
}
