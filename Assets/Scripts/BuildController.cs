using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildController : MonoBehaviour
{
	public AudioClip ammo_sound;
	public AudioClip build_sound;

	public GameObject[] ItemList;

	private GameObject[] tempGameObject;

	private Transform BuildingGroup;
	private int selectedItemIndex = 0;
	private bool isSelection = false;
	private NavMeshSurface nav;
	private GameManager gameManager;

	private KeyCode[] keyCodes = {
		 KeyCode.Alpha1,
		 KeyCode.Alpha2,
		 KeyCode.Alpha3,
		 KeyCode.Alpha4,
		 KeyCode.Alpha5,
	 };

	public void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		nav = GameObject.Find("NavMesh").GetComponent<NavMeshSurface>();
		BuildingGroup = GameObject.Find("Building").GetComponent<Transform>(); // get transform group of building
		tempGameObject = new GameObject[ItemList.Length]; // initial temp for gameObject pool
	}

	private void Update()
	{
		// Detect any number which press on keyboard
		for (int i = 0; i < keyCodes.Length; i++)
		{
			if (Input.GetKeyDown(keyCodes[i]))
			{
				if (keyCodes[i] == KeyCode.Alpha4)
				{
					if (gameManager.Gold >= 200)
					{
						gameManager.Gold -= 200;
						gameManager.HP += 200;
						gameManager.UpdateHpUI();
					}
					return;
				}
				else if (keyCodes[i] == KeyCode.Alpha5)
				{
					if (gameManager.Gold >= 100)
					{
						AudioSource.PlayClipAtPoint(ammo_sound, Camera.main.transform.position);
						gameManager.Gold -= 100;
						gameManager.Ammo += 30;
						gameManager.UpdateGoldAndAmmoUI();
					}
					return;
				}

				if (tempGameObject[i] == null)
				{
					tempGameObject[i] = Instantiate(
						ItemList[i],
						transform.position + (transform.forward * 2),
						transform.rotation,
						transform);

					if (keyCodes[i] == KeyCode.Alpha1)
					{
						Destroy(tempGameObject[i].GetComponentInChildren<BoxCollider>());
					}
					else if (keyCodes[i] == KeyCode.Alpha2)
					{
						Destroy(tempGameObject[i].GetComponentInChildren<Light>());
						Destroy(tempGameObject[i].GetComponentInChildren<BoxCollider>());
					}
					else if (keyCodes[i] == KeyCode.Alpha3)
					{
						Destroy(tempGameObject[i].GetComponentInChildren<BoxCollider>());
					}
				}
				else
				{
					tempGameObject[i].SetActive(true);
				}

				if (tempGameObject[selectedItemIndex] != null && (i != selectedItemIndex))
					tempGameObject[selectedItemIndex].SetActive(false);

				selectedItemIndex = i;
				isSelection = true;

				break;
			}
		}

		// Create item when pressed E
		if (isSelection && tempGameObject[selectedItemIndex] != null && Input.GetKeyDown(KeyCode.E))
		{
			switch (selectedItemIndex) {
				case 0:
					if (gameManager.Gold < 50) return;
					gameManager.Gold -= 50;
					break;
				case 1:
					if (gameManager.Gold < 100) return;
					gameManager.Gold -= 100;
					break;
				case 2:
					if (gameManager.Gold < 200) return;
					gameManager.Gold -= 200;
					break;
			}

			gameManager.UpdateGoldAndAmmoUI();

			AudioSource.PlayClipAtPoint(build_sound, Camera.main.transform.position);

			Instantiate(
				ItemList[selectedItemIndex], // Create Selected Item
				transform.position + (transform.forward * 2), // In front position of player
				transform.rotation, // same rotation
				BuildingGroup); // and add it to Building group

			tempGameObject[selectedItemIndex].SetActive(false);

			nav.BuildNavMesh();
			isSelection = false;
		}

		// Disable example build when pressed ESC
		if (tempGameObject[selectedItemIndex] != null && Input.GetKeyDown(KeyCode.Escape))
		{
			tempGameObject[selectedItemIndex].SetActive(false);
			isSelection = false;
		}
	}
}
