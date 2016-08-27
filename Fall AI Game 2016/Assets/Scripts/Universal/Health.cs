using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	// Health stuff
	private int maxHP;
	[SerializeField] private int hp;
	[SerializeField] private int regenHP;

	// Use this for initialization
	void Start () {
		// Initialize variables
		maxHP = 0;
		hp = 0;
		regenHP = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (hp >= maxHP) {
			hp = maxHP;
		}
	}

	// Set the health :D
	public void setHP (int h) {
		hp = maxHP = h;
	}

	// Deal damage
	public void dmg (int damage) {
		hp -= damage;
	}

	// Get health
	public int getHP () {
		return hp;
	}

	// Set the amount of regen HP
	public void setRegenHP (int rHP) {
		regenHP = rHP;
	}

	// Regen HP
	public void regen () {
		hp += regenHP;
	}
}
