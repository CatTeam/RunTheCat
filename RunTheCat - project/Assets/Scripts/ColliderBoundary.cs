using UnityEngine;
using System.Collections;

public class ColliderBoundary : MonoBehaviour {

    public float thickness;
    public Vector2 size;
    public void UpdateLines(float thickness, float width, float height)
    {
        #region Getting GOs
        GameObject temp;
        Transform tLeft = transform.Find("Left");
        if(tLeft==null)
        {
            temp = new GameObject("Left", typeof(BoxCollider2D));
            temp.transform.parent = transform;
            tLeft=temp.transform;
        }

        Transform tRight = transform.Find("Right");
        if (tRight == null)
        {
            temp = new GameObject("Right", typeof(BoxCollider2D));
            temp.transform.parent = transform;
            tRight=temp.transform;
        }

        Transform tTop = transform.Find("Top");
        if (tTop == null)
        {
            temp = new GameObject("Top", typeof(BoxCollider2D));
            temp.transform.parent = transform;
            tTop=temp.transform;
        }

        Transform tBottom = transform.Find("Bottom");
        if (tBottom == null)
        {
            temp = new GameObject("Bottom", typeof(BoxCollider2D));
            temp.transform.parent = transform;
            tBottom=temp.transform;
        }
        #endregion

        #region Getting Components
        BoxCollider2D Left = tLeft.GetComponent<BoxCollider2D>();
        if (Left == null)
        {
            tLeft.gameObject.AddComponent<BoxCollider2D>();
            Left = tLeft.GetComponent<BoxCollider2D>();
        }

        BoxCollider2D Right = tRight.GetComponent<BoxCollider2D>();
        if (Right == null)
        {
            tRight.gameObject.AddComponent<BoxCollider2D>();
            Right = tRight.GetComponent<BoxCollider2D>();
        }

        BoxCollider2D Top = tTop.GetComponent<BoxCollider2D>();
        if (Top == null)
        {
            tTop.gameObject.AddComponent<BoxCollider2D>();
            Top = tTop.GetComponent<BoxCollider2D>();
        }

        BoxCollider2D Bottom = tBottom.GetComponent<BoxCollider2D>();
        if (Bottom == null)
        {
            tBottom.gameObject.AddComponent<BoxCollider2D>();
            Bottom = tBottom.GetComponent<BoxCollider2D>();
        }
        #endregion
        
        Vector3 pos=transform.position;
        
        Left.size = new Vector2(thickness, height);
        Left.transform.position = new Vector3(pos.x - thickness / 2, 
                                                pos.y - height / 2);

        Right.size = new Vector2(thickness, height);
        Right.transform.position = new Vector3(pos.x + width + thickness / 2,
                                                pos.y - height / 2);

        Top.size = new Vector2(width, thickness);
        Top.transform.position = new Vector3(pos.x + width / 2,
                                            pos.y + thickness / 2);

        Bottom.size = new Vector2(width, thickness);
        Bottom.transform.position = new Vector3(pos.x + width / 2,
                                            pos.y - height - thickness / 2);
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 pos=transform.position;
        Gizmos.DrawWireCube(new Vector3(pos.x + size.x / 2, pos.y - size.y / 2),
                            new Vector3(size.x, size.y));
    }

}
