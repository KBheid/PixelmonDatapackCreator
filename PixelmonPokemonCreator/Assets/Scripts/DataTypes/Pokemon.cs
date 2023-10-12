using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Pokemon
{
	public string name;						// Done
	public int dex;							// Done
	public string[] defaultForms;
	public Form[] forms;					// Done
	public int generation;					// Done
}

[Serializable]
public class Form
{
	public string name;						// Done
	public string experienceGroup;			// Done
	public Dimensions dimensions;			// Done
	public Moves moves;
	public Abilities abilities;
	public Movement movement;
	public Aggression aggression;			// Done
	public Battlestats battleStats;			// Done
	public string[] tags;					// Done
	public Spawn spawn;						// Done
	public string[] possibleGenders;
	public Genderproperty[] genderProperties;
	public string[] eggGroups;				// Done
	public string[] types;					// Done
	public string[] preEvolutions;
	public string defaultBaseForm;
	public string[] megaItems;
	public string[] megas;
	public Gigantamax gigantamax;			// Done
	public int eggCycles;					// Done
	public float weight;					// Done
	public int catchRate;					// Done
	public float malePercentage;			// Done
	public Evolution[] evolutions;
	public Evyields evYields;				// Done
}

[Serializable]
public class Dimensions
{
	public float height;
	public float width;
	public float length;
	public float eyeHeight;
	public float hoverHeight;
}

[Serializable]
public class Moves
{
	public Levelupmove[] levelUpMoves;
	public string[] tutorMoves;
	public string[] eggMoves;
	public string[] tmMoves8;
	public string[] trMoves;
	public string[] hmMoves;
	public string[] transferMoves;
	public string[] tmMoves7;
	public string[] tmMoves6;
	public string[] tmMoves5;
	public string[] tmMoves4;
	public string[] tmMoves3;
	public string[] tmMoves2;
	public string[] tmMoves1;
	public string[] tmMoves;
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
	public string[] hiddenAbilities;
}

[Serializable]
public class Movement
{
	public bool rideable;
	public bool canFly;
	public bool canSurf;
	public bool canRideShoulder;
	public Ridingoffsets ridingOffsets;
	public Flyingparameters flyingParameters;
	public Mountedflyingparameters mountedFlyingParameters;
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
	public int aggressive;
	public int passive;
	public int timid;
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
	public string form;
	public string move;
}

[Serializable]
public class Evyields
{
	public int specialAttack;
	public int specialDefense;
	public int speed;
	public int defense;
	public int hp;
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
	public string texture;
	public string sprite;
	public string particle;
	public Modellocator modelLocator;
	public string[] sounds;
	public string emissive;
	public Flyingmodellocator flyingModelLocator;
	public string normalMap;
	public string[] source;
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
	public float yRotation;
	public float movementThreshold;
	public float rotateAngleX;
	public float animationIncrement;
	public float rotateAngleY;
	public float animationThreshold;
	public float zRotation;
	public float transparency2;
	public float yOffset;
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
	public float animationIncrement;
	public float movementThreshold;
	public float yRotation;
	public int rotateAngleX;
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
	public Condition[] conditions;
	public string evoType;
	public string[] moves;
	public Item item;
	public string with;
	public Condition[] anticonditions;
}

[Serializable]
public class Item
{
	public string itemID;
}

[Serializable]
public class Condition
{
	public string time;
	public string evoConditionType;
	public string[] withPokemon;
	public string[] withTypes;
	public string[] withForms;
	public int friendship;
	public Item item;
	public int critical;
	public string attackName;
	public string type;
	public string[] biomes;
	public string evolutionRock;
	public int maxRangeSquared;
	public string move;
	public int uses;
	public string stat1;
	public string stat2;
	public float ratio;
	public string[] withPalettes;
	public float chance;
	public string[] genders;
	public string recoil;
	public string health;
	public bool shiny;
	public float minAltitude;
	public string weather;
	public int nuggets;
	public string[] natures;
	public string evolutionScroll;
	public int blocksToWalk;
}

