using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPoint : MonoBehaviour
{
	public float hp;
	public GameObject dieEffect;

	public void Hit(float damage)
	{
		hp -= damage;

		if (hp <= 0)
		{
			var particle = Instantiate(dieEffect, transform.position, transform.rotation, null);
			particle.GetComponent<ParticleSystem>().Play();
			Destroy(gameObject);
		}
	}
}
