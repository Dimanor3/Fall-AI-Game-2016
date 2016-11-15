using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour {
	[SerializeField] private float lerpSpeed;
	[SerializeField] private float fillAmount;//filler variable
	[SerializeField] private Image filler;//image filler
	public float MaxValue{ get; set;}

	public float Value{
		set{
			fillAmount = Bound (value, 0, MaxValue, 0, 1);//sets the fill amount
		}

	}

	void Start () {
		// Initialize all necessary variables
		lerpSpeed = 5f;
		fillAmount = 1f;
	}

	// Update is called once per frame
	void Update () {
		BarHandler ();
	}

	/// <summary>
	/// makes sure the new filler amount changes the image filler amount.
	/// </summary>
	private void BarHandler(){
		if(fillAmount != filler.fillAmount){
			filler.fillAmount = Mathf.Lerp(filler.fillAmount, fillAmount, Time.deltaTime * lerpSpeed);
		}
	}

	/// <summary>
	/// bounds value between 0 and 1.
	/// </summary>
	/// <param name="value">Value.</param>
	/// <param name="inMin">In minimum.</param>
	/// <param name="inMax">In max.</param>
	/// <param name="outMin">Out minimum.</param>
	/// <param name="outMax">Out max.</param>
	private float Bound(float value, float inMin, float inMax, float outMin, float outMax){
		return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;

	}
}
