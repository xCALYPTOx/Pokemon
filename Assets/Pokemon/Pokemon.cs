using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;

public class Pokemon 
{
	// Represents the personality stat modifier that a Pokemon has.
	public enum PokemonNature { Hardy, Lonely, Brave, Adamant, Naughty, Bold, Docile, Relaxed, Impish, Lax, Timid,
								Hasty, Serious, Jolly, Naive, Modest, Mild, Quiet, Bashful, Rash, Calm, Gentle, 
								Sassy, Careful, Quirky };
	public enum PokemonGender { Male, Female, NONE };	// Represents the gender of the Pokemon with regards to breeding.

	/**
	 * The following constants refer to the indexes for each respective
	 * stat a Pokemon will have in the BaseStats, IV, and EV arrays.
	 */
	public const int HP 	= 0;
	public const int ATK 	= 1;
	public const int DEF 	= 2;
	public const int SP_ATK = 3;
	public const int SP_DEF = 4;
	public const int SPD 	= 5;

	public int ID 					{ get; private set; }	// The Pokemon's international Pokedex ID #.
	public int Level 				{ get; private set; }	// The Pokemon's level.
	public int Exp					{ get; private set; }	// The Pokemon's total amount of experience gained through battling.
	public string NickName 			{ get; set; }			// The name bestowed upon the Pokemon by its owner.
	public PokemonGender Gender 	{ get; private set; }	// The Pokemon's gender (this concept is confusing for some people).
	public PokemonNature Nature 	{ get; private set; }	// The Pokemon's personality type.
	public bool Shiny				{ get; private set; }	// The Pokemon's shiny status.
	public int[] IV					{ get; private set; }	// The Pokemon's individual values for each of the 6 stats.
	public int[] EV					{ get; private set; }	// The Pokemon's effor values for each of the 6 stats.
	// public Move[] Moves  		{ get; set; }			// The list of combat moves that the Pokemon knows.

	public Pokemon(int id, int lvl = 1)
	{
		this.ID = id;
		this.Level = 1;
		IV = new int[Constants.GlobalVariables.NUM_STATS];
		EV = new int[Constants.GlobalVariables.NUM_STATS];
	}

	/// <summary>
	/// Gets the Pokemon's ID as a string with preceeding zeros where necessary.
	/// </summary>
	/// <returns>The Pokemon ID as a string.</returns>
	public string getStringID()
	{
		if (this.ID < 10)
			return "00" + this.ID;
		else if (this.ID < 100)
			return "0" + this.ID;
		else
			return "" + ID;
	}

	public static Pokemon generatePokemon(int id, int lvl)
	{
		// Seed RNG
		Random.InitState(System.DateTime.Now.Millisecond);

		// Create Pokemon
		Pokemon p = new Pokemon (id, lvl);

		// Assign Exp

		// Assign Gender
		// Need to check for genderlessness!!
		if (Random.Range (0, 2) == 0)
			p.Gender = PokemonGender.Male;
		else
			p.Gender = PokemonGender.Female;

		// Assign Nature
		p.Nature = (PokemonNature) Random.Range (0, System.Enum.GetNames (typeof(PokemonNature)).Length);

		// Assign Individual Values
		for (int i = 0; i < Constants.GlobalVariables.NUM_STATS; i++)
			p.IV [i] = Random.Range (0, Constants.GlobalVariables.MAX_IV_VALUE + 1);

		// Determine shininess
		if (Random.Range (0, Constants.GlobalVariables.SHINY_CHANCE) == 0)
			p.Shiny = true;
		
		return p;
	}

}
