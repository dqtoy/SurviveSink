﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {
	float TimeInterval = 0.25f;
	float timePassed = 0f;
	float startedWithTime;
	public float expireTime = 180f;
	public GameObject fire;
	public bool onFire = true;
	Shader[] fireShader;
	
	Flammable burn;

	// Use this for initialization
	void Start () {
		StartCoroutine(createFire());
		startedWithTime = expireTime;
		
		burn = transform.parent.gameObject.GetComponent<Flammable>();
		
		MeshRenderer[] temp = GetComponentsInChildren<MeshRenderer>(true);
		fireShader = new Shader[temp.Length];
		for(int i = 0; i!=temp.Length; i++){
			fireShader[i] = temp[i].material.shader;
		}
		
		if(!onFire){
			StartCoroutine(stopFire());
		}
	}
	
	// Update is called once per frame
	void Update() {
		if(onFire){
			expireTime -= Time.deltaTime;
			
			float absTimePassed = startedWithTime * 1.1f - expireTime;
			float modifier = absTimePassed * absTimePassed / startedWithTime / startedWithTime;
			
			timePassed += Time.deltaTime * modifier;
			if(timePassed > TimeInterval){
				timePassed = 0;
				StartCoroutine(createFire());
			}
			
			if(expireTime < 0){
				if(burn != null)
					StartCoroutine(burn.burn());
				else
					Destroy(transform.parent.gameObject);
			}
		}
	}
	
	IEnumerator createFire(){
		Instantiate(fire, transform.position, transform.rotation);
		yield return null;
	}
	
	public IEnumerator stopFire(){
		onFire = false;
		MeshRenderer[] temp = GetComponentsInChildren<MeshRenderer>(true);
		foreach(MeshRenderer e in temp)
			e.material.shader = null;
		tag = "UnlitFire";
		yield return null;
	}
	
	public IEnumerator startFire(){
		onFire = true;
		MeshRenderer[] temp = GetComponentsInChildren<MeshRenderer>(true);
		for(int i = 0; i!=temp.Length; i++)
			temp[i].material.shader = fireShader[i];
		tag = "Fire";
		yield return null;
	}
}
