using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	// Health stuff
	private int maxHP = 0;
	[SerializeField] private int hp = 0;
	[SerializeField] private int regenHP = 0;
	
	// Update is called once per frame
	void Update () {
		if (hp >= maxHP) {
			hp = maxHP;
		}
	}

    // Get and Set health
    public int Hp {
        get {
            return hp;
        }

        set {
            hp = maxHP = value;
        }
    }

	// Deal damage
	public void dmg (int damage) {
		hp -= damage;
	}

	// Set the amount of regen HP
	public int RegenHP {
        set {
            regenHP = value;
        }
	}

	// Regen HP
	public void regen () {
		hp += regenHP;
	}
}
