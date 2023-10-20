using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Pokemon
{
	public string name;						
	public int dex;							
	[IgnoreEmptyArray][IgnoreEmptyStringArray]
	public string[] defaultForms;
	public Form[] forms;					
	public int generation;

	public Pokemon() { }
}

[Serializable]
public class Form
{
	public string name;						
	[IgnoreNull]
	public string[] tags;
	[IgnoreEmptyString][IgnoreNull]
	public string experienceGroup;
	[IgnoreNull]
	public Dimensions dimensions;
	[IgnoreAllFieldsNullFalseOrZero]
	public Moves moves;
	[IgnoreNull][IgnoreAllFieldsNullFalseOrZero]
	public Abilities abilities;
	[IgnoreAllFieldsNullFalseOrZero]
	public Movement movement;
	[IgnoreAllFieldsNullFalseOrZero]
	public Aggression aggression;
	[IgnoreAllFieldsNullFalseOrZero]
	public Battlestats battleStats;
	[IgnoreAllFieldsNullFalseOrZero]
	public Spawn spawn;
	[IgnoreNull][IgnoreEmptyArray][IgnoreEmptyStringArray]
	public string[] possibleGenders;
	[IgnoreNull][IgnoreEmptyArray]
	public Genderproperty[] genderProperties;
	[IgnoreEmptyArray][IgnoreEmptyStringArray]
	public string[] eggGroups;				
	[IgnoreEmptyArray][IgnoreEmptyStringArray]
	public string[] types;
	[IgnoreNull] /*[IgnoreEmptyArray]*/
	public string[] preEvolutions;
	[IgnoreNull]
	public string defaultBaseForm;
	//[IgnoreEmptyArray][IgnoreEmptyStringArray]
	[IgnoreNull]
	public string[] megaItems;
	//[IgnoreEmptyArray][IgnoreEmptyStringArray]
	[IgnoreNull]
	public string[] megas;
	[IgnoreAllFieldsNullFalseOrZero]
	public Gigantamax gigantamax;
	[IgnoreZero]
	public int eggCycles;
	[IgnoreZero]
	public float weight;
	[IgnoreZero]
	public int catchRate;
	[IgnoreZero]
	public float malePercentage;			
	[IgnoreNull][IgnoreEmptyArray]
	public Evolution[] evolutions;
	[IgnoreAllFieldsNullFalseOrZero]
	public Evyields evYields;

	public Form() { }
}

[Serializable]
public class Dimensions
{
	public float height;
	public float width;
	public float length;
	[IgnoreZero]
	public float eyeHeight;
	[IgnoreZero]
	public float hoverHeight;

	public Dimensions() { }
}

[Serializable]
public class Moves
{
	[IgnoreNull]
	public Levelupmove[] levelUpMoves;
	[IgnoreNull]
	public string[] tutorMoves;
	[IgnoreNull]
	public string[] eggMoves;
	[IgnoreNull]
	public string[] tmMoves8;
	[IgnoreNull]
	public string[] trMoves;
	[IgnoreNull]
	public string[] hmMoves;
	[IgnoreNull]
	public string[] transferMoves;
	[IgnoreNull]
	public string[] tmMoves7;
	[IgnoreNull]
	public string[] tmMoves6;
	[IgnoreNull]
	public string[] tmMoves5;
	[IgnoreNull]
	public string[] tmMoves4;
	[IgnoreNull]
	public string[] tmMoves3;
	[IgnoreNull]
	public string[] tmMoves2;
	[IgnoreNull]
	public string[] tmMoves1;
	[IgnoreNull]
	public string[] tmMoves;
	[IgnoreNull]
	public string[] tmMoves9;
}

[Serializable]
public class Levelupmove
{
	public int level;
	public string[] attacks;
}

[Serializable]
public class Abilities
{
	public string[] abilities;
	[IgnoreNull][IgnoreEmptyArray][IgnoreEmptyStringArray]
	public string[] hiddenAbilities;

	public Abilities() { }
}

[Serializable]
public class Movement
{
	public bool rideable;
	public bool canFly;
	public bool canSurf;
	public bool canRideShoulder;
	[IgnoreAllFieldsNullFalseOrZero]
	public Ridingoffsets ridingOffsets;
	[IgnoreNull][IgnoreAllFieldsNullFalseOrZero]
	public Flyingparameters flyingParameters;
	[IgnoreNull][IgnoreAllFieldsNullFalseOrZero]
	public Mountedflyingparameters mountedFlyingParameters;
	[IgnoreNull][IgnoreAllFieldsNullFalseOrZero]
	public Swimmingparameters swimmingParameters;
}

[Serializable]
public class Ridingoffsets
{
	public Standing standing;
	public Moving moving;
}

[Serializable]
public class Standing
{
	public float x;
	public float y;
	public float z;
}

[Serializable]
public class Moving
{
	public float x;
	public float y;
	public float z;
}

[Serializable]
public class Flyingparameters
{
	public int flyHeightMin;
	public int flyHeightMax;
	public float flySpeedModifier;
	public int flyRefreshRateY;
	public int flyRefreshRateXZ;
	public int flyRefreshRateSpeed;
	public int flightTimeMin;
	public int flightTimeMax;
	public int flapRate;
	public string landingMaterials;
}

[Serializable]
public class Mountedflyingparameters
{
	public string type;
	public int upper_angle_limit;
	public int lower_angle_limit;
	public float max_fly_speed;
	public float deceleration_rate;
	public float hover_deceleration_rate;
	public float acceleration_rate;
	public float strafe_acceleration_rate;
	public int strafe_roll_conversion;
	public float turn_rate;
	public float pitch_rate;
	public bool stays_horizontal_flying;
	public float gravity_drop_per_tick;
	public bool continuous_forward_motion;
	public int continuous_forward_motion_ticks;
	public int flying_stamina_charges;
	public int hover_ticks;
}

[Serializable]
public class Swimmingparameters
{
	public int depthRangeStart;
	public int depthRangeEnd;
	public int swimSpeed;
	public float decayRate;
	public int refreshRate;
	public float chanceToStopOnBlock;
	public string[] blocksToStopOn;
	public int minStopTime;
	public int maxStopTime;
	public int minStopCooldownTime;
	public int maxStopCooldownTime;
	public bool canRotateWhileStopped;
	public bool shouldSink;
}

[Serializable]
public class Aggression
{
	public int timid;
	public int passive;
	public int aggressive;
}

[Serializable]
public class Battlestats
{
	public int hp;
	public int attack;
	public int defense;
	public int specialAttack;
	public int specialDefense;
	public int speed;
}

[Serializable]
public class Spawn
{
	public int baseExp;
	public int baseFriendship;
	public int spawnLevel;
	public int spawnLevelRange;
	public string[] spawnLocations;
}

[Serializable]
public class Gigantamax
{
	public bool canHaveFactor;
	public bool canGigantamax;
	[IgnoreNull][IgnoreEmptyString]
	public string form;
	[IgnoreNull][IgnoreEmptyString]
	public string move;
}

[Serializable]
public class Evyields
{
	[IgnoreZero]
	public int specialAttack;
	[IgnoreZero]
	public int specialDefense;
	[IgnoreZero]
	public int speed;
	[IgnoreZero]
	public int defense;
	[IgnoreZero]
	public int hp;
	[IgnoreZero]
	public int attack;
}

[Serializable]
public class Genderproperty
{
	public string gender;
	public Palette[] palettes;

	public Genderproperty Copy()
	{
		Genderproperty gp = new Genderproperty
		{
			gender = gender,
			palettes = new Palette[palettes.Length]
		};

		for (int i=0; i<palettes.Length; i++)
		{
			gp.palettes[i] = palettes[i].Copy();
		}

		return gp;
	}
}

[Serializable]
public class Palette
{
	public string name;
	[IgnoreNull]
	public string texture;
	[IgnoreNull]
	public string emissive;
	[IgnoreNull]
	public string sprite;
	[IgnoreNull]
	public string particle;
	[IgnoreNull][IgnoreAllFieldsNullFalseOrZero]
	public Modellocator modelLocator;
	[IgnoreNull]
	public string[] sounds;
	[IgnoreNull][IgnoreAllFieldsNullFalseOrZero]
	public Flyingmodellocator flyingModelLocator;
	[IgnoreNull]
	public string normalMap;
	[IgnoreNull]
	public string[] source;
	[IgnoreNull]
	public string translationKey;

	public Palette Copy()
	{
		Palette cp = new Palette
		{
			name = name,
			texture = texture,
			sprite = sprite,
			particle = particle,
			emissive = emissive,
			normalMap = normalMap,
			translationKey = translationKey
		};

		if (modelLocator != null)
			cp.modelLocator = modelLocator.Copy();
		if (flyingModelLocator != null)
			cp.flyingModelLocator = flyingModelLocator.Copy();

		return cp;
	}
}

[Serializable]
public class Modellocator
{
	public string factoryType;
	public string[] pqc;
	[IgnoreZero]
	public float yRotation;
	[IgnoreZero]
	public float movementThreshold;
	[IgnoreZero]
	public float rotateAngleX;
	[IgnoreZero]
	public float animationIncrement;
	[IgnoreZero]
	public float rotateAngleY;
	[IgnoreZero]
	public float animationThreshold;
	[IgnoreZero]
	public float zRotation;
	[IgnoreZero]
	public float transparency2;
	[IgnoreZero]
	public float yOffset;
	[IgnoreZero]
	public int xRotation;

	public Modellocator Copy()
	{
		Modellocator cp = new Modellocator()
		{
			factoryType = factoryType,
			yRotation = yRotation,
			movementThreshold = movementThreshold,
			rotateAngleX = rotateAngleX,
			animationIncrement = animationIncrement,
			rotateAngleY = rotateAngleY,
			animationThreshold = animationThreshold,
			zRotation = zRotation,
			transparency2 = transparency2,
			yOffset = yOffset,
			xRotation = xRotation
		};
		return cp;
	}
}

[Serializable]
public class Flyingmodellocator
{
	public string factoryType;
	public string[] pqc;
	[IgnoreZero]
	public float animationIncrement;
	[IgnoreZero]
	public float movementThreshold;
	[IgnoreZero]
	public float yRotation;
	[IgnoreZero]
	public int rotateAngleX;
	[IgnoreZero]
	public int zRotation;

	public Flyingmodellocator Copy()
	{
		Flyingmodellocator cp = new Flyingmodellocator()
		{
			factoryType = factoryType,
			animationIncrement = animationIncrement,
			movementThreshold = movementThreshold,
			yRotation = yRotation,
			rotateAngleX = rotateAngleX,
			zRotation = zRotation
		};
		return cp;
	}
}

[Serializable]
public class Evolution
{
	public int level;
	public string to;
	[IgnoreNull]
	public Condition[] conditions;
	public string evoType;
	[IgnoreNull][IgnoreEmptyArray]
	public string[] moves;
	[IgnoreNull][IgnoreAllFieldsNullFalseOrZero]
	public Item item;
	[IgnoreNull]
	public string with;
	[IgnoreNull][IgnoreEmptyArray]
	public Condition[] anticonditions;

	public Evolution() { }
}

[Serializable]
public class Item
{
	public string itemID;
}

[Serializable]
public class Condition
{
	[IgnoreNull][IgnoreEmptyString]
	public string time;
	[IgnoreNull]
	public string[] withPokemon;
	[IgnoreNull]
	public string[] withTypes;
	[IgnoreNull]
	public string[] withForms;
	[IgnoreZero]
	public int friendship;
	[IgnoreNull][IgnoreAllFieldsNullFalseOrZero]
	public Item item;
	[IgnoreZero]
	public int critical;
	[IgnoreNull][IgnoreEmptyString]
	public string attackName;
	[IgnoreNull][IgnoreEmptyString]
	public string type;
	[IgnoreNull]
	public string[] biomes;
	[IgnoreNull][IgnoreEmptyString]
	public string evolutionRock;
	[IgnoreZero]
	public int maxRangeSquared;
	[IgnoreNull][IgnoreEmptyString]
	public string move;
	[IgnoreZero]
	public int uses;
	[IgnoreNull][IgnoreEmptyString]
	public string stat1;
	[IgnoreNull][IgnoreEmptyString]
	public string stat2;
	[IgnoreZero]
	public float ratio;
	[IgnoreNull]
	public string[] withPalettes;
	[IgnoreZero]
	public float chance;
	[IgnoreNull]
	public string[] genders;
	[IgnoreNull][IgnoreEmptyString]
	public string recoil;
	[IgnoreNull][IgnoreEmptyString]
	public string health;
	[Ignore]
	public bool shiny; // uuhh....
	[IgnoreZero]
	public float minAltitude;
	[IgnoreNull][IgnoreEmptyString]
	public string weather;
	[IgnoreZero]
	public int nuggets;
	[IgnoreNull]
	public string[] natures;
	[IgnoreNull][IgnoreEmptyString]
	public string evolutionScroll;
	[IgnoreZero]
	public int blocksToWalk;
	public string evoConditionType;
}

