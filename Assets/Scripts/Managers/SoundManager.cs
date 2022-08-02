using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unboxed.Pattern;

namespace Unboxed.Manager
{
    public class SoundManager : Singleton<SoundManager>
    {
        public bool IsMute => _isMute;

        private bool _isMute;

        public void MuteSound()
        {
            _isMute = true;
        }

        public void UnmuteSound()
        {
            _isMute = false;
        }
    }
}


