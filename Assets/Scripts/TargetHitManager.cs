using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHitManager : MonoBehaviour {
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
        //score.AddPoints(1);
    }
    // Called by GazeGestureManager when the user performs a Select gesture
    void OnSelect()
    {
        GameObject bullet = (GameObject)Object.Instantiate(BulletMark, gazeGestureManager.GetPos(), gazeGestureManager.GetRotation());
        score.AddPoints(100);
        Object.Destroy(bullet, 1.0f);
        Object.Destroy(gameObject, 1.0f);
    }
}
