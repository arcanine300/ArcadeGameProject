// GENERATED AUTOMATICALLY FROM 'Assets/Input/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""PS4"",
            ""id"": ""17345633-4826-4a4e-bacf-a139267f698c"",
            ""actions"": [
                {
                    ""name"": ""TurnAround"",
                    ""type"": ""Button"",
                    ""id"": ""bd163b0c-be46-47a5-b350-87e15c901b0a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""bd0caf76-1e26-4b23-bae6-29b2fab15918"",
                    ""path"": ""<DualShockGamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TurnAround"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""XboxOne"",
            ""id"": ""4aa65469-d37a-4157-8dbd-4830a59f22e3"",
            ""actions"": [
                {
                    ""name"": ""TurnAround"",
                    ""type"": ""Button"",
                    ""id"": ""67d92a11-86ef-443a-bc53-eead0637dd91"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a6283357-1b3f-4575-ba20-65d44f9c86e1"",
                    ""path"": ""<XInputController>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TurnAround"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""PC"",
            ""id"": ""9b4d1add-e84e-42d2-a21c-a9248c465c51"",
            ""actions"": [
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""b561ccb5-9def-48a5-880a-23f3b728deb0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f9eabf6d-5577-4302-ab72-f42071fbe0a2"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PS4
        m_PS4 = asset.FindActionMap("PS4", throwIfNotFound: true);
        m_PS4_TurnAround = m_PS4.FindAction("TurnAround", throwIfNotFound: true);
        // XboxOne
        m_XboxOne = asset.FindActionMap("XboxOne", throwIfNotFound: true);
        m_XboxOne_TurnAround = m_XboxOne.FindAction("TurnAround", throwIfNotFound: true);
        // PC
        m_PC = asset.FindActionMap("PC", throwIfNotFound: true);
        m_PC_Shoot = m_PC.FindAction("Shoot", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // PS4
    private readonly InputActionMap m_PS4;
    private IPS4Actions m_PS4ActionsCallbackInterface;
    private readonly InputAction m_PS4_TurnAround;
    public struct PS4Actions
    {
        private @PlayerInput m_Wrapper;
        public PS4Actions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @TurnAround => m_Wrapper.m_PS4_TurnAround;
        public InputActionMap Get() { return m_Wrapper.m_PS4; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PS4Actions set) { return set.Get(); }
        public void SetCallbacks(IPS4Actions instance)
        {
            if (m_Wrapper.m_PS4ActionsCallbackInterface != null)
            {
                @TurnAround.started -= m_Wrapper.m_PS4ActionsCallbackInterface.OnTurnAround;
                @TurnAround.performed -= m_Wrapper.m_PS4ActionsCallbackInterface.OnTurnAround;
                @TurnAround.canceled -= m_Wrapper.m_PS4ActionsCallbackInterface.OnTurnAround;
            }
            m_Wrapper.m_PS4ActionsCallbackInterface = instance;
            if (instance != null)
            {
                @TurnAround.started += instance.OnTurnAround;
                @TurnAround.performed += instance.OnTurnAround;
                @TurnAround.canceled += instance.OnTurnAround;
            }
        }
    }
    public PS4Actions @PS4 => new PS4Actions(this);

    // XboxOne
    private readonly InputActionMap m_XboxOne;
    private IXboxOneActions m_XboxOneActionsCallbackInterface;
    private readonly InputAction m_XboxOne_TurnAround;
    public struct XboxOneActions
    {
        private @PlayerInput m_Wrapper;
        public XboxOneActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @TurnAround => m_Wrapper.m_XboxOne_TurnAround;
        public InputActionMap Get() { return m_Wrapper.m_XboxOne; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(XboxOneActions set) { return set.Get(); }
        public void SetCallbacks(IXboxOneActions instance)
        {
            if (m_Wrapper.m_XboxOneActionsCallbackInterface != null)
            {
                @TurnAround.started -= m_Wrapper.m_XboxOneActionsCallbackInterface.OnTurnAround;
                @TurnAround.performed -= m_Wrapper.m_XboxOneActionsCallbackInterface.OnTurnAround;
                @TurnAround.canceled -= m_Wrapper.m_XboxOneActionsCallbackInterface.OnTurnAround;
            }
            m_Wrapper.m_XboxOneActionsCallbackInterface = instance;
            if (instance != null)
            {
                @TurnAround.started += instance.OnTurnAround;
                @TurnAround.performed += instance.OnTurnAround;
                @TurnAround.canceled += instance.OnTurnAround;
            }
        }
    }
    public XboxOneActions @XboxOne => new XboxOneActions(this);

    // PC
    private readonly InputActionMap m_PC;
    private IPCActions m_PCActionsCallbackInterface;
    private readonly InputAction m_PC_Shoot;
    public struct PCActions
    {
        private @PlayerInput m_Wrapper;
        public PCActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Shoot => m_Wrapper.m_PC_Shoot;
        public InputActionMap Get() { return m_Wrapper.m_PC; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PCActions set) { return set.Get(); }
        public void SetCallbacks(IPCActions instance)
        {
            if (m_Wrapper.m_PCActionsCallbackInterface != null)
            {
                @Shoot.started -= m_Wrapper.m_PCActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_PCActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_PCActionsCallbackInterface.OnShoot;
            }
            m_Wrapper.m_PCActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
            }
        }
    }
    public PCActions @PC => new PCActions(this);
    public interface IPS4Actions
    {
        void OnTurnAround(InputAction.CallbackContext context);
    }
    public interface IXboxOneActions
    {
        void OnTurnAround(InputAction.CallbackContext context);
    }
    public interface IPCActions
    {
        void OnShoot(InputAction.CallbackContext context);
    }
}
