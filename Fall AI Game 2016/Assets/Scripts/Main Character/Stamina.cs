using UnityEngine;
using System.Collections;

public class Stamina : MonoBehaviour {

	// Player's stamina
	[SerializeField] private float stamina = 0f;
	private float maxStamina = 0f;
	[SerializeField] private float staminaLoss = 0f;		// Stamina lost per frame
	[SerializeField] private float staminaRegen = 0f;	// Stamina regained per frame
	
	// Update is called once per frame
	void Update () {
		if(stamina >= maxStamina){
			stamina = maxStamina;
		}

		if (stamina <= 0f) {
			stamina = 0f;
		}
	}

	// Regenerate the players stamina over time
	public void regen(){
		stamina += staminaRegen;
	}

	// Regenerate the players stamin at half the speed
	public void halfRegen(){
		stamina += staminaRegen / 2;
	}

	// Set the amount of stamina the player has
	public void setStamina(float s){
		stamina = maxStamina = s;
	}

	// Set the players stamina regen speed
	public void setStaminaRegen(float r){
		staminaRegen = r;
	}

	// Use stamina
	public void useStamina(){
		stamina -= staminaLoss;
	}

	// Set the rate at which the player loses stamina
	public void setStaminaLoss(float sL){
		staminaLoss = sL;
	}

	// Get players stamina
	public float getStamina(){
		return stamina;
	}
}
