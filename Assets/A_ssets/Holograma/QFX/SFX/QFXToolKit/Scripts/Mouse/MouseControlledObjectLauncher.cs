using UnityEngine;

// ReSharper disable once CheckNamespace
namespace QFXToolKit
{
    public class MouseControlledObjectLauncher : MonoBehaviour
    {
        public ControlledObject ControlledObject;

        public int MouseButtonCode;

        private void OnEnable()
        {
            ControlledObject.Setup();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(MouseButtonCode))
            {
                ControlledObject.Run();
            }
            else if (Input.GetMouseButtonUp(MouseButtonCode))
            {
                ControlledObject.Stop();
            }
        }
    }
}