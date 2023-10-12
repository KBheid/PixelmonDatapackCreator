using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class PokemonSpawn
{
	public string id;
	public Spawninfo[] spawnInfos;
}

[Serializable]
public class Spawninfo
{
	public string spec;
	public string[] stringLocationTypes;
	public int minLevel;
	public int maxLevel;
	public string typeID;
	public Helditem[] heldItems;
	public SpawnCondition condition;
	public float rarity;
	public Groupspawnsetting[] groupSpawnSettings;
	public Raritymultiplier[] rarityMultipliers;
	public AntiSpawnCondition anticondition;
	public string[] tags;
	public int minMaxAge;
	public int maxMaxAge;
	public int minY;
	public int maxY;
	public int requiredSpace;
}

[Serializable]
public class SpawnCondition
{
	public string[] times;
	public string[] biomes;
	public string[] structures;
	public int minY;
	public string[] weathers;
	public int maxY;
	public string[] baseBlocks;
	public string[] stringBiomes;
	public string[] partyHeadSpecies;
	public int maxLightLevel;
	public string[] neededNearbyBlocks;
	public string[] dimensions;
	public int moonPhase;
}


[Serializable]
public class AntiSpawnCondition
{
	public string[] biomes;
}

[Serializable]
public class Helditem
{
	public string itemID;
	public int percentChance;
}

[Serializable]
public class Groupspawnsetting
{
	public string spec;
	public Amount amount;
	public Spawn_Range spawn_range_minimum;
	public Spawn_Range spawn_range_maximum;
	public float chance;
}

[Serializable]
public class Amount
{
	public int min_value;
	public int max_value;
}

[Serializable]
public class Spawn_Range
{
	public int x;
	public int y;
	public int z;
}

[Serializable]
public class Raritymultiplier
{
	public float multiplier;
	public SpawnCondition condition;
}
