// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Player/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Unboxed
{
    public class @PlayerInputActions : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @PlayerInputActions()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""511704a0-3108-41df-90cf-52d053e89add"",
            ""actions"": [
                {
                    ""name"": ""Click"",
                    ""type"": ""PassThrough"",
                    ""id"": ""affab2cb-b5ae-4f93-9c4d-948c7b9a5ccb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Hold"",
                    ""type"": ""PassThrough"",
                    ""id"": ""366a8e68-f3ae-4334-b7c9-1f5f904e5b4f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Release"",
                    ""type"": ""PassThrough"",
                    ""id"": ""dc4c749e-cc54-4477-a586-f03d47982291"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Point"",
                    ""type"": ""PassThrough"",
                    ""id"": ""809fce36-47ae-44c1-b368-839ef9a7a5c9"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""58cf6712-f851-4d52-8287-b16cb3dc1e3f"",
                    ""path"": ""<Mouse>/press"",
                    ""interactions"": ""Press(pressPoint=0.1)"",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""63192c82-4060-4996-b263-64802a51fa98"",
                    ""path"": ""<Touchscreen>/press"",
                    ""interactions"": ""Press(pressPoint=0.1)"",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d7ce1a1e-848f-4885-b35d-04194cb84660"",
                    ""path"": ""<Mouse>/press"",
                    ""interactions"": ""Hold(duration=0.125,pressPoint=0.1)"",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""Hold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bfca64b5-396a-4c6c-9a93-78af3650298b"",
                    ""path"": ""<Touchscreen>/press"",
                    ""interactions"": ""Hold(duration=0.125,pressPoint=0.1)"",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""Hold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3ac2d30a-b456-44e5-bce0-67ac976c26f5"",
                    ""path"": ""<Mouse>/press"",
                    ""interactions"": ""Press(pressPoint=0.1,behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""Release"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ef85f284-93a3-42a2-a65f-b5ec1024e875"",
                    ""path"": ""<Touchscreen>/press"",
                    ""interactions"": ""Press(pressPoint=0.1,behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""Release"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""41e2fb02-2f1f-408c-ab0e-1848dbcb6452"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""85a73c46-91a1-46c1-8d73-3720f1e5a4fb"",
                    ""path"": ""<Touchscreen>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Touch"",
            ""id"": ""3382b308-c5f4-438b-a93e-7d53e2e85ac5"",
            ""actions"": [
                {
                    ""name"": ""TouchInput"",
                    ""type"": ""PassThrough"",
                    ""id"": ""86fd0d27-365e-4bd2-bb5c-7a2fc6594ce1"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TouchPress"",
                    ""type"": ""Button"",
                    ""id"": ""0cff7094-2a7d-49e3-b572-4a9173594f0f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""TouchPosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""0bc07d4d-0cd1-457c-820b-bdab9d2604db"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3974c03a-6ebc-4d39-8a82-2d61b93cdc14"",
                    ""path"": ""<Touchscreen>/primaryTouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""TouchInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""68910aa1-501c-430f-90ad-658ac205225b"",
                    ""path"": ""<Touchscreen>/primaryTouch/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""TouchPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""16865da8-d73e-4f22-ac80-5fb37dbce70a"",
                    ""path"": ""<Touchscreen>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""TouchPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Mouse"",
            ""bindingGroup"": ""Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Touch"",
            ""bindingGroup"": ""Touch"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // Player
            m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
            m_Player_Click = m_Player.FindAction("Click", throwIfNotFound: true);
            m_Player_Hold = m_Player.FindAction("Hold", throwIfNotFound: true);
            m_Player_Release = m_Player.FindAction("Release", throwIfNotFound: true);
            m_Player_Point = m_Player.FindAction("Point", throwIfNotFound: true);
            // Touch
            m_Touch = asset.FindActionMap("Touch", throwIfNotFound: true);
            m_Touch_TouchInput = m_Touch.FindAction("TouchInput", throwIfNotFound: true);
            m_Touch_TouchPress = m_Touch.FindAction("TouchPress", throwIfNotFound: true);
            m_Touch_TouchPosition = m_Touch.FindAction("TouchPosition", throwIfNotFound: true);
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

        // Player
        private readonly InputActionMap m_Player;
        private IPlayerActions m_PlayerActionsCallbackInterface;
        private readonly InputAction m_Player_Click;
        private readonly InputAction m_Player_Hold;
        private readonly InputAction m_Player_Release;
        private readonly InputAction m_Player_Point;
        public struct PlayerActions
        {
            private @PlayerInputActions m_Wrapper;
            public PlayerActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @Click => m_Wrapper.m_Player_Click;
            public InputAction @Hold => m_Wrapper.m_Player_Hold;
            public InputAction @Release => m_Wrapper.m_Player_Release;
            public InputAction @Point => m_Wrapper.m_Player_Point;
            public InputActionMap Get() { return m_Wrapper.m_Player; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
            public void SetCallbacks(IPlayerActions instance)
            {
                if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
                {
                    @Click.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnClick;
                    @Click.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnClick;
                    @Click.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnClick;
                    @Hold.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHold;
                    @Hold.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHold;
                    @Hold.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHold;
                    @Release.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRelease;
                    @Release.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRelease;
                    @Release.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRelease;
                    @Point.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPoint;
                    @Point.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPoint;
                    @Point.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPoint;
                }
                m_Wrapper.m_PlayerActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Click.started += instance.OnClick;
                    @Click.performed += instance.OnClick;
                    @Click.canceled += instance.OnClick;
                    @Hold.started += instance.OnHold;
                    @Hold.performed += instance.OnHold;
                    @Hold.canceled += instance.OnHold;
                    @Release.started += instance.OnRelease;
                    @Release.performed += instance.OnRelease;
                    @Release.canceled += instance.OnRelease;
                    @Point.started += instance.OnPoint;
                    @Point.performed += instance.OnPoint;
                    @Point.canceled += instance.OnPoint;
                }
            }
        }
        public PlayerActions @Player => new PlayerActions(this);

        // Touch
        private readonly InputActionMap m_Touch;
        private ITouchActions m_TouchActionsCallbackInterface;
        private readonly InputAction m_Touch_TouchInput;
        private readonly InputAction m_Touch_TouchPress;
        private readonly InputAction m_Touch_TouchPosition;
        public struct TouchActions
        {
            private @PlayerInputActions m_Wrapper;
            public TouchActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @TouchInput => m_Wrapper.m_Touch_TouchInput;
            public InputAction @TouchPress => m_Wrapper.m_Touch_TouchPress;
            public InputAction @TouchPosition => m_Wrapper.m_Touch_TouchPosition;
            public InputActionMap Get() { return m_Wrapper.m_Touch; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(TouchActions set) { return set.Get(); }
            public void SetCallbacks(ITouchActions instance)
            {
                if (m_Wrapper.m_TouchActionsCallbackInterface != null)
                {
                    @TouchInput.started -= m_Wrapper.m_TouchActionsCallbackInterface.OnTouchInput;
                    @TouchInput.performed -= m_Wrapper.m_TouchActionsCallbackInterface.OnTouchInput;
                    @TouchInput.canceled -= m_Wrapper.m_TouchActionsCallbackInterface.OnTouchInput;
                    @TouchPress.started -= m_Wrapper.m_TouchActionsCallbackInterface.OnTouchPress;
                    @TouchPress.performed -= m_Wrapper.m_TouchActionsCallbackInterface.OnTouchPress;
                    @TouchPress.canceled -= m_Wrapper.m_TouchActionsCallbackInterface.OnTouchPress;
                    @TouchPosition.started -= m_Wrapper.m_TouchActionsCallbackInterface.OnTouchPosition;
                    @TouchPosition.performed -= m_Wrapper.m_TouchActionsCallbackInterface.OnTouchPosition;
                    @TouchPosition.canceled -= m_Wrapper.m_TouchActionsCallbackInterface.OnTouchPosition;
                }
                m_Wrapper.m_TouchActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @TouchInput.started += instance.OnTouchInput;
                    @TouchInput.performed += instance.OnTouchInput;
                    @TouchInput.canceled += instance.OnTouchInput;
                    @TouchPress.started += instance.OnTouchPress;
                    @TouchPress.performed += instance.OnTouchPress;
                    @TouchPress.canceled += instance.OnTouchPress;
                    @TouchPosition.started += instance.OnTouchPosition;
                    @TouchPosition.performed += instance.OnTouchPosition;
                    @TouchPosition.canceled += instance.OnTouchPosition;
                }
            }
        }
        public TouchActions @Touch => new TouchActions(this);
        private int m_MouseSchemeIndex = -1;
        public InputControlScheme MouseScheme
        {
            get
            {
                if (m_MouseSchemeIndex == -1) m_MouseSchemeIndex = asset.FindControlSchemeIndex("Mouse");
                return asset.controlSchemes[m_MouseSchemeIndex];
            }
        }
        private int m_TouchSchemeIndex = -1;
        public InputControlScheme TouchScheme
        {
            get
            {
                if (m_TouchSchemeIndex == -1) m_TouchSchemeIndex = asset.FindControlSchemeIndex("Touch");
                return asset.controlSchemes[m_TouchSchemeIndex];
            }
        }
        public interface IPlayerActions
        {
            void OnClick(InputAction.CallbackContext context);
            void OnHold(InputAction.CallbackContext context);
            void OnRelease(InputAction.CallbackContext context);
            void OnPoint(InputAction.CallbackContext context);
        }
        public interface ITouchActions
        {
            void OnTouchInput(InputAction.CallbackContext context);
            void OnTouchPress(InputAction.CallbackContext context);
            void OnTouchPosition(InputAction.CallbackContext context);
        }
    }
}
