using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameObject focalPoint;
    Rigidbody rbPlayer;
    Renderer rendererPlayer;
    public float speed = 10.0f;
    public float powerUpSpeed = 10.0f;
    public GameObject PowerUpInd;

    public bool hasPowerUp = false;


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
        PowerUpInd.transform.position = transform.position;


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountdown());
            PowerUpInd.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(hasPowerUp && collision.gameObject.CompareTag("enemy"))
        {
            Debug.Log("player has collided with" + collision.gameObject + "with power up set to" + hasPowerUp);
            Rigidbody rbEnemy = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayDir = collision.gameObject.transform.position - transform.position;

            rbEnemy.AddForce(awayDir * powerUpSpeed, ForceMode.Impulse);
        }
    }

    IEnumerator PowerUpCountdown()
    {
        yield return new WaitForSeconds(8);
        hasPowerUp = false;
        PowerUpInd.SetActive(false);
    }
}
