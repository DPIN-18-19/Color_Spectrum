using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace QFXToolKit
{
    [RequireComponent(typeof(MaterialAdder))]
    public class TargetMarker : ControlledObject
    {
        public float LifeTime;

        public FxObject MarkFx;

        public MarkMode MarkTargetMode;

        public int MaxTargets;

        private readonly Dictionary<int, GameObject> _markedObjects = new Dictionary<int, GameObject>();
        private readonly List<Vector3> _markedPositions = new List<Vector3>();

        private MaterialAdder _materialAdder;

        public List<GameObject> MarkedGameObjects
        {
            get { return _markedObjects.Values.ToList(); }
        }

        public List<Vector3> MarkedPositions
        {
            get { return new List<Vector3>(_markedPositions); }
        }

        private void Awake()
        {
            _materialAdder = GetComponent<MaterialAdder>();
        }

        public override void Run()
        {
            base.Run();

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (!Physics.Raycast(ray, out hit))
                return;

            if (MarkFx != null)
                FxObjectInstancer.InstantiateFx(MarkFx, hit.point, hit.normal);

            if (MarkTargetMode == MarkMode.GameObject)
            {
                if (_markedObjects.Count >= MaxTargets)
                    return;

                var gameObjectId = hit.transform.gameObject.GetInstanceID();
                if (_markedObjects.ContainsKey(gameObjectId))
                    return;

                _markedObjects[gameObjectId] = hit.transform.gameObject;
                var materialAdded = hit.transform.gameObject.AddComponent<MaterialAdder>();
                materialAdded.Material = _materialAdder.Material;
                materialAdded.LifeTime = _materialAdder.LifeTime;

                InvokeUtil.RunLater(this, () =>
                {
                    materialAdded.Revert();
                    Destroy(materialAdded);
                    _markedObjects.Remove(gameObjectId);
                }, LifeTime);
            }
            else
            {
                if (_markedPositions.Count >= MaxTargets)
                    return;
                _markedPositions.Add(hit.point);
                InvokeUtil.RunLater(this, () => { _markedPositions.Remove(hit.point); }, LifeTime);
            }
        }

        public enum MarkMode
        {
            GameObject = 0,
            Position = 1
        }
    }
}