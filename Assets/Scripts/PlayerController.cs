using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameObject focalPoint;
    Rigidbody rbPlayer;
    Renderer rendererPlayer;
    public float speed = 10.0f;


    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();
        rendererPlayer = GetComponent<Renderer>();
        focalPoint = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void Update()
    {
        float fowardInput = Input.GetAxis("Vertical");
        float magnitude = fowardInput * speed * Time.deltaTime;                         
        rbPlayer.AddForce(focalPoint.transform.forward * fowardInput * speed * Time.deltaTime, ForceMode.Force);

        Debug.Log("Mag:" + magnitude);
        Debug.Log("Fi:" + fowardInput);

        if (fowardInput > 0)
        {
            rendererPlayer.material.color = new Color(1.0f - fowardInput, 1.0f, 1.0f - fowardInput);
        }
        else
        {
            rendererPlayer.material.color = new Color(1.0f + fowardInput, 1.0f, 1.0f + fowardInput);
        }

    }
}
