using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataSaver : MonoBehaviour
{
	public void Save()
	{
		SavePokemon();
	}

	void SavePokemon()
    {
		string dataPath = UIState.instance.dataPath;
		dataPath += "output\\species\\";
		Directory.CreateDirectory(dataPath);

		foreach (Pokemon p in PokemonManager.instance.pokemon)
		{
			string cleanedPokemonName = p.name.Replace(' ', '_').Replace("'", "").ToLower();

			string fileName = p.dex.ToString("000") + "_" + cleanedPokemonName + ".json";

			JsonSerializerSettings settings = new JsonSerializerSettings();
			settings.Converters.Add(new PokemonJsonConverter<Pokemon>());
			settings.Converters.Add(new PokemonJsonConverter<Form>());
			settings.Converters.Add(new PokemonJsonConverter<Moves>());
			settings.Converters.Add(new PokemonJsonConverter<Evolution>());
			settings.Converters.Add(new PokemonJsonConverter<Condition>());
			settings.Converters.Add(new PokemonJsonConverter<Dimensions>());
			settings.Converters.Add(new PokemonJsonConverter<Movement>());
			settings.Converters.Add(new PokemonJsonConverter<Gigantamax>());
			settings.Converters.Add(new PokemonJsonConverter<Abilities>());
			settings.Converters.Add(new PokemonJsonConverter<Genderproperty>());
			settings.Converters.Add(new PokemonJsonConverter<Palette>());
			settings.Converters.Add(new PokemonJsonConverter<Modellocator>());
			settings.Converters.Add(new PokemonJsonConverter<Flyingmodellocator>());
			//settings.Converters.Add(new PokemonJsonConverter<Flyingparameters>());
			//settings.Converters.Add(new PokemonJsonConverter<Mountedflyingparameters>());
			//settings.Converters.Add(new PokemonJsonConverter<Swimmingparameters>());
			settings.Converters.Add(new PokemonJsonConverter<Evyields>());
			string serializedData = JsonConvert.SerializeObject(p, Formatting.Indented, settings);
			
			File.WriteAllText(dataPath + fileName, serializedData);
		}
	}

}
