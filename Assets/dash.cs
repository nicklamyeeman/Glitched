using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dash : MonoBehaviour
{
    private Vector3 mousePosition;
    private Vector2 dir;
    private float ms = 10f;
    private bool isDashing = false;
    public float targetTime = 60.0f;
    // Start is called before the first frame update
    public Rigidbody2D rb;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isDashing == false) {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("HELLO2");
            dir = (mousePosition - transform.position).normalized;
            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y)) {
                dir.x = (dir.x) / (2 * Mathf.Abs(dir.x));
                dir.y = (dir.y) / (2 * Mathf.Abs(dir.x));
            } else {
                dir.x = (dir.x) / (2 * Mathf.Abs(dir.y));
                dir.y = (dir.y) / (2 * Mathf.Abs(dir.y));
            }
            rb.velocity = new Vector2(dir.x * ms, dir.y * ms);
            isDashing = true;
            targetTime = 0.3f;
        } if (targetTime <= 0.0f) {
            isDashing = false;
            rb.velocity = Vector2.zero;
        } else {
            targetTime -= Time.deltaTime;
        }
    }
}
