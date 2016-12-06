using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	// Health stuff
	private int maxHP = 0;
	[SerializeField] private int hp = 0;
	[SerializeField] private int regenHP = 0;
	private bool cheat = false;

	// Update is called once per frame
	void Update () {
		if (cheat) {
			hp = 99999;
		}

		if (hp >= maxHP && !cheat) {
			hp = maxHP;
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

	/// <summary>
	/// Heal the injured!.
	/// </summary>
	/// <param name="heal">How much do you want to heal by?</param>
	public void heal (int heal) {
		hp += heal;
	}
}
