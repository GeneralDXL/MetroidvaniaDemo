using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class UI_TreeNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [Header("Unlock details")]
    public UI_TreeNode[] needs;
    public UI_TreeNode[] conflicts;
    public bool isLockedOut;
    public bool isUncloked;

    [Header("Skill details")]
    public Skill_DataSo data;
    [SerializeField] private string skillName;
    [SerializeField] private Image skillIcon;
    [SerializeField] private int skillCost;
    [SerializeField] private string lockedColorHex;
    private UI ui;
    private RectTransform rect;
    private UI_SkillTree skillTree;
    private UI_TreeConnectionHandler connectionHandler;
    private Color lastColor;



    private void Awake()
    {
        UpdateColor(HexToColor(lockedColorHex));
        ui = GetComponentInParent<UI>();
        rect = GetComponent<RectTransform>();
        skillTree = GetComponentInParent<UI_SkillTree>();
        connectionHandler = GetComponent<UI_TreeConnectionHandler>();
        lastColor = skillIcon.color;
    }
    private void Start()
    {
        if(data.isUnlockedByDefault)
            Unlock();
        
    }
    private bool CanUnclock()
    {
        if (isLockedOut || isUncloked) return false;
        if (!skillTree.IsEnough(skillCost)) return false;
        foreach (var need in needs)
            if (!need.isUncloked) return false;
        foreach (var conflict in conflicts)
            if (conflict.isUncloked) return false;
        return true;
    }
    public void Refound()
    {
        isUncloked = false;
        isLockedOut = false;
        UpdateColor(HexToColor(lockedColorHex));

        skillTree.AddPoints(skillCost);
        connectionHandler.UnlockConnectionImage(false);
    }
    private void UpdateColor(Color color)
    {
        skillIcon.color = color;
    }
    private void Unlock()
    {
        skillTree.CostPoints(skillCost);
        isUncloked = true;
        skillIcon.color = Color.white;
        LockOutConflictNodes();
        connectionHandler.UnlockConnectionImage(true);

        skillTree.skillManager.GetSkillByType(data.skillType).SetSkillUpgrade(data.upgradeData);
    }

    private void LockOutConflictNodes()
    {
        foreach (var node in conflicts)
            node.LockOutChildNodes();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        ui.skillToolTip.ShowToolTip(true, rect, this);
        if (isUncloked || isLockedOut) return;
        ToggleNodeHighlight(true);
    }

    public void LockOutChildNodes()
    {
        isLockedOut = true;
        foreach(var node in connectionHandler.GetChildNodes())
        {
            if(node==null) continue;
            node.LockOutChildNodes();
        }
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (CanUnclock())
            Unlock();
        else if (isLockedOut)
            ui.skillToolTip.LockedSkillEffect();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        ui.skillToolTip.ShowToolTip(false, rect);
        if (isUncloked || isLockedOut) return;
        ToggleNodeHighlight(false);
    }

    public Color HexToColor(string hex)
    {
        ColorUtility.TryParseHtmlString(hex, out Color color);
        return color;
    }
    private void OnValidate()
    {
        if (skillName == null) return;
        skillName = data.displayName;
        skillCost = data.cost;
        skillIcon.sprite = data.icon;
        gameObject.name = "UI_TreeNode - " + skillName;
        
    }

    private void ToggleNodeHighlight(bool highlight)
    {
        Color highlightColor = Color.white * 0.9f;
        highlightColor.a = 1;
        Color colorToApply = highlight ? highlightColor : lastColor;
        UpdateColor(colorToApply);
    }
}
