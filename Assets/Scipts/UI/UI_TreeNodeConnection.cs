using UnityEngine;
using UnityEngine.UI;

public class UI_TreeNodeConnection : MonoBehaviour
{
    [SerializeField] private RectTransform rotationPoint;
    [SerializeField] private RectTransform connectionLength;
    [SerializeField] private RectTransform childNodeConnectionPoint;
    private float GetDirectionAngle(NodeDirectionType type)
    {
        switch (type)
        {
            case NodeDirectionType.Left:return 180f;
            case NodeDirectionType.UpLeft:return 135f;
            case NodeDirectionType.Up:return 90f;
            case NodeDirectionType.UpRight:return 45f;
            case NodeDirectionType.Right:return 0f;
            case NodeDirectionType.DownRight:return -45;
            case NodeDirectionType.Down:return -90f;
            case NodeDirectionType.DownLeft:return -135f;
            default: return 0f;
        }
    }

    public Image GetConnectionImage() => connectionLength.GetComponent<Image>();
    public void DirectConnection(NodeDirectionType direction,float length,float offset)
    {
        bool shouldConnect = direction != NodeDirectionType.None;
        float angle=GetDirectionAngle(direction);
        float finalLength = shouldConnect ? length : 0f;

        rotationPoint.localRotation= Quaternion.Euler(0,0,angle+offset);

        connectionLength.sizeDelta=new Vector2(finalLength,connectionLength.sizeDelta.y);
        
    }
    public Vector2 GetChildNodeConnectionPoint(RectTransform target)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            target.parent as RectTransform,
            childNodeConnectionPoint.position,
            null,
            out Vector2 localPoint
            );
        return localPoint;
    }
}

public enum NodeDirectionType
{
    None,
    Up,
    UpRight,
    Right,
    DownRight,
    Down,
    DownLeft,
    Left,
    UpLeft
}
