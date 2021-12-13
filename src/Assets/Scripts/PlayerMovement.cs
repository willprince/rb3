using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using static PermManager;

public class PlayerMovement : MonoBehaviour {
	
	[SerializeField]
	private float speed;

	[SerializeField]
	private Animator animator;

	void Start () {
		Debug.Log("Starting movement");
		gameObject.GetComponent<Rigidbody2D> ().position = PermManager.lastPosition;
	}

	void FixedUpdate () {

		// Set lastest player position
		

		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector2 currentVelocity = gameObject.GetComponent<Rigidbody2D> ().velocity;

		float newVelocityX = 0f;
		if (moveHorizontal < 0 && currentVelocity.x <= 0) {
			newVelocityX = -speed - 150;
			animator.SetInteger ("DirectionX", -1);
		} else if (moveHorizontal > 0 && currentVelocity.x >= 0) {
			newVelocityX = speed + 150;
			animator.SetInteger ("DirectionX", 1);
		} else {
			animator.SetInteger ("DirectionX", 0);
		}



		float newVelocityY = 0f;
		if (moveVertical < 0 && currentVelocity.y <= 0) {
			newVelocityY = -speed - 150;
			animator.SetInteger ("DirectionY", -1);
		} else if (moveVertical > 0 && currentVelocity.y >= 0) {
			newVelocityY = speed + 150;
			animator.SetInteger ("DirectionY", 1);
		} else {
			animator.SetInteger ("DirectionY", 0);
		}

		gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (newVelocityX, newVelocityY);

		
		PermManager.pManager.setLastPosition(gameObject.GetComponent<Rigidbody2D> ().position);

	}

	

}
