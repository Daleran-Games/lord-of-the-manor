using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace UnityEngine
{
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Return the component if it exsists and adds it if it does not.
        /// </summary>
        /// <typeparam name="T">The type of component to retrieve or add</typeparam>
        /// <param name="obj">Game object to be searched</param>
        /// <returns>A game component</returns>
        public static T GetOrAddComponent<T>(this GameObject obj) where T : Component
        {
            T component = obj.GetComponent<T>();

            if (component == null)
            {
                obj.AddComponent<T>();
                component = obj.GetComponent<T>();
            }

            return component;
        }

        /// <summary>
        /// Only return the component if it is a non-null type
        /// </summary>
        /// <typeparam name="T">The type of component to retrieve</typeparam>
        /// <param name="obj">Game object to be searched</param>
        /// <returns>A game component</returns>
        public static T GetRequiredComponent<T>(this GameObject obj) where T : Component
        {
            T component = obj.GetComponent<T>();

            if (component == null)
            {
                Debug.LogError("Expected to find component of type " + typeof(T) + " but found none", obj);
            }

            return component;
        }

        public static Transform ClearChildren (this Transform transform)
        {
            foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            return transform;
        }

        public static RectTransform ClearChildren (this RectTransform rectTransform)
        {
            foreach (RectTransform child in rectTransform)
            {
                GameObject.Destroy(child.gameObject);
            }
            return rectTransform;
        }

        public static Vector3 WorldToCanvasPoint (this Camera camera, RectTransform canvasRect ,Vector3 objectPosition)
        {
            Vector3 screenPos = camera.WorldToViewportPoint(objectPosition);
            screenPos.x *= canvasRect.rect.width;
            screenPos.y *= canvasRect.rect.height;
            return screenPos;
        }

        public static float GetMaxDimmensionFromSprite(this SpriteRenderer renderer)
        {
            Vector3 maxSpritePoint = renderer.bounds.extents;
            return MathExtensions.GetMaxAbsoluteDimmension(maxSpritePoint);

        }

        public static bool IsGameObjectUIObject(this GameObject gameObj)
        {
            RectTransform rectTrans = gameObj.GetComponent<RectTransform>();

            if (rectTrans != null)
                return true;
            else
                return false;
        }

        public static void SetRectTransformAnchorsAndPivot (this RectTransform rectTrans, Vector2 anchorMin, Vector2 anchorMax, Vector2 pivot)
        {
            rectTrans.anchorMin = anchorMin;
            rectTrans.anchorMax = anchorMax;
            rectTrans.pivot = pivot;
        }

        public static Vector3 VectorToPoint(this Transform trans, Vector3 point)
        {
            return point - trans.position;
        }

        public static float DistanceToPoint(this Transform trans, Vector3 point)
        {
            return trans.VectorToPoint(point).magnitude;
        }

    }
}

