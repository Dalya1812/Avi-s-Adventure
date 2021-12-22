using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentManager : MonoBehaviour
{
	Population population;
	Timer timer;
	int currentIndex;
	[SerializeField] int populationSize;
	[SerializeField] GameObject player;
	List<GameObject> rooms;

	private void Start()
	{
		rooms = new List<GameObject>(GameObject.FindGameObjectsWithTag("Room"));
		timer = GetComponent<Timer>();
		currentIndex = 0;
		population = new Population(0.2f, rooms.Count, player);
		List<Vector2> positions = new List<Vector2>();
		foreach(GameObject g in rooms)
		{
			GameObject pos;
			foreach(Transform t in g.transform)
			{
				if (t.CompareTag("Respawn"))
				{
					pos = t.gameObject;
					positions.Add(pos.transform.position);
				}
			}

			
		}

		print("Vector count: " + positions.Count);
		foreach(Vector2 v in positions)
		{
			print(v);
		}

		population.altInitPopulation(positions.ToArray());
		//population[0].Run();
		//timer.Fire(5, RunSingleGeneration);

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
			RunGeneticOperators();
		}

	}

	public void RunGeneticOperators()
	{
		population.CrossOver();
		population.Mutate();
		population.ResetPopulation();
		currentIndex = 0;
		RunSingleGeneration();
	}
}
