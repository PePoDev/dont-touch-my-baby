using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daddy : MonoBehaviour
{
	public AudioClip gun_sound;

	public GameObject Bullet;
	public float BulletSpeed;

	private GameObject bulletPool;
	private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	private void Update()
	{
		RaycastHit hit;
		var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out hit))
		{
			var point = hit.point;
			point.y = transform.position.y;
			transform.LookAt(point);
		}

		if (Input.GetButtonDown("Fire1"))
		{
			if (gameManager.Ammo > 0)
			{
				AudioSource.PlayClipAtPoint(gun_sound, Camera.main.transform.position);
				gameManager.Ammo--;
				Rigidbody bulletClone = Instantiate(Bullet, transform.position + transform.forward, transform.rotation).GetComponent<Rigidbody>();
				bulletClone.velocity = transform.forward * BulletSpeed;
				gameManager.UpdateGoldAndAmmoUI();
			}
		}
	}
}
