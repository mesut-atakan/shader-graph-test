using Aventra.Nugget.Common.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Aventra.Game
{
    public sealed class CrossairCanvas : BaseMenu
    {
        [SerializeField] private CharacterInteractController characterInteractController;
        [SerializeField] private CharacterController characterController;
        
        [Header("Crossair Settings")]
        [SerializeField] private Image imgCrossair;
        [SerializeField] private Color defaultCrossairColor = Color.white;
        [SerializeField] private Color lookInteractCrossairColor = Color.red;
        [SerializeField] private Color holdCrossairColor = Color.green;
        
        [Header("Feel")]
        [SerializeField] private float crossairMoveSpeed = 0.5f;
        [SerializeField] private float maxSwayDistance = 10f;
        [SerializeField] private float swayFrequency = 9f;
        [SerializeField] private float swayReturnSpeed = 12f;
        [SerializeField] private float maxVelocityForFullSway = 7f;

        private RectTransform _crossairRectTransform;
        private Vector2 _defaultAnchoredPosition;
        private Vector2 _currentOffset;
        private float _swayTimer;

        void Start()
        {
            if (imgCrossair != null)
            {
                _crossairRectTransform = imgCrossair.rectTransform;
                _defaultAnchoredPosition = _crossairRectTransform.anchoredPosition;
            }
        }

        void Update()
        {
            UpdateCrossairColor();
            UpdateCrossairSway();
        }

        private void UpdateCrossairColor()
        {
            if (imgCrossair == null)
                return;

            if (characterInteractController != null && characterInteractController.HoldingHoldable != null)
            {
                imgCrossair.color = holdCrossairColor;
                return;
            }

            if (characterInteractController != null && characterInteractController.LookingHoldable != null)
            {
                imgCrossair.color = lookInteractCrossairColor;
                return;
            }

            imgCrossair.color = defaultCrossairColor;
        }

        private void UpdateCrossairSway()
        {
            if (_crossairRectTransform == null)
                return;

            float currentVelocity = characterController != null ? characterController.CurrentVelocity : 0f;
            float normalizedVelocity = Mathf.Clamp01(currentVelocity / Mathf.Max(0.01f, maxVelocityForFullSway));

            Vector2 targetOffset = Vector2.zero;
            if (normalizedVelocity > 0f)
            {
                _swayTimer += Time.deltaTime * swayFrequency * Mathf.Max(0.01f, crossairMoveSpeed);

                float horizontal = Mathf.Sin(_swayTimer);
                float vertical = Mathf.Cos(_swayTimer * 2f) * 0.5f;
                targetOffset = new Vector2(horizontal, vertical) * (maxSwayDistance * normalizedVelocity);
            }

            _currentOffset = Vector2.Lerp(_currentOffset, targetOffset, Time.deltaTime * swayReturnSpeed);
            _crossairRectTransform.anchoredPosition = _defaultAnchoredPosition + _currentOffset;
        }
    } 
}