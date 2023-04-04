using InputMaps;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectionManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private RectTransform selectionBox;
    [SerializeField] private RectTransform p1;
    [SerializeField] private RectTransform p2;
    [SerializeField] private float dragTreshold = 5;
    [SerializeField] private LayerMask unitMask;

    private PlayerActions _inputs;

    //Mouse Variables
    private bool _mouseHeld;
    private Vector2 _mousePos;
    private Vector2 _startPos;
    #endregion

    #region Properties
    public HashSet<SelectableUnit> units;
    #endregion

    #region Builts_In
    private void Awake()
    {
        _inputs = new PlayerActions();
        units = new HashSet<SelectableUnit>();
    }

    private void OnEnable()
    {
        _inputs.Enable();
        _inputs.Selection.Click.canceled += EndClick;
    }

    private void OnDisable()
    {
        _inputs.Disable();
        _inputs.Selection.Click.canceled -= EndClick;
    }

    private void Update()
    {
        HandleSelection();
        DebugMouse();
    }
    #endregion

    #region Methods
    private void DebugMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(_mousePos);
        Debug.DrawRay(ray.origin, ray.direction * 50f, Color.cyan);
    }

    #region Mouse Selection
    /// <summary>
    /// Handle the selection of units
    /// </summary>
    private void HandleSelection()
    {
        _mouseHeld = Input.GetMouseButton(0);
        _mousePos = Input.mousePosition;

        //Update trhe last click
        if (Input.GetMouseButtonDown(0))
            _startPos = _mousePos;

        //Update the selection box
        if (IsDragging())
            UpdateSelectionBox(_mousePos);
    }

    /// <summary>
    /// End click aciton
    /// </summary>
    private void EndClick(InputAction.CallbackContext ctx)
    {
        //Rectangle selection
        if (IsDragging())
        {
            //Looking for all units in the rectangle
            selectionBox.gameObject.SetActive(false);
        }
        //Simple Selection
        else
            SelectOnClick();
    }

    /// <summary>
    /// Update the selection box size
    /// </summary>
    /// <param name="curMousePos">Current mouse position </param>
    void UpdateSelectionBox(Vector2 curMousePos)
    {
        if (!selectionBox.gameObject.activeInHierarchy)
            selectionBox.gameObject.SetActive(true);

        float width = curMousePos.x - _startPos.x;
        float height = curMousePos.y - _startPos.y;

        //Temp
        p1.anchoredPosition = _startPos;
        p2.anchoredPosition = curMousePos;

        selectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        selectionBox.anchoredPosition = _startPos + new Vector2(width / 2, height / 2);
    }

    /// <summary>
    /// Shoot a raycast and select the unit if possible
    /// </summary>
    private void SelectOnClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(_mousePos);

        if (!Physics.Raycast(ray, out RaycastHit hit, unitMask))
            return;

        if (!hit.collider.TryGetComponent(out SelectableUnit unit))
            return;

        unit.OnSelected();
    }

    /// <summary>
    /// Detecting iuf the mouse is clicking or dragging
    /// </summary>
    private bool IsDragging()
    {
        return _mouseHeld && Vector2.Distance(_startPos, _mousePos) >= dragTreshold;
    }
    #endregion

    #endregion
}