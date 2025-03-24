using UnityEngine;

public interface IInteractable
{
    public void Interact(InteractableManager manager);
    public void OnSelect();
    public void OnUnselect();
}
