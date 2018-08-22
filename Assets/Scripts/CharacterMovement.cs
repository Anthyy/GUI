using System.Collections;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]//makes it so you don't have to go to 'Add Component' in the Inspector and find it yourself
[AddComponentMenu("FirstPerson/Player Movement")] // Makes it easy to find this script in our own category called "FirstPerson"
public class CharacterMovement : MonoBehaviour 
{
	[Header("Movement Variables")]
	[Space(10)]
	[Header("Mini Header")]
	[Range(0f,10f)]
		public float speed = 6.0f;
		public float jumpSpeed = 8.0f;
		public float gravity = 20.0f;
		private Vector3 moveDirection = Vector3.zero;//'.zero' is zeroing it out (0,0,0) so nothing is moving when we start the game
		public CharacterController controller;
	[Header("Example")]
	public float a;
	public float b, c;
	void Start () 
	{
		controller = this.GetComponent<CharacterController>();
		//single line comment
		/*
		multi
		line
		comment
		*/
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(controller.isGrounded)
		{
			moveDirection = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;
			if(Input.GetButton("Jump"))
			{
				moveDirection.y = jumpSpeed;
			}
		}
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
	}
}
