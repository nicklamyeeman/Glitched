using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;	//Allows us to use UI.

namespace Completed
{

public class Velocity : MonoBehaviour {


    public float restartLevelDelay = 1f;		//Delay time in seconds to restart level.
    private int food;                           //Used to store player food points total during level.
    public int pointsPerFood = 10;				//Number of points to add to player food points when picking up a food object.
    public int pointsPerSoda = 20;				//Number of points to add to player food points when picking up a soda object.
    public Text foodText;						//UI Text to display current player food total.
    public Rigidbody2D rb;
    private Vector3 mousePosition;
    private Vector2 dir;
    private float ms = 5f;
    private bool isDashing = false;
    public float targetTime = 60.0f;
    public float speed;
    public Animator anim;
    public GameObject enemy;

    public int physicDamage;
    public int life;
    private int l_life;

    private bool alreadyHit = false;
    private Vector3 myScale;
    private Vector3 reverse;

    public Cooldown skill1;
    public Cooldown skill2;
    public Cooldown skill3;
    public Cooldown skill4;

    // Start is called before the first frame update
    void Start() {
        l_life = life;
        myScale.x = transform.localScale.x;
        myScale.y = transform.localScale.y;
        myScale.z = transform.localScale.z;
        reverse = -myScale;
        reverse.y = transform.localScale.y;

        food = GameManager.instance.playerFoodPoints;
			
			//Set the foodText to reflect the current player food total.
        foodText.text = "Golds: " + food;
    }

    public void changeAttackState()
    {
        anim.SetBool("isHitting", false);
        alreadyHit = false;
    }

    public void move()
    {
        Vector3 Movement;

        if (Input.GetKey(KeyCode.Q))
            transform.localScale = reverse;
        else if (Input.GetKey(KeyCode.D))
            transform.localScale = myScale;
        if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.D))
            anim.SetBool("PlayerRun", true);
        else
            anim.SetBool("PlayerRun", false);
//        if (Input.GetMouseButton(0))
//        {
//            anim.SetBool("isHitting", true);
//        }
        if (anim.GetBool("isHitting") == false)
            Movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        else
            Movement = Vector3.zero;
        transform.position += Movement * speed * Time.deltaTime;
    }

    // Update is called once per frame
    private void FixedUpdate() {
        if (life <= 0)
            gameOver();
        if (l_life != life) {
            anim.SetBool("isHurt", true);
            l_life = life;
        }
        move();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && isDashing == false && skill3.getCooldown() == false) {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dir = (mousePosition - transform.position).normalized;
            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y)) {
                dir.x = (dir.x) / (2 * Mathf.Abs(dir.x));
                dir.y = (dir.y) / (2 * Mathf.Abs(dir.x));
            }
            else {
                dir.x = (dir.x) / (2 * Mathf.Abs(dir.y));
                dir.y = (dir.y) / (2 * Mathf.Abs(dir.y));
            }
            rb.velocity = new Vector2(dir.x * ms, dir.y * ms);
            isDashing = true;
            targetTime = 0.3f;
            anim.SetTrigger("PlayerDash");
        }
        if (targetTime <= 0.0f) {
            isDashing = false;
            rb.velocity = Vector2.zero;
        } else {
            targetTime -= Time.deltaTime;
        }
    }

		
    //Restart reloads the scene when called.
    private void Restart ()
    {
        //Load the last scene loaded, in this case Main, the only scene in the game. And we load it in "Single" mode so it replace the existing one
        //and not load all the scene object in the current scene.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
    

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && anim.GetBool("isHitting") && anim.GetBool("isHitting"))
        {
            Debug.Log("BOUFFE CA");
            alreadyHit = true;
            enemy.GetComponent<BasicEnemy>().life -= physicDamage;
        }
        //Check if the tag of the trigger collided with is Exit.
        if(collision.tag == "Exit")
        {
            //Invoke the Restart function to start the next level with a delay of restartLevelDelay (default 1 second).
            Invoke ("Restart", restartLevelDelay);
            //Disable the player object since level is over.
            enabled = false;
        }
        
        //Check if the tag of the trigger collided with is Food.
        else if(collision.tag == "Food")
        {
            //Add pointsPerFood to the players current food total.
            food += pointsPerFood;
            //Update foodText to represent current total and notify player that they gained points
            foodText.text = "+" + pointsPerFood + " Golds: " + food;
            //Call the RandomizeSfx function of SoundManager and pass in two eating sounds to choose between to play the eating sound effect.
            //SoundManager.instance.RandomizeSfx (eatSound1, eatSound2);
            //Disable the food object the player collided with.
            collision.gameObject.SetActive (false);
        }
        
        //Check if the tag of the trigger collided with is Soda.
        else if(collision.tag == "Soda")
        {
            //Add pointsPerSoda to players food points total
            food += pointsPerSoda;
            //Update foodText to represent current total and notify player that they gained points
            foodText.text = "+" + pointsPerSoda + " Golds: " + food;
            //Call the RandomizeSfx function of SoundManager and pass in two drinking sounds to choose between to play the drinking sound effect.
            //SoundManager.instance.RandomizeSfx (drinkSound1, drinkSound2);
            //Disable the soda object the player collided with.
            collision.gameObject.SetActive (false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && anim.GetBool("isHitting") && anim.GetBool("isHitting"))
        {
            alreadyHit = true;
            enemy.GetComponent<BasicEnemy>().life -= physicDamage;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            alreadyHit = false;
        }
    }

    public void unset_hurt()
    {
        anim.SetBool("isHurt", false);
    }

    private IEnumerator destroy_player()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("GameOver");
        Destroy(this.gameObject);
    }

    private void gameOver()
    {
        anim.SetBool("isDead", true);
    }
}
}