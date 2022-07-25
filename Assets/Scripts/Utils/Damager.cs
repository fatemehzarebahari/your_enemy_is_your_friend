using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damager : MonoBehaviour
{
	public UnityEvent onDamage;

	private void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.tag == "Bullet" && other.gameObject.GetComponent<bullet>().shooter != this.gameObject){
			onDamage?.Invoke();
		}
	}
}
