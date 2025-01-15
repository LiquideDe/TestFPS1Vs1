using UnityEngine;

namespace FPS
{
    public class AudioManager : MonoBehaviour, IBulletSounds, IBattleCubeSounds, IMenuSounds, IBattlegroundSounds
    {
        [SerializeField]
        private AudioSource SoundClick, SoundCancel, SoundMoving, SoundRebound, SoundStartBullet, SoundStartRound, SoundBigExplosion,
            SoundLooseGame, SoundHelicopter, SoundMusic, SoundPopUp;
        public void PlayClick() => SoundClick.Play();

        public void PlayCancel() => SoundCancel.Play();

        public void StartPlayMoving() => SoundMoving.Play();

        public void StopPlayMoving() => SoundMoving.Stop();

        public void PlayRebound() => SoundRebound.Play();

        public void PlayStartBullet() => SoundStartBullet.Play();

        public void PlayStartRound() => SoundStartRound.Play();

        public void PlayStopRound() { }

        public void PlayBigExplosion() => SoundBigExplosion.Play();

        public void PlayLooseGame() => SoundLooseGame.Play();

        public void PlayWinGame() { }

        public void PlayHelicopter() => SoundHelicopter.Play();

        public void StopHelicopter() => SoundHelicopter.Stop();

        public void PlayPopUp() => SoundPopUp.Play();
    }
}


