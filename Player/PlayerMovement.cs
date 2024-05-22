using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 5f;
    public bool playerHasBoost = false;
    public float boostSpeed = 10f;
    public GameObject playerObject;
    public Rigidbody rb;

    private Animator playerAnimator;
    private float initialSpeed;
    private float leftBoundary = -7;
    private float rightBoundary = 8;

    private void Start()
    {
        playerAnimator = playerObject.GetComponent<Animator>();
        initialSpeed = playerSpeed;
    }

    void Update()
    {
        // Als het spel niet actief is of gepauzeerd is dan ervoor zorgen dat de speler niet kan bewegen 
        if (!LogicManager.instance.isGameActive || LogicManager.instance.isGamePaused) return;

        // Als de speler snelheidboost heeft dan 'playerHasBoost = true', dus boostSpeed aanhouden 
        if (LogicManager.instance.playerHasSpeedBoost)
        {
            playerHasBoost = true;
        } else
        {
            playerHasBoost = false;
        }
        

        HandleSpeed();
        HandleMovement();
    }

    private void HandleMovement()
    {
        // Variable om de bewegingsrichting te bewaren; het bereik is van -1 tot 1
        float moveDirection = 0f;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            // Ervoor zorgen dat de speler niet verder beweegt dan de linkergrens 
            if (transform.position.x >= leftBoundary)
            {
                ChangeScale(-1);
                moveDirection = -1f;
            }
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            // Ervoor zorgen dat de speler niet verder beweegt dan de rechtergrens 
            if (transform.position.x <= rightBoundary)
            {
                ChangeScale(1);
                moveDirection = 1f;
            }
        }

        // De horizontale snelheid van het Rigitbody aanpassen
        Vector3 velocity = rb.velocity;
        velocity.x = moveDirection * playerSpeed;
        rb.velocity = velocity;

        // De animatie updated op basis van als de speler beweegt
        playerAnimator.SetBool("isRunning", moveDirection != 0);
    }

    private void ChangeScale(float val)
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, val);
    }

    private void HandleSpeed()
    {
        if (playerHasBoost)
        {
            playerSpeed = boostSpeed;
        }
        else
        {
            playerSpeed = initialSpeed;
        }
    }
}
