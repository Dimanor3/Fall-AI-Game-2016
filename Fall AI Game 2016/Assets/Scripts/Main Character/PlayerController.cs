using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(Stamina))]
[RequireComponent(typeof(Health))]
public class PlayerController : MonoBehaviour {

	// Player movement
	private PlayerMotor motor;					// Used to move the player
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

	// Use this for initialization
	void Start () {
		// Initialize access to all outside classes
		motor = GetComponent<PlayerMotor> ();
		stamina = GetComponent<Stamina> ();
		health = GetComponent<Health> ();

		// Initialize all required variables
		moveSpeed = 28f;
		runSpeed = 70f;
		playerStamina = 1000f;
		useStaminaSpeed = 5f;
		staminaRegen = 2f;
		hp = 100;

		// Initialize stamina properties
		stamina.setStamina(playerStamina);
		stamina.setStaminaLoss (useStaminaSpeed);
		stamina.setStaminaRegen (staminaRegen);

		// Initialize health properties
		health.setHP (hp);
	}

	// Update is called once per frame
	void Update () {
		// Running?
		float run = Input.GetAxis("Run");

		// Main Character left right up and down movement
		float horizontalMovement = Input.GetAxis("Horizontal");
		float verticalMovement = Input.GetAxis("Vertical");

		// Calculations for main characters movements
		Vector2 moveHorizontal = transform.right * horizontalMovement;
		Vector2 moveVertical = transform.up * verticalMovement;
		Vector2 movement = (moveHorizontal + moveVertical).normalized;

		// Set player's movement speed
		movement *= running(run);

		// Use the player's stamina?
		useStamina (run, horizontalMovement, verticalMovement);

		// Regen the player's stamina?
		regenStamina (run, horizontalMovement, verticalMovement);

		// Move the player
		motor.SetMovement (movement);
	}

	// Determines whether or not the player is running or walking
	// and outputs the speed accordingly
	float running(float run){
		if (run != 0 && stamina.getStamina () > 0) {
			return runSpeed;
		} else {
			return moveSpeed;
		}
	}

	// Checks to see if stamina is being used and uses it if it is
	void useStamina(float run, float hM, float vM){
		if (run != 0 && stamina.getStamina () > 0 && (hM != 0 || vM != 0)) {
			stamina.useStamina ();
		}
	}

	// Checks to see whether or not the player can regen their stamina
	void regenStamina(float run, float hM, float vM){
		if (run == 0 && hM == 0 && vM == 0){
			stamina.regen ();
		}

		if (run == 0 && (hM != 0 || vM != 0)){
			stamina.halfRegen ();
		}
	}
}
