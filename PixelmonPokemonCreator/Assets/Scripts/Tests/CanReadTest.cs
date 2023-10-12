using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CanReadTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string[] files;
        
        files = Directory.GetFiles(@"E:\tmp\poke_moves", "*.json", SearchOption.AllDirectories);
        foreach (string filename in files)
		{
            string contents = File.ReadAllText(filename);

            Move m = JsonUtility.FromJson<Move>(contents);
            print(m.attackName);
		}

        
        files = Directory.GetFiles(@"E:\tmp\poke_species", "*.json", SearchOption.AllDirectories);
        foreach (string filename in files)
        {
            string contents = File.ReadAllText(filename);

            Pokemon p = JsonUtility.FromJson<Pokemon>(contents);
            print(p.name);
        }

        
        files = Directory.GetFiles(@"E:\tmp\poke_spawning", "*.json", SearchOption.AllDirectories);
        foreach (string filename in files)
        {
            string contents = File.ReadAllText(filename);

            PokemonSpawn p = JsonUtility.FromJson<PokemonSpawn>(contents);
            print(p.id);
        }
    }
}
