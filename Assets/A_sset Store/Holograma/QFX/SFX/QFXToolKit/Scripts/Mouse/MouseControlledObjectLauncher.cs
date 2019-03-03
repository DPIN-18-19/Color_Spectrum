using UnityEngine;
using QFXToolKit;
// ReSharper disable once CheckNamespace

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
