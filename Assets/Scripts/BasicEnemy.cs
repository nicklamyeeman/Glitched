using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Completed 
{

public class BasicEnemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject target;
    public float speed = 3f;
    public float attack1Range = 1f;
    public float timeBetweenAttacks;
    public int attack1Damage = 1;
    public int life;

    private int l_life;
    private float ms = 1f;
    private float hitbox_offset = 0.3f;

    private bool alreadyHit = false;
    private Vector2 dir;
    private Vector3 reverse;
    private Vector3 myScale;
    private Vector3 playerPosition;

    public Animator anim;

    public HealthBar playerHealth;

    // Use this for initialization
    void Start()
    {
        Rest();
        l_life = life;
        myScale.x = transform.localScale.x;
        myScale.y = transform.localScale.y;
        myScale.z = transform.localScale.z;
        reverse = -myScale;
        reverse.y = transform.localScale.y;
        //StartCoroutine(death());
    }

    public void reset_auto()
    {
        alreadyHit = false;
    }

    private void real_hit()
    {
        if (target.GetComponent<Velocity>().life > 0)
        {
            anim.SetBool("isHitting", true);
            rb.velocity = Vector2.zero;
        }
        else
            anim.SetBool("isHitting", false);
    }

    private void FixedUpdate()
    {
        if (life <= 0)
            death();
    }

    void Update()
    {
        if (l_life != life && life > 0)
        {
            l_life = life;
            anim.SetBool("isHurt", true);
            Debug.Log("Hello");
        }
    }

    private void do_hit()
    {
        anim.SetBool("isHurt", false);
    }

    private void run()
    {
        anim.SetBool("isHitting", false);
        anim.SetBool("isRunning", true);
        rb.velocity = new Vector2(dir.x * ms, dir.y * ms);
    }

    private void check_dist()
    {
        if (target.transform.position.y > transform.position.y && transform.position.y > target.transform.position.y - hitbox_offset) {
            real_hit();
        } else if (target.transform.position.y < transform.position.y && transform.position.y < target.transform.position.y + hitbox_offset) {
            real_hit();
        } else
            run();
    }

    public void MoveToPlayer()
    {
        if (anim.GetBool("isDead") == false)
        {
            Debug.Log("FALSE");
            dir = (target.transform.position - transform.position).normalized;
            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            {
                dir.x = (dir.x) / (2 * Mathf.Abs(dir.x));
                dir.y = (dir.y) / (2 * Mathf.Abs(dir.x));
            }
            else
            {
                dir.x = (dir.x) / (2 * Mathf.Abs(dir.y));
                dir.y = (dir.y) / (2 * Mathf.Abs(dir.y));
            }
            if (target.transform.position.x > transform.position.x)
            {
                transform.localScale = myScale;
                if (transform.position.x > target.transform.position.x - hitbox_offset)
                {
                    check_dist();
                }
                else
                    run();
            }
            else
            {
                transform.localScale = reverse;
                if (transform.position.x < target.transform.position.x + hitbox_offset)
                {
                    check_dist();
                }
                else
                    run();
            }
        }
    }

    public void Rest()
    {
        anim.SetBool("isRunning", false);
        rb.velocity = Vector2.zero;
    }

    public IEnumerator destroy_component()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }

    private void death()
    {
        Debug.Log("Is dead");
        anim.SetBool("isDead", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && anim.GetBool("isHitting") == true && alreadyHit == false)
        {
            alreadyHit = true;
            target.GetComponent<Velocity>().life -= 1;
            playerHealth.decreaseLife();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && anim.GetBool("isHitting") == true && alreadyHit == false)
        {
            alreadyHit = true;
            target.GetComponent<Velocity>().life -= 1;
            playerHealth.decreaseLife();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        alreadyHit = false;
        Debug.Log("EXIT");
    }
}
}