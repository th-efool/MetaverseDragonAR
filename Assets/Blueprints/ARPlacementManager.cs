using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class ARPlacementManager : MonoBehaviour
{
    [Header("Ray Casting")]
    ARRaycastManager m_RaycastManager;
    List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();
    [SerializeField] public Camera AR_Camera;
    Vector3 centerOfScreen;
    Ray RayToCenter;
    IAARInteraction IAARInteraction;

    [Header("Dragon Placement")]
    bool DragonGotPlaced=false;
    [SerializeField] GameObject Dragon;

    private void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
        IAARInteraction = new IAARInteraction();
        IAARInteraction.ARPlacement.PlaceDragon.started += ctx => { DragonGotPlaced = true; };
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
        if (!DragonGotPlaced) { 
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
}

