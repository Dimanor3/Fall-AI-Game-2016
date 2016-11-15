using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof (PlayerMotor))]
[RequireComponent (typeof (Stamina))]
[RequireComponent (typeof (Health))]
[RequireComponent (typeof (Hidding))]
public class PlayerController : MonoBehaviour {

	[SerializeField] private SFXManager sfxMan;								// Get access to the SFXManager

	//status bars
	[SerializeField] private StatusBar healthBar, staminaBar;

	// Player movement
	private PlayerMotor motor;                                              // Used to move the player
	[SerializeField] private float crawlSpeed, moveSpeed, runSpeed;
	[SerializeField] private float run, crawl;												// Is the player running? Is the player crawling?

    // Player stamina
    private Stamina stamina;
    [SerializeField] private float playerStamina, useStaminaSpeed, staminaRegen;

    // Player health
    private Health health;
    [SerializeField] private int hp;

    // Stuff for hidding the player
    private Hidding hidding;
    [SerializeField] private bool hidden;									// Checks to see if hte player is currently hidding or not
    [SerializeField] private Vector2 hiddingSpotLocation;					// Holds the hidding spots location so that the player can move there

    // Player score
    private int score;                                                      // Stores the player's score
    [SerializeField] private GameObject playerScore, playerWinState;
    [SerializeField] private TextMesh playerScoreTextMesh, playerWinStateTextMesh;
    private GameObject[] winOrLoseObjects;                                  // Holds an array of GameObjects that are meant to be shown when the player losses.

	[SerializeField] private bool goal;                                     // Checks to see if the player has reached the goal

	[SerializeField] private soundMade soundMaker;							// Used to make sounds that guards can hear
	private float soundLevelWalking, soundLevelRunning, soundLevelCrawling;	// Amount of sound made by door

	void Awake () {
		// Instantiate the sfxMan to an object containing the SFXManager
		sfxMan = FindObjectOfType<SFXManager> ();

		// Initialize access to all outside classes
		motor = GetComponent<PlayerMotor> ();
		stamina = GetComponent<Stamina> ();
		health = GetComponent<Health> ();
		hidding = GetComponent<Hidding> ();

		// Initialize all required variables
		winOrLoseObjects = GameObject.FindGameObjectsWithTag ("WinOrLose");

		// Initialize soundMaker
		soundMaker = FindObjectOfType<soundMade> ();
	}

    // Use this for initialization
    void Start () {
        // Initialize all required variables
        moveSpeed = 28f;
        runSpeed = 70f;
		crawlSpeed = 14f;
        playerStamina = 1000f;
        useStaminaSpeed = 5f;
        staminaRegen = 2f;
        hp = 100;
        score = 0;
        goal = false;
		hidden = false;
		hiddingSpotLocation = Vector3.zero;
		soundLevelRunning = 100f;
		soundLevelWalking = 50f;
		soundLevelCrawling = 10f;

        // Initialize stamina properties
        stamina.StaminaSG = playerStamina;
        stamina.StaminaLoss = useStaminaSpeed;
        stamina.StaminaRegen = staminaRegen;
		staminaBar.MaxValue = playerStamina;

        // Initialize health properties
		health.Hp = hp;
		healthBar.MaxValue = hp;

        hideWinOrLose (winOrLoseObjects);
    }

    // Update is called once per frame
    void Update () {
		run = Input.GetAxis ("Run");

		crawl = Input.GetAxis ("Crawl");

		// Main Character left right up and down movement
		float horizontalMovement = Input.GetAxis ("Horizontal");
		float verticalMovement = Input.GetAxis ("Vertical");

		// Check to see if the player is hidding or not
		hidden = hidding.getHidding ();

        // Set hidden in motor
		motor.Hidden = hidden;

        if (!hidden) {
            // Calculations for main characters movements
            Vector2 moveHorizontal = transform.right * horizontalMovement;
            Vector2 moveVertical = transform.up * verticalMovement;
            Vector2 movement = (moveHorizontal + moveVertical).normalized;

			if ((horizontalMovement != 0 || verticalMovement != 0) && stamina.StaminaSG > 0) {
				if (run == 0 && crawl == 0) {
					if (!sfxMan.LightFootSteps.isPlaying) {
						sfxMan.Running.Stop ();
						sfxMan.Crawling.Stop ();
						sfxMan.LightFootSteps.Play ();
					}
				} else if (crawl != 0) {
					if (!sfxMan.Crawling.isPlaying) {
						sfxMan.Running.Stop ();
						sfxMan.LightFootSteps.Stop ();
						sfxMan.Crawling.Play ();
					}
				} else {
					if (!sfxMan.Running.isPlaying) {
						sfxMan.LightFootSteps.Stop ();
						sfxMan.Crawling.Stop ();
						sfxMan.Running.Play ();
					}
				}
			} else if ((horizontalMovement != 0 || verticalMovement != 0) && stamina.StaminaSG <= 0 && crawl == 0) {
				if (!sfxMan.LightFootSteps.isPlaying) {
					sfxMan.Running.Stop ();
					sfxMan.Crawling.Stop ();
					sfxMan.LightFootSteps.Play ();
				}
			} else if ((horizontalMovement != 0 || verticalMovement != 0) && stamina.StaminaSG <= 0 && crawl != 0) {
				if (!sfxMan.Crawling.isPlaying) {
					sfxMan.Running.Stop ();
					sfxMan.LightFootSteps.Stop ();
					sfxMan.Crawling.Play ();
				}
			} else {
				sfxMan.Crawling.Stop ();
				sfxMan.LightFootSteps.Stop ();
				sfxMan.Running.Stop ();
			}

			if (run != 0 && (horizontalMovement != 0 || verticalMovement != 0) && crawl == 0) {
				soundMaker.makeSound (soundLevelRunning, this.gameObject);
			} else if (run == 0 && (horizontalMovement != 0 | verticalMovement != 0) && crawl != 0) {
				soundMaker.makeSound (soundLevelCrawling, this.gameObject);
			} else if (run == 0 && crawl == 0 && (horizontalMovement != 0 || verticalMovement != 0)) {
				soundMaker.makeSound (soundLevelWalking, this.gameObject);
			}

            // Set player's movement speed
            movement *= running (run, crawl);

            // Use the player's stamina?
            useStamina (run, horizontalMovement, verticalMovement);

            // Regen the player's stamina?
            regenStamina (run, crawl, horizontalMovement, verticalMovement);

            // Move the player
			motor.Movement = movement;
        } else {
            // This is used to move the player into the
            // hidding spot
            Vector2 moveToPos = hiddingSpotLocation - (Vector2)transform.position;

            if (moveToPos.magnitude > moveSpeed) {
                moveToPos.Normalize ();
                moveToPos *= moveSpeed;
            }

			motor.AiMovement = moveToPos;
        }

		if (stamina.StaminaSG <= 200 && !sfxMan.HeavyBreathing.isPlaying) {
			//print ("TEST " + stamina.StaminaSG);
			sfxMan.HeavyBreathing.Play ();
		}

        // Stops the game after the player's health is
        // reduced to 0
        if (health.Hp <= 0) {
            wonOrLost ();
        }

        // Stops the game after the player has won
        if (goal) {
            wonOrLost ();
        }
    }

	/// <summary>
	/// Determines whether or not the player is running or walking
    /// and outputs the speed accordingly
	/// </summary>
	/// <param name="run">To see whether the player is pressing the running key.</param>
	/// <param name="crawl">To see whether the player is pressing the crawl key.</param>
	/// <returns>Returns the correct movement multiplyer.</returns>
	float running (float run, float crawl) {
        if (run != 0 && stamina.StaminaSG > 0) {
            return runSpeed;
		} else if (crawl != 0) {
			return crawlSpeed;
		} else {
            return moveSpeed;
        }
    }

	/// <summary>
    /// Checks to see if stamina is being used and uses it if it is.
	/// </summary>
	/// <param name="run">To see whether the player is pressing the running key.</param>
	/// <param name="hM">To see whether the player is moving horizontally.</param>
	/// <param name="vM">To see whether the player is moving vertically.</param>
    void useStamina (float run, float hM, float vM) {
        if (run != 0 && stamina.StaminaSG > 0 && (hM != 0 || vM != 0)) {
            stamina.useStamina ();
			staminaBar.Value = stamina.StaminaSG;//changes the status bar when stamina drains
        }
    }

	/// <summary>
	/// Deals damage towards the player.
	/// </summary>
	/// <param name="dmg">The amount of damage being dealt to the player.</param>
    public void dealDamage (int dmg) {
        health.dmg (dmg);
		healthBar.Value = health.Hp;	//changes the status bar when damage is taken
    }

	/// <summary>
	/// Checks to see whether or not the player can regen their stamina.
	/// </summary>
	/// <param name="run">To see whether the player is pressing the running key.</param>
	/// <param name="hM">To see whether the player is moving horizontally.</param>
	/// <param name="vM">To see whether the player is moving vertically.</param>
	void regenStamina (float run, float crawl, float hM, float vM) {
        if (run == 0 && hM == 0 && vM == 0) {
            stamina.regen ();
			staminaBar.Value = stamina.StaminaSG;//changes the status bar when stamina regens
        }

        if (run == 0 && (hM != 0 || vM != 0)) {
            stamina.halfRegen ();
			staminaBar.Value = stamina.StaminaSG;//changes the status bar when stamina regens by half
        }

		if (run == 0 && crawl != 0 && (hM != 0 || vM != 0)) {
			stamina.threeQuaterRegen ();
			staminaBar.Value = stamina.StaminaSG;//changes the status bar when stamina regens by three fourths
		}

		if (stamina.StaminaSG >= stamina.MaxStamina) {
			stamina.StaminaSG = stamina.MaxStamina;
			staminaBar.Value = stamina.StaminaSG;
		}
    }

	/// <summary>
    /// Checks to see if the player has entered a certain space.
	/// </summary>
	/// <param name="col">The collider of the object that has collided with the player.</param>
    void OnTriggerEnter2D (Collider2D col) {
        // Checks to see if the player is in the range
        // of a hidding spot
        if (col.CompareTag ("Hidding Spot")) {
            hiddingSpotLocation = col.gameObject.transform.position;
        }
    }

	/// <summary>
    /// Return whether the player is currently hidden or not.
	/// </summary>
    public bool Hidden {
        get {
            return hidden;
        }
    }

	/// <summary>
	/// Increment the player's score as necessary
	/// </summary>
	/// <param name="inc">The amount the player's score needs to be incremented by.</param>
    public void incrementScore (int inc) {
        score += inc;
        playerScoreTextMesh.text = "Player Score: " + score;
    }

	/// <summary>
	/// Get the player's score.
	/// </summary>
    public int Score {
        get {
            return score;
        }
    }

	/// <summary>
    /// Shows all gameobjects that are meant to be shown when the player loses.
	/// </summary>
	/// <param name="gO">The list of GameObjects that are going to be displayed to the player.</param>
    private void showWinOrLose (GameObject[] gO) {
        foreach (GameObject g in gO) {
            g.SetActive (true);
        }
    }

	/// <summary>
    /// Hides all gameobjects that are meant to be shown when the player loses.
	/// </summary>
	/// <param name="gO">The list of GameObjects that are going to be displayed to the player.</param>
    private void hideWinOrLose (GameObject[] gO) {
        foreach (GameObject g in gO) {
            g.SetActive (false);
        }
    }

	/// <summary>
	/// Determine whether the player has won or lost the game and displays the necessary information.
	/// </summary>
    private void wonOrLost () {
        moveScoreText ();

        if (goal) {
            playerWinStateTextMesh.text = "You Have Won!";
        } else {
            playerWinStateTextMesh.text = "You Have Lost!";
        }


        playerScoreTextMesh.text = "Final Player Score: " + score;

        // Shows the objects that let's the player know they've lost
		showWinOrLose (winOrLoseObjects);

        // Stops the game
        Time.timeScale = 0f;
    }

	/// <summary>
    /// This is used to both resize, and move, the score text box.
	/// </summary>
    private void moveScoreText () {
        playerScore.GetComponent<MeshRenderer> ().sortingOrder = 10;

        playerScoreTextMesh.characterSize = Mathf.Lerp (playerScoreTextMesh.characterSize, .6f, .008f);

        playerScore.transform.localPosition = Vector3.Lerp (playerScore.transform.localPosition, new Vector3 (0f, 0f, 0f), .01f);
    }

	/// <summary>
	/// Sets a value indicating whether this <see cref="PlayerController"/> is goal.
	/// </summary>
	/// <value><c>true</c> if goal; otherwise, <c>false</c>.</value>
    public bool Goal {
        set {
            goal = value;
        }
    }
}
