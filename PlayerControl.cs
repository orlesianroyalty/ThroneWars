using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public float speed;
    public bool eventOn = false;

	private Rigidbody2D rb;
	private Animator anim;

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();

	}

	void Update () {
		if (!eventOn) {
			if (Input.GetKey (KeyCode.W) ^ Input.GetKey (KeyCode.A)
			    ^ Input.GetKey (KeyCode.S) ^ Input.GetKey (KeyCode.D)) {

				if (Input.GetKey (KeyCode.W)) {
					rb.velocity = new Vector2 (0, speed);
					SetOtherDirectionsFalse ("wdown");

				} else if (Input.GetKey (KeyCode.A)) {
					rb.velocity = new Vector2 (-1 * speed, 0);
					SetOtherDirectionsFalse ("adown");

				} else if (Input.GetKey (KeyCode.S)) {
					rb.velocity = new Vector2 (0, -1 * speed);
					SetOtherDirectionsFalse ("sdown");

				} else if (Input.GetKey (KeyCode.D)) {
					rb.velocity = new Vector2 (speed, 0);
					SetOtherDirectionsFalse ("ddown");
				}

			} else {
				StopMoving ();
			}
		} else {
			StopMoving ();
		}
	}

	void SetOtherDirectionsFalse(string dir) {
		anim.SetBool ("wdown", false);
		anim.SetBool ("adown", false);
		anim.SetBool ("sdown", false);
		anim.SetBool ("ddown", false);
		anim.SetBool (dir, true);
	}

	void StopMoving() {
		rb.velocity = new Vector2 (0, 0);
		SetOtherDirectionsFalse (" ");
	}
}
