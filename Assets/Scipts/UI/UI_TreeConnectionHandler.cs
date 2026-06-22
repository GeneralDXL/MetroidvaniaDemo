using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_TreeConnectionHandler : MonoBehaviour
{
    private RectTransform rect => GetComponent<RectTransform>();
    [SerializeField]private UI_ConnectionDetails[] connectionDetails;
    [SerializeField]private UI_TreeNodeConnection[] connections;
    private Image connectionImage;
    private Color originalColor;

    private void Awake()
    {
        if(connectionImage!=null)
            originalColor = connectionImage.color;
    }
    private void OnValidate()
    {
        if (connections.Length == 0)
            return;
        if (connections.Length != connectionDetails.Length)
        {
            Debug.Log("connections' amount should be equal to their details' .");
            return;
        }
        UpdateConnections();
    }

    public void UpdateAllConnections()
    {
        UpdateConnections();
        foreach(var node in connectionDetails)
        {
            if(node.childNode ==null)continue;
            node.childNode.UpdateConnections();
        }
    }
    private void UpdateConnections()
    {
        for(int i=0; i<connections.Length; i++)
        {
            var connection=connections[i];
            var detail = connectionDetails[i];
            Vector2 newPosition = connection.GetChildNodeConnectionPoint(rect);
            Image connectionImage = connection.GetConnectionImage();
            connection.DirectConnection(detail.direction, detail.length,detail.rotation);
            if (detail.childNode == null) continue;
            detail.childNode.SetConnectionImage(connectionImage);
            detail.childNode.SetPosition(newPosition);
            detail.childNode.transform.SetAsLastSibling();
        }
    }

    public void UnlockConnectionImage(bool unlocked)
    {
        if(connectionImage == null) return;

        connectionImage.color=unlocked?Color.white:originalColor;
    }
    public void SetConnectionImage(Image image) => connectionImage = image;

    public void SetPosition(Vector2 position) => rect.anchoredPosition = position;

    public UI_TreeNode[] GetChildNodes()
    {
        List<UI_TreeNode> nodes = new List<UI_TreeNode>();
        foreach (var node in connectionDetails)
        {
            if(node==null || node.childNode==null) continue;
            nodes.Add(node.childNode.GetComponent<UI_TreeNode>());
        }

        return nodes.ToArray();
    }
}

[Serializable]
public class UI_ConnectionDetails
{
    public UI_TreeConnectionHandler childNode;
    public NodeDirectionType direction;
    [Range(100f, 350f)] public float length;
    [Range(-50f,50f)] public float rotation;
}
