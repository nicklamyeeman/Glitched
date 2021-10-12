using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddBonus : MonoBehaviour
{
    public GameObject bonusPrefab;
    public Sprite[] bonusSprites;
    string bonusName;
    static private Vector3 nextPosition = new Vector3(-15f, 0f, 0f);

    public void AddNewBonus(string bonusName) {
        Sprite bonusSprite = null;
        for (int i = 0 ; i < bonusSprites.Length; i += 1) {
            if (bonusSprites[i].name == bonusName) {
                Debug.Log(bonusSprites[i].name);
                bonusSprite = bonusSprites[i];
                bonusName = bonusSprite.name;
            }
        }
        if (bonusSprite == null)
            bonusSprite = Resources.Load<Sprite>("diamond");

        if (!bonusSprite)
            Debug.Log("Failed");
//        bonusName = bonusSprite.name;

        GameObject newBonus = Instantiate(bonusPrefab);

        newBonus.name = bonusName;
        newBonus.GetComponent<Bonus>().bonusName = bonusName;
        newBonus.GetComponent<Image>().sprite = bonusSprite;

        GameObject obj = GameObject.Find("CanvasBonus");
        newBonus.transform.SetParent(obj.transform);

        Transform transform = newBonus.GetComponent<Transform>();
//        transform.anchorMin = new Vector2(0f, 0.5f);
//        transform.anchorMax = new Vector2(0f, 0.5f);

        nextPosition += new Vector3(60f, 0f, 0f);
        transform.localPosition = nextPosition;

        transform.localScale = new Vector3(0.73f, 0.73f, 0f);

    }
}
