using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CarController : MonoBehaviour
{
    public static CarController instance;
    public enum WheelDriveType
    {
        NONE,
        REAR_WD,
        FRONT_WD,
        FOUR_WD
    }

    public WheelDriveType wheelDriveType;
    public bool isUsInput;
    public bool isInputMobile;
    public Transform centerOfMassa;
    public AudioClip soundEngine;
    public Transform driverParent;
    public float fuel;
    public float maxTurn;
    public float powerEngine;
    public float powerBreake;
    public float speedRotationInAir;
    public float positionZ;
    float rotationY = 90f;
    float rotationZ = 0f;
    public GameObject objSmoke;
    public Transform parentSmoke;
    public List<WheelCollider> steerWheels;
    public List<WheelCollider> throttleWheels;

    float motorTorque;
    float steerAngle;
    ParticleSystem smoke;
    float minSizeSmoke = 1.5f;
    float maxSizeSmoke = 6f;
    Rigidbody _rigidbody;
    AudioSource audioSource;
    [HideInInspector]
    public bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.maxAngularVelocity = 2f;
        if (objSmoke != null && parentSmoke != null)
        {
            var mySmoke = Instantiate(objSmoke, parentSmoke);
            smoke = mySmoke.GetComponent<ParticleSystem>();
        }
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = true;
        isGrounded = false;
        PlaySoundEngine();
    }

    // Update is called once per frame
    void Update()
    {
        SetUpInput();
        rotasiVehicle();
        setSmoke();
        setCenterOfMassa();
        // SetSoundEngineSystem();
    }

    void SetUpInput()
    {
        if (isUsInput && !isInputMobile)
        {
            motorTorque = Input.GetAxis("Horizontal");
            steerAngle = Input.GetAxis("Vertical");
        }

        if (isUsInput && isInputMobile)
        {
            motorTorque = CrossPlatformInputManager.GetAxis("Horizontal");
            steerAngle = CrossPlatformInputManager.GetAxis("Vertical");

        }

        SetUpWheels();

        if (DriverController.instance != null)
            DriverController.instance.SetOnGamePlay(motorTorque);

    }

    void SetUpWheels()
    {
        if (throttleWheels.Count > 0)
        {
            foreach (var wheel in throttleWheels)
            {
                if (wheelDriveType == WheelDriveType.REAR_WD || wheelDriveType == WheelDriveType.FOUR_WD)
                    wheel.motorTorque = motorTorque * powerEngine;
                SetPosRotObjWheel(wheel);
                breakWheel(wheel);

                if (wheel.isGrounded)
                {
                    if (GamePlayManager.instance != null && GamePlayManager.instance.state == GamePlayManager.State.play)
                        stableCar();
                    isGrounded = true;
                }
            }
        }
        if (steerWheels.Count > 0)
        {
            foreach (var wheel in steerWheels)
            {
                if (wheelDriveType == WheelDriveType.FRONT_WD || wheelDriveType == WheelDriveType.FOUR_WD)
                {
                    wheel.motorTorque = motorTorque * powerEngine;
                    breakWheel(wheel);
                }

                wheel.steerAngle = steerAngle * maxTurn;
                SetPosRotObjWheel(wheel);

                if (wheel.isGrounded)
                {
                    if (GamePlayManager.instance != null && GamePlayManager.instance.state == GamePlayManager.State.play)
                        stableCar();
                    isGrounded = true;
                }

            }
        }
    }

    void SetPosRotObjWheel(WheelCollider wheel)
    {
        Vector3 positionWheels;
        Quaternion rotationWheels;

        wheel.GetWorldPose(out (positionWheels), out (rotationWheels));
        wheel.transform.GetChild(0).transform.position = positionWheels;
        wheel.transform.GetChild(0).transform.rotation = rotationWheels;
    }

    float breakWheel(WheelCollider wheel)
    {

        if (!isInputMobile && (Mathf.Sign(Input.GetAxis("Horizontal")) != Mathf.Sign(wheel.rpm)
    && Mathf.Abs(wheel.rpm) > 20
    && Input.GetAxis("Horizontal") != 0) || Input.GetKey(KeyCode.Space))
            wheel.brakeTorque = powerBreake;

        if (isInputMobile && (Mathf.Sign(CrossPlatformInputManager.GetAxis("Horizontal")) != Mathf.Sign(wheel.rpm)
    && Mathf.Abs(wheel.rpm) > 20
    && CrossPlatformInputManager.GetAxis("Horizontal") != 0) || Input.GetKey(KeyCode.Space))
            wheel.brakeTorque = powerBreake;

        else
            wheel.brakeTorque = 0;

        return wheel.brakeTorque;
    }

    void rotasiVehicle()
    {
        _rigidbody.AddTorque(Vector3.forward * motorTorque * speedRotationInAir * Time.deltaTime, ForceMode.Acceleration);
    }

    void stableCar()
    {
        if (isUsInput)
        {
            if (transform.eulerAngles.y != rotationY || transform.eulerAngles.z != rotationZ)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, rotationY, rotationZ);
            }
            if (transform.position.z != positionZ)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, positionZ);
            }
        }

    }

    private void setSmoke()
    {
        if (objSmoke != null && parentSmoke != null && smoke != null)
        {
            var sizeSmoke = Mathf.Abs(motorTorque);
            var main = smoke.main;
            if (sizeSmoke >= 1)
                main.startSize = maxSizeSmoke;
            else
                main.startSize = minSizeSmoke;
        }
    }

    void PlaySoundEngine()
    {
        audioSource.clip = soundEngine;
        audioSource.Play();
    }


    void SetSoundEngineSystem()
    {
        if (motorTorque != 0)
        {
            var currentSpeed = _rigidbody.velocity.magnitude * 3.6f;
            Debug.Log("currentSpeed :" + currentSpeed);
            var jos = currentSpeed * Time.deltaTime;
            Debug.Log("jos : " + jos);
            if (currentSpeed > 100)
            {
                audioSource.pitch = 3f;

            }
            else
            {
                var ini = Mathf.Clamp(Mathf.Abs(motorTorque) * 120f * Time.deltaTime, 1, 3);
                audioSource.pitch = ini;
                Debug.Log(ini);
            }
            // var pitch = currentSpeed / 30f;
            // Debug.Log("currentSpeed : " + currentSpeed);
            // Debug.Log("Pitch : "+pitch);
            // audioSource.pitch = pitch;

        }
        else
        {
            audioSource.pitch = 1f;
        }
    }

    void setCenterOfMassa()
    {
        if (centerOfMassa == null)
            return;
        if (transform.InverseTransformPoint(centerOfMassa.position) == _rigidbody.centerOfMass)
            return;
        _rigidbody.centerOfMass = transform.InverseTransformPoint(centerOfMassa.transform.position);
    }

}
