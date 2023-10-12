using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SearchBar : MonoBehaviour
{
	public UIState state;
	public SpawningManager spawningManager;
	public PokemonManager pokemonManager;
	public MovesManager movesManager;
	public FileContentPanel fcp;
	public TMPro.TMP_InputField input;

	public void Clear()
	{
		input.text = "";
		ValueChanged();
	}

	public void ValueChanged()
	{
		string searchString = input.text.ToLower();

		switch (state.state)
		{
			case UIMode.Spawning:
				fcp.UpdateContents(FilterSpawns(searchString));
				break;
			case UIMode.Pokemon:
				fcp.UpdateContents(FilterPokemon(searchString));
				break;
			case UIMode.Moves:
				fcp.UpdateContents(FilterMoves(searchString));
				break;
		}

	}


	List<PokemonSpawn> FilterSpawns(string filterString)
	{
		IEnumerable<PokemonSpawn> valid = 
			spawningManager.pokemonSpawns.Where(
				spawn => 
					spawn.id.ToLower().Contains(filterString) ||
					spawn.spawnInfos.Any(
						(info) => {
							if (info.condition.biomes == null)
								return false;

							return info.condition.biomes.Any(
								biome => 
									biome.Split("/")[^1].ToLower() == filterString ||   // #pixelmon:spawning/jungles === jungles
									biome.ToLower() == filterString						// #pixelmon:spawning/jungles
							); 
						})
					);

		return new List<PokemonSpawn>(valid);
	}
	List<Pokemon> FilterPokemon(string filterString)
	{
		bool isInt = int.TryParse(filterString, out int dexNum);

		IEnumerable<Pokemon> valid =
			pokemonManager.pokemon.Where(
				pokemon =>
					pokemon.name.ToLower().Contains(filterString) ||            // Pokemon name
					(isInt && dexNum == pokemon.dex) ||                         // Pokedex number
					pokemon.forms.Any((form) => {                               // Has Ability
						if (form.abilities == null || form.abilities.abilities == null || form.abilities.hiddenAbilities == null) return false;

						return form.abilities.abilities.Any((ability) => { return ability != null && ability.ToLower() == filterString; }) ||
						form.abilities.hiddenAbilities.Any((ability) => { return ability != null && ability.ToLower() == filterString; });
					})
			);

		return new List<Pokemon>(valid);
	}
	List<Move> FilterMoves(string filterString)
	{
		IEnumerable<Move> valid =
			movesManager.moves.Where(
				move =>
					move.attackName.ToLower().Contains(filterString) ||
					move.effects.Any(
						(effect) => { return (effect != null) && effect.effectTypeID.ToLower() == filterString; } 
					)
			);

		return new List<Move>(valid);
	}
}

