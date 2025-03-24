using UnityEngine;

public class HealthController : MonoBehaviour
{
    public float MaxHealth = 100;

    private float currentHealth;


    private void Start()
    {
        currentHealth = MaxHealth;
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        if(currentHealth <= 0f) currentHealth = 0f;
        PopupText.instance.ShowMsg(Mathf.RoundToInt(dmg).ToString(), transform.position);
    }

    [ContextMenu("TakeDamage")]
    public void TestTakeDamage()
    {
        float rnd = Random.Range(1f, 35f);
        TakeDamage(rnd);
    }
}
