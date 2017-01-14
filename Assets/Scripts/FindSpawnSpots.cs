using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindSpawnSpots : MonoBehaviour {

    public GameObject target;
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
        text.text = "";
        for (int i = 0; i < 4; ++i)
        {
            
            Vector3 gazeDirection = Camera.main.transform.forward;
            Vector3 gazeSideDirection = Camera.main.transform.right;
            Vector3 gaze = gazeDirection;
            float rot = 0;
            switch (i)
            {
                case 0: rot = 0; gaze = gazeDirection; break;
                case 1: rot = -90; gaze = gazeSideDirection; gaze.z *= -1; gaze.x *= -1; break;
                case 2: rot = 90; gaze = gazeSideDirection; break;
                case 3: rot = 180; gaze = gazeDirection; gaze.z *= -1; gaze.x *= -1; break;
            }
            rot = AngleBetweenDirections(gaze, originalGazeDirection);
            text.text += "rot: " + rot + "\n";
            //text.text += "gaze: " + gaze + "\n";
            RaycastHit hitInfo;
            if (Physics.Raycast(headPosition, gaze, out hitInfo))
            {
                //text.text += "hit: " + hitInfo.point + "\n";
                //tr.Rotate(Vector3.up, rot);
                Quaternion quat = target.transform.rotation;
                Quaternion quatRot = new Quaternion(0,Mathf.Sin(Mathf.PI * rot / (2 * 180)),0,Mathf.Cos(Mathf.PI*rot/(2*180)));
                quat *= quatRot;
                GameObject obj = (GameObject)Instantiate(target, hitInfo.point, quat);//Quaternion.FromToRotation(Vector3.up, -gaze));
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
