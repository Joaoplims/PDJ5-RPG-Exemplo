using System.Collections.Generic;
using UnityEngine;

public class RPGEntity : MonoBehaviour
{
    public PrimaryStats primaryStats;
    public SecondaryStats secondaryStats;

    private Dictionary<StatType, StatModifier> modifiers;


    private void Update()
    {
        UpdateRegenerationStats();
    }

    public void Initialize()
    {
        primaryStats?.Initialize();
    }

    public void AddModifier() { }
    public void RemoveModifier() { }
    private void UpdateRegenerationStats()
    {
        primaryStats?.RegenerateStats();
    }


}

// Enums
public enum ModifierType { Flat, Percentage, Multiplicative }
public enum StackingType { Override, Add, Multiply, Independent }
public enum StatType
{
    // Atributos Base
    MaxHealth, MaxMana, HealthRegen, ManaRegen, MoveSpeed,

    // Ofensivos
    AttackDamage, AbilityPower, AttackSpeed,
    CritChance, CritDamage, ArmorPenetration, MagicPenetration,
    LifeSteal, SpellVamp,

    // Defensivos
    Armor, MagicResist, DodgeChance, Tenacity,

    // Especiais
    CooldownReduction, ExperienceGain, GoldGain
}
