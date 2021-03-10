using UnityEngine;
using System.Collections;

public class ReactiveTarget : MonoBehaviour {

	[SerializeField] private GameObject enemyPrefab;
	[SerializeField] private GameObject tombstone_01Prefab;
	[SerializeField] private GameObject tombSpawner;

	public void ReactToHit() {
		WanderingAI behavior = GetComponent<WanderingAI>();
		if (behavior != null && behavior.IsAlive()) {
			behavior.SetAlive(false);
			StartCoroutine(Die());
		}
	}

	IEnumerator RotateMe(Vector3 byAngles, float inTime) {
    	var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
       	for(var t = 0f; t < 1; t += Time.deltaTime/inTime) {
        	transform.rotation = Quaternion.Lerp(fromAngle, toAngle, t);
            yield return null;
        }
    }

	private IEnumerator Die() {
		Spawn();
		Spawn();
		SpawnTomb();

		StartCoroutine(RotateMe(Vector3.right * 90, 1));
		yield return new WaitForSeconds(1.5f);

		Destroy(this.gameObject);
	}

	void Spawn() {
		GameObject _enemy = Instantiate(enemyPrefab) as GameObject;
		_enemy.transform.position = new Vector3(0, 0, 0);
		float angle = Random.Range(0, 360);
		_enemy.transform.Rotate(0, angle, 0);
	}

	void SpawnTomb() {
		tombSpawner = Instantiate(tombSpawner) as GameObject;
		tombSpawner.transform.position = this.transform.position;
	}
}
