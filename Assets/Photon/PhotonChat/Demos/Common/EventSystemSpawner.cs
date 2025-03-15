using UnityEngine;
using UnityEngine.EventSystems;

namespace Photon.Chat.UtilityScripts
{
    /// <summary>
    /// Event system spawner. Will add an EventSystem GameObject with an EventSystem component and a StandaloneInputModule component.
    /// Use this in additive scene loading context where you would otherwise get a "Multiple EventSystem in scene... this is not supported" error from Unity.
    /// </summary>
    public class EventSystemSpawner : MonoBehaviour
    {
        void OnEnable()
        {
            #if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
                Debug.LogError("PUN Demos are not compatible with the New Input System, unless you enable \"Both\" in: Edit > Project Settings > Player > Active Input Handling. Pausing App.");
                Debug.Break();
                return;
            #else
                #if UNITY_6000_0_OR_NEWER
                    EventSystem sceneEventSystem = FindFirstObjectByType<EventSystem>();
                #else
                    EventSystem sceneEventSystem = FindObjectOfType<EventSystem>();
                #endif

                if (sceneEventSystem == null)
                {
                    GameObject eventSystem = new GameObject("EventSystem");
                    eventSystem.AddComponent<EventSystem>();
                    eventSystem.AddComponent<StandaloneInputModule>();
                }
            #endif
        }
    }
}
