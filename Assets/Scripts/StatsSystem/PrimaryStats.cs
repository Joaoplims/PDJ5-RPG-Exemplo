using UnityEngine;
[System.Serializable]
public class PrimaryStats
{
    public float maxHealth;
    public float maxMana;
    public float healthRegen;
    public float manaRegen;
    public float attackDamage;
    public float abilityPower;
    public float attackSpeed;
    public float magicResist;
    public float armor;
    public float critChance;
    public float moveSpeed;

    private float currentHealth;
    private float currentMana;


    public void Initialize()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;
    }
    public void RegenerateStats()
    {
        currentHealth = Mathf.Min(currentHealth + healthRegen * Time.deltaTime, maxHealth);
        currentMana = Mathf.Min(currentMana + manaRegen * Time.deltaTime, maxMana);
    }

    public float CalculatePhysicalDamage(float baseDamage, SecondaryStats secondaryStats, bool canCrit = true )
    {
        float damage = baseDamage + attackDamage;

        // Penetração de armadura
        float effectiveArmor = Mathf.Max(0, armor - secondaryStats?.armorPenetration ?? 0f);
        float damageReduction = effectiveArmor / (100 + effectiveArmor);

        damage *= (1 - damageReduction);

        // Crítico
        if (canCrit && UnityEngine.Random.value <= critChance)
        {
            damage *= secondaryStats?.critDamage ?? 1f;
        }

        return damage;
    }

    public float CalculateMagicDamage(float baseDamage, SecondaryStats secondaryStats)
    {
        float damage = baseDamage + abilityPower;

        // Penetração mágica
        float effectiveMR = Mathf.Max(0, magicResist - secondaryStats?.magicPenetration ?? 0f);
        float damageReduction = effectiveMR / (100 + effectiveMR);

        return damage * (1 - damageReduction);
    }

    public bool UpdateHealth(float cLife){
            currentHealth -= cLife;
            return currentHealth <= 0f;

    }

    public bool UpdateMana(float cMana){
        currentMana -= cMana;
        return currentMana <= 0f;
    }
}
