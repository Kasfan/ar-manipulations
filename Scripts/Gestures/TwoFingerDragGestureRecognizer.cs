//-----------------------------------------------------------------------
// <copyright file="TwoFingerDragGestureRecognizer.cs" company="Google LLC">
//
// Copyright 2018 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------

using UnityEngine;

namespace ArManipulations.Gestures
{
    /// <summary>
    /// Gesture Recognizer for when the user performs a two finger drag motion on the touch screen.
    /// </summary>
    public class TwoFingerDragGestureRecognizer : GestureRecognizer<TwoFingerDragGesture>
    {
        internal const float _slopInches = 0.1f;
        internal const float _angleThresholdRadians = Mathf.PI / 6;

        /// <summary>
        /// Creates a two finger drag gesture with the given touches.
        /// </summary>
        /// <param name="touch1">The first touch that started this gesture.</param>
        /// <param name="touch2">The second touch that started this gesture.</param>
        /// <returns>The created Swipe gesture.</returns>
        internal TwoFingerDragGesture CreateGesture(Touch touch1, Touch touch2)
        {
            return new TwoFingerDragGesture(this, touch1, touch2);
        }

        /// <summary>
        /// Tries to create a two finger drag gesture.
        /// </summary>
        protected internal override void TryCreateGestures()
        {
            TryCreateTwoFingerGestureOnTouchBegan(CreateGesture);
        }
    }
}
