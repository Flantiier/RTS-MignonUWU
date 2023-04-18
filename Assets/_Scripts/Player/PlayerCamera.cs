using InputMaps;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    #region Variables
    [Header("Movement")]
    [SerializeField] private float speed = 3f;
    [SerializeField, Range(0, 0.1f)] private float lerp = 0.05f;
    [SerializeField] private Vector2 boundsX;
    [SerializeField] private Vector2 boundsZ;
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
        Vector3 position = transform.position + speed * Time.deltaTime * movement;
        position.x = Mathf.Clamp(position.x, boundsX.x, boundsX.y);
        position.z = Mathf.Clamp(position.z, boundsZ.x, boundsZ.y);

        transform.position = position;
    }
    #endregion
}
