using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PokemonManager : MonoBehaviour
{
	public static PokemonManager instance;

	[HideInInspector]
	public Pokemon selectedPokemon;
	[HideInInspector]
	public Form selectedForm;

	public delegate void PokemonSwitched(Pokemon p);
	public delegate void PokemonFormSwitched(Form m);

	public static event PokemonSwitched OnPokemonSwitched;
	public static event PokemonFormSwitched OnPokemonFormSwitched;

	private bool hasSelection = false;

	private void Awake()
	{
		instance = this;

		selectedPokemon = null;
		selectedForm = null;
	}

	public List<Pokemon> pokemon;

	public void CreateNewPokemon()
	{
		string filename = UIState.instance.dataPath + "assets\\editor\\defaultNewPokemon.json";
		string contents = File.ReadAllText(filename);

		Pokemon p = JsonUtility.FromJson<Pokemon>(contents);
		AddPokemon(p);
		SelectPokemon(p);
	}

	public void AddPokemon(Pokemon p)
	{
		pokemon.Add(p);
		if (!hasSelection)
		{
			selectedPokemon = p;
			selectedForm = p.forms[0];
			hasSelection = true;

			OnPokemonSwitched?.Invoke(selectedPokemon);
			OnPokemonFormSwitched?.Invoke(selectedForm);
		}
	}

	public void RemovePokemon(Pokemon p)
	{
		pokemon.Remove(p);
		// TODO
	}

	public void SelectPokemon(Pokemon p)
	{
		Pokemon tmp = selectedPokemon;
		selectedPokemon = p;
		OnPokemonSwitched?.Invoke(p);

		if (tmp != selectedPokemon)
			SelectForm(selectedPokemon.forms[0]);
	}

	public void SelectForm(Form f)
	{
		selectedForm = f;
		OnPokemonFormSwitched?.Invoke(f);
	}

}
