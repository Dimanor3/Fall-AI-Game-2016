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

    /// <summary>
	/// Get and Set health.
    /// </summary>
    /// <value>The hp.</value>
    public int Hp {
        get {
            return hp;
        }

        set {
            hp = maxHP = value;
        }
    }

	/// <summary>
	/// Deal damage.
	/// </summary>
	/// <param name="damage">Damage.</param>
	public void dmg (int damage) {
		hp -= damage;
	}

	/// <summary>
	/// Set the amount of regen HP.
	/// </summary>
	/// <value>The regen H.</value>
	public int RegenHP {
        set {
            regenHP = value;
        }
	}

	/// <summary>
	/// Regen HP.
	/// </summary>
	public void regen () {
		hp += regenHP;
	}
}
