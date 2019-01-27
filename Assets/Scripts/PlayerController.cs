using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
	public float Speed;
	public bool isPlayer2;

	private string m_horizontal = "Horizontal";
	private string m_vertical = "Vertical";
	private Animator animator;
	private CharacterController characterController;

	private void Start()
	{
		if (isPlayer2)
		{
			m_horizontal += "2";
			m_vertical += "2";
		}

		animator = GetComponent<Animator>();
		characterController = GetComponent<CharacterController>();
	}

	private void Update()
	{
		// move character 
		var horizontal = Input.GetAxis(m_horizontal);
		var vertical = Input.GetAxis(m_vertical);

		var target = new Vector3(horizontal, -9.8f, vertical);

		if (horizontal != 0 || vertical != 0)
		{
			animator.SetBool("isWalk", true);
		}
		else
		{
			animator.SetBool("isWalk", false);
		}

		characterController.Move(target * Time.deltaTime * Speed);
	}
}
