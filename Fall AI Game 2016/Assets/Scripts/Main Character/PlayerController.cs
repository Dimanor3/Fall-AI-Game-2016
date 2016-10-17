using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof (PlayerMotor))]
[RequireComponent (typeof (Stamina))]
[RequireComponent (typeof (Health))]
[RequireComponent (typeof (Hidding))]
public class PlayerController : MonoBehaviour {

    // Player movement
    private PlayerMotor motor;                                              // Used to move the player
    [SerializeField] private float moveSpeed;
    [SerializeField] private float runSpeed;

    // Player stamina
    private Stamina stamina;
    [SerializeField] private float playerStamina;
    [SerializeField] private float useStaminaSpeed;
    [SerializeField] private float staminaRegen;

    // Player health
    private Health health;
    [SerializeField] private int hp;

    // Stuff for hidding the player
    private Hidding hidding;
    [SerializeField] private bool hidden = false;                           // Checks to see if hte player is currently hidding or not
    [SerializeField] private Vector2 hiddingSpotLocation = Vector3.zero;	// Holds the hidding spots location so that the player can move there

    // Player score
    private int score;                                                      // Stores the player's score
    [SerializeField] private Text playerScore;
    private GameObject[] lostObjects;                                       // Holds an array of GameObjects that are meant to be shown when the player losses.

    // Use this for initialization
    void Start () {
        // Initialize access to all outside classes
        motor = GetComponent<PlayerMotor> ();
        stamina = GetComponent<Stamina> ();
        health = GetComponent<Health> ();
        hidding = GetComponent<Hidding> ();

        // Initialize all required variables
        moveSpeed = 28f;
        runSpeed = 70f;
        playerStamina = 1000f;
        useStaminaSpeed = 5f;
        staminaRegen = 2f;
        hp = 100;
        score = 0;
        lostObjects = GameObject.FindGameObjectsWithTag ("Lost");

        // Initialize stamina properties
        stamina.setStamina (playerStamina);
        stamina.setStaminaLoss (useStaminaSpeed);
        stamina.setStaminaRegen (staminaRegen);

        // Initialize health properties
        health.Hp = hp;

        hideLost (lostObjects);
    }

    // Update is called once per frame
    void Update () {
        // Check to see if the player is hidding or not
        hidden = hidding.getHidding ();

        // Set hidden in motor
        motor.SetHidden (hidden);

        if (!hidden) {
            // Running?
            float run = Input.GetAxis ("Run");

            // Main Character left right up and down movement
            float horizontalMovement = Input.GetAxis ("Horizontal");
            float verticalMovement = Input.GetAxis ("Vertical");

            // Calculations for main characters movements
            Vector2 moveHorizontal = transform.right * horizontalMovement;
            Vector2 moveVertical = transform.up * verticalMovement;
            Vector2 movement = (moveHorizontal + moveVertical).normalized;

            // Set player's movement speed
            movement *= running (run);

            // Use the player's stamina?
            useStamina (run, horizontalMovement, verticalMovement);

            // Regen the player's stamina?
            regenStamina (run, horizontalMovement, verticalMovement);

            // Move the player
            motor.SetMovement (movement);
        } else {
            // This is used to move the player into the
            // hidding spot
            Vector2 moveToPos = hiddingSpotLocation - (Vector2)transform.position;

            if (moveToPos.magnitude > moveSpeed) {
                moveToPos.Normalize ();
                moveToPos *= moveSpeed;
            }

            motor.SetAIMovement (moveToPos);
        }

        // Stops the game after the player's health is
        // reduced to 0
        if (health.Hp <= 0) {
            moveScoreText ();

            playerScore.text = "Final Player Score: " + score;

            // Shows the objects that let's the player know they've lost
            showLost (lostObjects);

            // Stops the game
            Time.timeScale = 0f;
        }
    }

    // Determines whether or not the player is running or walking
    // and outputs the speed accordingly
    float running (float run) {
        if (run != 0 && stamina.getStamina () > 0) {
            return runSpeed;
        } else {
            return moveSpeed;
        }
    }

    // Checks to see if stamina is being used and uses it if it is
    void useStamina (float run, float hM, float vM) {
        if (run != 0 && stamina.getStamina () > 0 && (hM != 0 || vM != 0)) {
            stamina.useStamina ();
        }
    }

    // Deals damage towards the player
    public void dealDamage (int dmg) {
        health.dmg (dmg);
    }

    // Checks to see whether or not the player can regen their stamina
    void regenStamina (float run, float hM, float vM) {
        if (run == 0 && hM == 0 && vM == 0) {
            stamina.regen ();
        }

        if (run == 0 && (hM != 0 || vM != 0)) {
            stamina.halfRegen ();
        }
    }

    // Checks to see if the player has entered
    // a certain space
    void OnTriggerEnter2D (Collider2D col) {
        // Checks to see if the player is in the range
        // of a hidding spot
        if (col.CompareTag ("Hidding Spot")) {
            hiddingSpotLocation = col.gameObject.transform.position;
        }
    }

    // Return whether the player is currently hidden or not
    public bool Hidden {
        get {
            return hidden;
        }
    }

    // Increment the player's score as necessary
    public void incrementScore (int inc) {
        score += inc;
        playerScore.text = "Player Score: " + score;
    }

    // Get the player's score
    public int Score {
        get {
            return score;
        }
    }

    // Shows all gameobjects that are meant to be 
    // shown when the player loses
    private void showLost (GameObject[] gO) {
        foreach (GameObject g in gO) {
            g.SetActive (true);
        }
    }
    
    // Hides all gameobjects that are meant to be 
    // shown when the player loses
    private void hideLost (GameObject[] gO) {
        foreach (GameObject g in gO) {
            g.SetActive (false);
        }
    }

    // This is used to both resize, and move,
    // the score text box
    private void moveScoreText () {
        if (playerScore.fontSize < 25) {
            playerScore.fontSize++;
        }

        playerScore.transform.localPosition = Vector3.Lerp (playerScore.transform.localPosition, new Vector3 (0f, 50f, 0f), .01f);
    }
}
