using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonSelectionEditor : MonoBehaviour
{
	public TMPro.TMP_InputField input;
	public Transform pokemonListHolder;
	public PokemonListItem pokemonListItemPrefab;

	private List<PokemonListItem> pokemon = new List<PokemonListItem>();
	private PokemonListItem selectedPokemon;

	private void Start()
	{
		foreach (Transform t in pokemonListHolder)
		{
			PokemonListItem pli = t.gameObject.GetComponent<PokemonListItem>();
			pli.editor = this;
			pokemon.Add(pli);

			if (selectedPokemon == null)
				selectedPokemon = pli;
		}
	}

	public void SelectPokemon(PokemonListItem selected)
	{
		selectedPokemon = selected;
	}

    public void AddPokemon()
	{
		if (input.text == "")
			return;

		string biomeText = input.text;
		PokemonListItem added = Instantiate(pokemonListItemPrefab, pokemonListHolder);
		pokemon.Add(added);
		added.textItem.text = biomeText;
		added.editor = this;

		input.text = "";

		selectedPokemon = added;
	}

    public void RemovePokemon()
	{
		if (selectedPokemon == null)
			return;

		int index = pokemon.IndexOf(selectedPokemon);

		pokemon.Remove(selectedPokemon);
		Destroy(selectedPokemon.gameObject);

		index = (index - 1 >= 0) ? index - 1 : 0;
		
		if (index+1 < pokemon.Count)
			selectedPokemon = pokemon[index];
	}

}
