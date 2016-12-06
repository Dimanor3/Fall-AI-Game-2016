using UnityEngine;
using System.Collections;

public class Stamina : MonoBehaviour {

	// Player's stamina
	[SerializeField] private float stamina = 0f;
	private float maxStamina = 0f;
	[SerializeField] private float staminaLoss = 0f;	// Stamina lost per frame
	[SerializeField] private float staminaRegen = 0f;	// Stamina regained per frame
	private bool cheat = false;
	
	// Update is called once per frame
	void Update () {
		if (cheat) {
			stamina = 999999;
		}

		if(stamina >= maxStamina && !cheat){
			stamina = maxStamina;
		}

		if (stamina <= 0f && !cheat) {
			stamina = 0f;
		}
	}

	/// <summary>
	/// Sets a value indicating whether to cheat or not.
	/// </summary>
	/// <value><c>true</c> if cheat; otherwise, <c>false</c>.</value>
	public bool Cheat {
		set {
			cheat = value;
		}
	}

	/// <summary>
	/// Regenerate the player's stamina over time.
	/// </summary>
	public void regen(){
		stamina += staminaRegen;
	}

	/// <summary>
	/// Regenerate the player's stamina at half the speed.
	/// </summary>
	public void halfRegen(){
		stamina += staminaRegen / 2;
	}

	/// <summary>
	/// Regenerate the player's stamina at 3/4ths the speed.
	/// </summary>
	public void threeQuaterRegen () {
		stamina += (3 * staminaRegen) / 4;
	}

	/// <summary>
	/// Gets or sets the stamina S.
	/// </summary>
	/// <value>The amount of stamina.</value>
	public float StaminaSG {
		get {
			return stamina;
		}

		set {
			stamina = maxStamina = value;
		}
	}

	/// <summary>
	/// Sets the stamina regen.
	/// </summary>
	/// <value>The stamina regen.</value>
	public float StaminaRegen {
		set {
			staminaRegen = value;
		}
	}

	/// <summary>
	/// Use stamina.
	/// </summary>
	public void useStamina(){
		stamina -= staminaLoss;
	}

	/// <summary>
	/// Sets the stamina loss.
	/// </summary>
	/// <value>The stamina loss.</value>
	public float StaminaLoss {
		set {
			staminaLoss = value;
		}
	}

	/// <summary>
	/// Gets the max stamina.
	/// </summary>
	/// <value>The max stamina.</value>
	public float MaxStamina {
		get {
			return maxStamina;
		}
	}
}
