using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, System.IComparable<Player>
{
	Vector2 initialPosition;
	Movement movementM;
	CoinCollection collectibleM;
	Perceptron brainM;
	Timer timer;

	public float Fitness
	{
		get { return brainM.Fitness; }
		set { brainM.Fitness = value; }
	}
	public void InitSelf()
	{

		timer = GetComponent<Timer>();
		initialPosition = transform.position;
		movementM = GetComponent<Movement>();
		movementM.InitSelf();

		collectibleM = GetComponent<CoinCollection>();
		
		brainM = GetComponent<Perceptron>();
		brainM.InitSelf();

		SetMovementBasedOnGuess();
	}

	public void ResetPosition()
	{
		transform.position = initialPosition;
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

	public int CompareTo(Player other)
	{
		Perceptron brain2 = other.GetComponent<Perceptron>();
		return brainM.CompareTo(brain2);
	}

	public void LoadBrain(float[] i_NewWeights)
	{
		brainM.InitFromOther(i_NewWeights);
	}
}
