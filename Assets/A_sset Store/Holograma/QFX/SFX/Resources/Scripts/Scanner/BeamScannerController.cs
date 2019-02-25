using System.Linq;
using QFXToolKit;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace QFX.SFX
{
    public class BeamScannerController : MonoBehaviour
    {
        public GameObject Scanner;
        public AnimationModule AppearAnimation;

        public bool IsDetectionEnabled;
        public float DetectionRadius;
        public Transform DetectionAnchor;

        public bool OverrideColor;

        [ColorUsageAttribute(true, true, 0f, 8f, 0.125f, 3f)]
        public Color NormalColor;

        [ColorUsageAttribute(true, true, 0f, 8f, 0.125f, 3f)]
        public Color DetectionColor;

        private Material _scannerMaterial;

        private float _startTime;
        private bool _canAnimate;

        private bool _isEnabled;

        private bool _wasDetected;

        public void ActivateScanner()
        {
            Scanner.SetActive(true);
            _startTime = Time.time;
            _canAnimate = true;
        }

        private void Start()
        {
            _scannerMaterial = Scanner.GetComponent<Renderer>().material;

            var eval = AppearAnimation.Evaluate(0);

            if (_scannerMaterial != null)
                _scannerMaterial.SetFloat("_AppearProgress", eval);
        }

        private void OnEnable()
        {
            _isEnabled = true;
            _startTime = Time.time;
            _canAnimate = true;

            if (OverrideColor && _scannerMaterial != null)
            {
                _scannerMaterial.SetColor("_TintColor", NormalColor);
                _scannerMaterial.SetColor("_DepthColor", NormalColor);
            }
        }

        private void OnDisable()
        {
            _isEnabled = false;
        }

        private void Update()
        {
            if (!_isEnabled)
                return;

            if (IsDetectionEnabled)
            {
                var colliders = Physics.OverlapBox(DetectionAnchor.position,
                    new Vector3(DetectionRadius, DetectionRadius, DetectionRadius));

                bool isObjectInRadius = false;

                foreach (var coll in colliders)
                {
                    var beamScannable = coll.GetComponentsInChildren<BeamScannable>();
                    if (beamScannable.Any())
                    {
                        isObjectInRadius = true;
                        break;
                    }
                }

                if (isObjectInRadius && !_wasDetected)
                {
                    _wasDetected = true;
                }
                else if (!isObjectInRadius && _wasDetected)
                {
                    _wasDetected = false;
                }

                if (OverrideColor && _scannerMaterial != null)
                {
                    _scannerMaterial.SetColor("_TintColor", _wasDetected ? DetectionColor : NormalColor);
                    _scannerMaterial.SetColor("_DepthColor", _wasDetected ? DetectionColor : NormalColor);
                }
            }

            if (!_canAnimate)
                return;

            var time = Time.time - _startTime;
            var appearValue = AppearAnimation.Evaluate(time);
            if (_scannerMaterial != null)
                _scannerMaterial.SetFloat("_AppearProgress", appearValue);
        }
    }
}