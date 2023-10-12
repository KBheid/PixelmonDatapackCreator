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
}
