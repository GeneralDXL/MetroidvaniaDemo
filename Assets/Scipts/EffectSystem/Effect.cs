using UnityEngine;
using System;
[Serializable]
public class Effect
{
    public EffectDataSO data;

    public string effectId;
    public Coroutine applyEffectCo;

    public Effect(EffectDataSO data)=>this.data = data;
}
