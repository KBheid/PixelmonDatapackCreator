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
			string cleanedPokemonName = p.name.Replace(' ', '_').Replace("'", "");
			
			string fileName = p.dex.ToString() + "_" + cleanedPokemonName + ".json";

			string data = JsonUtility.ToJson(p, true);

			File.WriteAllText(dataPath + fileName, data);
		}
	}

}
