using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System;

public class ARPlacementManager : MonoBehaviour
{
    [Header("Ray Casting")]
    [SerializeField] public Camera AR_Camera;
    ARRaycastManager m_RaycastManager;
    List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();
    Vector3 centerOfScreen;
    Ray RayToCenter;
    IAARInteraction IAARInteraction;

    [Header("Dragon Placement")]
    [SerializeField] private GameObject[] dragonPrefabs; // Array to hold dragon prefabs
    bool DragonAligned =false;
    bool DragonInstantiated = false;
    GameObject Dragon;

    [Header("Test Variables Shit")]
    [SerializeField] GameObject SpawnTransform;

    private void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
        IAARInteraction = new IAARInteraction();
        IAARInteraction.ARPlacement.PlaceDragon.started += ctx => InstantiateDragon(ctx,(int)PlayerData.Instance.DragonChoice);
        IAARInteraction.ARPlacement.AlignmentDone.started += FinalizeAlignment;
        IAARInteraction.ARPlacement.AutoSpawnAlign.started += ctx => Debug.Log("Meow Meow YAYYY!!~");
        IAARInteraction.ARPlacement.AutoSpawnAlign.started += ctx => DebugSpawnAlign(ctx, (int)PlayerData.Instance.DragonChoice);


    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        centerOfScreen = new Vector3(Screen.width / 2, Screen.height / 2, 0);

    }

    // Update is called once per frame
    void Update()
    {
        RayToCenter = AR_Camera.ScreenPointToRay(centerOfScreen);
         if (!DragonAligned && DragonInstantiated) { 
            KeepDragonCenter();
        } 


    }
    
        void KeepDragonCenter()
        {
            if (m_RaycastManager.Raycast(RayToCenter, m_Hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = m_Hits[0].pose;
                Dragon.transform.position = hitPose.position;   

            }

        }


        void InstantiateDragon(InputAction.CallbackContext ctx,int index)
        {
                if (m_RaycastManager.Raycast(RayToCenter, m_Hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = m_Hits[0].pose;
                    Dragon = Instantiate(dragonPrefabs[index], hitPose.position, Quaternion.identity);
                    DragonInstantiated = true;
                    DragonUI.Instance.DragonPlacementStage(1);
                }

        }
  
    void FinalizeAlignment(InputAction.CallbackContext ctx)
    {
        DragonAligned = true;
        DragonUI.Instance.DragonPlacementStage(2);

    }

    void DebugSpawnAlign(InputAction.CallbackContext ctx, int index)
    {
        Debug.Log("I was called from ARPlacementManger - DebugSpawnAligh");
        PlayerData.Instance.ChangeMaterialBasedOnChoice(dragonPrefabs);
        Dragon = Instantiate(dragonPrefabs[(int)PlayerData.Instance.DragonChoice], SpawnTransform.transform.position, Quaternion.identity);
        DragonInstantiated = true;
        DragonAligned = true;
        DragonUI.Instance.DragonPlacementStage(2);
    }

    private void OnEnable()
    {
        IAARInteraction.Enable();
    }

    private void OnDisable()
    {
        IAARInteraction.Disable();
    }

}

