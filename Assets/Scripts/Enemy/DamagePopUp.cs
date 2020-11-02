using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopUp : MonoBehaviour
{  

    // Create a Damage Popup
    public static DamagePopUp Create(Vector3 pos, float damage)
    {
        Transform dmgPopUpTransform = Instantiate(GameAssets.instance.damagePopUp, pos, Quaternion.identity);

        DamagePopUp damagePopUp = dmgPopUpTransform.GetComponent<DamagePopUp>();
        damagePopUp.Setup(damage);

        return damagePopUp;
    }

    public void Setup(float damage)
    {
        textMesh.SetText(damage.ToString());
        textColor = textMesh.color;
        disappearTimer = maxDisappearTimer;

        moveVector = new Vector3(Random.Range(-.5f, .5f), 1.5f) * 40; ;

        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;
        sortingOrder = 0;
    }

    private static int sortingOrder = 0;

    private TextMeshPro textMesh;
    private Color textColor;

    private float disappearTimer;
    private const float maxDisappearTimer = .8f;

    private Vector3 moveVector;

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 8f * Time.deltaTime;

        if (disappearTimer > maxDisappearTimer * 0.5f)
        {
            // First half of the popup
            transform.localScale += Vector3.one * 0.6f * Time.deltaTime;
        }
        else
        {
            // Second half of the popup
            transform.localScale -= Vector3.one * 1 * Time.deltaTime;
        }

        disappearTimer -= Time.deltaTime;
        if (disappearTimer <0)
        {
            // Start dissapearing
            float disSpeed = 7f;
            textColor.a -= disSpeed * Time.deltaTime;
            textMesh.color = textColor;

            if(textColor.a <= 0) { Destroy(gameObject); }
        }
    }
}
