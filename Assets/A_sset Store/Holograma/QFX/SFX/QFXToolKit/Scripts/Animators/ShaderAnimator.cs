using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace QFXToolKit
{
    public class ShaderAnimator : ControlledObject
    {
        public DynamicShaderParameter[] DynamicShaderParameters;

        private float _startTime;
        private List<Material> _materials;

        public override void Setup()
        {
            base.Setup();

            _startTime = Time.time;
            _materials = new List<Material>();

            var rends = GetComponentsInChildren<Renderer>();
            foreach (var rend in rends)
                _materials.AddRange(rend.sharedMaterials);

            UpdateMaterials(0);
        }

        public override void Run()
        {
            base.Run();
            _startTime = Time.time;
        }

        private void Update()
        {
            if (!IsRunning)
                return;

            var time = Time.time - _startTime;

            UpdateMaterials(time);
        }

        private void UpdateMaterials(float time)
        {
            if (DynamicShaderParameters == null)
                return;

            foreach (var shaderParameter in DynamicShaderParameters)
            {
                if (shaderParameter.AnimationModule == null)
                    continue;

                var val = shaderParameter.AnimationModule.Evaluate(time);

                foreach (var material in _materials)
                    material.SetFloat(shaderParameter.ParameterName, val);
            }
        }
    }
}