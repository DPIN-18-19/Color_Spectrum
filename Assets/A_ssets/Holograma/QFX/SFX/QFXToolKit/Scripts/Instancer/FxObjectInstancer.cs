using UnityEngine;

// ReSharper disable once CheckNamespace
namespace QFXToolKit
{
    public static class FxObjectInstancer
    {
        public static void InstantiateFx(FxObject fxObject, Vector3 targetPosition, Vector3 targetRotation)
        {
            var go = Object.Instantiate(fxObject.Fx);
            go.transform.position = targetPosition;

            if (fxObject.FxRotation == FxRotationType.Normal)
            {
                go.transform.rotation = Quaternion.FromToRotation(go.transform.up, targetRotation) *
                                        go.transform.rotation;
            }
            else if (fxObject.FxRotation == FxRotationType.Default)
            {
                go.transform.rotation = Quaternion.identity;
            }
            else if (fxObject.FxRotation == FxRotationType.LookAtEmitter)
            {
                go.transform.LookAt(targetRotation);
            }

            go.SetActive(true);
        }
    }
}