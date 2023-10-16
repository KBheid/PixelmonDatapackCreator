using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class Validator : MonoBehaviour
{
	[SerializeField] ErrorPanel errorPanel;

	static readonly string FORM_ERROR_FORMAT = "Pokemon: {0}\nForm: {1}\n";
	static readonly string EVOLUTION_ERROR_FORMAT = "Pokemon: {0}\nForm: {1}\nEvolution: {2}\n";
	static readonly string CONDITION_ERROR_FORMAT = "Pokemon: {0}\nForm: {1}\nEvolution: {2}\nCondition: {3}\n";

    public void Validate()
    {
		List<List<ValidationError>> totalErrors = new List<List<ValidationError>>();
        foreach (Pokemon p in PokemonManager.instance.pokemon)
        {
			List<ValidationError> errors = new List<ValidationError>();
			errors.AddRange(ValidateStats(p));
            errors.AddRange(ValidateEvolutions(p));
            errors.AddRange(ValidateMoves(p));
			
			if (errors.Count > 0)
				totalErrors.Add(errors);
        }

		errorPanel.SetAllErrors(totalErrors);
    }

	#region Stat Validation
	public List<ValidationError> ValidateStats(Pokemon p)
	{

		List<ValidationError> errors = new List<ValidationError>();
		if (p.name == null || p.name == "")
			errors.Add(new ValidationError() { 
				errorType = "Pokemon name error",
				pokemon = p,
				errorMessage = "Error: This Pokemon does not have a name set."
			});

		string dexError = "Pokemon: {0}\nError: invalid dex number \"{1}\"";
		if (p.dex < 0)
			errors.Add(new ValidationError()
			{
				errorType = "Pokemon dex error",
				pokemon = p,
				errorMessage = string.Format(dexError, p.name, p.dex)
			});


		string genError = "Pokemon: {0}\nError: invalid gen number \"{1}\"";
		if (p.dex < 0)
			errors.Add(new ValidationError()
			{
				errorType = "Pokemon dex error",
				pokemon = p,
				errorMessage = string.Format(genError, p.name, p.generation)
			});

		

		
		foreach (Form f in p.forms)
		{
			// EXP Group
			List<string> validExpGroups = new List<string>() { "SLOW", "MEDIUM_SLOW", "MEDIUM_FAST", "FAST", "ERRATIC", "FLUCTUATING" };
			string expGroupError	= FORM_ERROR_FORMAT + "Error: Invalid exp group \"{2}\"";
			if (f.experienceGroup != null)
			if (!validExpGroups.Contains(f.experienceGroup))
				errors.Add(new ValidationError()
				{
					errorType = "Invalid experience group",
					pokemon = p,
					form = f,
					errorMessage = string.Format(expGroupError, p.name, f.name, f.experienceGroup)
				});

			// Types
			List<string> validTypes = new List<string>() { "NORMAL", "FIRE", "WATER", "GRASS", "FLYING", "FIGHTING",
				"POISON", "ELECTRIC", "GROUND", "ROCK", "PSYCHIC", "ICE", "BUG", "GHOST", "STEEL", "DRAGON", "DARK", "FAIRY" };
			string typeError		= FORM_ERROR_FORMAT + "Error: Invalid pokemon type \"{2}\"";
			if (f.types != null && f.types.Length > 0)
			foreach (string type in f.types)
            {

				if (!validTypes.Contains(type))
					errors.Add(new ValidationError()
					{
						errorType = "Invalid Pokemon type",
						pokemon = p,
						form = f,
						errorMessage = string.Format(typeError, p.name, f.name, type)
					});
			}

			// Egg cycle
			string eggCycleError = FORM_ERROR_FORMAT + "Error: Invalid egg cycles \"{2}\" - below 0";
			if (f.eggCycles < 0) 
				errors.Add(new ValidationError()
				{
					errorType = "Invalid egg cycles",
					pokemon = p,
					form = f,
					errorMessage = string.Format(eggCycleError, p.name, f.name, f.eggCycles)
				});

			// Weight
			string weightError = FORM_ERROR_FORMAT + "Error: Invalid pokemon weight \"{2}\" - below 0";
			if (f.weight < 0)
				errors.Add(new ValidationError()
				{
					errorType = "Invalid Pokemon weight",
					pokemon = p,
					form = f,
					errorMessage = string.Format(weightError, p.name, f.name, f.weight)
				});

			// Catch rate
			string catchRateError = FORM_ERROR_FORMAT + "Error: Invalid catch rate \"{2}\" - below 0";
			if (f.catchRate < 0)
				errors.Add(new ValidationError()
				{
					errorType = "Invalid catch rate",
					pokemon = p,
					form = f,
					errorMessage = string.Format(catchRateError, p.name, f.name, f.catchRate)
				});

			// Male percentage (value can be -1)

			// Battle stats
			string battleStatsError = FORM_ERROR_FORMAT + "Error: Invalid battle stats - below 0";
			int minBattleStat = Mathf.Min(f.battleStats.hp, f.battleStats.attack, f.battleStats.defense, f.battleStats.specialAttack, f.battleStats.specialDefense, f.battleStats.speed);
			if (minBattleStat < 0)
				errors.Add(new ValidationError()
				{
					errorType = "Invalid battle stats",
					pokemon = p,
					form = f,
					errorMessage = string.Format(battleStatsError, p.name, f.name)
				});

			// EV Yield
			string evYieldError = FORM_ERROR_FORMAT + "Error: Invalid EV yields - below 0";
			int minEVYield = Mathf.Min(f.evYields.hp, f.evYields.attack, f.evYields.defense, f.evYields.specialAttack, f.evYields.specialDefense, f.evYields.speed);
			if (minEVYield < 0)
				errors.Add(new ValidationError()
				{
					errorType = "Invalid EV yields",
					pokemon = p,
					form = f,
					errorMessage = string.Format(evYieldError, p.name, f.name)
				});

			// Aggression - values can be below 0
			/*
			string aggressionError = FORM_ERROR_FORMAT + "Error: Invalid aggression - below 0";
			int minAggression = Mathf.Min(f.aggression.aggressive, f.aggression.passive, f.aggression.timid);
			if (minAggression < 0)
				errors.Add(new ValidationError()
				{
					errorType = "Invalid aggression value",
					pokemon = p,
					form = f,
					errorMessage = string.Format(aggressionError, p.name, f.name)
				});
			*/

			// Spawning
			List<string> validSpawnLocations = new List<string>() { "LAND", "WATER", "UNDERGROUND", "AIR", "AIR_PERSISTENT" };
			string spawnLocationError = FORM_ERROR_FORMAT + "Error: Invalid spawn location \"{2}\"";
			if (f.spawn != null && f.spawn.spawnLocations != null)
				foreach (string location in f.spawn.spawnLocations)
					if (!validSpawnLocations.Contains(location))
						errors.Add(new ValidationError()
						{
							errorType = "Invalid spawn location",
							pokemon = p,
							form = f,
							errorMessage = string.Format(spawnLocationError, p.name, f.name, location)
						});

			if (f.spawn != null)
			{
				string spawnValueError = FORM_ERROR_FORMAT + "Error: Invalid spawn value - below 0";
				int minSpawnValue = Mathf.Min(f.spawn.baseExp, f.spawn.baseFriendship, f.spawn.spawnLevel);
				if (minSpawnValue < 0)
					errors.Add(new ValidationError()
					{
						errorType = "Invalid spawn value",
						pokemon = p,
						form = f,
						errorMessage = string.Format(spawnValueError, p.name, f.name)
					});
			}

			// Dimensions (?)

			// Gigantamax
			if (f.gigantamax != null) {
				string gigantamaxMoveError = FORM_ERROR_FORMAT + "Error: Invalid gigantamax move \"{2}\"";
				if (f.gigantamax.move == "" && MovesManager.instance.FindMove(f.gigantamax.move) == null)
					errors.Add(new ValidationError()
					{
						errorType = "Invalid gigantamax move",
						pokemon = p,
						form = f,
						errorMessage = string.Format(gigantamaxMoveError, p.name, f.name, f.gigantamax.move)
					});
				
				
				string gigantamaxFormError = FORM_ERROR_FORMAT + "Error: Invalid gigantamax form \"{2}\"";
				List<string> validForms = new List<string>(Array.ConvertAll(p.forms, form => form.name));
				if (f.gigantamax.form == "" && !validForms.Contains(f.gigantamax.form))
					errors.Add(new ValidationError()
					{
						errorType = "Invalid gigantamax form",
						pokemon = p,
						form = f,
						errorMessage = string.Format(gigantamaxFormError, p.name, f.name, f.gigantamax.form)
					});
			}
		}

		return errors;

	}
    #endregion

    #region Evolution Validation
    public List<ValidationError> ValidateEvolutions(Pokemon p)
	{
		List<ValidationError> errors = new List<ValidationError>();

		foreach (Form f in p.forms)
		{

			ValidatePreevolutions(p, f, errors);

			if (f.evolutions == null) continue;

			foreach (Evolution e in f.evolutions)
			{
				ValidateEvolutionTo(p, f, e, errors);
				ValidateEvolutionType(p, f, e, errors);
				ValidateEvolutionConditions(p, f, e, errors);
				ValidateEvolutionMoves(p, f, e, errors);
			}

		}

		return errors;
	}
	private void ValidatePreevolutions(Pokemon p, Form f, List<ValidationError> errors)
	{
		string preevolutionError = FORM_ERROR_FORMAT + "Error: Cannot verify pre-evolution {2}.";

		if (f.preEvolutions == null)
			return;

		foreach (string preevo in f.preEvolutions)
		{
			if (PokemonManager.instance.FindPokemon(preevo) == null)
				errors.Add(new ValidationError()
				{
					errorType = "Pre-Evolution",
					pokemon = p,
					form = f,
					errorMessage = string.Format(preevolutionError, p.name, f.name, preevo)
				});
		}
	}
	private void ValidateEvolutionTo(Pokemon p, Form f, Evolution e, List<ValidationError> errors)
	{
		string evolutionToNullError			= FORM_ERROR_FORMAT + "Error: Evolution `to` field empty.";
		string evolutionToNotFoundError		= EVOLUTION_ERROR_FORMAT + "Error: Evolution `to` field invalid species value \"{3}\"";
		string evolutionInvalidFormError	= EVOLUTION_ERROR_FORMAT + "Error: Evolution `to` field invalid form value \"{3}\"";
		string evolutionInvalidPaletteError = EVOLUTION_ERROR_FORMAT + "Error: Evolution `to` field invalid palette value \"{3}\"";

		if (e.to == null)
		{
			errors.Add(new ValidationError()
			{
				errorType = "Unset evolution",
				pokemon = p,
				form = f,
				errorMessage = string.Format(evolutionToNullError, p.name, f.name)
			});
			return;
		}

		string[] formSplit = Regex.Split(e.to, "form:");
		if (formSplit.Length == 1) formSplit = Regex.Split(e.to, "f:");
		string[] paletteSplit = Regex.Split(e.to, "palette:");
		string species = (formSplit[0].Length <= paletteSplit[0].Length) ? formSplit[0] : paletteSplit[0];
		species = species.Trim();
		string formName = (formSplit.Length > 1) ? formSplit[1] : null;
		if (formName == "base") formName = "";
		string paletteName = (paletteSplit.Length > 1) ? paletteSplit[1] : null;

		// Validate evolution to selection
		Pokemon to = PokemonManager.instance.FindPokemon(species);
		if (to == null)
		{
			errors.Add(new ValidationError()
			{
				errorType = "Unresolvable \"to\" Pokemon",
				pokemon = p,
				form = f,
				evolution = e,
				errorMessage = string.Format(evolutionToNotFoundError, p.name, f.name, e.to, species)
			});
			return;
		}


		// Validate form selection
		if (formName != null)
			if (to.forms.Any(form => form.name == formName) == false)
			{
				errors.Add(new ValidationError()
				{
					errorType = "Unresolvable \"to\" form",
					pokemon = p,
					form = f,
					evolution = e,
					errorMessage = string.Format(evolutionInvalidFormError, p.name, f.name, e.to, formName)
				});
				return;
			}

		// Validate palette selection
		if (paletteName != null)
			if (false == to.forms.Any(
				form => form.genderProperties.Any(prop => prop.palettes.Any(
					palette => palette.name == paletteName
				))
			))
			{
				errors.Add(new ValidationError()
				{
					errorType = "Unresolvable \"to\" palette",
					pokemon = p,
					form = f,
					evolution = e,
					errorMessage = string.Format(evolutionInvalidPaletteError, p.name, f.name, e.to, paletteName)
				});
			}

	}
	private void ValidateEvolutionType(Pokemon p, Form f, Evolution e, List<ValidationError> errors)
	{
		string evolutionTypeError	= EVOLUTION_ERROR_FORMAT + "Error: Evolution type invalid value \"{3}\"";
		string evolutionLevelError	= EVOLUTION_ERROR_FORMAT + "Error: Evolution level invalid value \"{3}\"";

		List<string> validEvoTypes = new List<string>() { "leveling", "interact", "trade", "ticking" };
		if (!validEvoTypes.Contains(e.evoType))
			errors.Add(new ValidationError()
			{
				errorType = "Invalid evolution type",
				pokemon = p,
				form = f,
				evolution = e,
				errorMessage = string.Format(evolutionTypeError, p.name, f.name, e.to, e.evoType)
			});

		if (e.evoType == "leveling" && e.level < 0)
			errors.Add(new ValidationError()
			{
				errorType = "Invalid evolution level",
				pokemon = p,
				form = f,
				evolution = e,
				errorMessage = string.Format(evolutionLevelError, p.name, f.name, e.to, e.level)
			});
	}
	private void ValidateEvolutionConditions(Pokemon p, Form f, Evolution e, List<ValidationError> errors)
	{
		if (e.conditions != null)
			foreach (Condition c in e.conditions)
			{
				ValidateCondition(p, f, e, c, errors);
			}

		if (e.anticonditions != null)
			foreach (Condition c in e.anticonditions)
			{
				ValidateCondition(p, f, e, c, errors);
			}
	}
	private void ValidateCondition(Pokemon p, Form f, Evolution e, Condition c, List<ValidationError> errors)
	{
		string evolutionConditionTypeError	= CONDITION_ERROR_FORMAT + "Error: Evolution condition type invalid.";
		string evolutionConditionBiomeError = CONDITION_ERROR_FORMAT + "Error: Biomes empty.";
		string evolutionConditionOpenError	= CONDITION_ERROR_FORMAT + "Error: Invalid `{4}` entry \"{5}\".";

		List<string> validEvoConditionTypes = new List<string>() { "time", "biome", "weather", "chance", "heldItem", "evolutionRock",
			"evolutionScroll", "shiny", "friendship", "move", "moveType", "moveUses", "party", "highAltitude", "blocksWalkedOutsideBall",
			"nature", "gender", "statRatio", "status", "critical", "recoil", "nuggets", "healthAbsence", "insideBattle", "invert"
		};

		if (!validEvoConditionTypes.Contains(c.evoConditionType))
		{
			errors.Add(new ValidationError()
			{
				errorType = "Invalid evolution condition type",
				pokemon = p,
				form = f,
				evolution = e,
				condition = c,
				errorMessage = string.Format(evolutionConditionTypeError, p, f, e, c)
			});
			return;
		}

		string condType = c.evoConditionType;

		if (condType == "time")
		{
			List<string> validTimes = new List<string>() { "DAY", "NIGHT", "DAWN", "DUSK", "MORNING", "AFTERNOON", "MIDNIGHT" };
			if (!validTimes.Contains(c.time))
				errors.Add(new ValidationError()
				{
					errorType = "Invalid time condition",
					pokemon = p,
					form = f,
					evolution = e,
					condition = c,
					errorMessage = string.Format(evolutionConditionOpenError, p.name, f.name, e.to, c.evoConditionType, "time", c.time)
				});
		}
		if (condType == "biome")
		{
			if (c.biomes == null)
				errors.Add(new ValidationError
				{
					errorType = "Invalid biome condition",
					pokemon = p,
					form = f,
					evolution = e,
					condition = c,
					errorMessage = string.Format(evolutionConditionBiomeError, p.name, f.name, e.to, c.evoConditionType)
				});
		}
		if (condType == "weather")
		{
			List<string> validTimes = new List<string>() { "RAIN", "STORM" };
			if (!validTimes.Contains(c.weather))
				errors.Add(new ValidationError()
				{
					errorType = "Invalid weather condition",
					pokemon = p,
					form = f,
					evolution = e,
					condition = c,
					errorMessage = string.Format(evolutionConditionOpenError, p.name, f.name, e.to, c.evoConditionType, "weather", c.weather)
				});
		}
		if (condType == "chance")
		{
			if (c.chance <= 0f || 
				c.chance > 1.0f) errors.Add(new ValidationError()
			{
				errorType = "Invalid chance condition",
				pokemon = p,
				form = f,
				evolution = e,
				condition = c,
				errorMessage = string.Format(evolutionConditionOpenError, p.name, f.name, e.to, c.evoConditionType, "chance", c.chance.ToString())
			});
		}
		if (condType == "heldItem") { /* TODO, idk how to validate items rn */ }
		if (condType == "evolutionRock")
		{
			List<string> validRocks = new List<string>() { "MOSSY_ROCK", "ICE_ROCK" };
			if (!validRocks.Contains(c.evolutionRock))
				errors.Add(new ValidationError()
				{
					errorType = "Invalid evolutionRock condition",
					pokemon = p,
					form = f,
					evolution = e,
					condition = c,
					errorMessage = string.Format(evolutionConditionOpenError, p.name, f.name, e.to, c.evoConditionType, "evolutionRock", c.evolutionRock)
				});
		}
		if (condType == "evolutionScroll")
		{
			List<string> validScrolls = new List<string>() { "DARKNESS", "WATERS" };
			if (!validScrolls.Contains(c.evolutionScroll))
				errors.Add(new ValidationError()
				{
					errorType = "Invalid evolutionScroll condition",
					pokemon = p,
					form = f,
					evolution = e,
					condition = c,
					errorMessage = string.Format(evolutionConditionOpenError, p.name, f.name, e.to, c.evoConditionType, "evolutionScroll", c.evolutionScroll)
				});
		}
		if (condType == "shiny") { /* No way to validate? */ }
		if (condType == "friendship")
		{
			if (c.friendship < 0 || c.friendship > 255)
				errors.Add(new ValidationError()
				{
					errorType = "Invalid friendship condition",
					pokemon = p,
					form = f,
					evolution = e,
					condition = c,
					errorMessage = string.Format(evolutionConditionOpenError, p.name, f.name, e.to, c.evoConditionType, "evolutionScroll", c.friendship)
				});
		}
		if (condType == "move")
		{
			if (MovesManager.instance.FindMove(c.attackName) == null)
				errors.Add(new ValidationError()
				{
					errorType = "Invalid move condition",
					pokemon = p,
					form = f,
					evolution = e,
					condition = c,
					errorMessage = string.Format(evolutionConditionOpenError, p.name, f.name, e.to, c.evoConditionType, "move", c.move)
				});
		}
		if (condType == "moveType")
		{
			List<string> validTypes = new List<string>() { "NORMAL", "FIRE", "WATER", "GRASS", "FLYING", "FIGHTING",
				"POISON", "ELECTRIC", "GROUND", "ROCK", "PSYCHIC", "ICE", "BUG", "GHOST", "STEEL", "DRAGON", "DARK", "FAIRY" };

			if (!validTypes.Contains(c.type))
				errors.Add(new ValidationError()
				{
					errorType = "Invalid moveType condition",
					pokemon = p,
					form = f,
					evolution = e,
					condition = c,
					errorMessage = string.Format(evolutionConditionOpenError, p.name, f.name, e.to, c.evoConditionType, "type", c.type)
				});
		}
		if (condType == "moveUses")
		{
			if (MovesManager.instance.FindMove(c.move) == null)
				errors.Add(new ValidationError()
				{
					errorType = "Invalid moveUses condition",
					pokemon = p,
					form = f,
					evolution = e,
					condition = c,
					errorMessage = string.Format(evolutionConditionOpenError, p.name, f.name, e.to, c.evoConditionType, "move", c.move)
				});
			if (c.uses < 0)
				errors.Add(new ValidationError()
				{
					errorType = "Invalid moveUses condition",
					pokemon = p,
					form = f,
					evolution = e,
					condition = c,
					errorMessage = string.Format(evolutionConditionOpenError, p.name, f.name, e.to, c.evoConditionType, "uses", c.uses)
				});
		}
		if (condType == "party") { /* TODO, big job*/ }
		if (condType == "highAltitude") { /* no way to validate? */ }
		if (condType == "blocksWalkedOutsideBall")
		{
			if (c.blocksToWalk < 0)
				errors.Add(new ValidationError()
				{
					errorType = "Invalid blocksWalkedOutsideBall condition",
					pokemon = p,
					form = f,
					evolution = e,
					condition = c,
					errorMessage = string.Format(evolutionConditionOpenError, p.name, f.name, e.to, c.evoConditionType, "blocksToWalk", c.blocksToWalk)
				});
		}
		if (condType == "nature")
		{
			List<string> validNatures = new List<string>() { "ADAMANT", "BASHFUL", "BOLD", "BRAVE", "CALM", "CAREFUL", "DOCILE", "GENTLE", "HARDY",
				"HASTY", "IMPISH", "JOLLY", "LAX", "LONELY", "MILD", "MODEST", "NAIVE", "NAUGHTY", "QUIET", "QUIRKY",
				"RASH", "RELAXED", "SASSY", "SERIOUS", "TIMID"};
			foreach (string nature in c.natures)
				if (!validNatures.Contains(nature))
					errors.Add(new ValidationError()
					{
						errorType = "Invalid nature condition",
						pokemon = p,
						form = f,
						evolution = e,
						condition = c,
						errorMessage = string.Format(evolutionConditionOpenError, p.name, f.name, e.to, c.evoConditionType, "natures", nature)
					});
		}
		if (condType == "gender")
		{
			List<string> validGenders = new List<string>() { "MALE", "FEMALE" };
			foreach (string gender in c.genders)
				if (!validGenders.Contains(gender))
					errors.Add(new ValidationError()
					{
						errorType = "Invalid gender condition",
						pokemon = p,
						form = f,
						evolution = e,
						condition = c,
						errorMessage = string.Format(evolutionConditionOpenError, p.name, f.name, e.to, c.evoConditionType, "genders", gender)
					});
		}
		if (condType == "statRatio")
		{
			// I don't know if SP. ATTACK and SP. DEFENSE are proper...
			List<string> validStats = new List<string>() { "HP", "ATTACK", "DEFENSE", "SPEED", "SP. ATTACK", "SP. DEFENSE" };
			if (!validStats.Contains(c.stat1))
				errors.Add(new ValidationError()
				{
					errorType = "Invalid statRatio condition",
					pokemon = p,
					form = f,
					evolution = e,
					condition = c,
					errorMessage = string.Format(evolutionConditionOpenError, p.name, f.name, e.to, c.evoConditionType, "stat1", c.stat1)
				});
			if (!validStats.Contains(c.stat2))
				errors.Add(new ValidationError()
				{
					errorType = "Invalid statRatio condition",
					pokemon = p,
					form = f,
					evolution = e,
					condition = c,
					errorMessage = string.Format(evolutionConditionOpenError, p.name, f.name, e.to, c.evoConditionType, "stat2", c.stat2)
				});
			if (c.ratio < 0)
				errors.Add(new ValidationError()
				{
					errorType = "Invalid statRatio condition",
					pokemon = p,
					form = f,
					evolution = e,
					condition = c,
					errorMessage = string.Format(evolutionConditionOpenError, p.name, f.name, e.to, c.evoConditionType, "ratio", c.ratio)
				});
		}
		if (condType == "status")
		{
			List<string> validStats = new List<string>() { "Burn", "Freeze", "Paralysis", "Poison", "Sleep" };
			if (!validStats.Contains(c.type))
				errors.Add(new ValidationError()
				{
					errorType = "Invalid status condition",
					pokemon = p,
					form = f,
					evolution = e,
					condition = c,
					errorMessage = string.Format(evolutionConditionOpenError, p.name, f.name, e.to, c.evoConditionType, "type", c.type)
				});
		}
		if (condType == "critical")
		{
			if (c.critical <= 0)
				errors.Add(new ValidationError()
				{
					errorType = "Invalid critical condition",
					pokemon = p,
					form = f,
					evolution = e,
					condition = c,
					errorMessage = string.Format(evolutionConditionOpenError, p.name, f.name, e.to, c.evoConditionType, "critical", c.critical)
				});
		}
		if (condType == "recoil")
		{
			if (!int.TryParse(c.recoil, out int recoil))
				errors.Add(new ValidationError()
				{
					errorType = "Invalid recoil condition",
					pokemon = p,
					form = f,
					evolution = e,
					condition = c,
					errorMessage = string.Format(evolutionConditionOpenError, p.name, f.name, e.to, c.evoConditionType, "recoil", c.recoil)
				});
			if (recoil <= 0)
				errors.Add(new ValidationError()
				{
					errorType = "Invalid recoil condition",
					pokemon = p,
					form = f,
					evolution = e,
					condition = c,
					errorMessage = string.Format(evolutionConditionOpenError, p.name, f.name, e.to, c.evoConditionType, "recoil", c.recoil)
				});
		}
		if (condType == "nuggets")
		{
			if (c.nuggets <= 0)
				errors.Add(new ValidationError()
				{
					errorType = "Invalid nuggets condition",
					pokemon = p,
					form = f,
					evolution = e,
					condition = c,
					errorMessage = string.Format(evolutionConditionOpenError, p.name, f.name, e.to, c.evoConditionType, "nuggets", c.nuggets)
				});
		}
		if (condType == "healthAbsence")
		{
			if (!int.TryParse(c.health, out int health))
				errors.Add(new ValidationError()
				{
					errorType = "Invalid healthAbsence condition",
					pokemon = p,
					form = f,
					evolution = e,
					condition = c,
					errorMessage = string.Format(evolutionConditionOpenError, p.name, f.name, e.to, c.evoConditionType, "health", c.health)
				}); ;
			if (health <= 0)
				errors.Add(new ValidationError()
				{
					errorType = "Invalid healthAbsence condition",
					pokemon = p,
					form = f,
					evolution = e,
					condition = c,
					errorMessage = string.Format(evolutionConditionOpenError, p.name, f.name, e.to, c.evoConditionType, "health", c.health)
				});
		}
		if (condType == "insideBattle") { /* Cannot validate? */ }
		if (condType == "invert") { /* Cannot validate? */ }
	}
	private void ValidateEvolutionMoves(Pokemon p, Form f, Evolution e, List<ValidationError> errors)
	{
		if (e.moves == null) return;

		string evolutionMoveError = EVOLUTION_ERROR_FORMAT + "Error: Evolution move error \"{3}\".";

		foreach (string moveName in e.moves)
		{
			if (moveName == "")
				continue;
			if (MovesManager.instance.FindMove(moveName) == null)
				errors.Add(new ValidationError()
				{
					errorType = "Invalid evolution move",
					pokemon = p,
					form = f,
					evolution = e,
					errorMessage = string.Format(evolutionMoveError, p.name, f.name, e.to, moveName)
				});
		}
	}
    #endregion

    #region Move Validation
	List<ValidationError> ValidateMoves(Pokemon p)
    {
		string levelUpMoveUnresolvableError = FORM_ERROR_FORMAT + "Error: Move could not be resolved \"{2}\"";
		string levelUpMoveInvalidLevelError = FORM_ERROR_FORMAT + "Error: Move cannot be learned at Pokemon level {2}";


		List<ValidationError> errors = new List<ValidationError>();

		foreach (Form f in p.forms)
        {
			if (f.moves == null)
				continue;

			// Levelup Moves
			if (f.moves.levelUpMoves != null) {
				foreach (Levelupmove lum in f.moves.levelUpMoves)
                {
					if (lum.attacks == null)
						continue;

					foreach (string att in lum.attacks)
                    {
						if (MovesManager.instance.FindMove(att) == null)
							errors.Add(new ValidationError()
							{
								errorType = "Unresolvable level-up move",
								pokemon = p,
								form = f,
								errorMessage = string.Format(levelUpMoveUnresolvableError, p.name, f.name, att)
							});
					}


					if (lum.level < 0)
						errors.Add(new ValidationError()
						{
							errorType = "Invalid level for level up move",
							pokemon = p,
							form = f,
							errorMessage = string.Format(levelUpMoveInvalidLevelError, p.name, f.name, lum.level)
						});
				}
			}

        }


		return errors;
    }
    #endregion
}

[Serializable]
public struct ValidationError
{
    public Pokemon pokemon;
    public Form form;
    public Evolution evolution;
    public Condition condition;
	public string errorType;
    public string errorMessage;
} 