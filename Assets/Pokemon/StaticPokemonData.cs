using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using Constants;

public class StaticPokemonData {

	private static bool dataLoaded = false;
	private static StaticPokemonData[] pokemonData = new StaticPokemonData[GlobalVariables.NUM_POKEMON];

	public int ID 					{ get; private set; }	// The Pokemon's international Pokedex ID #.
	public string Name 				{ get; private set; }	// The Pokemon's internatioinal Pokedex name.
	public Type Type1				{ get; private set; }	// The Pokemon's first type.
	public Type Type2 				{ get; private set; }	// The Pokemon's secondary type, if they have one.
	// public Move[] Moves  		{ get; set; }			// The list of combat moves that the Pokemon knows.
	public Rarity RarityLevel 		{ get; private set; }	// The Pokemon's rarity level.
	public int[] BaseStats			{ get; private set; }

	public StaticPokemonData(int id)
	{
		this.ID = id;
	}

	public static StaticPokemonData getPokemonData(int id)
	{
		return pokemonData [id];
	}

	public static void load()
	{
		if (dataLoaded)
			return;

		XmlDocument doc = new XmlDocument ();
		doc.Load ("Assets/Pokemon/StaticPokemonData.xml");
		XmlNodeList nodeList = doc.DocumentElement.SelectNodes ("/PokemonData/Pokemon");
		foreach (XmlNode node in nodeList) 
		{
			int id = int.Parse (node.SelectSingleNode ("ID").InnerText);
			pokemonData [id] = new StaticPokemonData (id);
			pokemonData [id].BaseStats = new int[Constants.GlobalVariables.NUM_STATS];
			pokemonData [id].Name = node.SelectSingleNode ("Name").InnerText;
			pokemonData [id].Type1 = (Type) System.Enum.Parse (typeof(Type), node.SelectSingleNode ("Type1").InnerText);
			pokemonData [id].Type2 = (Type) System.Enum.Parse (typeof(Type), node.SelectSingleNode ("Type2").InnerText);
			pokemonData [id].RarityLevel = (Rarity) System.Enum.Parse (typeof(Rarity), node.SelectSingleNode ("Rarity").InnerText);
			pokemonData[id].BaseStats[Pokemon.HP] = int.Parse(node.SelectSingleNode ("BaseStats/HP").InnerText);
			pokemonData[id].BaseStats[Pokemon.ATK] = int.Parse(node.SelectSingleNode ("BaseStats/ATK").InnerText);
			pokemonData[id].BaseStats[Pokemon.DEF] = int.Parse(node.SelectSingleNode ("BaseStats/DEF").InnerText);
			pokemonData[id].BaseStats[Pokemon.SP_ATK] = int.Parse(node.SelectSingleNode ("BaseStats/SP_ATK").InnerText);
			pokemonData[id].BaseStats[Pokemon.SP_DEF] = int.Parse(node.SelectSingleNode ("BaseStats/SP_DEF").InnerText);
			pokemonData[id].BaseStats[Pokemon.SPD] = int.Parse(node.SelectSingleNode ("BaseStats/SPD").InnerText);
		}

		dataLoaded = true;
	}
}
