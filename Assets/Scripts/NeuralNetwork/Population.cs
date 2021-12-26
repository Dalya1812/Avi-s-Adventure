using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population
{
	List<Player> pop;
	List<PlayerInfo> newGenerationInfo;
	[SerializeField] float m_MutationRate;
	[SerializeField] int m_PopulationSize;
	GameObject[] rooms;
	GameObject player;

	public Population(float i_MutationRate, int i_PopulationSize, GameObject i_Player)
	{
		m_MutationRate = i_MutationRate;
		m_PopulationSize = i_PopulationSize;
		player = i_Player;

		//initPopulation();
	}

	public void altInitPopulation(Vector2[] positions, GameObject[] rooms)
	{
		pop = new List<Player>();
		this.rooms = rooms;
		InitPopulationElements(positions, rooms);
	}

	private void InitPopulationElements(Vector2[] positions, GameObject[] rooms)
	{
		for (int i = 0; i < positions.Length; i++)
		{
			Player p = GameObject.Instantiate(player).GetComponent<Player>();
			p.transform.parent = rooms[i].transform;
			p.transform.position = positions[i];
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
			p.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));
			pop.Add(p);
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
		foreach(PlayerInfo p in newGenerationInfo)
		{
			p.Mutate(0.5f);
		}

		//foreach(Player p in pop)
		//{
		//	p.Mutate(m_MutationRate);
		//}
	}

	public void CrossOver()
	{
		newGenerationInfo = new List<PlayerInfo>();
		List<Player> temp = new List<Player>();
		for(int i = 0; i < m_PopulationSize; i++)
		{
			temp.Add(ThreeWayTournement());
		}

		List<PlayerInfo> newPop = new List<PlayerInfo>();
		foreach(Player p in temp)
		{
			newGenerationInfo.Add(p.GetInfo());
		}

		//Destroying the old generation.

		for(int i = 0; i < temp.Count; i++)
		{
			GameObject.Destroy(temp[i].gameObject);
			GameObject.Destroy(pop[i].gameObject);
		}

		temp.Clear();
		pop.Clear();
	}

	internal void RegeneratePopulation()
	{
		pop = new List<Player>();

		for(int i = 0; i < m_PopulationSize; i++)
		{
			Player p = GameObject.Instantiate(player).GetComponent<Player>();
			p.InitSelf();
			p.LoadProperties(newGenerationInfo[i], rooms[i]);

			pop.Add(p);
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
