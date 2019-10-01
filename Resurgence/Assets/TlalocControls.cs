// GENERATED AUTOMATICALLY FROM 'Assets/TlalocControls.inputactions'

using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class TlalocControls : IInputActionCollection
{
    private InputActionAsset asset;
    public TlalocControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""TlalocControls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""d940a418-a4e9-41f9-9160-bd08f4fec8ea"",
            ""actions"": [
                {
                    ""name"": ""JumpTlaloc"",
                    ""type"": ""Button"",
                    ""id"": ""ee4c0f19-4854-463c-9ba2-e1d441b65053"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""65c06b01-cfcc-4344-8f51-cfebff1e8867"",
                    ""path"": ""<DualShockGamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Tlaloc"",
                    ""action"": ""JumpTlaloc"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Tlaloc"",
            ""bindingGroup"": ""Tlaloc"",
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
        m_Gameplay_JumpTlaloc = m_Gameplay.FindAction("JumpTlaloc", throwIfNotFound: true);
    }

    ~TlalocControls()
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
    private readonly InputAction m_Gameplay_JumpTlaloc;
    public struct GameplayActions
    {
        private TlalocControls m_Wrapper;
        public GameplayActions(TlalocControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @JumpTlaloc => m_Wrapper.m_Gameplay_JumpTlaloc;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                JumpTlaloc.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJumpTlaloc;
                JumpTlaloc.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJumpTlaloc;
                JumpTlaloc.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJumpTlaloc;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                JumpTlaloc.started += instance.OnJumpTlaloc;
                JumpTlaloc.performed += instance.OnJumpTlaloc;
                JumpTlaloc.canceled += instance.OnJumpTlaloc;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    private int m_TlalocSchemeIndex = -1;
    public InputControlScheme TlalocScheme
    {
        get
        {
            if (m_TlalocSchemeIndex == -1) m_TlalocSchemeIndex = asset.FindControlSchemeIndex("Tlaloc");
            return asset.controlSchemes[m_TlalocSchemeIndex];
        }
    }
    public interface IGameplayActions
    {
        void OnJumpTlaloc(InputAction.CallbackContext context);
    }
}
