using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class LeftCubeControls : MonoBehaviour
{
    // Input System
    PlayerControls controls; // Reference to the Input System controls object

    // Input State Variables
    bool hatUp, hatDown, hatLeft, hatRight; // Track the state of hat switch inputs
    bool button3, button4; // Track the state of additional button inputs

    // Movement and Rotation Settings
    public float rotationSpeed = 100f; // How fast cubes rotate (degrees per second)
    public float moveSpeed = 5f;       // How fast cubes move (units per second)

    // Control State Variables 
    private bool movementMode = false;  // Toggles between movement and rotation mode
    private bool mirrorMode = false;    // Toggles between normal and mirrored behavior

    // Cube References
    public Transform leftCubeTransform;  // Reference to the left cube's Transform 
    public Transform rightCubeTransform; // Reference to the right cube's Transform

    // Hand Control Mode
    private enum HandMode { Left, Right, Both } // Options for controlling one or both cubes
    private HandMode currentHandMode = HandMode.Left; // The currently selected hand mode


    private void Awake()
    {
        // 1. Initialize Input System
        controls = new PlayerControls();

        // 2. Set Up Input Actions and Behavior

        // -- Hat Switch Inputs --
        controls.Gameplay.HatUp.performed += ctx => hatUp = true;
        controls.Gameplay.HatUp.canceled += ctx => hatUp = false;

        controls.Gameplay.HatDown.performed += ctx => hatDown = true;
        controls.Gameplay.HatDown.canceled += ctx => hatDown = false;

        controls.Gameplay.HatLeft.performed += ctx => hatLeft = true; 
        controls.Gameplay.HatLeft.canceled += ctx => hatLeft = false; 

        controls.Gameplay.HatRight.performed += ctx => hatRight = true; 
        controls.Gameplay.HatRight.canceled += ctx => hatRight = false;

        // -- Button Inputs --
        controls.Gameplay.Button3.performed += ctx => button3 = true;
        controls.Gameplay.Button3.canceled += ctx => button3 = false;

        controls.Gameplay.Button4.performed += ctx => button4 = true;
        controls.Gameplay.Button4.canceled += ctx => button4 = false;

        // -- Mode and Function Buttons -- 
        controls.Gameplay.Button4.performed += ctx => ToggleMovementMode(); 
        controls.Gameplay.Button6.performed += ctx => CycleHandMode();
        controls.Gameplay.Button2.performed += ctx => ToggleMirrorMode();
        controls.Gameplay.Button9.performed += ctx => ResetCube();


    }

    void Update()
    {
        // --- 1. Determine Which Cube to Control ---
        Transform targetCube = (currentHandMode == HandMode.Right) ? rightCubeTransform : leftCubeTransform;

        // --- 2. Both Hands Mode ---
        if (currentHandMode == HandMode.Both)
        {
            // -- Movement Controls --
            if (movementMode)
            {
                if (hatLeft)
                {
                    leftCubeTransform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
                    if (mirrorMode)
                    {
                        rightCubeTransform.Translate(Vector3.right * moveSpeed * Time.deltaTime); // Mirror X 
                    }
                    else
                    {
                        rightCubeTransform.Translate(Vector3.left * moveSpeed * Time.deltaTime); // Mimic
                    }
                }
                if (hatRight)
                {
                    leftCubeTransform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                    if (mirrorMode)
                    {
                        rightCubeTransform.Translate(Vector3.left * moveSpeed * Time.deltaTime); // Mirror X
                    }
                    else
                    {
                        rightCubeTransform.Translate(Vector3.right * moveSpeed * Time.deltaTime); // Mimic
                    }
                }

                if (button3) // Button3 modifies movement controls
                {
                    if (hatUp)
                    {
                        leftCubeTransform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                        if (mirrorMode)
                        {
                            rightCubeTransform.Translate(Vector3.back * moveSpeed * Time.deltaTime); // Mirror Z
                        }
                        else
                        {
                            rightCubeTransform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                        }
                    }
                    if (hatDown)
                    {
                        leftCubeTransform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
                        if (mirrorMode)
                        {
                            rightCubeTransform.Translate(Vector3.forward * moveSpeed * Time.deltaTime); // Mirror Z
                        }
                        else
                        {
                            rightCubeTransform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
                        }
                    }
                }
                else // Default movement without button3
                {
                    if (hatUp)
                    {
                        leftCubeTransform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
                        if (mirrorMode)
                        {
                            rightCubeTransform.Translate(Vector3.down * moveSpeed * Time.deltaTime); // Mirror Y
                        }
                        else
                        {
                            rightCubeTransform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
                        }
                    }
                    if (hatDown)
                    {
                        leftCubeTransform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
                        if (mirrorMode)
                        {
                            rightCubeTransform.Translate(Vector3.up * moveSpeed * Time.deltaTime); // Mirror Y
                        }
                        else
                        {
                            rightCubeTransform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
                        }
                    }
                }
            }
            // -- Rotation Controls --
            else  // Rotation mode 
            {
                // Logic for rotation controls in 'Both' hand mode, including mirroring 
                if (hatUp)
                {
                    leftCubeTransform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime, Space.World);
                    if (mirrorMode)
                    {
                        rightCubeTransform.Rotate(Vector3.right * -rotationSpeed * Time.deltaTime, Space.World); // Mirror X rotation
                    }
                    else
                    {
                        rightCubeTransform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime, Space.World);
                    }
                }
                if (hatDown)
                {
                    leftCubeTransform.Rotate(Vector3.left * rotationSpeed * Time.deltaTime, Space.World);
                    if (mirrorMode)
                    {
                        rightCubeTransform.Rotate(Vector3.left * -rotationSpeed * Time.deltaTime, Space.World);  // Mirror X rotation
                    }
                    else
                    {
                        rightCubeTransform.Rotate(Vector3.left * rotationSpeed * Time.deltaTime, Space.World);
                    }
                }
                if (button3) // Button3 modifies rotation controls
                {
                    if (hatLeft)
                    {
                        leftCubeTransform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime, Space.World);
                        if (mirrorMode)
                        {
                            rightCubeTransform.Rotate(Vector3.forward * -rotationSpeed * Time.deltaTime, Space.World); // Mirror Z rotation
                        }
                        else
                        {
                            rightCubeTransform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime, Space.World);
                        }
                    }
                    if (hatRight) // Mirroring the Z axis with hatRight
                    {
                        leftCubeTransform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime, Space.World);
                        if (mirrorMode)
                        {
                            rightCubeTransform.Rotate(Vector3.back * -rotationSpeed * Time.deltaTime, Space.World); // Mirror Z rotation
                        }
                        else
                        {
                            rightCubeTransform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime, Space.World);
                        }
                    }
                }
                else
                {
                    if (hatLeft)
                    {
                        leftCubeTransform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
                        if (mirrorMode)
                        {
                            rightCubeTransform.Rotate(Vector3.down * rotationSpeed * Time.deltaTime, Space.World); // Mirror Y rotation
                        }
                        else
                        {
                            rightCubeTransform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
                        }
                    }
                    if (hatRight)  // Mirroring the Y axis with hatRight
                    {
                        leftCubeTransform.Rotate(Vector3.down * rotationSpeed * Time.deltaTime, Space.World);
                        if (mirrorMode)
                        {
                            rightCubeTransform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World); // Mirror Y rotation
                        }
                        else
                        {
                            rightCubeTransform.Rotate(Vector3.down * rotationSpeed * Time.deltaTime, Space.World);
                        }
                    }
                }
            }
        }
        // --- 3. Left or Right Hand Mode ---
        else 
        {

            if (movementMode)
            {
                // Movement mode controls
                if (hatLeft)
                    targetCube.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.World);
                if (hatRight)
                    targetCube.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.World);

                if (button3) // Button3 modifies movement controls
                {
                    if (hatUp)
                        targetCube.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.World);
                    if (hatDown)
                        targetCube.Translate(Vector3.back * moveSpeed * Time.deltaTime, Space.World);
                }
                else // Default behavior
                {
                    if (hatUp)
                        targetCube.Translate(Vector3.up * moveSpeed * Time.deltaTime, Space.World);
                    if (hatDown)
                        targetCube.Translate(Vector3.down * moveSpeed * Time.deltaTime, Space.World);
                }
            }
            else
            {
                // Rotation mode controls 
                if (hatUp)
                    targetCube.Rotate(Vector3.right * rotationSpeed * Time.deltaTime, Space.World);
                if (hatDown)
                    targetCube.Rotate(Vector3.left * rotationSpeed * Time.deltaTime, Space.World);


                if (button3) // Button3 modifies rotation controls
                {
                    if (hatLeft)
                        targetCube.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime, Space.World);
                    if (hatRight)
                        targetCube.Rotate(Vector3.back * rotationSpeed * Time.deltaTime, Space.World);
                    // hatLeft and hatRight now rotate around global X while Button3 is held
                }
                else // Default behavior
                {
                    if (hatLeft)
                        targetCube.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
                    if (hatRight)
                        targetCube.Rotate(Vector3.down * rotationSpeed * Time.deltaTime, Space.World);
                }
            }
        }

    }

    // --- Mode Toggling Functions ---

    private void ToggleMovementMode()
    {
        movementMode = !movementMode; // Toggle the movment mode (rotation vs movement)
    }

    private void CycleHandMode()
    {
        currentHandMode = (HandMode)(((int)currentHandMode + 1) % 3); // Cycle through hand modes (Left, Right, Both)
    }

    // --- Reset Function ---

    private void ResetCube()
    {
        if (currentHandMode == HandMode.Left || currentHandMode == HandMode.Both)
        {
            leftCubeTransform.position = new Vector3(0, 1, 0);  // Original left cube position
            leftCubeTransform.rotation = Quaternion.identity;   // Reset rotation
        }

        if (currentHandMode == HandMode.Right || currentHandMode == HandMode.Both)
        {
            rightCubeTransform.position = new Vector3(2, 1, 0);  // Original right cube position
            rightCubeTransform.rotation = Quaternion.identity;  // Reset rotation
        }
    }

    // --- Mirror Mode Toggle ---

    private void ToggleMirrorMode()
    {
        mirrorMode = !mirrorMode; // Hands mirror each other
    }

    // --- Input System: Enabling & Disabling ---

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }
}
