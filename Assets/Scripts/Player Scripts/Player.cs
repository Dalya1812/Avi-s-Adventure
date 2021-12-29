using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
	Vector2 initialPosition;
	Movement movementM;
	CoinCollection collectibleM;

    private void Start()
    {
		movementM = GetComponent<Movement>();
		movementM.InitSelf();
		collectibleM = GetComponent<CoinCollection>();
	}


    public void Win()
	{
		print("Avi Won!!!");
	}


	


}
