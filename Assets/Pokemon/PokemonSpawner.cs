using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;

public class PokemonSpawner : MonoBehaviour {

	public int spawnRadius;
	public int spawnLimit;
	public int minLevel;
	public int maxLevel;
	public int[] possiblePokemon;
	public float[] spawnChance;
	public Transform[] prefabs;
	private ArrayList currentlySpawned;
	private Transform spawnerTransform;

	// Use this for initialization
	void Start () {
		StaticPokemonData.load ();
		currentlySpawned = new ArrayList ();
		spawnerTransform = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (currentlySpawned.Count < spawnLimit) {
			// Seed RNG
			Random.InitState(System.DateTime.Now.Millisecond + currentlySpawned.Count);

			// Generate Pokemon
			int index = Random.Range (0, possiblePokemon.Length);
			int id = possiblePokemon[index];
			int lvl = Random.Range (minLevel, maxLevel);
			Pokemon p = Pokemon.generatePokemon (id, lvl);
			currentlySpawned.Add (p);

			// Spawn Pokemon Prefab
			float x = Random.Range (0, spawnRadius * 2) - spawnRadius;
			float z = Random.Range (0, spawnRadius * 2) - spawnRadius;
			float y = StaticPokemonData.getPokemonData (id).Type1 == Type.Flying ? 3 : 0;
			Vector3 location = new Vector3 (x, y, z);
			var instance = Instantiate (prefabs [index], location, Quaternion.identity);
			instance.transform.Rotate (new Vector3 (0, Random.Range (0, 360), 0));

			// Attach Pokemon to Prefab
			instance.GetComponent<PokemonRelation> ().setPokemon (p, this);
		}
	}

}
