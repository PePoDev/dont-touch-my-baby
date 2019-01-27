using UnityEngine;

public class momy : MonoBehaviour
{
	public AudioClip hit_sound, fix_sound;

	private Animator animator;
	private PlayerController playerController;

	private void Start()
	{
		animator = GetComponent<Animator>();
		playerController = GetComponent<PlayerController>();
	}

	private void Update()
	{
		var h = Input.GetAxis("Horizontal2");
		var v = Input.GetAxis("Vertical2");

		var vector = new Vector3(h, 0f, v);

		transform.LookAt(transform.position + vector);

		if (Input.GetButton("Fire12"))
		{
			animator.SetBool("isAttack", true);
			playerController.Speed = 4f;
		}
		else
		{
			animator.SetBool("isAttack", false);
			playerController.Speed = 9f;
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Enemy"))
		{
			if (Input.GetButton("Fire12"))
			{
				AudioSource.PlayClipAtPoint(hit_sound, Camera.main.transform.position);
				other.GetComponent<HealthPoint>().Hit(15f * Time.deltaTime);
			}
		}

		if (other.name.Equals("Baby"))
		{
			if (Input.GetButtonDown("Fire22") && other.GetComponent<Baby>().isProblem)
			{
				AudioSource.PlayClipAtPoint(fix_sound, Camera.main.transform.position);
				other.GetComponent<Baby>().Fix();
			}
		}
	}
}
