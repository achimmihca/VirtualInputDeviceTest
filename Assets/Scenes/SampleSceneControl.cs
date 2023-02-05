using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class SampleSceneControl : MonoBehaviour
{
    public TMP_Text uiText;
    public Button simulateLeftButton;
    public Button simulateRightButton;
    public InputActionAsset inputActionAsset;

    private InputAction leftAction;
    private InputAction rightAction;
    
    private Keyboard virtualKeyboard;
    private Mouse virtualMouse;

    private void Awake()
    {
        leftAction = inputActionAsset.FindAction("sampleActionMap/left", true);
        rightAction = inputActionAsset.FindAction("sampleActionMap/right", true);
    }

    private void OnEnable()
    {
        inputActionAsset.Enable();
        leftAction.performed += OnLeft;
        rightAction.performed += OnRight;
    }

    private void OnDisable()
    {
        leftAction.performed += OnLeft;
        rightAction.performed += OnRight;
    }

    private void Start()
    {
        virtualKeyboard = InputSystem.AddDevice<Keyboard>("Virtual Keyboard");
        virtualMouse = InputSystem.AddDevice<Mouse>("Virtual Mouse");
        
        simulateLeftButton.onClick.AddListener(() => { SimulateLeft(); });
        simulateRightButton.onClick.AddListener(() => { SimulateRight(); });
    }

    private void SimulateRight()
    {
        SimulateButtonClick(virtualKeyboard, virtualKeyboard.rightArrowKey);
    }

    private void SimulateLeft()
    {
        SimulateButtonClick(virtualKeyboard, virtualKeyboard.leftArrowKey);
    }
    
    private void SimulateButtonClick(InputDevice inputDevice, InputControl inputControl)
    {
        Debug.Log($"Triggering button click on input control {inputControl} by setting its value to 1 and afterwards to 0");
        StartCoroutine(CoroutineUtils.ExecuteAfterDelayInFrames(0, () =>
        {
            using (StateEvent.From(inputDevice, out InputEventPtr eventPtr))
            {
                inputControl.WriteValueIntoEvent(1f, eventPtr);
                InputSystem.QueueEvent(eventPtr);
            }
        }));

        StartCoroutine(CoroutineUtils.ExecuteAfterDelayInFrames(1, () =>
        {
            using (StateEvent.From(inputDevice, out InputEventPtr eventPtr))
            {
                inputControl.WriteValueIntoEvent(0f, eventPtr);
                InputSystem.QueueEvent(eventPtr);
            }
        }));
    }
    
    private void OnRight(InputAction.CallbackContext obj)
    {
        uiText.text = "Right";
    }

    private void OnLeft(InputAction.CallbackContext obj)
    {
        uiText.text = "Left";
    }
}