using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "RPG SetUp/Effect Data", fileName = "Effect data - ")]
public class EffectDataSO : ScriptableObject
{
    public EffectModifier[] modifiers;
    public float duration;
    public string effectName;

    [TextArea]
    public string effectDescription;
}
