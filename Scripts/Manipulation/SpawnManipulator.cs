using System.Collections.Generic;
using ArManipulations.Gestures;
using UnityEngine.XR.ARFoundation;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;

namespace ArManipulations.Manipulation
{
	/// <summary>
	/// Spawns an object in AR scene via a tap gesture.
	/// </summary>
	public class SpawnManipulator : Manipulator
	{
		[SerializeField]
		[Tooltip("A prefab to place when a raycast from a user touch hits a plane.")]
		private Camera firstPersonCamera;
		[SerializeField]
		[Tooltip("A prefab to place when a raycast from a user touch hits a plane.")]
		private GameObject pawnPrefab;
		[SerializeField]
		[Tooltip("Manipulator prefab to attach placed objects to.")]
		private GameObject manipulatorPrefab;

		private ARSessionOrigin sessionOrigin;
		private static List<ARRaycastHit> hits = new List<ARRaycastHit>();
		private ARRaycastManager raycastManager;
		private ARPlaneManager planeManager;
		private ARAnchorManager referencePointManager;

		void Awake()
		{
			sessionOrigin = GetComponent<ARSessionOrigin>();
			raycastManager = FindObjectOfType<ARRaycastManager>();
			planeManager = FindObjectOfType<ARPlaneManager>();
		}

		protected override bool CanStartManipulationForGesture(TapGesture gesture) => gesture.TargetObject == null;

		protected override void OnEndManipulation(TapGesture gesture)
		{
			if (gesture.WasCancelled)
				return;
			
			// If gesture is targeting an existing object we are done.
			if (gesture.TargetObject != null)
				return;

			if (raycastManager.Raycast(new Vector2(gesture.StartPosition.x, gesture.StartPosition.y),
				hits,
				TrackableType.PlaneWithinPolygon))
			{
				
				var hit = hits[0];
				var plane = planeManager.GetPlane(hit.trackableId);
				if (
					Vector3.Dot(firstPersonCamera.transform.position - hit.pose.position,
						hit.pose.rotation * Vector3.up) < 0)
				{
					Debug.Log("Hit at back of the current DetectedPlane");
				}
				else
				{
					var manipulator = Instantiate(manipulatorPrefab, hit.pose.position, hit.pose.rotation);
					var arObject = Instantiate(pawnPrefab, hit.pose.position, hit.pose.rotation);
					arObject.transform.parent = manipulator.transform;
					
					sessionOrigin.MakeContentAppearAt(manipulator.transform, hit.pose.position);
					var anchor = referencePointManager.AttachAnchor(planeManager.GetPlane(hit.trackableId), hit.pose);
					manipulator.transform.parent = anchor.transform;

					// Select the placed object for manipulations
					manipulator.GetComponent<Manipulator>().Select();
				}

			}
		}
	}
}
