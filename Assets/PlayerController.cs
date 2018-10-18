using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {

    public float speed;
    public float maxTurnAngle;
    public MovementTypes controlType = MovementTypes.Accelerometer;

    public WheelCollider frontDriverW, frontPassengerW;
    public WheelCollider rearDriverW, rearPassengerW;

    bool dead = false;

    private Rigidbody rb;

    float h = 0;
    float steerAngle;


    public enum MovementTypes
    {
        Accelerometer, touch
    }
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (dead)
        {
            rb.velocity = Vector3.zero;
            return;

        }

        GetInput();
        Steer();
        Accelerate();

    }


    public void GetInput()
    {
#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR

        h = Input.GetAxis("Horizontal");





#elif UNITY_IOS || UNITY_ANDROID

                 if(controlType == MovementTypes.Accelerometer)
                {
                    h = Input.acceleration.x * maxTurnAngle;
                }


#endif
    }
    private void Steer()
    {
        steerAngle = maxTurnAngle * h;
        frontDriverW.steerAngle = steerAngle;
        frontPassengerW.steerAngle = steerAngle;
    }

    private void Accelerate()
    {
        frontDriverW.motorTorque = speed * Time.fixedDeltaTime * 60;
        frontPassengerW.motorTorque = speed * Time.fixedDeltaTime * 60;
    }
    private void UpdateWheelPoses()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Trail"))
        {
            dead = true;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }

            Invoke("Restart", 3);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        dead = true;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);

        }

        Invoke("Restart", 3);
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

   
}
