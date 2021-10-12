using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPressed : MonoBehaviour
{
    public AddBonus addBonus;
    public KeyCode KeyCode1;
    public KeyCode KeyCode2;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode1)) {
            addBonus.AddNewBonus("diamond");
        }

        if (Input.GetKeyDown(KeyCode2)) {
            addBonus.AddNewBonus("sword");
        }
    }
}
