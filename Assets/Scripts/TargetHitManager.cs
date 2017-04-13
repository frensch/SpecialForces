using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHitManager : MonoBehaviour {
    public int points;
    public GameObject BulletMark;
    private ScoreManager score;
    private GazeGestureManager gazeGestureManager;

    void Start()
    {
        StartCoroutine(GetParentScoreManager());
    }
    IEnumerator GetParentScoreManager() {
        yield return new WaitForSeconds(1.0f);
        score = transform.parent.GetComponentInParent<ScoreManager>();
        gazeGestureManager = transform.parent.GetComponentInParent<GazeGestureManager>();
    }
    // Called by GazeGestureManager when the user performs a Select gesture
    void OnSelect()
    {
        GameObject bullet = (GameObject)Object.Instantiate(BulletMark, gazeGestureManager.GetPos(), gazeGestureManager.GetRotation());
        score.AddPoints(points);
        bullet.transform.parent = gameObject.transform;
        Object.Destroy(gameObject, 1.0f);
    }
}
