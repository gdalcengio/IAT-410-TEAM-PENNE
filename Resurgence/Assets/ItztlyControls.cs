// GENERATED AUTOMATICALLY FROM 'Assets/ItztlyControls.inputactions'

using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class ItztlyControls : IInputActionCollection
{
    private InputActionAsset asset;
    public ItztlyControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""ItztlyControls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""709af164-3801-4d4e-a95c-65b0459386a7"",
            ""actions"": [
                {
                    ""name"": ""JumpItztly"",
                    ""type"": ""Button"",
                    ""id"": ""e160fdd2-c930-473e-80e9-7b98d88ecf65"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""53cc1c30-dc44-4588-9e17-a25178bbcc9a"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Itztly"",
                    ""action"": ""JumpItztly"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Itztly"",
            ""bindingGroup"": ""Itztly"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_JumpItztly = m_Gameplay.FindAction("JumpItztly", throwIfNotFound: true);
    }

    ~ItztlyControls()
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_JumpItztly;
    public struct GameplayActions
    {
        private ItztlyControls m_Wrapper;
        public GameplayActions(ItztlyControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @JumpItztly => m_Wrapper.m_Gameplay_JumpItztly;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                JumpItztly.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJumpItztly;
                JumpItztly.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJumpItztly;
                JumpItztly.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJumpItztly;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                JumpItztly.started += instance.OnJumpItztly;
                JumpItztly.performed += instance.OnJumpItztly;
                JumpItztly.canceled += instance.OnJumpItztly;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    private int m_ItztlySchemeIndex = -1;
    public InputControlScheme ItztlyScheme
    {
        get
        {
            if (m_ItztlySchemeIndex == -1) m_ItztlySchemeIndex = asset.FindControlSchemeIndex("Itztly");
            return asset.controlSchemes[m_ItztlySchemeIndex];
        }
    }
    public interface IGameplayActions
    {
        void OnJumpItztly(InputAction.CallbackContext context);
    }
}
