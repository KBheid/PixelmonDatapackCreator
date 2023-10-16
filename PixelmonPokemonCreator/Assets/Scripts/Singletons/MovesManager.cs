using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovesManager : MonoBehaviour
{
	public static MovesManager instance;

	private void Awake()
	{
		instance = this;
	}

	public List<Move> moves;
	public List<string> abilities;

	public Move FindMove(string name)
    {
		foreach (Move m in moves)
        {
			if (m.attackName == name)
				return m;
        }

		return null;
    }

	public bool AbilityExists(string ability)
    {
		return abilities.Contains(ability);
    }
}
