using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace QFX.SFX
{
    public static class ObjectAreaFinder
    {
        public static List<T> FindObjects<T>(Vector3 position, float areaRadius, LayerMask layerMask)
        {
            var colliders = Physics.OverlapSphere(position, areaRadius, layerMask);
            var objectsOfType = colliders.Where(c => c.GetComponent<T>() != null)
                .Select(c => c.GetComponent<T>());
            return objectsOfType.ToList();
        }
    }
}