using Modding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Vasi;
using UObject = UnityEngine.Object;
namespace AspidQueen
{
    internal class AspidQueen : Mod, ILocalSettings<LocalSettings>
    {
        internal static AspidQueen Instance { get; private set; }

        public Dictionary<string, AssetBundle> Bundles = new();
        public Dictionary<string, GameObject> GameObjects { get; private set; } = new();
        public Dictionary<string, Texture2D> Textures { get; private set; } = new();

        private LocalSettings _localSettings = new();
        public LocalSettings LocalSettings => _localSettings;

        private Dictionary<string, (string, string)> _preloads = new()
        {
            ["Boss Scene Controller"] = ("GG_Gruz_Mother", "Boss Scene Controller"),
            ["Godseeker"] = ("GG_Gruz_Mother", "GG_Arena_Prefab/Godseeker Crowd"),
            ["Primal Aspid"] = ("sharedassets495", "Super Spitter R"),
            ["Aspid Shot"] = ("sharedassets32", "Spitter Shot R"),
            ["Vomit Glob"] = ("sharedassets290", "Vomit Glob Nosk"),
            ["Roar Wave"] = ("sharedassets13", "Roar Wave Emitter"),
            ["Explosion"] = ("sharedassets156", "Gas Explosion Uumuu"),
            ["Corpse"] = ("sharedassets40", "Corpse Big Fly 1"),
            ["Death Puff"] = ("sharedassets32", "Death Puff Boss"),
            ["Death Explode"] = ("sharedassets32", "Death Explode Boss"),
        };

        private Material _blurMat;

        public AspidQueen() : base("Aspid Queen") { }

        public override List<(string, string)> GetPreloadNames()
        {
            return _preloads.Values.ToList();
        }

        public override string GetVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
        {
            Instance = this;

            Unload();

            _blurMat = Resources.FindObjectsOfTypeAll<Material>().First(mat => mat.shader.name.Contains("UI/Blur/UIBlur"));
            foreach (var (name, (scene, path)) in _preloads)
            {
                GameObjects[name] = preloadedObjects[scene][path];
            }

            LoadAssets();

            ModHooks.AfterSavegameLoadHook += AfterSaveGameLoad;
            ModHooks.GetPlayerVariableHook += GetVariableHook;
            ModHooks.LanguageGetHook += LangGet;
            ModHooks.NewGameHook += AddComponent;
            ModHooks.SetPlayerVariableHook += SetVariableHook;

            On.BlurPlane.Awake += OnBlurPlaneAwake;
            On.SceneManager.Start += OnSceneManagerStart;
            On.tk2dTileMap.Awake += OnTileMapAwake;
        }

        public void OnLoadLocal(LocalSettings localSettings) => _localSettings = localSettings;
        public LocalSettings OnSaveLocal() => _localSettings;

        private void AfterSaveGameLoad(SaveGameData data) => AddComponent();

        private object GetVariableHook(Type t, string key, object orig)
        {
            if (key == "statueStateAspid")
            {
                return _localSettings.CompletionAspid;
            }

            return orig;
        }

        private string LangGet(string key, string sheettitle, string orig)
        {
            switch (key)
            {
                case "ASPID_MAIN": return "Aspid Queen";
                case "ASPID_SUB": return "";
                case "ASPID_SUPER": return "";
                case "ASPID_NAME": return "Aspid Queen";
                case "ASPID_DESC": return "Despised god of the extinct.";
                default: return orig;
            }
        }

        private void AddComponent()
        {
            GameManager.instance.gameObject.AddComponent<StatueCreator>();
            GameManager.instance.gameObject.AddComponent<SceneLoader>();
        }

        private object SetVariableHook(Type t, string key, object obj)
        {
            if (key == "statueStateAspid")
            {
                _localSettings.CompletionAspid = (BossStatue.Completion)obj;
            }

            return obj;
        }


        private void OnBlurPlaneAwake(On.BlurPlane.orig_Awake orig, BlurPlane self)
        {
            orig(self);

            if (self.OriginalMaterial.shader.name == "UI/Default")
            {
                self.SetPlaneMaterial(_blurMat);
            }
        }

        private void OnSceneManagerStart(On.SceneManager.orig_Start orig, SceneManager self)
        {
            orig(self);

            self.tag = "SceneManager";
        }

        private void OnTileMapAwake(On.tk2dTileMap.orig_Awake orig, tk2dTileMap self)
        {
            orig(self);

            self.tag = "TileMap";
        }

        private void LoadAssets()
        {
            var assembly = Assembly.GetExecutingAssembly();
            foreach (string resourceName in assembly.GetManifestResourceNames())
            {
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream == null) continue;

                    if (resourceName.Contains("aspid"))
                    {
                        var bundle = AssetBundle.LoadFromStream(stream);
                        Bundles.Add(bundle.name, bundle);
                    }
                    else if (resourceName.Contains("GG_Statue_AspidQueen"))
                    {
                        var buffer = new byte[stream.Length];
                        stream.Read(buffer, 0, buffer.Length);
                        var statueTex = new Texture2D(2, 2);
                        statueTex.LoadImage(buffer);
                        Textures.Add("GG_Statue_AspidQueen", statueTex);
                    }

                    stream.Dispose();
                }
            }
        }

        internal IEnumerator DreamReturnDelayed()
        {
            StatueCreator.WonFight = true;

            yield return new WaitForSeconds(6);

            var bsc = SceneLoader.SceneController.GetComponent<BossSceneController>();
            GameObject transition = UObject.Instantiate(bsc.transitionPrefab);
            PlayMakerFSM transitionsFSM = transition.LocateMyFSM("Transitions");
            transitionsFSM.SetState("Out Statue");
            yield return new WaitForSeconds(1);
            bsc.DoDreamReturn();
        }

        private void Unload()
        {
            ModHooks.AfterSavegameLoadHook -= AfterSaveGameLoad;
            ModHooks.GetPlayerVariableHook -= GetVariableHook;
            ModHooks.LanguageGetHook -= LangGet;
            ModHooks.SetPlayerVariableHook -= SetVariableHook;
            ModHooks.NewGameHook -= AddComponent;

            On.BlurPlane.Awake -= OnBlurPlaneAwake;
            On.SceneManager.Start -= OnSceneManagerStart;
            On.tk2dTileMap.Awake -= OnTileMapAwake;

            var finder = GameManager.instance?.gameObject.GetComponent<StatueCreator>();
            if (finder == null)
            {
                return;
            }

            UObject.Destroy(finder);
        }
    }
}