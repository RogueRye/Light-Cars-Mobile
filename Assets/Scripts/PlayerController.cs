using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using TMPro;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : NetworkBehaviour {

    public float speed;
    public float maxTurnAngle;
    public float turnDamper = 2;
    public MovementTypes controlType = MovementTypes.Accelerometer;
    public Material[] trailMats;
    public WheelCollider frontDriverW, frontPassengerW;
    public WheelCollider rearDriverW, rearPassengerW;
    

    bool dead = false;

    private Rigidbody rb;

    float h = 0;
    float steerAngle;
    ObjectTrail trail;
    CameraController cam;
    LobbyManager network;
    public enum MovementTypes
    {
        Accelerometer, touch
    }
	// Use this for initialization
	void Start () {

        trail = GetComponentInChildren<ObjectTrail>();
        network = NetworkManager.singleton as LobbyManager;        
        trail.SetCurrentMat(trailMats[ netId.Value-2]);

        if (!isLocalPlayer)
            return;

        rb = GetComponent<Rigidbody>();
        cam = Camera.main.GetComponent<CameraController>();
        cam.Init(transform);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (!isLocalPlayer)
            return;

        cam.FixedTick();

        if (dead)
        {
            rb.velocity = Vector3.zero;
            return;

        }

        GetInput();
        //debugText.text = string.Format("H value = {0}", h);
        Steer();
        Accelerate();

    }


    public void GetInput()
    {
#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR

        h = Input.GetAxis("Horizontal");







#elif UNITY_IOS || UNITY_ANDROID

         if (controlType == MovementTypes.Accelerometer)
        {
            if (Input.gyro.enabled)
            {
                h = Input.gyro.attitude.z;
            }
            else 
                h = Input.acceleration.x;

            h *= turnDamper;
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
