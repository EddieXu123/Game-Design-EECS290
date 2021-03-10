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

		while(true)
		{
			tomb.transform.Rotate(2, 0, 0);
			yield return null;
		}
	}
}