using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Image healthBar = null;
    float maxHealth = 3f;
    public static float health;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Image>();
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = health / maxHealth;
        if (Input.GetKeyDown(KeyCode.B)) {
            health = health - 1;
        }
    }

    public Image getHealthBar() {
        return healthBar;
    }

    public void decreaseLife() {
        health = health - 1;
    }
}
