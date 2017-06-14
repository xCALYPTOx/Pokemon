using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Constants
{
	/**
	 * A Type references a Pokemon's type or a Move's type.
	 */
	public enum Type { Normal, Fighting, Flying, Poison, Ground, Rock, Bug, Ghost, Steel, Fire, 
		Water, Grass, Electric, Psychic, Ice, Dragon, Dark, Fairy, NONE };

	/**
	 * Rarity referse to the rarity level of a Pokemon or a Pokemon Egg
	 */
	public enum Rarity { Common, Uncommon, Rare, L3, L2, L1 };

	public class GlobalVariables 
	{
		/**
		 * The number of Pokemon in the game.
		 */
		public const int NUM_POKEMON = 151;

		/**
		 * The number of Stats a Pokemon has.
		 */
		public const int NUM_STATS = 6;

		/**
		 * The highest number that an IV can be.
		 */
		public const int MAX_IV_VALUE = 31;

		/**
		 * The change of encountering a shiny pokemon.
		 */
		public const int SHINY_CHANCE = 8192;
	}
}
