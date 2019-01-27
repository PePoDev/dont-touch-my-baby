using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float damage = 20f;

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Building"))
		{
			var hp = collision.gameObject.GetComponent<HealthPoint>();
			hp.Hit(damage);
			Destroy(GetComponent<Bullet>());
		}
	}
}
