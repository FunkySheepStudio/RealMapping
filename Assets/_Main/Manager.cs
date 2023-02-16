using Assets.WasapiAudio.Scripts.Unity;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

namespace RealMapper
{
    public class Manager : FunkySheep.Types.Singleton<Manager>
    {
        [SerializeField]
        GameObject BlenderScenePrefab;
        public GameObject scene;
        public List<Effects.ShaderFx> shaderFxs;
        public List<Effects.VfxGraphFx> vfxGraphFxs;

        private void Start()
        {
            scene = GameObject.Instantiate(BlenderScenePrefab);
            SetCameras();
            InitFxs();
            SetFxs();
        }

        private void SetCameras()
        {
            Camera camera = scene.GetComponentInChildren<Camera>();
            camera.GetComponent<HDAdditionalCameraData>().clearColorMode = HDAdditionalCameraData.ClearColorMode.Color;
            camera.GetComponent<HDAdditionalCameraData>().backgroundColorHDR = Color.black;
        }

        private void InitFxs()
        {
            MeshRenderer[] meshRenderers = scene.GetComponentsInChildren<MeshRenderer>();

            for (int i = 0; i < meshRenderers.Length; i++)
            {
                meshRenderers[i].gameObject.AddComponent<VisualEffect>();
                VFXPropertyBinder binder = meshRenderers[i].gameObject.AddComponent<VFXPropertyBinder>();
                VfxAudioSpectrumDataBinder spectrumData = binder.AddPropertyBinder<VfxAudioSpectrumDataBinder>();
                spectrumData.WasapiAudioSource = GetComponent<WasapiAudioSource>();
            }
        }

        private void SetFxs()
        {
            MeshRenderer[] meshRenderers = scene.GetComponentsInChildren<MeshRenderer>();

            for (int i = 0; i < meshRenderers.Length; i++)
            {
                meshRenderers[i].material = shaderFxs[Random.Range(0, shaderFxs.Count)].material;
                meshRenderers[i].gameObject.GetComponent<VisualEffect>().visualEffectAsset = vfxGraphFxs[Random.Range(0, vfxGraphFxs.Count)].vfx;
                meshRenderers[i].gameObject.GetComponent<VisualEffect>().SetMesh("Mesh", meshRenderers[i].GetComponent<MeshFilter>().mesh);
            }
        }
    }
}
