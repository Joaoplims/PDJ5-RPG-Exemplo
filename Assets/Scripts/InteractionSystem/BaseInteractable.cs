using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class  BaseInteractable : MonoBehaviour, IInteractable
{
protected Material _myMat;
    private void Start()
    {
        _myMat = GetComponent<Renderer>().material;
    }
    public abstract void Interact(InteractableManager manager);

    public virtual void OnSelect()
    {
        _myMat.SetFloat("_ActiaveHighlight",1f);
    }

    public virtual void OnUnselect()
    {
        _myMat.SetFloat("_ActiaveHighlight", 0f);
    }
}
