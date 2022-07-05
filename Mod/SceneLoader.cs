using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using UObject = UnityEngine.Object;
using USceneManager = UnityEngine.SceneManagement.SceneManager;

namespace AspidQueen
{
    internal class SceneLoader : MonoBehaviour
    {
        internal static BossSceneController SceneController;

        private void Awake()
        {
            On.GameManager.EnterHero += OnEnterHero;
            USceneManager.activeSceneChanged += OnSceneChange;
        }

        private void OnEnterHero(On.GameManager.orig_EnterHero orig, GameManager gm, bool additiveGateSearch)
        {
            if (gm.sceneName == "GG_Aspid_Queen")
            {
                GameObject.Find("Aspid Queen").AddComponent<Aspid>();
            }

            orig(gm, additiveGateSearch);

            if (gm.sceneName == "GG_Aspid_Queen")
            {
                HeroController.instance.transform.SetPosition2D(185.4f, 131.5f);
            }
        }

        private void OnSceneChange(Scene prevScene, Scene nextScene)
        {
            if (nextScene.name == "GG_Aspid_Queen")
            {
                var bsc = Instantiate(AspidQueen.Instance.GameObjects["Boss Scene Controller"]);
                bsc.SetActive(true);
                SceneController = bsc.GetComponent<BossSceneController>();
                StatueCreator.BossLevel = SceneController.BossLevel;

                var godseeker = Instantiate(AspidQueen.Instance.GameObjects["Godseeker"], new Vector3(189, 139, 28.39f), Quaternion.identity);
                godseeker.SetActive(true);
                godseeker.transform.localScale = Vector3.one * 1.5f;

                var rootGOs = nextScene.GetRootGameObjects();
                foreach (var go in rootGOs)
                {
                    foreach (var sprRend in go.GetComponentsInChildren<SpriteRenderer>(true))
                    {
                        sprRend.material.shader = Shader.Find("Sprites/Default");
                    }

                    foreach (var meshRend in go.GetComponentsInChildren<MeshRenderer>(true))
                    {
                        meshRend.material.shader = Shader.Find(meshRend.GetComponent<BlurPlane>() ? "UI/Blur/UIBlur" : "Sprites/Default-ColorFlash");
                    }

                    foreach (var tileRend in UObject.FindObjectsOfType<TilemapRenderer>(true))
                    {
                        tileRend.material.shader = Shader.Find("Sprites/Default");
                    }
                }
            }
        }

        private void OnDestroy()
        {
            On.GameManager.EnterHero -= OnEnterHero;
        }
    }
}