using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baby : MonoBehaviour
{
	public GameObject[] emote;

	private GameManager gameManager;
	private float timer = 0f;
	public bool isProblem = false;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();		
	}

	private void Update()
	{
		if (isProblem == false) {
			timer += Time.deltaTime;
			if (timer > 39f)
			{
				timer -= 39f;
				var r = UnityEngine.Random.Range(0, 2);
				emote[r].SetActive(true);
				isProblem = true;
			}
		}

		if (isProblem) {
			gameManager.AttackHome(10 * Time.deltaTime);
		}
	}

	public void Fix()
	{
		emote[0].SetActive(false);
		emote[1].SetActive(false);
		isProblem = false;
	}
}
