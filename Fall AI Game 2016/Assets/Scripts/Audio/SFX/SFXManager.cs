using UnityEngine;
using System.Collections;

public class SFXManager : MonoBehaviour {

	// List of AudioSources
	[SerializeField] private AudioSource buttonClick;
	[SerializeField] private AudioSource openDoor;
	[SerializeField] private AudioSource closeDoor;
	[SerializeField] private AudioSource gunShot;
	[SerializeField] private AudioSource lightFootSteps;
	[SerializeField] private AudioSource running;
	[SerializeField] private AudioSource heavyBreathing;

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

	public AudioSource ButtonClick {
		get {
			return buttonClick;
		}
	}

	public AudioSource OpenDoor {
		get {
			return openDoor;
		}
	}

	public AudioSource CloseDoor {
		get {
			return closeDoor;
		}
	}

	public AudioSource GunShot {
		get {
			return gunShot;
		}
	}

	public AudioSource LightFootSteps {
		get {
			return lightFootSteps;
		}
	}

	public AudioSource Running {
		get {
			return running;
		}
	}

	public AudioSource HeavyBreathing {
		get {
			return heavyBreathing;
		}
	}
}
