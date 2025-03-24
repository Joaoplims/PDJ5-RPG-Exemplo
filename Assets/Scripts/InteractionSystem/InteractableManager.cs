using UnityEngine;
using UnityEngine.Rendering;

public class InteractableManager : MonoBehaviour
{
    public float interactableRadius = 10f;
    [SerializeField] private IInteractable currentInteractableObj;
    [SerializeField] private LayerMask interactableMask;


    private void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, interactableMask))
        {
            currentInteractableObj = hitInfo.collider.GetComponent<IInteractable>();
            currentInteractableObj.OnSelect();
        }
        if (currentInteractableObj != null && Input.GetMouseButtonDown(0))
        {
            currentInteractableObj.Interact(this);
        }


    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, interactableRadius);
    }
}
