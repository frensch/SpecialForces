using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindSpawnSpots : MonoBehaviour {

    public GameObject target;
    public GameObject hostage;
    public GameObject targetsPivot;
    public Text text;
    private Vector3 headPosition;
    private Vector3 originalGazeDirection;
    private Vector3 originalGazeSideDirection;
    // Use this for initialization
    void Start () {
        headPosition = Camera.main.transform.position;
        originalGazeDirection = Camera.main.transform.forward;
        originalGazeSideDirection = Camera.main.transform.right;

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
        //text.text = "headPosition: " + headPosition + "\n";
        int hostageIndex = Random.Range(0, 3);
        text.text = "";
        for (int i = 0; i < 4; ++i)
        {
            text.text = "hostage: " + hostageIndex + "\n";
            GameObject targetPrefab = target;
            if (i == hostageIndex)
                targetPrefab = hostage;
            Vector3 gazeDirection = Camera.main.transform.forward;
            Vector3 gazeSideDirection = Camera.main.transform.right;
            Vector3 gaze = Quaternion.Euler(0, i*90 + Random.Range(-45.0f,45.0f), 0) * gazeDirection;
            float rot = AngleBetweenDirections(gaze, originalGazeDirection);
            text.text += "rot: " + rot + "\n";
            //text.text += "gaze: " + gaze + "\n";
            RaycastHit hitInfo;
            if (Physics.Raycast(headPosition, gaze, out hitInfo))
            {
                //text.text += "hit: " + hitInfo.point + "\n";
                //tr.Rotate(Vector3.up, rot);
                Quaternion quat = targetPrefab.transform.rotation;
                Quaternion quatRot = new Quaternion(0,Mathf.Sin(Mathf.PI * rot / (2 * 180)),0,Mathf.Cos(Mathf.PI*rot/(2*180)));
                quat *= quatRot;
                GameObject obj = (GameObject)Instantiate(targetPrefab, hitInfo.point, quat);//Quaternion.FromToRotation(Vector3.up, -gaze));
                obj.transform.parent = targetsPivot.transform;
            }
        }
        yield return new WaitForSeconds(30.0f);
        StartCoroutine(CreateSpawnPoints());
    }
    
    // Update is called once per frame
    void Update () {
		
	}
}
