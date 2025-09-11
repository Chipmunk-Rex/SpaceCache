using System;
using Ami.BroAudio;
using UnityEngine;

namespace Code.Scripts.Items.UI.Sounds
{
    public class PlayBackMusic : MonoBehaviour
    {
        [SerializeField] private SoundID soundID;

        private void Start()
        {
            BroAudio.SetVolume(BroAudioType.All, 1f);
            IAudioPlayer player = BroAudio.Play(soundID);
            
            if (player == null)
            {
                Debug.LogError("재생 실패: Player가 null입니다. SoundID나 초기화를 확인하세요.");
            }
        }
        
    }
}