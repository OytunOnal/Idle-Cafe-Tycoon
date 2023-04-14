using UnityEngine;
using UnityEngine.AI;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private NavMeshAgent navMeshAgent;
    private bool isMoving = false;
    private Vector3 moveDirection = Vector3.zero;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        EventsManager.OnMove += Move;
        EventsManager.EndMove += EndMove;
    }

    private void OnDisable()
    {
        EventsManager.OnMove -= Move;
        EventsManager.EndMove -= EndMove;
    }

    private void Update() 
    {
        if(!isMoving) return;
        navMeshAgent.Move(moveDirection * moveSpeed * Time.deltaTime);

    }

    private void Move(Vector2 direction)
    {
        isMoving = true;
        // Move the character based on the input direction
        moveDirection = Camera.main.transform.TransformDirection(new Vector3(direction.x, 0f, direction.y));
        
        transform.LookAt(new Vector3(moveDirection.x + transform.position.x,transform.position.y,moveDirection.z + transform.position.z) );
    }

    private void EndMove()
    {
        isMoving = false;
        moveDirection = Vector3.zero;
    }
}
