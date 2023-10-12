using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Move
{
	public int attackIndex;
	public string attackName;
	public string attackType;
	public string attackCategory;
	public int basePower;
	public int ppBase;
	public int ppMax;
	public int accuracy;
	public bool makesContact;
	public Effect[] effects;
	public object[] animations;
	public Targetinginfo targetingInfo;
	public Z[] z;
	public Flags flags;
	public bool ignoreAbility;
}

[Serializable]
public class Targetinginfo
{
	public bool hitsAll;
	public bool hitsOppositeFoe;
	public bool hitsAdjacentFoe;
	public bool hitsExtendedFoe;
	public bool hitsSelf;
	public bool hitsAdjacentAlly;
	public bool hitsExtendedAlly;
}

[Serializable]
public class Flags
{
	public bool authentic;
	public bool sound;
	public bool nontarget;
}

[Serializable]
public class Effect
{
	public int drainPercent;
	public Modifier[] modifiers;
	public bool persists;
	public string effectTypeID;
	public int priority;
	public string type;
	public int amount;
	public bool isUser;
	public int stages;
	public int minHits;
	public int maxHits;
	public string langStart;
	public string langFail;
	public string langEnd;
	public string weather;
	public string message;
	public string _base;
	public int percentRecoil;
	public string weatherRock;
	public Boosts boosts;
	public float multiplier;
	public string[] users;
	public int damage;
	public int increment;
	public string stat1;
	public string stat2;
	public int statValue1;
	public int statValue2;
	public string langString;
	public string[] swapStats;
	public string stat;
	public string affectedType;
	public string moveName;
	public float percent;
	public string ability;
	public int maxLayers;
	public bool onlyLowered;
}

[Serializable]
public class Boosts
{
	public int def;
}

[Serializable]
public class Modifier
{
	public string type;
	public float value;
}

[Serializable]
public class Z
{
	public string crystal;
	public string attackName;
	public int basePower;
	public Effect[] effects;
	public string[] allowedPokemon;
}
