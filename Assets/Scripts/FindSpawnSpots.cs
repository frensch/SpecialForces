using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindSpawnSpots : MonoBehaviour {

    public GameObject target;
    public GameObject hostage;
    public GameObject targetsPivot;
    public Text text;

    private float targetLifeTime = 10.0f;
    private float targetCreationTime = 20.0f;
    private Vector3 headPosition;
    private Vector3 originalGazeDirection;
    private bool abortSpawn = false;
    public void StopTime()
    {
        abortSpawn = true;
    }
    public void ResetTime()
    {
        targetLifeTime = 10.0f;
        targetCreationTime = 20.0f;
        abortSpawn = false;
        StartCoroutine(CreateSpawnPoints());
    }

    public void DecreaseSpawnTime(float value)
    {
        targetLifeTime *= 1-value;
        targetCreationTime *= 1-value;
    }

    // Use this for initialization
    void Start () {
        headPosition = Camera.main.transform.position;
        originalGazeDirection = Camera.main.transform.forward;

        StartCoroutine(CreateSpawnPoints());
    }
    float AngleBetweenDirections(Vector3 vec1, Vector3 vec2)
    {
        var angle = -Vector3.Angle(vec1, vec2); // calculate angle
                                         // assume the sign of the cross product's Y component:
        return angle * Mathf.Sign(Vector3.Cross(vec1, vec2).y);
    }
    IEnumerator CreateSpawnPoints()
    {
        string logRot = "";
        int hostageIndex = Random.Range(0, 3);
        for (int i = 0; i < 4; ++i)
        {
            GameObject targetPrefab = target;
            if (i == hostageIndex)
                targetPrefab = hostage;
            Vector3 gazeDirection = Camera.main.transform.forward;
            Vector3 gaze = Quaternion.Euler(0, i*90 + Random.Range(-45.0f,45.0f), 0) * gazeDirection;
            float rot = AngleBetweenDirections(gaze, originalGazeDirection);
            logRot += "" + i + ": " + rot + ",";
            RaycastHit hitInfo;
            if (Physics.Raycast(headPosition, gaze, out hitInfo))
            {
                Quaternion quat = targetPrefab.transform.rotation;
                Quaternion quatRot = new Quaternion(0,Mathf.Sin(Mathf.PI * rot / (2 * 180)),0,Mathf.Cos(Mathf.PI*rot/(2*180)));
                quat *= quatRot;
                GameObject obj = (GameObject)Instantiate(targetPrefab, hitInfo.point, quat);//Quaternion.FromToRotation(Vector3.up, -gaze));
                obj.transform.parent = targetsPivot.transform;
                Destroy(obj, targetLifeTime);
            }
        }
        text.text = logRot + "\n" + text.text;
        text.text = "spawn: " + targetCreationTime + " lifetime: " + targetLifeTime + "\n" + text.text;
        yield return new WaitForSeconds(targetCreationTime);
        if(!abortSpawn)
            StartCoroutine(CreateSpawnPoints());
    }
    
    // Update is called once per frame
    void Update () {
		
	}
}
