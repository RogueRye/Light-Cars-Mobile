using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using TMPro;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : NetworkBehaviour {

    [SyncVar]
    private int playerNum;
    [SyncVar]
    private string playerName;


    public float speed;
    public float maxTurnAngle;
    public float turnDamper = 2;
    public MovementTypes controlType = MovementTypes.Accelerometer;
    public ParticleSystem deathFx;
    public Material[] trailMats;
    public WheelCollider frontDriverW, frontPassengerW;
    public WheelCollider rearDriverW, rearPassengerW;
    

    bool dead = false;

    private Rigidbody rb;

    float h = 0;
    float v = 0;
    float steerAngle;
    ObjectTrail trail;
    CameraController cam;
    LobbyManager network;
    public enum MovementTypes
    {
        Accelerometer, touch
    }

    public void SetNameAndNumber(string _name, int _num)
    {
        playerName = _name;
        playerNum = _num;
    }

	// Use this for initialization
	void Start () {

        trail = GetComponentInChildren<ObjectTrail>();
        network = NetworkManager.singleton as LobbyManager;

        trail.SetCurrentMat(trailMats[(playerNum -1)]);
        
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
        Steer();
        Accelerate();

    }


    public void GetInput()
    {
#if UNITY_STANDALONE || UNITY_WEBPLAYER// || UNITY_EDITOR

        h = Input.GetAxis("Horizontal");
        v = speed * Time.fixedDeltaTime * 60;

#elif UNITY_IOS || UNITY_ANDROID

        v = speed * Time.fixedDeltaTime * 60;

        

         if (controlType == MovementTypes.Accelerometer)
        {
            if (Input.gyro.enabled)
            {
                h = Input.gyro.attitude.eulerAngles.z;
                if (-Input.gyro.attitude.eulerAngles.x == -0.45)
                    v /= 1;
                else
                    v /= ((-Input.gyro.attitude.eulerAngles.x + 0.45f) * turnDamper);
            }
            else
            {
                h = Input.acceleration.z;
                if (-Input.gyro.attitude.x == -0.45)
                    v /= 1;
                else
                    v /= ((-Input.gyro.attitude.x + .45f) * turnDamper);
            }

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
        frontDriverW.motorTorque = v;
        frontPassengerW.motorTorque = v;
    }
    private void UpdateWheelPoses()
    {

    }


    private void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("Trail"))
        {
            Die();   
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Die();    
    }


    void Die()
    {
        dead = true;

        var particles = GameObject.Instantiate(deathFx, transform.position + (Vector3.up * .5f), Quaternion.identity);

        particles.GetComponent<ParticleSystemRenderer>().material = trailMats[playerNum - 1];
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);

        }

        Invoke("Restart", 3);

    }

    void Restart()
    {
        if (isLocalPlayer)
            network.myCanvas.SetActive(true);

        network.StopClient();
       
    }

   
}
