using UnityEngine;

/// <summary>
/// Handles the mouse look functions
/// </summary>
[AddComponentMenu("RPG/Player/Mouse Look")]
public class MouseLook : MonoBehaviour
{
    public enum RotationalAxis
    {
        MouseX,
        MouseY,
    }
    
    
    [Header("Rotation Variables")]
    public RotationalAxis axis = RotationalAxis.MouseX;
    [Range(0,200)]
    public float sensitivity = 100;
    public float minY = -60, maxY = 60;
    private float _rotY;

    void Start()
    {
        if(GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if(GetComponent<Camera>())
        {
            axis = RotationalAxis.MouseY;
        }
    }
    void Update()
    {
        if(axis == RotationalAxis.MouseX)
        {
            transform.Rotate(0,Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime,0);
        }
        else
        {
            _rotY += Input.GetAxis("Mouse Y")  * sensitivity * Time.deltaTime;
            _rotY = Mathf.Clamp(_rotY,minY,maxY);
            transform.localEulerAngles = new Vector3(-_rotY,0,0);
        }
    }
}   

