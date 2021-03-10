using UnityEngine;
using System.Collections;

public class Tombstone : MonoBehaviour {
	[SerializeField] private GameObject tombstone_01Prefab;
	private bool isRotating;
    void Update() {
        Rotate();
    }
    
	public void Rotate()
	{
		if(isRotating) return;
		StartCoroutine(RotateRoutine());
		isRotating = true;
	}


	private IEnumerator RotateRoutine()
	{
		GameObject tomb = Instantiate(tombstone_01Prefab) as GameObject;
		tomb.transform.position = this.transform.position;
		// tomb.transform.position = new Vector3(this.transform.position.x, 1, this.transform.position.z);


		while(true)
		{
			tomb.transform.Rotate(0, 0, 4);
			yield return null;
		}
	}
}