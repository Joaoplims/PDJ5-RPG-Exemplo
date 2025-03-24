using UnityEngine;

public class BaseEnemy : BaseInteractable
{
    public override void Interact(InteractableManager manager)
    {
        Debug.Log("Player me focou!");
    }


}
