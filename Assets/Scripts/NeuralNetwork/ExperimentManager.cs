using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentManager : MonoBehaviour
{
	Population population;
	Timer timer;
	int currentIndex;
	bool canRun;
	[SerializeField] int populationSize;
	[SerializeField] GameObject player;
	List<GameObject> rooms;
	List<Vector2> positions;

	private void Start()
	{
		canRun = true;
		rooms = new List<GameObject>(GameObject.FindGameObjectsWithTag("Room"));
		timer = GetComponent<Timer>();
		currentIndex = 0;
		population = new Population(0.2f, rooms.Count, player);
		positions = new List<Vector2>();
		foreach (GameObject g in rooms)
		{
			GameObject pos;
			foreach (Transform t in g.transform)
			{
				if (t.CompareTag("Respawn"))
				{
					pos = t.gameObject;
					positions.Add(pos.transform.position);
				}
			}


		}

		population.altInitPopulation(positions.ToArray(), rooms.ToArray());

		RunAll();

	}

	private void RunAll()
	{
		for (int i = 0; i < population.Size(); i++)
		{
			population[i].transform.parent = rooms[i].transform;
			population[i].Run();
		}
	}

	private void Update()
	{
		if (canRun)
		{
			for (int i = 0; i < population.Size(); i++)
			{
				if (population[i].GetComponent<Rigidbody2D>().velocity != Vector2.zero)
				{
					return;
				}
			}

			canRun = false;
			timer.Fire(0.5f, ApplyGeneticOperators);

		}
	}
	public bool isGenerationTested()
	{
		if (currentIndex < population.Size())
		{
			return false;
		}

		else
		{
			return true;
		}
	}
	public void RunSingleGeneration()
	{
		Player temp = population[currentIndex++];
		temp.Stop();
		print(population[currentIndex - 1].GetInstanceID() + " Stopped");
		if (!isGenerationTested())
		{
			population[currentIndex].Run();
			timer.Fire(3, RunSingleGeneration);
		}

		else
		{
			ApplyGeneticOperators();
		}

	}

	public void ApplyGeneticOperators()
	{
		population.CrossOver();
		population.Mutate();
		population.RegeneratePopulation();
		canRun = true;
		RunAll();
		
	}
}
