using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakItem : MonoBehaviour
{
    GameObject obj;
    protected BreakItem_Type type;

    public void Break()
    {
        gameObject.SetActive(false);
        if (type == BreakItem_Type.Leaf)
        {
            obj = BreakItemPool.Instance.GetListLeaf_ExplosionFromPool();
            if (obj != null)
            {
                obj.transform.position = transform.position;
                obj.transform.rotation = transform.rotation;
                obj.SetActive(true);
            }
        }
        else
        {
            obj = BreakItemPool.Instance.GetListRock_ExplosionFromPool();
            if (obj != null)
            {
                obj.transform.position = new Vector2(transform.position.x, transform.position.y - 5);
                obj.transform.rotation = transform.rotation;
                obj.SetActive(true);
            }
        }
    }

}


public enum BreakItem_Type
{
    Leaf, Rock
}