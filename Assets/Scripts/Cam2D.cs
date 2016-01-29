using UnityEngine;
using System.Collections;

public class Cam2D : MonoBehaviour {

    public Transform tarjet;

    void FixedUpdate()
    {
        this.transform.position = new Vector3(tarjet.position.x, tarjet.position.y, this.transform.position.z);
    }
}
