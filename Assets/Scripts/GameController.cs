using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    private float dificultyIncrease = 0.3f;
    private FindSpawnSpots spawn;


	// Use this for initialization
	void Start () {
        spawn = GetComponent<FindSpawnSpots>();
    }
	
	// Update is called once per frame
	void Update () {
        spawn.DecreaseSpawnTime(dificultyIncrease/(100*30));
    }
}
