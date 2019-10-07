using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public GameObject earth;
    private int count;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - earth.transform.position;
        Camera.main.backgroundColor = new Color(0.2f, 0.2f, 0.2f, 1);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKey(KeyCode.Space) && count > 10)
        {
            count = 0;
            if (GetComponent<Camera>().orthographicSize == 50)
            {
                GetComponent<Camera>().orthographicSize = 100;
            }
            else if (GetComponent<Camera>().orthographicSize == 100)
            {
                GetComponent<Camera>().orthographicSize = 200;
            }
            else if (GetComponent<Camera>().orthographicSize == 200)
            {
                GetComponent<Camera>().orthographicSize = 400;
            }
            else if (GetComponent<Camera>().orthographicSize == 400)
            {
                GetComponent<Camera>().orthographicSize = 50;
            }
        }
        count++;

        transform.position = earth.transform.position + offset;
    }

}