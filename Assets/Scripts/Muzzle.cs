using UnityEngine;

public class Muzzle : MonoBehaviour
{
	private float timeDestroy = 0.1f;
	void Start()
	{
		Destroy(gameObject, timeDestroy);
	}

	// Update is called once per frame
	void Update()
	{

	}
}
