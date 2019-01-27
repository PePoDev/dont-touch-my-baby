using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
	public float damage = 50f;
	public GameObject sfx;

	public bool isTrue = false;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Enemy"))
		{
			// play audio
			Instantiate(sfx, transform.position, transform.rotation, null);
			other.GetComponent<HealthPoint>().Hit(damage);
			
			if (isTrue) {
				Destroy(gameObject);
			}
			else
			{
				isTrue = true;
			}
		}
	}
}