using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileContent : MonoBehaviour
{
	public TMPro.TMP_Text text;

	private Pokemon p;
	private Move m;
	private PokemonSpawn ps;

	public void SetContent(Pokemon p)
	{
		this.p = p;
		text.text = p.name;
	}
	public void SetContent(Move m)
	{
		this.m = m;
		text.text = m.attackName;
	}
	public void SetContent(PokemonSpawn ps)
	{
		this.ps = ps;
		text.text = ps.id;
	}

	public void Select()
	{
		switch (UIState.instance.state)
		{
			case UIMode.Spawning:
				break;
			case UIMode.Pokemon:
				PokemonManager.instance.SelectPokemon(p);
				break;
			case UIMode.Moves:
				break;
		}
	}
}
