using UnityEngine;
using UnityEngine.UI;

namespace Hourai.SmashBrew.UI {

    public class CharacterLayoutGroup : LayoutGroup, ILayoutSelfController {

        [SerializeField]
        private float _selfAspectRatio;

        [SerializeField]
        private float _childAspectRatio;

        public override void CalculateLayoutInputHorizontal() {
            base.CalculateLayoutInputHorizontal();
            SetLayoutInputForAxis(padding.horizontal, padding.horizontal, -1, 0);
        }

        public override void CalculateLayoutInputVertical() {
            SetLayoutInputForAxis(padding.vertical, padding.vertical, -1, 1);
        }

        public override void SetLayoutHorizontal() {
            SetLayout(true);
        }

        public override void SetLayoutVertical() {
            SetLayout(false);
        }

        private void SetLayout(bool axis) {
            // Givens
            // Child Aspect Ratio
            Vector2 availableSpace = rectTransform.rect.size;
            int count = rectChildren.Count;

            if (availableSpace.x / availableSpace.y > _selfAspectRatio) {
                availableSpace.x = availableSpace.y*_selfAspectRatio;
            }

            // Calculated
            int bestRows = 1;
            int bestCols = 1;
            float itemWidth = 0;
            float itemHeight = 0;
            bool isPrime = count <= 3;
            int effectiveCount = count;
            float maxArea = float.MaxValue;

            // Prime numbers tend to generate very poorly made layouts.
            // Repeat until the the layout is generated with a non-prime count
            // Shouldn't need to run more than twice. 
            do {
                for (var rows = 1; rows <= effectiveCount; rows++) {
                    // Reject any "non-rectangle" layouts.
                    if (effectiveCount % rows != 0)
                        continue;
                    isPrime |= rows != 1 && rows != effectiveCount;
                    int cols = effectiveCount / rows;
                    var width = availableSpace.x / cols;
                    var height = availableSpace.y / rows;
                    float area = Mathf.Abs(rows * width - cols * height * _childAspectRatio);
                    if (area >= maxArea)
                        continue;
                    maxArea = area;
                    bestRows = rows;
                    bestCols = cols;
                    itemWidth = width;
                    itemHeight = height;
                }
                effectiveCount++;
            } while (!isPrime);

            // Only set the sizes when invoked for horizontal axis, not the positions.

            if (axis) {
                for (int i = 0; i < rectChildren.Count; i++) {
                    RectTransform rect = rectChildren[i];

                    m_Tracker.Add(this, rect,
                        DrivenTransformProperties.Anchors |
                        DrivenTransformProperties.AnchoredPosition |
                        DrivenTransformProperties.SizeDelta);

                    rect.anchorMin = Vector2.up;
                    rect.anchorMax = Vector2.up;
                    rect.sizeDelta = new Vector2(itemWidth, itemHeight);
                }
                return;
            }

            Vector2 center = rectTransform.rect.size / 2;
            Vector2 extents = 0.5f * new Vector2(bestCols * itemWidth, bestRows * itemHeight);
            Vector2 start = center - extents;

            for(var i = 0; i < bestRows; i++) {
                float x = start.x;
                float y = start.y + i * itemHeight;
                if(count - i * bestCols < bestCols)
                    x = center.x - 0.5f * (count % bestCols * itemWidth);

                for (var j = 0; j < bestCols; j++) {
                    int index = bestCols * i + j;
                    if (index >= count)
                        break;

                    SetChildAlongAxis(rectChildren[index], 0, x + j * itemWidth, itemWidth);
                    SetChildAlongAxis(rectChildren[index], 1, y, itemHeight);
                }
            }
        }
    }
}