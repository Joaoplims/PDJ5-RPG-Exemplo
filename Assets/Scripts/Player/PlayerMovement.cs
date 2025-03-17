using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public float speed;

    private Plane wrldPlane = new Plane(Vector3.up, Vector3.zero);
    private Vector3 targetPos;
    private Vector3 walkDir;
    private Animator animator;
    [SerializeField] private LayerMask obsLayer;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            targetPos = GetMouseWorldPos(Input.mousePosition);
        }
        Move();
    }

    private void Move()
    {
        float distance = Vector3.Distance(transform.position, targetPos);
        if (distance > 0.5f)
        {
            float adjustedSpeed = speed / distance;
            transform.position = Vector3.Lerp(transform.position, targetPos, adjustedSpeed * Time.deltaTime);
            transform.LookAt(targetPos);
            walkDir = targetPos - transform.position;
        }
        else
        {
            walkDir = Vector3.zero;
        }
        HandleWalkAnimation();
    }

    private void HandleWalkAnimation()
    {
        animator.SetFloat("Speed", walkDir.magnitude);
    }

    private Vector3 GetMouseWorldPos(Vector3 mousePos)
    {
        Ray screenPointToRay = Camera.main.ScreenPointToRay(mousePos);
        float dist = 0;
        Vector3 targetPoint = Vector3.zero;
        if (wrldPlane.Raycast(screenPointToRay, out dist))
        {
            targetPoint = screenPointToRay.GetPoint(dist);
        }
        RaycastHit hit; // Resultado do Raycast
        if (Physics.Raycast(screenPointToRay, out hit, Mathf.Infinity, obsLayer))
        {
            Debug.Log("Obstáculo Detectado: " + hit.collider.gameObject.name);
            // Define o ponto de parada próximo ao obstáculo
            Vector3 directionToObstacle = (transform.position - hit.point).normalized;
            Debug.DrawRay(hit.point, directionToObstacle * 2f,Color.blue,2f);
            targetPoint = hit.point + (directionToObstacle * 2f);

        }
        return targetPoint;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(targetPos, 0.5f);
    }

}
