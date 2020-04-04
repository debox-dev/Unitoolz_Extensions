using System;
using UnityEngine;

namespace Unitoolz.Extensions
{

    public static class TransformExtensions 
    {
        /// <summary>
        /// Reset local position
        /// </summary>
        public static void ResetPosition(this Transform self)
        {
            self.localPosition = Vector3.zero;
        }

        /// <summary>
        /// Resets the local rotation to Quaternion.identity
        /// </summary>
        public static void ResetRotation(this Transform self)
        {
            self.localRotation = Quaternion.identity;
        }

        /// <summary>
        /// Resets the localScale to Vector3.one
        /// </summary>
        public static void ResetScale(this Transform self)
        {
            self.localScale = Vector3.one;
        }


        public static Bounds CalculateBoundsRecursive(this Transform rootParent)
        {
            var meshRenderers =  rootParent.GetComponentsInChildren<Renderer>();
            if (meshRenderers.Length == 0)
            {
                throw new InvalidOperationException("Cannot calculate bounds center for something that doesnt contain at least one mesh");
            }
            var totalBounds = new Bounds(rootParent.position, Vector3.zero);  
            for (int i = 0; i < meshRenderers.Length; i++)
            {
                var meshFilter = meshRenderers[i];
                totalBounds.Encapsulate(meshFilter.bounds);
            }
            return totalBounds;
        }

        /// <summary>
        /// Calculates the average mesh positions out of all the meshes parented by this transform
        /// </summary>
        /// <returns>The average meshes center.</returns>
        public static Vector3 CalculateAverageBoundsCenter(this Transform rootParent, Space space)
        {
            var totalBounds = CalculateBoundsRecursive(rootParent);
            var worldPosition = totalBounds.center;
            if (space == Space.Self)
            {
                return rootParent.InverseTransformPoint(worldPosition);
            }
            return worldPosition;
        }
    }
}