using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Utils.Extended
{
    public static class GraphicFixer
    {
        public static Dispatcher Dispatcher => Dispatcher.Instance();
        
        public static void RebuildGraphic(this GameObject go, bool awaitFrame = true)
        {
            if (go.TryGetComponent(out RectTransform rt)) 
                RebuildGraphic(rt, awaitFrame);
        }

        public static void RebuildGraphic(this Behaviour behaviour, bool awaitFrame = true)
        {
            if (behaviour.TryGetComponent(out RectTransform rt))
                RebuildGraphic(rt, awaitFrame);
        }

        public static void RebuildGraphic(this Transform transform, bool awaitFrame = true)
        {
            RebuildGraphic(transform.gameObject, awaitFrame);
        }
         
        public static void RebuildGraphic(this RectTransform transform, bool awaitFrame = true)
        {
            if (awaitFrame)
            {
                RunActionAfterFrame(() => LayoutRebuilder.MarkLayoutForRebuild(transform));
            }
            else
            {
                LayoutRebuilder.MarkLayoutForRebuild(transform);
            }
        }
        
        
        public static void RebuildGraphic(this Transform[] transforms)
        {
            RunActionAfterFrame(() =>
            {
                foreach (var transform in transforms)
                {
                    RebuildGraphic(transform, false);
                }
            });
        }
        
        public static void RebuildGraphic(this RectTransform[] transforms)
        {
            RunActionAfterFrame(() =>
            {
                foreach (var transform in transforms)
                {
                    RebuildGraphic(transform, false);
                }
            });
        }

        public static void RebuildGraphic(this GameObject[] gameObjects)
        {
            RunActionAfterFrame(() =>
            {
                foreach (var go in gameObjects)
                {
                    RebuildGraphic(go, false);
                }
            });
        }

        private static void RunActionAfterFrame(Action action) => 
            Dispatcher.StartCoroutine(RebuildAwait(action));


        private static IEnumerator RebuildAwait(Action action)
        {
            yield return null;
            action?.Invoke();
        }
    }
}