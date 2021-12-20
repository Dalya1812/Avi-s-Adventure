using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
	// Start is called before the first frame update
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Goal"))
		{
			GetComponent<Player>().Win();
			Destroy(collision.gameObject);
		}
	}
}
