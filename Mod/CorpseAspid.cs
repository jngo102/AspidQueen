using HutongGames.PlayMaker.Actions;
using Modding;
using UnityEngine;
using Vasi;

namespace AspidQueen
{
    internal class CorpseAspid : MonoBehaviour
    {
        private void Awake() 
        {
            FixFSM();
        }

        private void FixFSM()
        {
            var corpse = gameObject.LocateMyFSM("Corpse");
            var audioPlayer = GameManager.instance.transform.Find("GlobalPool/Audio Player Actor(Clone)").gameObject;
            audioPlayer.CreatePool(5);
            corpse.GetAction<AudioPlayerOneShotSingle>("Init", 1).audioPlayer =
            corpse.GetAction<AudioPlayerOneShotSingle>("Steam", 0).audioPlayer = audioPlayer;
            var deathWave = AspidQueen.Instance.GameObjects["Corpse"].LocateMyFSM("corpse")
                .GetAction<SpawnObjectFromGlobalPool>("Init", 8).gameObject;
            corpse.GetAction<SpawnObjectFromGlobalPool>("Init", 2).gameObject = deathWave;
            corpse.GetAction<CreateObject>("Steam", 2).gameObject = AspidQueen.Instance.GameObjects["Death Puff"];
            corpse.GetAction<CreateObject>("Blow", 0).gameObject = AspidQueen.Instance.GameObjects["Death Explode"];
        }
    }
}