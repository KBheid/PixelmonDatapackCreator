using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileContentPanel : MonoBehaviour
{
    public FileContent fileContentPrefab;

	public void UpdateContents(List<Pokemon> pokemon)
	{
		ClearItems();
		foreach (Pokemon p in  pokemon)
		{
			Instantiate(fileContentPrefab, transform).SetContent(p);
		}
	}
	public void UpdateContents(List<Move> moves)
	{
		ClearItems();
		foreach (Move m in moves)
		{
			Instantiate(fileContentPrefab, transform).SetContent(m);
		}
	}
	public void UpdateContents(List<PokemonSpawn> spawns)
	{
		ClearItems();
		foreach (PokemonSpawn ps in spawns)
		{
			Instantiate(fileContentPrefab, transform).SetContent(ps);
		}
	}

	private void ClearItems()
	{
		foreach (Transform t in transform)
		{
			Destroy(t.gameObject);
		}
	}
}
