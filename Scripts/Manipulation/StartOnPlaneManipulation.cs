using System;
using System.Collections.Generic;
using ArManipulations.Gestures;
using UnityEngine.XR.ARFoundation;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;

namespace ArManipulations.Manipulation
{
	/// <summary>
	/// Uses a ray to target the object on a plane and place it on the plane via a tap gesture.
	/// </summary>
	[RequireComponent(typeof(SelectionManipulator))]
	public class StartOnPlaneManipulation : Manipulator
	{
		[SerializeField]
		private Camera firstPersonCamera;
		[SerializeField]
		private GameObject placingHint;
		[SerializeField]
		private GameObject manipulatorContent;

		public bool IsPlaced { get; protected set; } = false;
		public event Action<bool> placedStateChanged;
		
		private List<ARRaycastHit> hits = new List<ARRaycastHit>();
		private ARRaycastManager raycastManager;
		private ARPlaneManager planeManager;
		private ARAnchorManager referencePointManager;
		private ARRaycastHit lastHit;
		private Transform myTransform;
		
		private void Awake()
		{
			myTransform = transform;
			raycastManager = FindObjectOfType<ARRaycastManager>();
			planeManager = FindObjectOfType<ARPlaneManager>();
			referencePointManager = FindObjectOfType<ARAnchorManager>();
			
			if(placingHint != null)
				placingHint.SetActive(true);
		}
		
		protected override void Update()
		{
			if(!IsPlaced)
			{
				base.Update();
				if (raycastManager.Raycast(new Vector2(Screen.width/2f, Screen.height/2f),
					hits,
					TrackableType.PlaneWithinPolygon))
				{
					var hit = hits[0];
					if (Vector3.Dot(firstPersonCamera.transform.position - hit.pose.position,
							hit.pose.rotation * Vector3.up) < 0)
					{
						Debug.Log("Hit at back of the current DetectedPlane");
					}
					else
					{
						
						myTransform.position = hit.pose.position;
						myTransform.rotation = hit.pose.rotation;

						lastHit = hit;
						
						if(!manipulatorContent.activeSelf)
							manipulatorContent.SetActive(true);
					}

				}
			}
		}
		
		protected override bool CanStartManipulationForGesture(TapGesture gesture) => !IsPlaced;

		protected override void OnEndManipulation(TapGesture gesture)
		{
			if (gesture.WasCancelled || IsPlaced)
				return;
			
			if(placingHint != null)
				placingHint.SetActive(false);
			
			var anchor = referencePointManager.AttachAnchor(planeManager.GetPlane(lastHit.trackableId), lastHit.pose);
			myTransform.parent = anchor.transform;
			myTransform.localPosition = Vector3.zero;
			myTransform.localRotation = Quaternion.Euler(Vector3.zero);
			Select(); //select current object for manipulations
			
			IsPlaced = true;
			placedStateChanged?.Invoke(true);
		}
	}
}
