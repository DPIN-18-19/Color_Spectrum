using System.Collections.Generic;
using QFXToolKit;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace QFX.SFX
{
    public class MotionCloner : ControlledObject
    {
        public ColorChangingController ColorPlayer;

        public GameObject TargetGameObject;
        

        public float CloneLifeTime = 1f;
        public float CloneRate = 1f;

        public bool ReplaceMaterial = true;
        public Material CloneMaterial;
        public Material CloneMaterialYellow;
        public Material CloneMaterialBlue;
        public Material CloneMaterialPink;

        public bool ReplaceMaterialInMotion;
        public Material MotionMaterial;

        public bool ShowCloneGameObject;

        public GameObject TrailParticleSystem;
        public GameObject ActivateCloneParticleSystem;
        public GameObject FinishMotionParticleSystem;

        public GameObject CloneParticleSystemBlue;
        public GameObject CloneParticleSystemYellow;
        public GameObject CloneParticleSystemPink;
        public MeshRenderer MeshRenderer;
        public SkinnedMeshRenderer SkinnedMeshRenderer;

        private GameObject _activateCloneGo;
        private ParticleSystem _activateClonePs;

        private GameObject _trailGo;
        private ParticleSystem _trailPs;

        private GameObject _finishGo;
        private ParticleSystem _finishPs;

        private readonly Dictionary<Renderer, Material[]> _rendererToSharedMaterials =
            new Dictionary<Renderer, Material[]>();

        private float _time;

        public override void Setup()
        {
            base.Setup();

            if (ReplaceMaterialInMotion)
            {
                var rends = TargetGameObject.GetComponentsInChildren<Renderer>();
                foreach (var rend in rends)
                    _rendererToSharedMaterials[rend] = rend.sharedMaterials;
            }

            if (ActivateCloneParticleSystem != null)
            {
                _activateCloneGo = Instantiate(ActivateCloneParticleSystem, Vector3.zero, Quaternion.identity);
                _activateClonePs = _activateCloneGo.GetComponent<ParticleSystem>();
                _activateCloneGo.SetActive(true);
                _activateCloneGo.transform.parent = TargetGameObject.transform;
                if (MeshRenderer != null)
                    ParticleSystemMeshAttacher.Attach(_activateClonePs, MeshRenderer, 0f);
                else if (SkinnedMeshRenderer != null)
                    ParticleSystemMeshAttacher.Attach(_activateClonePs, SkinnedMeshRenderer, 0f);
            }

            if (TrailParticleSystem != null)
            {
                _trailGo = Instantiate(TrailParticleSystem, Vector3.zero, Quaternion.identity);
                _trailPs = _trailGo.GetComponent<ParticleSystem>();
                _trailGo.SetActive(true);
                _trailGo.transform.parent = TargetGameObject.transform;
                if (MeshRenderer != null)
                    ParticleSystemMeshAttacher.Attach(_trailPs, MeshRenderer, 0f);
                else if (SkinnedMeshRenderer != null)
                    ParticleSystemMeshAttacher.Attach(_trailPs, SkinnedMeshRenderer, 0f);
            }

            if (FinishMotionParticleSystem != null)
            {
                _finishGo = Instantiate(FinishMotionParticleSystem, Vector3.zero, Quaternion.identity);
                _finishPs = _finishGo.GetComponent<ParticleSystem>();
                _finishGo.SetActive(true);
                _finishGo.transform.parent = TargetGameObject.transform;
                if (MeshRenderer != null)
                    ParticleSystemMeshAttacher.Attach(_finishPs, MeshRenderer, 0f);
                else if (SkinnedMeshRenderer != null)
                    ParticleSystemMeshAttacher.Attach(_finishPs, SkinnedMeshRenderer, 0f);
            }
        }

        public override void Run()
        {
            base.Run();
            Activate();
        }

        public override void Stop()
        {
            base.Stop();
            DeActivate();
        }

        public void MakeClone()
        {
            if (_activateCloneGo != null && _activateClonePs != null)
            {
                _activateCloneGo.transform.position = transform.position;
                _activateClonePs.Play();
            }

            var clone = Instantiate(TargetGameObject, TargetGameObject.transform.position,
                TargetGameObject.transform.rotation);

            foreach (var comp in clone.GetComponentsInChildren<Component>())
                if (!(comp is Renderer) && !(comp is Transform) && !(comp is MeshFilter))
                    Destroy(comp);

            var cloneDestroyer = clone.AddComponent<SelfDestroyer>();
            cloneDestroyer.LifeTime = CloneLifeTime;
            cloneDestroyer.Run();

            if (ReplaceMaterial)
            {
                if (ColorPlayer.GetColor() == 0)
                {
                    CloneMaterial = CloneMaterialYellow;
                }
                if (ColorPlayer.GetColor() == 1)
                {
                    CloneMaterial = CloneMaterialBlue;
                }
                if (ColorPlayer.GetColor() == 2)
                {
                    CloneMaterial = CloneMaterialPink;
                }
                var newMaterial = new Material(CloneMaterial);
                

                var rends = clone.GetComponentsInChildren<Renderer>();
                foreach (var rend in rends)
                {
                    var materialsLength = rend.materials.Length;
                    var materials = new Material[materialsLength];
                    for (int i = 0; i < materialsLength; i++)
                        materials[i] = newMaterial;
                    rend.materials = materials;
                }
            }

            if (!ShowCloneGameObject)
                clone.SetActive(false);
            if (ColorPlayer.GetColor() == 0)
            {
                if (CloneParticleSystemYellow != null)
               
                    {
                        var go = Instantiate(CloneParticleSystemYellow, TargetGameObject.transform.position,
                            TargetGameObject.transform.transform.rotation);
                        var ps = go.GetComponent<ParticleSystem>();

                        if (MeshRenderer != null)
                            ParticleSystemMeshAttacher.Attach(ps, MeshRenderer, 0f);
                        else if (SkinnedMeshRenderer != null)
                            ParticleSystemMeshAttacher.Attach(ps, SkinnedMeshRenderer, 0f);

                        ps.gameObject.SetActive(true);
                        ps.Play();
                    }
                }
            if (ColorPlayer.GetColor() == 1)
            {
                if (CloneParticleSystemBlue != null)

                {
                    var go = Instantiate(CloneParticleSystemBlue, TargetGameObject.transform.position,
                        TargetGameObject.transform.transform.rotation);
                    var ps = go.GetComponent<ParticleSystem>();

                    if (MeshRenderer != null)
                        ParticleSystemMeshAttacher.Attach(ps, MeshRenderer, 0f);
                    else if (SkinnedMeshRenderer != null)
                        ParticleSystemMeshAttacher.Attach(ps, SkinnedMeshRenderer, 0f);

                    ps.gameObject.SetActive(true);
                    ps.Play();
                }
            }
            if (ColorPlayer.GetColor() == 2)
            {
                if (CloneParticleSystemPink != null)

                {
                    var go = Instantiate(CloneParticleSystemPink, TargetGameObject.transform.position,
                        TargetGameObject.transform.transform.rotation);
                    var ps = go.GetComponent<ParticleSystem>();

                    if (MeshRenderer != null)
                        ParticleSystemMeshAttacher.Attach(ps, MeshRenderer, 0f);
                    else if (SkinnedMeshRenderer != null)
                        ParticleSystemMeshAttacher.Attach(ps, SkinnedMeshRenderer, 0f);

                    ps.gameObject.SetActive(true);
                    ps.Play();
                }
            }

            Destroy(clone, CloneLifeTime);
        }

        private void Activate()
        {
            _time = 0;

            if (ReplaceMaterialInMotion)
            {
               foreach (var originalMaterial in _rendererToSharedMaterials.Keys)
               {
                    var newMaterials = new Material[originalMaterial.sharedMaterials.Length];
                   for (int i = 0; i < newMaterials.Length; i++)
                       newMaterials[i] = MotionMaterial;
                    originalMaterial.sharedMaterials = newMaterials;
                }
            }

            if (_trailPs != null)
                _trailPs.Play();

            MakeClone();
        }

        private void DeActivate()
        {
            if (_trailPs != null)
                _trailPs.Stop();

           foreach (var originalMaterial in _rendererToSharedMaterials)
                originalMaterial.Key.sharedMaterials = originalMaterial.Value;

            if (_finishPs != null)
                _finishPs.Play();
        }

        private void Update()
        {
           // MeshRenderer = GameObject.FindGameObjectWithTag("Body").GetComponent<MeshRenderer>();
            if (!IsRunning)
                return;

            _time += Time.deltaTime;

            if (_time >= CloneRate)
            {
                MakeClone();
                _time = 0;
            }
        }

        private void OnDestroy()
        {
            Destroy(_activateCloneGo);
            Destroy(_trailGo);
            Destroy(_finishGo);
        }
    }
}