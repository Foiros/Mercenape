using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopUp : MonoBehaviour
{  

    // Create a Damage Popup
    public static DamagePopUp Create(Vector3 pos, float damage)
    {
        //Transform dmgPopUpTransform = Instantiate(GameAssets.instance.damagePopUp, pos, Quaternion.identity);

        GameObject dmgPopUpObj = ObjectPooler.Instance.SpawnFromPool("DamagePopUp", pos, Quaternion.identity);

        DamagePopUp damagePopUp = dmgPopUpObj.GetComponent<DamagePopUp>();

        damagePopUp.Setup(damage);

        return damagePopUp;
    }

    public void Setup(float damage)
    {
        textMesh.SetText(damage.ToString());
        //textColor = textMesh.color;
       
        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;
        sortingOrder = 0;
    }

    private static int sortingOrder = 0;

    private TextMeshPro textMesh;
    //private Color textColor; 

    private void Awake()
    {
        textMesh = transform.GetChild(0).GetComponent<TextMeshPro>();
    }

    private void OnEnable()
    {
        Invoke("Sleep", 1.1f);
    }
 
    private void Sleep()
    {
        gameObject.SetActive(false);
    }
}
