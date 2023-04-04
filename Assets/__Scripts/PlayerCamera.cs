using InputMaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    #region Variables
    [Header("Cam Properties")]
    [SerializeField, Range(1f, 25f)] private float viewHeight = 10;
    [SerializeField, Range(0f, 180f)] private float viewAngle = 45;
    [SerializeField, Range(1, 20)] private int orthographicSize = 10;

    [Header("Movement")]
    [SerializeField] private float speed = 3f;
    [SerializeField, Range(0, 0.1f)] private float lerp = 0.05f;
    private Vector2 _rawInputs;
    private Vector2 _currentInputs;
    private Vector2 _SmoothInputRef;

    private PlayerActions _inputs;
    private Camera _cam;
    #endregion

    #region Builts_In
    private void Awake()
    {
        _cam = GetComponent<Camera>();
        _inputs = new PlayerActions();
    }

    private void OnEnable()
    {
        _inputs.Enable();
        _inputs.Camera.Move.performed += ctx => _rawInputs = ctx.ReadValue<Vector2>();
        _inputs.Camera.Move.canceled += ctx => _rawInputs = Vector2.zero;
    }

    private void OnDisable()
    {
        _inputs.Disable();
        _inputs.Camera.Move.performed -= ctx => _rawInputs = ctx.ReadValue<Vector2>();
        _inputs.Camera.Move.canceled -= ctx => _rawInputs = Vector2.zero;
    }

    private void Update()
    {
        SetView();
        HandleCameraMovement();
    }
    #endregion

    #region Methods
    /// <summary>
    /// Move the camera based on the player inputs
    /// </summary>
    public void HandleCameraMovement()
    {
        _currentInputs = Vector2.SmoothDamp(_currentInputs, _rawInputs, ref _SmoothInputRef, lerp);
        Vector3 movement = Vector3.forward * _currentInputs.y + Vector3.right * _currentInputs.x;

        transform.position += speed * Time.deltaTime * movement;
    }

    /// <summary>
    /// Set camera heigth and view angle
    /// </summary>
    private void SetView()
    {
        //Set camera height
        transform.position = new Vector3(transform.position.x, viewHeight, transform.position.z);
        //Set vierw angle
        transform.rotation = Quaternion.Euler(viewAngle, transform.rotation.y, transform.rotation.z);
        //Set camera size
        _cam.orthographicSize = orthographicSize;
    }
    #endregion
}
