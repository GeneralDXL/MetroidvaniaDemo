using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_EffectManager : MonoBehaviour
{
    private List<Effect> effects = new List<Effect>();
    private Player_Stats stats;
    public event Action OnEffectsChanged;
    private void Awake()
    {
        stats = GetComponent<Player_Stats>();
    }

    public void AddEffect(Effect effect)
    {
        ApplyEffect(effect);
    }

    private void ApplyEffect(Effect effect)
    {
        Effect existing = effects.Find(e => e.data == effect.data);
        if (existing != null && existing.applyEffectCo != null)
        {
            foreach (var mod in existing.data.modifiers)
                stats.GetStatByType(mod.statType).RemoveModifier(existing.effectId);
            StopCoroutine(existing.applyEffectCo);
            effects.Remove(existing);
        }
        effect.effectId = effect.data.effectName + Guid.NewGuid();
        effect.applyEffectCo = StartCoroutine(ApplyEffectCo(effect));
        effects.Add(effect);
    }

    private IEnumerator ApplyEffectCo(Effect effect)
    {
        foreach (var mod in effect.data.modifiers)
            stats.GetStatByType(mod.statType).AddModifier(mod.value, effect.effectId);
        yield return new WaitForSeconds(effect.data.duration);
        foreach (var mod in effect.data.modifiers)
            stats.GetStatByType(mod.statType).RemoveModifier(effect.effectId);
        effects.Remove(effect);
        OnEffectsChanged?.Invoke();
    }
}
