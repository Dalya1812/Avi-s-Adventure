using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population
{
	List<Player> pop;
	[SerializeField] float m_MutationRate;
	[SerializeField] int m_PopulationSize;
	GameObject player;

	public Population(float i_MutationRate, int i_PopulationSize, GameObject i_Player)
	{
		m_MutationRate = i_MutationRate;
		m_PopulationSize = i_PopulationSize;
		player = i_Player;

		//initPopulation();
	}

	public void altInitPopulation(Vector2[] positions)
	{
			pop = new List<Player>();
			for (int i = 0; i < positions.Length; i++)
			{
				Player p = GameObject.Instantiate(player, positions[i], Quaternion.identity).GetComponent<Player>();
				p.InitSelf();
				p.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
				pop.Add(p);
			}
	}
	private void initPopulation()
	{
		pop = new List<Player>();
		for (int i = 0; i < m_PopulationSize; i++)
		{
			Player p = GameObject.Instantiate(player).GetComponent<Player>();
			p.InitSelf();
			p.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
			pop.Add(p);
		}
	}

	public void ResetPopulation()
	{
		foreach(Player p in pop)
		{
			p.ResetPosition();
		}

		for (int i = 0; i < m_PopulationSize; i++)
		{
			Player p = GameObject.Instantiate(player).GetComponent<Player>();
			p.InitSelf();
			p.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
		}
	}

	public Player this[int i]
	{
		get { return pop[i]; }
		set { pop[i] = value; }
	}

	public int Size()
	{
		return pop.Count;
	}

	public void Mutate()
	{
		foreach(Player p in pop)
		{
			p.Mutate(m_MutationRate);
		}
	}

	public void CrossOver()
	{
		List<Player> temp = new List<Player>();
		for(int i = 0; i < m_PopulationSize; i++)
		{
			temp.Add(ThreeWayTournement());
		}

		List<float[]> weights = new List<float[]>();
		foreach (Player p in temp)
		{
		//	brains.Add(p.GetComponent<Perceptron>().CloneWithType());
			weights.Add(p.GetComponent<Perceptron>().weights);
		}

		foreach(Player p in pop)
		{
			GameObject.Destroy(p.gameObject);
		}

		foreach(Player p in temp)
		{
			GameObject.Destroy(p.gameObject);
		}

		pop.Clear();
		temp.Clear();

		for(int i = 0; i < m_PopulationSize; i++)
		{
			Player p = GameObject.Instantiate(player).GetComponent<Player>();
			p.InitSelf();
			p.LoadBrain(weights[i]);
			pop.Add(p);
			//comment
		}
	}

	private Player ThreeWayTournement()
	{
		Player p1 = pop[Random.Range(0, pop.Count)];
		Player p2 = pop[Random.Range(0, pop.Count)];
		Player p3 = pop[Random.Range(0, pop.Count)];

		return Fitter(Fitter(p1, p2), p3);
	}

	private Player Fitter(Player i_Player1, Player i_Player2)
	{
		return (i_Player1.Fitness > i_Player2.Fitness ? i_Player1 : i_Player2);
	}

}
