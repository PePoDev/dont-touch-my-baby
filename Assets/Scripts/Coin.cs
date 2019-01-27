using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
	public AudioClip coin_sound;

	public float Speed;

	private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	private void Update()
	{
		transform.Rotate(Vector3.up * Speed * Time.deltaTime);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			AudioSource.PlayClipAtPoint(coin_sound, Camera.main.transform.position);
			gameManager.Gold += 10;
			gameManager.UpdateGoldAndAmmoUI();
			Destroy(transform.parent.gameObject);
		}
	}
}
