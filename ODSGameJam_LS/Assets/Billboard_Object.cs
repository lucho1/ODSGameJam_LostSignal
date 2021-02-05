using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard_Object : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera mycam;
    void Start()
    {
        mycam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the camera every frame so it keeps looking at the target
        transform.LookAt(mycam.gameObject.transform);

        // Same as above, but setting the worldUp parameter to Vector3.left in this example turns the camera on its side
        //transform.LookAt(target, Vector3.left);
    }
}
