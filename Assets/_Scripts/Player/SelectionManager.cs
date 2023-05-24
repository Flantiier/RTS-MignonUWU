using InputMaps;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class SelectionManager : MonoBehaviour
{
    #region Variables
    [Header("Selection properties")]
    [SerializeField] private float dragTreshold = 5;
    [SerializeField] private float rayDistance = 10f;
    [SerializeField] private LayerMask walkableMask;
    [SerializeField] private LayerMask unitMask;
    [SerializeField] private LayerMask enemyMask;

    //Inputs
    private PlayerActions _inputs;

    //Mouse Variables
    private bool _mouseHeld;
    private Vector2 _mousePos;
    private Vector2 _startPos;
    #endregion

    #region Properties
    public HashSet<SelectableUnit> selectedUnits;
    #endregion

    #region Builts_In
    private void Awake()
    {
        _inputs = new PlayerActions();
        selectedUnits = new HashSet<SelectableUnit>();
    }

    private void OnEnable()
    {
        _inputs.Enable();
        _inputs.Selection.LeftClick.canceled += SetSelection;
        _inputs.Selection.RighClick.started += HandleUnitsAction;
    }

    private void OnDisable()
    {
        _inputs.Disable();
        _inputs.Selection.LeftClick.canceled -= SetSelection;
        _inputs.Selection.RighClick.started -= HandleUnitsAction;
    }

    private void Update()
    {
        HandleMouseInput();
        DebugMouseRay();
    }
    #endregion

    #region Methods
    private void DebugMouseRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(_mousePos);
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.cyan);
    }

    #region Mouse Selection
    /// <summary>
    /// Handle the selection of units
    /// </summary>
    private void HandleMouseInput()
    {
        _mouseHeld = Input.GetMouseButton(0);
        _mousePos = Input.mousePosition;

        //Update trhe last click
        if (Input.GetMouseButtonDown(0))
            _startPos = _mousePos;

        ///////////A FINIR////////////////
        //Update the selection box
        /*if (IsDragging())
            UpdateSelectionBox(_mousePos);*/
    }

    /// <summary>
    /// End click aciton
    /// </summary>
    private void SetSelection(InputAction.CallbackContext ctx)
    {
        ///////////A FINIR////////////////

        //Rectangle selection
        /*if (IsDragging())
        {
            //Looking for all units in the rectangle
            DragSelect();
            selectionBox.gameObject.SetActive(false);
            return;
        }*/

        //Simple Selection
        Ray ray = Camera.main.ScreenPointToRay(_mousePos);

        //If unit touched => Add to the selection
        if (Physics.Raycast(ray, out RaycastHit hit, unitMask) && hit.collider.TryGetComponent(out SelectableUnit unit))
        {
            if (ShiftPressed())
                ShiftSelect(unit);
            else
                Select(unit);

            return;
        }

        //If nothing touched => Deselect All
        DeselectAll();
    }

    /// <summary>
    /// Update the selection box size
    /// </summary>
    /// <param name="curMousePos">Current mouse position </param>
    void UpdateSelectionBox(Vector2 curMousePos)
    {
        /*if (!selectionBox.gameObject.activeInHierarchy)
            selectionBox.gameObject.SetActive(true);

        float width = curMousePos.x - _startPos.x;
        float height = curMousePos.y - _startPos.y;

        //Temp
        p1.anchoredPosition = _startPos;
        p2.anchoredPosition = curMousePos;

        selectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        selectionBox.anchoredPosition = _startPos + new Vector2(width / 2, height / 2);*/
    }

    /// <summary>
    /// Detecting iuf the mouse is clicking or dragging
    /// </summary>
    private bool IsDragging()
    {
        return _mouseHeld && Vector2.Distance(_startPos, _mousePos) >= dragTreshold;
    }

    /// <summary>
    /// Returns if shift is pressed
    /// </summary>
    private bool ShiftPressed()
    {
        return _inputs.Selection.Shift.IsPressed();
    }
    #endregion

    #region Units Selection
    /// <summary>
    /// Remove all the selection and select the last selected unit
    /// </summary>
    private void Select(SelectableUnit unit)
    {
        DeselectAll();

        unit.Select();
        selectedUnits.Add(unit);
    }

    /// <summary>
    /// Add the unit to the current selection
    /// </summary>
    private void ShiftSelect(SelectableUnit unit)
    {
        unit.Select();
        selectedUnits.Add(unit);
    }

    /// <summary>
    /// Select all the units inside the selection box
    /// </summary>
    private void DragSelect()
    {

    }

    /// <summary>
    /// Remove all the units inside the selection box
    /// </summary>
    private void DragRemove()
    {

    }

    /// <summary>
    /// Deselect all the current selection
    /// </summary>
    private void DeselectAll()
    {
        foreach (SelectableUnit unit in selectedUnits)
        {
            if (!unit)
                continue;

            unit.Deselect();
        }

        selectedUnits.Clear();
    }
    #endregion

    #region Unit Actions
    /// <summary>
    /// Move all the selected units
    /// </summary>
    private void HandleUnitsAction(InputAction.CallbackContext ctx)
    {
        if (selectedUnits.Count <= 0)
            return;

        Ray ray = Camera.main.ScreenPointToRay(_mousePos);

        //Enemy
        if (Physics.Raycast(ray, out RaycastHit enemyHit, rayDistance, enemyMask))
        {
            Debug.Log("Enemy attack");
            UnitsAttack(enemyHit.collider.transform);
            return;
        }
        //Move
        if (Physics.Raycast(ray, out RaycastHit groundHit, rayDistance, walkableMask))
        {
            Debug.Log("Move");
            MoveUnits(groundHit.point);
        }
    }

    /// <summary>
    /// Move all the selected units to a point
    /// </summary>
    /// <param name="point"> Target Position </param>
    private void MoveUnits(Vector3 point)
    {
        foreach (SelectableUnit unit in selectedUnits)
        {
            if (!unit)
                continue;

            unit.EnterMoveState(point);
        }
    }

    /// <summary>
    /// Call the units to start attacking a target
    /// </summary>
    /// <param name="target"> Target to attack </param>
    private void UnitsAttack(Transform target)
    {
        foreach (SelectableUnit unit in selectedUnits)
        {
            if (!unit)
                continue;

            unit.EnterCombatState(target);
        }
    }
    #endregion

    #endregion
}