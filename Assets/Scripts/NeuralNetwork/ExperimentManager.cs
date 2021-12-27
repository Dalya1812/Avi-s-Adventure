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
		timer.Fire(10f, ApplyGeneticOperators);

	}

	private void RunAll()
	{
		for (int i = 0; i < population.Size(); i++)
		{
			//population[i].transform.parent = rooms[i].transform;
			population[i].Run();
		}
		//timer.Fire(1f, ApplyGeneticOperators);
	}

	private void Update()
	{

		//if(Input.GetKeyDown(KeyCode.U))
		//{
		//	Time.timeScale += 1;
		//}

		//if (Input.GetKeyDown(KeyCode.D))
		//{
		//	Time.timeScale -= 1;
		//}

		if (canRun)
		{
			for (int i = 0; i < population.Size(); i++)
			{
				//if (population[i].GetComponent<Rigidbody2D>().velocity != Vector2.zero)
				if(!population[i].HasStopped())
				{
					return;
				}
			}

			canRun = false;
			timer.Stop();
			ApplyGeneticOperators();
			//timer.Fire(0.1f, ApplyGeneticOperators);
		}
	}


	public void ApplyGeneticOperators()
	{
		print(population.ShowFittest());
		population.CrossOver();
		population.RegeneratePopulation();
		population.Mutate();
		ResetRooms();
		canRun = true;
		RunAll();
		timer.Fire(10f, ApplyGeneticOperators);
		
	}

	public void ResetRooms()
	{
		foreach(GameObject room in rooms)
		{
			room.GetComponent<Level>().RegenerateContainer();
		}
	}
}

//public bool isGenerationTested()
//{
//	if (currentIndex < population.Size())
//	{
//		return false;
//	}

//	else
//	{
//		return true;
//	}
//}


//public void RunSingleGeneration()
//{
//	Player temp = population[currentIndex++];
//	temp.Stop();
//	print(population[currentIndex - 1].GetInstanceID() + " Stopped");
//	if (!isGenerationTested())
//	{
//		population[currentIndex].Run();
//		timer.Fire(3, RunSingleGeneration);
//	}

//	else
//	{
//		ApplyGeneticOperators();
//	}

//}