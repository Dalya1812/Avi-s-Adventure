using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	Movement movementM;
	CoinCollection collectibleM;
	Perceptron brainM;
	Timer timer;
	public void InitSelf()
	{
		timer = GetComponent<Timer>();
		transform.position = new Vector2(-8, 3.5f);
		movementM = GetComponent<Movement>();
		movementM.InitSelf();

		collectibleM = GetComponent<CoinCollection>();
		
		brainM = GetComponent<Perceptron>();
		brainM.InitSelf();

		SetMovementBasedOnGuess();
	}

    public void Win()
	{
		print("Avi Won!!!");
	}

	public void Mutate(float i_MutationRate)
	{
		brainM.Mutate(i_MutationRate);
	}


	public void SetMovementBasedOnGuess()
	{
		Vector2 guess = brainM.Guess(transform.position);
		movementM.SetMovementVector(guess);
		timer.Fire(1, SetMovementBasedOnGuess);
	}

	public void Run()
	{
		movementM.canRun = true;
		timer.Fire(1, SetMovementBasedOnGuess);
	}

	public void Stop()
	{
		movementM.canRun = false;
	}
}
