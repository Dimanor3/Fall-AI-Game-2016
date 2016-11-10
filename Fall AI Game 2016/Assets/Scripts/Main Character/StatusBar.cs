﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour {
	[SerializeField] private float lerpSpeed = 5;
	[SerializeField] private float fillAmount = 1;//filler variable
	[SerializeField] private Image filler;//image filler
	public float MaxValue{ get; set;}

	public float Value{
		set{
			fillAmount = Bound (value, 0, MaxValue, 0, 1);//sets the fill amount
		}

	}
		
	// Update is called once per frame
	void Update () {
		BarHandler ();
	}

	private void BarHandler(){//makes sure the new filler amount changes the image filler amount
		if(fillAmount != filler.fillAmount){
			filler.fillAmount = Mathf.Lerp(filler.fillAmount, fillAmount, Time.deltaTime * lerpSpeed);
		}
	}

	private float Bound(float value, float inMin, float inMax, float outMin, float outMax){//bounds value between 0 and 1
		return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;

	}
}
