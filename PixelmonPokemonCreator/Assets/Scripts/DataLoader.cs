using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
	public GameObject openingPanel;
	public TMPro.TMP_InputField dirNameInput;

	public SpawningManager spawningManager;
	public PokemonManager pokemonManager;
	public MovesManager movesManager;

	public FileContentPanel fileContentPanel;

	public void OnEnterDirectoryInfo()
	{
		openingPanel.SetActive(false);

		string dirName = dirNameInput.text;

		OpenDirectory(dirName);
	}

	void OpenDirectory(string dir)
	{
		if (dir[^1] != Path.DirectorySeparatorChar)
		{
			dir += Path.DirectorySeparatorChar;
		}

		UIState.instance.dataPath = dir;

		string spawningPath		= dir + "spawning";
		string pokemonPath		= dir + "species";
		string movesPath		= dir + "moves";
		string abilitiesPath	= dir + "abilities";

		GetAllSpawning(spawningPath);
		GetAllPokemon(pokemonPath);
		GetAllMoves(movesPath);
		GetAllAbilities(abilitiesPath);

		fileContentPanel.UpdateContents(pokemonManager.pokemon);

	}

	void GetAllSpawning(string path)
	{
		string[] files = Directory.GetFiles(path, "*.json", SearchOption.AllDirectories);

		foreach (string filename in files)
		{
			string contents = File.ReadAllText(filename);

			PokemonSpawn ps = JsonUtility.FromJson<PokemonSpawn>(contents);

			spawningManager.pokemonSpawns.Add(ps);
		}
	}

	void GetAllPokemon(string path)
	{
		string[] files = Directory.GetFiles(path, "*.json", SearchOption.AllDirectories);

		foreach (string filename in files)
		{
			string contents = File.ReadAllText(filename);

			Pokemon p = JsonUtility.FromJson<Pokemon>(contents);

			pokemonManager.AddPokemon(p);
		}
	}


	void GetAllMoves(string path)
	{
		string[] files = Directory.GetFiles(path, "*.json", SearchOption.AllDirectories);

		foreach (string filename in files)
		{
			string contents = File.ReadAllText(filename);

			Move m = JsonUtility.FromJson<Move>(contents);

			movesManager.moves.Add(m);
		}

	}

	readonly string GET_ABILITY_NAME_REGEX = ".*\\.(.*)\".*";
	void GetAllAbilities(string path)
    {
		string[] files = Directory.GetFiles(path, "*.json", SearchOption.AllDirectories);

		foreach (string filename in files)
        {
			string contents = File.ReadAllText(filename);

			Match m = Regex.Match(contents, GET_ABILITY_NAME_REGEX);
			string ability = m.Groups[1].Value;

			MovesManager.instance.abilities.Add(ability);
        }
    }

}
