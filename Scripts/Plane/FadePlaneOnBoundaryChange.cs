using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace ArManipulations.Planes
{
    [RequireComponent(typeof(ARPlane))]
    [RequireComponent(typeof(Animator))]
    public class FadePlaneOnBoundaryChange : MonoBehaviour
    {
        private const string FADE_OFF_ANIM = "FadeOff";
        private const string FADE_ON_ANIM = "FadeOn";
        private const float TIMEOUT = 2.0f;

        private Animator animator;
        private ARPlane plane;
        private float showTime = 0;
        private bool updatingPlane = false;

        private void OnEnable()
        {
            plane = GetComponent<ARPlane>();
            animator = GetComponent<Animator>();

            plane.boundaryChanged += PlaneOnBoundaryChanged;
        }

        private void OnDisable()
        {
            plane.boundaryChanged -= PlaneOnBoundaryChanged;
        }

        private void Update()
        {
            if (updatingPlane)
            {
                showTime -= Time.deltaTime;

                if (showTime <= 0)
                {
                    updatingPlane = false;
                    animator.SetBool(FADE_OFF_ANIM, true);
                    animator.SetBool(FADE_ON_ANIM, false);
                }
            }
        }

        private void PlaneOnBoundaryChanged(ARPlaneBoundaryChangedEventArgs obj)
        {
            animator.SetBool(FADE_OFF_ANIM, false);
            animator.SetBool(FADE_ON_ANIM, true);
            updatingPlane = true;
            showTime = TIMEOUT;
        }
    }
}