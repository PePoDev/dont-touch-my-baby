using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public bool IsGameOver
	{
		get { return (HP < 0); }
	}
	public float HP { get; set; } = 1080;
	public int Ammo { get; set; } = 20;
	public int Gold { get; set; } = 150;

	public AudioClip ui_sound, attack_sound, gameover_sound;

	public GameObject Ghost, GoldObj;
	public Transform EnemyGroup;
	public Transform[] Respawn;
	public Transform[] RespawnGold;
	public RectTransform UI_buildingInfo;

	public TextMeshProUGUI goldText;
	public TextMeshProUGUI ammoText;

	public GameObject gameOver;
	public TextMeshProUGUI daysCanSurvive;

	private int days = 1;

	private void Start()
	{
		var sun = GameObject.Find("Sun");
		sun.GetComponent<TimeController>().OnNextDay += OnNextDay;
		sun.GetComponent<TimeController>().OnNight += OnNight;

		switch (PlayerPrefs.GetInt("GameMode"))
		{
			case 1:
				sun.GetComponent<TimeController>().timeAllDay = 120f;
				break;
			case 2:
				sun.GetComponent<TimeController>().timeAllDay = 90f;
				break;
			case 3:
				sun.GetComponent<TimeController>().timeAllDay = 60f;
				break;
			case 4:
				sun.GetComponent<TimeController>().timeAllDay = 10f;
				break;
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.F1))
		{
			ToggleBuildingInfo();
		}
		else if (Input.GetKeyDown(KeyCode.F2))
		{
			Gold += 999999;
			Ammo += 999999;
			UpdateGoldAndAmmoUI();
		}

		if (Input.GetKeyDown(KeyCode.P) && !IsGameOver)
		{
			Time.timeScale = 0f;
		}

		if (IsGameOver) {
			if (Input.GetKeyDown(KeyCode.Backspace)) {
				SceneManager.LoadScene(0);
			}
		}
	}

	public void UpdateGoldAndAmmoUI()
	{
		goldText.text = Gold.ToString();
		ammoText.text = Ammo.ToString();
	}

	public void AttackHome(float damage)
	{
		AudioSource.PlayClipAtPoint(attack_sound, Camera.main.transform.position);
		HP -= damage;
		UpdateHpUI();
		if (IsGameOver) GameOver();
	}

	public void UpdateHpUI()
	{
		GameObject.Find("HP").GetComponent<RectTransform>().sizeDelta = new Vector2(40, HP);
	}

	private void GameOver()
	{
		AudioSource.PlayClipAtPoint(gameover_sound, Camera.main.transform.position);
		Time.timeScale = 0f;
		daysCanSurvive.text = "You survived in " + days + " days";
		gameOver.SetActive(true);
	}

	private void OnNextDay()
	{
		days++;
		var dayObj = GameObject.Find("Day");
		dayObj.GetComponent<TextMeshProUGUI>().text = "Days " + days;
		dayObj.GetComponent<Animator>().SetTrigger("NextDay");

		for (int i = 0; i < days; i++)
		{
			for (int j = 0; j < 2; j++)
			{
				Instantiate(GoldObj, RespawnGold[UnityEngine.Random.Range(0, RespawnGold.Length)].position, EnemyGroup.transform.rotation, EnemyGroup);
			}
		}
	}

	private void OnNight()
	{
		for (int i = 0; i < days; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				Instantiate(Ghost, Respawn[UnityEngine.Random.Range(0, Respawn.Length)].position, EnemyGroup.transform.rotation, EnemyGroup);
			}
		}
	}

	public void ToggleBuildingInfo()
	{
		AudioSource.PlayClipAtPoint(ui_sound, Camera.main.transform.position);
		UI_buildingInfo.DOAnchorPosY(UI_buildingInfo.anchoredPosition.y < -47f ? -47f : -281.45f, .5f);
	}
}
