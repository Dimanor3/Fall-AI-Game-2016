using UnityEngine;
using System.Collections;

public class SFXManager : MonoBehaviour {

	// List of AudioSources
	[SerializeField] private AudioSource buttonClick, openDoor, closeDoor, gunShot, lightFootSteps, running, heavyBreathing, crawling, itemPickup;

	// Used to check if the SFX Manager already exists
	private static SFXManager sfxManagerExists;

	// Use this for initialization
	void Start () {
		// If the SFX Manager doesn't exist make
		// it exist and don't destroy it on load
		// otherwise destroy it.
		if (sfxManagerExists == null) {
			DontDestroyOnLoad (transform.gameObject);
			sfxManagerExists = this;
		} else if (sfxManagerExists != this) {
			Destroy (gameObject);
		}
	}

	/// <summary>
	/// Plays the button click sound effect.
	/// </summary>
	/// <returns>The button click sound effect.</returns>
	public AudioSource ButtonClick {
		get {
			return buttonClick;
		}
	}

	/// <summary>
	/// Plays the open door sound effect.
	/// </summary>
	/// <returns>The open door sound effect.</returns>
	public AudioSource OpenDoor {
		get {
			return openDoor;
		}
	}


	/// <summary>
	/// Plays the close door sound effect.
	/// </summary>
	/// <returns>The close door sound effect.</returns>
	public AudioSource CloseDoor {
		get {
			return closeDoor;
		}
	}

	/// <summary>
	/// Plays the gun shot sound effect.
	/// </summary>
	/// <returns>The gun shot sound effect.</returns>
	public AudioSource GunShot {
		get {
			return gunShot;
		}
	}

	/// <summary>
	/// Plays the light foot steps sound effect.
	/// </summary>
	/// <returns>The light foot steps sound effect.</returns>
	public AudioSource LightFootSteps {
		get {
			return lightFootSteps;
		}
	}

	/// <summary>
	/// Plays the running sound effect.
	/// </summary>
	/// <returns>The running sound effect.</returns>
	public AudioSource Running {
		get {
			return running;
		}
	}

	/// <summary>
	/// Plays the heavy breathing sound effect.
	/// </summary>
	/// <returns>The heavy breathing sound effect.</returns>
	public AudioSource HeavyBreathing {
		get {
			return heavyBreathing;
		}
	}

	/// <summary>
	/// Playes the crawling sound effect.
	/// </summary>
	/// <returns>The crawling sound effect.</returns>
	public AudioSource Crawling {
		get {
			return crawling;
		}
	}

	/// <summary>
	/// Playes the item pickup sound effect.
	/// </summary>
	/// <returns>The item pickup sound effect.</returns>
	public AudioSource ItemPickup {
		get {
			return itemPickup;
		}
	}
}
