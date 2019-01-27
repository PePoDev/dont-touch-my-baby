using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
	public float Speed = 9;

	public Transform Home;
	public NavMeshAgent agent;

	private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		Home = GameObject.Find("Home").GetComponent<Transform>();
		agent = GetComponent<NavMeshAgent>();
		agent.SetDestination(Home.position);
	}

	private void Update()
	{
		transform.LookAt(Home);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Home"))
		{
			agent.isStopped = true;
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Home"))
		{
			gameManager.AttackHome(30f * Time.deltaTime);
		}
	}

	private void OnCollisionStay(Collision collision)
	{
		// play audio
		if (collision.gameObject.CompareTag("Building"))
		{
			collision.gameObject.GetComponent<HealthPoint>().Hit(5f);
		}
	}

	private void OnDestroy()
	{
		// play audio
		gameManager.Gold += 50;
		gameManager.UpdateGoldAndAmmoUI();
	}
}
