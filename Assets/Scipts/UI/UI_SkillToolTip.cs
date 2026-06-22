using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class UI_SkillToolTip : UI_ToopTip
{
    private UI ui;
    private UI_SkillTree skillTree;
    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private TextMeshProUGUI skillDescription;
    [SerializeField] private TextMeshProUGUI skillRequirements;
    [Space]
    [SerializeField] private string metConditionHex;
    [SerializeField] private string notMetConditionHex;
    [SerializeField] private string importantInfoHex;
    [SerializeField] private string lockedSkillText = "You've taken a diffrent path -- this skill is now locked!";
    [SerializeField] private Color EXAMPLE;

    private Coroutine textEffecCo;
    protected override void Awake()
    {
        base.Awake();
        ui = GetComponentInParent<UI>();
        skillTree=ui.GetComponentInChildren<UI_SkillTree>(true);
    }


    public override void ShowToolTip(bool show, RectTransform targetRect)
    {
        base.ShowToolTip(show, targetRect);
    }


    public void ShowToolTip(bool show, RectTransform targetRect, UI_TreeNode node)
    {
        base.ShowToolTip(show, targetRect);
        if (!show) return;
        skillName.text = node.data.displayName;
        skillDescription.text = node.data.description;
        string skillLockedText = GetColoredText(importantInfoHex, lockedSkillText);
        string requirenments = node.isLockedOut ? skillLockedText : GetRequirements(node.data.cost, node.needs, node.conflicts);
        skillRequirements.text = requirenments;
    }

    private string GetRequirements(int skillCost, UI_TreeNode[] needs, UI_TreeNode[] conflicts)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Requirements: ");
        string costColor = skillTree.IsEnough(skillCost) ? metConditionHex : notMetConditionHex;
        string costText = $"- {skillCost} skill point(s)";
        string finalCostText=GetColoredText(costColor,costText);
        sb.AppendLine(finalCostText);
        foreach (var node in needs)
        {
            if(node==null) continue;
            string nodeColor = node.isUncloked ? metConditionHex : notMetConditionHex;
            string nodeText = $"- {node.data.displayName} ";
            string finalNodeText=GetColoredText(nodeColor,nodeText);
            sb.AppendLine(finalNodeText);
        }
        if(conflicts.Length==0)
            return sb.ToString();
        sb.AppendLine();
        sb.AppendLine(GetColoredText(importantInfoHex,"Locks out: "));
        foreach (var node in conflicts)
        {
            if(node==null) continue;
            string nodeText = $"- {node.data.displayName} ";
            string finalNodeText=GetColoredText(importantInfoHex,nodeText);
            sb.AppendLine(finalNodeText);
        }
        return sb.ToString();
    }

    

    public void LockedSkillEffect()
    {
        if(textEffecCo!=null)
            StopCoroutine(textEffecCo);
        textEffecCo = StartCoroutine(TextBlinkEffectCo(skillRequirements, 0.15f, 3));
    }
    private IEnumerator TextBlinkEffectCo(TextMeshProUGUI text,float blinkInterval,int blinkCount)
    {
        for(int i=0;i<blinkCount;i++)
        {
            text.text = GetColoredText(notMetConditionHex, lockedSkillText);
            yield return new WaitForSeconds(blinkInterval);

            text.text=GetColoredText(importantInfoHex, lockedSkillText);
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
