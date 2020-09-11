using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractWeaponComponent : MonoBehaviour
{
    private int compomnentID;

    private int componentCost;

    private SpriteRenderer componentNotBougthImage;

    private SpriteRenderer componentBoughtImage;

    private SpriteRenderer placedComponentImage;

    int GetID(int compID)
    {
        compomnentID = compID;
        return compomnentID;
    }

    int GetCost(int cost)
    {
        componentCost = cost;
        return componentCost;
    }

    SpriteRenderer GetNotBoughtImage(SpriteRenderer notBought)
    {
        componentNotBougthImage = notBought;
        return componentNotBougthImage;
    }

    SpriteRenderer GetBoughtImage(SpriteRenderer bought)
    {
        componentBoughtImage = bought;
        return componentBoughtImage;
    }

    SpriteRenderer GetPlacedImage(SpriteRenderer placed)
    {
        placedComponentImage = placed;
        return placedComponentImage;
    }

    public virtual void ComponentEffect()
    {
    }
}
