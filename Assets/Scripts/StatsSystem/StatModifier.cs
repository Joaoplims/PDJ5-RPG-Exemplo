
using System;
using UnityEngine;


public class StatModifier: IEquatable<StatModifier>
{
    // Identificação única
    [Tooltip("Identificador único para remoção/modificação")]
    public string id;

    // Configuração básica
    [Header("Modifier Settings")]
    [Tooltip("Tipo de atributo que será modificado")]
    public StatType statType;

    [Tooltip("Valor do modificador (pode ser negativo)")]
    public float value;

    [Tooltip("Como o valor será aplicado")]
    public ModifierType modifierType;

    [Tooltip("Duração em segundos (-1 = permanente)")]
    public float duration = -1f;

    // Sistema de Stacking
    [Header("Stacking Behavior")]
    [Tooltip("Como múltiplas instâncias deste modificador se comportam")]
    public StackingType stackingType = StackingType.Override;

    [Tooltip("Número máximo de stacks permitidos (0 = ilimitado)")]
    public int maxStacks = 1;

    [NonSerialized] public int currentStacks = 1;

    // Callbacks (opcional)
    public Action<StatModifier> onApplied;
    public Action<StatModifier> onRemoved;
    public Action<StatModifier> onStackChanged;
    public StatModifier() { }

    public StatModifier(string id, StatType statType, float value,
                       ModifierType modifierType, float duration = -1f)
    {
        this.id = id;
        this.statType = statType;
        this.value = value;
        this.modifierType = modifierType;
        this.duration = duration;
    }

    public bool Stack(StatModifier other)
    {
        if (!CanStackWith(other)) return false;

        switch (stackingType)
        {
            case StackingType.Override:
                value = other.value;
                break;

            case StackingType.Add:
                value += other.value;
                break;

            case StackingType.Multiply:
                value *= other.value;
                break;

            case StackingType.Independent:
                if (maxStacks == 0 || currentStacks < maxStacks)
                {
                    currentStacks++;
                    onStackChanged?.Invoke(this);
                    return true;
                }
                return false;
        }

        duration = Mathf.Max(duration, other.duration);
        onStackChanged?.Invoke(this);
        return true;
    }

    public bool CanStackWith(StatModifier other)
    {
        return id == other.id &&
               statType == other.statType &&
               modifierType == other.modifierType &&
               stackingType == other.stackingType;
    }
    public float ApplyModifier(float baseValue)
    {
        switch (modifierType)
        {
            case ModifierType.Flat:
                return baseValue + (value * currentStacks);

            case ModifierType.Percentage:
                return baseValue * (1 + (value * currentStacks));

            case ModifierType.Multiplicative:
                return baseValue * Mathf.Pow(value, currentStacks);

            default:
                return baseValue;
        }
    }

    public bool IsExpired => duration > 0 && duration <= Time.time;

    public bool IsPermanent => duration < 0;
    public StatModifier Clone()
    {
        return new StatModifier()
        {
            id = this.id,
            statType = this.statType,
            value = this.value,
            modifierType = this.modifierType,
            duration = this.duration,
            stackingType = this.stackingType,
            maxStacks = this.maxStacks
        };
    }

    #region Igualdade (para comparações)
    public bool Equals(StatModifier other)
    {
        return id == other.id &&
               statType == other.statType &&
               Mathf.Approximately(value, other.value) &&
               modifierType == other.modifierType;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(id, statType, value, modifierType);
    }
    #endregion

}
