using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
	[SerializeField] GameObject coinContainer;
	GameObject currentContainer;
	private void Start()
	{
		foreach(Transform t in transform)
		{
			if(t.CompareTag("CoinContainer"))
			{
				currentContainer = t.gameObject;
			}
		}
	}

	public void RegenerateContainer()
	{
		Destroy(currentContainer.gameObject);
		currentContainer = Instantiate(coinContainer, transform.localPosition, Quaternion.identity );
		currentContainer.transform.SetParent(transform.parent);
	}
}
