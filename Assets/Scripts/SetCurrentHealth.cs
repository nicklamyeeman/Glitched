using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetCurrentHealth : MonoBehaviour
{
    public HealthBar healthBar;
    public Text currentHealth;

    void Update()
    {
//        Debug.Log(healthBar.getHealthBar().fillAmount * 100);
        currentHealth.text = (healthBar.getHealthBar().fillAmount * 3).ToString();
    }
}
