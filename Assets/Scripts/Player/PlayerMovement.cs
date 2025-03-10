using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public float speed;

    private Plane wrldPlane = new Plane(Vector3.up, Vector3.zero);
    private Vector3 targetPos;
    private Vector3 walkDir;
    private Animator animator;

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
        if (wrldPlane.Raycast(screenPointToRay, out dist))
        {
            return screenPointToRay.GetPoint(dist);
        }
        return Vector3.zero;
    }

}
