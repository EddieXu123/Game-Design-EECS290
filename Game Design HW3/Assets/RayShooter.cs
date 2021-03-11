using UnityEngine;
using System.Collections;
using UnityEngine.UI; /* Required for controlling Canvas UI system */


public class RayShooter : MonoBehaviour {
	private Camera _camera;
	[SerializeField] private GameObject reticle;
	[SerializeField] private GameObject ammo;

	void Start() {
		_camera = GetComponent<Camera>();

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;


		reticle = GameObject.Find("Reticle");
		reticle.GetComponent<Text>().text = "_*_";
		reticle.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
		reticle.GetComponent<RectTransform>().position =
            new Vector3(_camera.pixelWidth / 2.0f - reticle.GetComponent<Text>().fontSize / 4.0f,
                        _camera.pixelHeight / 2.0f - reticle.GetComponent<Text>().fontSize / 2.0f,
                        0.0f);
	}

    /** Deprecated in Unity 2018 
	void OnGUI() {
		int size = 12;
		float posX = _camera.pixelWidth/2 - size/4;
		float posY = _camera.pixelHeight/2 - size/2;
		GUI.Label(new Rect(posX, posY, size, size), "*");
	}
    **/
    

	void Update() {
		GameObject.Find("Floor").GetComponent<Renderer> ().material.color = Color.green;
		if (Input.GetMouseButtonDown(0)) {
			Vector3 point = new Vector3(_camera.pixelWidth/2, _camera.pixelHeight/2, 0);
			Ray ray = _camera.ScreenPointToRay(point);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)) {
				GameObject hitObject = hit.transform.gameObject;
				ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
				if (target != null) {
					target.ReactToHit();
					StartCoroutine(SphereIndicatorHit(hit.point));
				} else {
					StartCoroutine(SphereIndicator(hit.point));
				}
			}
		}
	}

// Player's gun (bullet)
	private IEnumerator SphereIndicator(Vector3 pos) {
		GameObject sphere = Instantiate(ammo) as GameObject;
		// GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphere.transform.position = new Vector3(pos.x, pos.y, pos.z);
		//sphere.transform.Translate(Vector3.forward);

		yield return new WaitForSeconds(2);

		Destroy(sphere);
	}

		private IEnumerator SphereIndicatorHit(Vector3 pos) {
		GameObject sphere = Instantiate(ammo) as GameObject;
		// GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphere.transform.position = new Vector3(pos.x, pos.y, pos.z);
		//sphere.transform.Translate(Vector3.forward);

		yield return new WaitForSeconds(0.2f);

		Destroy(sphere);
	}
}

// Player's rotation