using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollection : MonoBehaviour
{
	public int Fitness
	{	get; private set;	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Coin"))
		{
			Fitness += 10;
			Destroy(collision.gameObject);
		}
	}

}
