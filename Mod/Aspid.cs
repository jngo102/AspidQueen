using HutongGames.PlayMaker.Actions;
using Modding;
using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Vasi;
using Random = UnityEngine.Random;

namespace AspidQueen
{
    internal class Aspid : MonoBehaviour
    {
        private PlayMakerFSM _control;
        private EnemyDeathEffects _deathEffects;
        private EnemyDreamnailReaction _dreamNailReaction;
        private InfectedEnemyEffects _effects;
        private ExtraDamageable _extraDamageable;
        private HealthManager _healthManager;
        private GameObject _aspid;

        private void Awake()
        {
            _aspid = AspidQueen.Instance.GameObjects["Primal Aspid"];

            _control = gameObject.LocateMyFSM("Control");
            
            _deathEffects = gameObject.AddComponent<EnemyDeathEffects>();
            _dreamNailReaction = gameObject.AddComponent<EnemyDreamnailReaction>();
            _effects = gameObject.AddComponent<InfectedEnemyEffects>();
            _extraDamageable = gameObject.AddComponent<ExtraDamageable>();
            _healthManager = gameObject.AddComponent<HealthManager>();

            _healthManager.OnDeath += OnDeath;

            On.PlayMakerFSM.Start += OnPFSMStart;

            CopyFields();

            FixFSM();
        }

        private void Start() 
        {
#if DEBUG
            _healthManager.hp = 300;
#else
            _healthManager.hp = SceneLoader.SceneController.BossLevel > 0 ? 1700 : 1450;
#endif
        }

        private void OnDestroy()
        {
            _healthManager.OnDeath -= OnDeath;

            On.PlayMakerFSM.Start -= OnPFSMStart;
        }

        private void OnDeath()
        {
            GameManager.instance.StartCoroutine(AspidQueen.Instance.DreamReturnDelayed());

            foreach (var hm in FindObjectsOfType<HealthManager>(true))
            {
                hm.Die(null, AttackTypes.Nail, true);
            }
        }

        private void OnPFSMStart(On.PlayMakerFSM.orig_Start orig, PlayMakerFSM self)
        {
            if (self.name.Contains("Explosive Glob") && self.FsmName == "Vomit Glob")
            {
                self.GetState("Land").InsertMethod(0, () => {
                    var explosion = Instantiate(AspidQueen.Instance.GameObjects["Explosion"], self.transform.position, Quaternion.identity);
                    explosion.SetActive(true);
                    Destroy(explosion.LocateMyFSM("damages_enemy"));
                });
            }
        }

        private void CopyFields()
        {
            var deathEffects = _aspid.GetComponent<EnemyDeathEffects>();
            foreach (FieldInfo fi in typeof(EnemyDeathEffects).GetFields(BindingFlags.Instance | BindingFlags.NonPublic).Where(x => x.Name.Contains("Prefab")))
            {
                fi.SetValue(_deathEffects, fi.GetValue(deathEffects));
            }

            var dreamNailReaction = _aspid.GetComponent<EnemyDreamnailReaction>();
            foreach (FieldInfo fi in typeof(EnemyDreamnailReaction).GetFields(BindingFlags.Instance | BindingFlags.NonPublic).Where(x => x.Name.Contains("Prefab")))
            {
                fi.SetValue(_dreamNailReaction, fi.GetValue(dreamNailReaction));
            }

            var effects = _aspid.GetComponent<InfectedEnemyEffects>();
            foreach (FieldInfo fi in typeof(InfectedEnemyEffects).GetFields(BindingFlags.Instance | BindingFlags.NonPublic).Where(x => x.Name.Contains("Prefab") || x.Name.Contains("Audio")))
            {
                fi.SetValue(_effects, fi.GetValue(effects));
            }

            var extraDamageable = _aspid.GetComponent<ExtraDamageable>();
            foreach (FieldInfo fi in typeof(ExtraDamageable).GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
            {
                fi.SetValue(_extraDamageable, fi.GetValue(extraDamageable));
            }       

            var healthManager = _aspid.GetComponent<HealthManager>();
            foreach (FieldInfo fi in typeof(HealthManager).GetFields(BindingFlags.Instance | BindingFlags.NonPublic).Where(x => x.Name.Contains("Prefab")))            {
                fi.SetValue(_healthManager, fi.GetValue(healthManager));
            }

            var corpse = FindObjectsOfType<GameObject>(true).First(go => go.name == "Corpse Aspid Queen");
            corpse.AddComponent<CorpseAspid>();
            ReflectionHelper.SetField(_deathEffects, "corpsePrefab", corpse);
            _deathEffects.PreInstantiate();
        }

        private void FixFSM()
        {
            var explosiveGlob = Instantiate(AspidQueen.Instance.GameObjects["Vomit Glob"]);
            explosiveGlob.SetActive(false);
            explosiveGlob.name = "Explosive Glob";
            explosiveGlob.GetComponent<Renderer>().material.shader = Shader.Find("Sprites/Default-ColorFlash");
            var globFlash = explosiveGlob.AddComponent<SpriteFlash>();
            globFlash.FlashingFury();
            AspidQueen.Instance.GameObjects["Aspid Shot"].CreatePool(10);
            explosiveGlob.CreatePool(25);
            var spitState = _control.GetState("Spit");

            _control.GetAction<CreateObject>("Summon", 2).gameObject =
            _control.GetAction<CreateObject>("Summon", 5).gameObject = _aspid;

            _control.GetState("Summon").InsertCoroutine(3, TweenAspid);
            _control.GetState("Summon").InsertCoroutine(7, TweenAspid);

            Action decrementAspidCount = () => _control.Fsm.GetFsmGameObject("Aspid").Value.GetComponent<HealthManager>().OnDeath += () => 
                _control.Fsm.GetFsmInt("Aspid Count").Value--;
            _control.GetState("Summon").InsertMethod(4, decrementAspidCount);
            _control.GetState("Summon").InsertMethod(9, decrementAspidCount);

            var roarWave = AspidQueen.Instance.GameObjects["Roar Wave"];
            _control.GetAction<CreateObject>("Intro Roar", 4).gameObject =
            _control.GetAction<CreateObject>("Summon Roar", 3).gameObject = roarWave;

            var areaTitle = GameCameras.instance.hudCamera.transform.Find("Area Title Holder/Area Title").gameObject;
            _control.GetAction<ActivateGameObject>("Intro Roar", 5).gameObject.GameObject = 
            _control.GetAction<SetFsmBool>("Intro Roar", 6).gameObject.GameObject =
            _control.GetAction<SetFsmString>("Intro Roar", 7).gameObject.GameObject = areaTitle;

            _control.GetAction<SendEventByName>("Intro Roar", 8).eventTarget.gameObject.GameObject =
            _control.GetAction<SendEventByName>("Summon Roar", 4).eventTarget.gameObject.GameObject = GameCameras.instance.gameObject;

            var audioPlayer = GameManager.instance.transform.Find("GlobalPool/Audio Player Actor(Clone)").gameObject;
            audioPlayer.CreatePool(5);
            foreach (var state in _control.FsmStates)
            {
                foreach (var action in state.Actions)
                {
                    if (action is AudioPlayerOneShotSingle)
                    {
                        ((AudioPlayerOneShotSingle)action).audioPlayer = audioPlayer;
                    }
                    else if (action is FlingObjectsFromGlobalPoolVel)
                    {
                        ((FlingObjectsFromGlobalPoolVel)action).gameObject = explosiveGlob;
                    }
                }
            }
        }

        private IEnumerator TweenAspid()
        {
            float tweenTime = Random.Range(0.75f, 1.2f);
            var aspid = _control.Fsm.GetFsmGameObject("Aspid").Value;
            aspid.transform.Translate(0, 8, 17);
            var sprite = aspid.GetComponent<tk2dSprite>();
            var spriteColor = Color.black;
            iTween.MoveBy(aspid,
                iTween.Hash(
                    "y", -8,
                    "z", -17,
                    "time", tweenTime,
                    "easeType", iTween.EaseType.easeOutSine,
                    "loopType", iTween.LoopType.none
                )
            );
            iTween.Init(aspid);
            yield return new WaitUntil(() =>
            {
                sprite.color = spriteColor;
                spriteColor += Color.white * Time.deltaTime * tweenTime;
                sprite.color = spriteColor;
                return sprite.color.r >= 1 && sprite.color.g >= 1 && sprite.color.b >= 1;
            });
        }
    }
}