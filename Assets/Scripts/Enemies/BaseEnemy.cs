using UnityEngine;

public class BaseEnemy : BaseInteractable
{
    public PrimaryStats primaryStats;
    public override void Interact(InteractableManager manager)
    {
        Debug.Log("Player me focou!");
    }

    public void TakeDamage(float damage)
    {
        primaryStats.UpdateHealth(damage);
        PopupText.instance.ShowMsg(damage.ToString(), transform.position);
    }
}
