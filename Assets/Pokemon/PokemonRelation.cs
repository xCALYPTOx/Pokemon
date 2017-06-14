using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonRelation : MonoBehaviour {

	Pokemon pokemon;
	Transform prefab;
	object source;
	float randomMoveTimer;

	// Use this for initialization
	void Start () {
		prefab = GetComponent<Transform> ();
		randomMoveTimer = 10f;
	}
	
	// Update is called once per frame
	void Update () {
		randomMoveTimer -= Time.deltaTime;
		print (randomMoveTimer);
		if (randomMoveTimer <= 0) {
			randomMoveTimer = 10f;
			if (this.isWild()) {
				MoveRandom ();
			}
		}
	}

	public void setPokemon(Pokemon p, object source)
	{
		this.pokemon = p;
		this.source = source;
	}

	public Pokemon getPokemon()
	{
		return pokemon;
	}

	bool isWild()
	{
		return (source != null && source.GetType () == typeof(PokemonSpawner));
	}

	public void Translate(Vector3 location)
	{
		prefab.Translate (location);
	}

	public Vector3 getPosition()
	{
		return prefab.transform.position;
	}

	private void MoveRandom()
	{
		print ("in random");
		PokemonSpawner spawner = (PokemonSpawner)source;
		Random.InitState (System.DateTime.Now.Millisecond);
		float x = spawner.transform.position.x + Random.Range (0f, (float)spawner.spawnRadius * 2) - (float)spawner.spawnRadius;
		float z = spawner.transform.position.z + Random.Range (0f, (float)spawner.spawnRadius * 2) - (float)spawner.spawnRadius;
		prefab.Translate (new Vector3 (x, prefab.position.y, z));
	}

	public void Move(Vector3 direction)
	{
		Animator anim = GetComponent<Animator> ();
		// animate
		prefab.Translate (direction);
	}
}
