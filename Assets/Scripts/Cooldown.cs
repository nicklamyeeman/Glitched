using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{
    [SerializeField]
    KeyCode skillKey;

    public Image imageCooldown;
    public float cooldown;
    bool isCooldown;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(skillKey)) {
            isCooldown = true;
        }

        if (isCooldown) {
            imageCooldown.fillAmount += 1 / cooldown * Time.deltaTime;

            if (imageCooldown.fillAmount >= 1) {
                imageCooldown.fillAmount = 0;
                isCooldown = false;
            }
        }
    }

    public bool getCooldown() {
        return isCooldown;
    }
}
