using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector3 mousePosition;
    private Vector2 dir;
    private float ms = 10f;
    private bool isDashing = false;
    public float targetTime = 60.0f;

    public Rigidbody2D rb;
    // public RectTransform transform;
    public Animator animator;
    private Vector3 direction;
    private Vector3 reverse;
    private Vector2 movement;

    public Cooldown skill3;

    // Start is called before the first frame update
    void Start()
    {
        direction.x = 1;
        direction.y = 1;
        direction.z = 0;
        reverse = -direction;
        reverse.y = 1;
    }

    // Update is called once per frame // Inputs
    void Update() {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePosition.x < transform.position.x)
            transform.localScale = reverse;
        if (mousePosition.x > transform.position.x)
            transform.localScale = direction;
        if (movement.x != 0 || movement.y != 0)
            animator.SetBool("PlayerRun", true);
        else
            animator.SetBool("PlayerRun", false);
        if (movement.x < 0)
            transform.localScale = reverse;
        else if (movement.x > 0)
            transform.localScale = direction;
        if (Input.GetKeyDown(KeyCode.Space))
            animator.SetTrigger("PlayerDash");
        if (Input.GetKeyDown(KeyCode.Space) && isDashing == false && skill3.getCooldown() == false) {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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

        if (isDashing == false)
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    // Movements
    void FixedUpdate() {
    }
}
