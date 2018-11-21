using UnityEngine;

// ReSharper disable once CheckNamespace
namespace QFXToolKit
{
    public class ShaderParameterSetter : ControlledObject
    {
        public string ParameterName;
        public float ParameterValue;

        public override void Run()
        {
            base.Run();

            var rends = GetComponentsInChildren<Renderer>();
            foreach (var rend in rends)
            {
                foreach (var sharedMaterial in rend.sharedMaterials)
                {
                    sharedMaterial.SetFloat(ParameterName, ParameterValue);
                }
            }
        }
    }
}