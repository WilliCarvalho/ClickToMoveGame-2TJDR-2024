using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerBehavior : MonoBehaviour
{
    private NavMeshAgent agent;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float Acceleration;
    [SerializeField] private float turnSpeed;
    [SerializeField] private LayerMask attackLayer;
    [SerializeField] private ParticleSystem clickParticle;
    [SerializeField] private FireBall spell;
    [SerializeField] private Transform shotPosition;

    private Vector3 moveDirection;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        agent.acceleration = Acceleration;
        agent.angularSpeed = turnSpeed;
    }

    private void Start()
    {
        GameManager.Instance.InputManager.OnPlayerMove += HandleClick;
    }

    private void Update()
    {
        HandleMove();
    }

    private void HandleMove()
    {
        Vector2 inputData = GameManager.Instance.InputManager.MoveDirection;
        moveDirection.x = inputData.x;
        moveDirection.z = inputData.y;

        transform.Translate(moveDirection * Time.deltaTime * moveSpeed);
    }

    private void HandleClick()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                HandleAttack(hit);
            }
            else
            {
                HandleMovement(hit);
            }            
        }
    }

    private void HandleAttack(RaycastHit hit)
    {
        print("Attacking");
        GetComponent<Animator>().SetTrigger("attack");
        transform.LookAt(hit.point);
        Instantiate(spell, shotPosition.position, Quaternion.identity);
    }

    private void HandleMovement(RaycastHit hit)
    {
        agent.SetDestination(hit.point);
        if (clickParticle != null)
        {
            ParticleSystem particle = Instantiate(clickParticle, hit.point, clickParticle.transform.rotation);
            Destroy(particle.gameObject, 0.5f);
        }
        GameManager.Instance.AudioManager.PlaySFX(SFX.click);
    }    

    public NavMeshAgent GetPlayerAgent()
    {
        return agent;
    }
}
