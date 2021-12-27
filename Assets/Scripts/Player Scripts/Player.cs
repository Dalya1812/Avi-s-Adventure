using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct PlayerInfo
{
	public float[] weights;
	public Perceptron[] perceptrons;
	public Vector3 localPosition;
	public Color color;
	public Transform parent;

	public PlayerInfo(Perceptron[] i_Perceptrons, Vector3 i_LocalPosition, Color i_Color)
	{
		weights = null;
		perceptrons = i_Perceptrons;
		localPosition = i_LocalPosition;
		color = i_Color;
		parent = null;
	}

	public PlayerInfo(float[] i_Weights, Vector3 i_LocalPosition, Color i_Color, Transform i_Parent)
	{
		this.weights = i_Weights;
		this.color = i_Color;
		this.parent = i_Parent;
		perceptrons = null;
		this.localPosition = i_LocalPosition;
	}

	public override string ToString()
	{
		string weights = string.Empty;
		Debug.Log("Number of weights: " + weights.Length);
		foreach(float weight in weights)
		{
			Debug.Log("calcing weights");
			weights += weights + weight.ToString() + " ";
		}
		return $"Weights: {weights}, position: {localPosition}, color: {color}, parent: {parent.gameObject}";
	}

	public void Mutate(float i_MutationChance)
	{
		float chance = Random.Range(0, 1f);
		Color mutatedAddition = new Color(0, 0, 0);
		for(int i = 0; i < perceptrons.Length; i++)
		{
			if (chance <= i_MutationChance)
			{
				perceptrons[i].Mutate(i_MutationChance);
				mutatedAddition.r += Random.Range(-0.2f, 0.2f);
				mutatedAddition.g += Random.Range(-0.2f, 0.2f);
				mutatedAddition.b += Random.Range(-0.2f, 0.2f);
			}
		}

		color += mutatedAddition;
	}
}
public class Player : MonoBehaviour
{
	Vector2 initialPosition;
	Movement movementM;
	CoinCollection collectibleM;
	NeuralNetwork brainM;
	Timer timer;

	public float Fitness { get; set; }
	public void InitSelf()
	{

		timer = GetComponent<Timer>();
		movementM = GetComponent<Movement>();
		movementM.InitSelf();

		collectibleM = GetComponent<CoinCollection>();
		
		brainM = GetComponent<NeuralNetwork>();
		brainM.InitSelf();

		//SetMovementBasedOnGuess();
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
		Vector2 guess = brainM.GenerateOutput(transform.localPosition);
	
		movementM.SetMovementVector(guess);
		timer.Fire(0.1f, SetMovementBasedOnGuess);
	}

	public void Run()
	{
		movementM.canRun = true;

		SetMovementBasedOnGuess();
	}

	public void Stop()
	{
		movementM.canRun = false;
	}

	public void LoadProperties(PlayerInfo i_Info, GameObject i_Room)
	{
		brainM.InitFromOther(i_Info.perceptrons);
		GetComponent<SpriteRenderer>().color = i_Info.color;
		transform.parent = i_Room.transform;
		transform.localPosition = new Vector3(-9.6f, 5.4f, 0f);
	}

	public PlayerInfo GetInfo()
	{
		Color color = GetComponent<SpriteRenderer>().color;
		PlayerInfo p = new PlayerInfo(brainM.GetPerceptrons(), transform.localPosition, color);
		return p;

		//Transform parent = transform.parent;
		//PlayerInfo p = new PlayerInfo((float[])brainM.weights.Clone(), transform.localPosition, color, parent);

	}

	public bool HasStopped()
	{
		return movementM.hasStopped;
	}
}
