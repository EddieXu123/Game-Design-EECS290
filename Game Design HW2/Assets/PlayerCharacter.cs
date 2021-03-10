using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour {
	private int _health;
	[SerializeField] private GameObject healthBar;
	[SerializeField] private GameObject deathScreen;
	private Camera _camera;
	private int h_velocity = 3;
	private int v_velocity = 2;

	void Start() {
		_health = 5;
	}

	void Update() {
		_camera = GetComponent<Camera>();

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		healthBar = GameObject.Find("HealthBar");

		if (_health > 0) {
			string message = "";
			for (int i = 0; i < _health; i++) {
				message += "*";
			}
			healthBar.GetComponent<Text>().text = string.Concat("Health: ", message);
		}

		else {
			healthBar.GetComponent<Text>().text = "RIP";
			bounceDeath();
		}
	}

	public void Hurt(int damage) {
		_health -= damage;
	}

	void bounceDeath() {
		deathScreen = GameObject.Find("Death");
		deathScreen.GetComponent<Text>().text = "You have died!";
		if (deathScreen.transform.position.x > Screen.width ||
		deathScreen.transform.position.x < 0) {
			h_velocity *= -1;
		}

		if (deathScreen.transform.position.y > Screen.height ||
		deathScreen.transform.position.y < 0) {
			v_velocity *= -1;
		}

		deathScreen.transform.Translate(Vector3.left * h_velocity);
		deathScreen.transform.Translate(Vector3.up * v_velocity); 
		
	}
}


		