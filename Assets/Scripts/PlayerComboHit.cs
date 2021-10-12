using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComboHit : MonoBehaviour
{

    public Animator anim;
    public int nbOfClick = 0;
    private float lastClickedTime = 0;
    private float maxCombotDelay = 1.5f;

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastClickedTime > maxCombotDelay) {
            nbOfClick = 0;
        }
        if (Input.GetMouseButtonDown(0)) {
            lastClickedTime = Time.time;
            nbOfClick ++;
            if (nbOfClick == 1) {
                anim.SetBool("isHitting", true);
                anim.SetBool("Attack1", true);
            }
            nbOfClick = Mathf.Clamp(nbOfClick, 0, 3);
        }
    }

    void return1() {
        if (nbOfClick >= 2) {
            anim.SetBool("Attack2", true);
        } else {
            anim.SetBool("Attack1", false);
            nbOfClick = 0;
        }
    }

    void return2() {
        if (nbOfClick >= 3) {
            anim.SetBool("Attack3", true);
        } else {
            anim.SetBool("Attack2", false);
            nbOfClick = 0;
        }
    }

    void return3() {
        anim.SetBool("Attack1", false);
        anim.SetBool("Attack2", false);
        anim.SetBool("Attack3", false);
        anim.SetBool("isHitting", false);
        nbOfClick = 0;
    }
}
