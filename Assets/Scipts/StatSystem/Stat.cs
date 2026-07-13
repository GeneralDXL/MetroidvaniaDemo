using System.Collections.Generic;
using System;
using UnityEngine;
[Serializable]
public class Stat 
{
    [SerializeField]private float baseValue;
    [SerializeField]private List<StatModifier> modifiers=new List<StatModifier>();
    private float finalValue;
    private bool needToRecaculate=true;

    public event Action OnStatChanged;
    public float GetBaseValue()
    {
        if (needToRecaculate)
        {
            finalValue = GetFinalValue();
            needToRecaculate = false;
        }
        return finalValue;
    }
    public void AddModifier(float value,string source)
    {
        needToRecaculate = true;
        StatModifier modToAdd = new StatModifier(value,source);
        modifiers.Add(modToAdd);
    }

    public void RemoveModifier(string source)
    {
        needToRecaculate = true;
        modifiers.RemoveAll(mod => mod.source == source);
    }
    private float GetFinalValue()
    {
        finalValue = baseValue;
        foreach (var mod in modifiers)
            finalValue += mod.value;
        // 消除浮点累加噪声，保留4位小数精度
        return Mathf.Round(finalValue * 10000f) / 10000f;
    }
    public void SetBaseValue(float value) => baseValue = value;
}
[Serializable]
public class StatModifier
{
    public float value;
    public string source;

    public StatModifier(float value, string source)
    {
        this.value = value;
        this.source = source;
    }
}