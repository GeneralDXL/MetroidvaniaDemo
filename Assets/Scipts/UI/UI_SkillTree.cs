using UnityEngine;

public class UI_SkillTree : MonoBehaviour
{
    [SerializeField]private int skillPoints;
    [SerializeField] private UI_TreeConnectionHandler[] nodes;
    public Player_SkillManager skillManager { get; private set; }

    [ContextMenu("UpdateAllConnections")]
    public void UpdateAllConnections()
    {
        foreach (var node in nodes)
        {
            if(node==null) continue;
            node.UpdateAllConnections();
        }
    }

    private void Awake()
    {
        skillManager=FindAnyObjectByType<Player_SkillManager>();
    }
    private void Start()
    {
        UpdateAllConnections();
    }

    [ContextMenu("Refound All Skills")]
    public void RefoundAllSkills()
    {
        UI_TreeNode[] nodes = GetComponentsInChildren<UI_TreeNode>();

        foreach (var node in nodes)
            node.Refound();
    }
    public bool IsEnough(int cost) => skillPoints >= cost;
    public void CostPoints(int cost) => skillPoints-=cost;
    public void AddPoints(int points) => skillPoints+=points;
}
