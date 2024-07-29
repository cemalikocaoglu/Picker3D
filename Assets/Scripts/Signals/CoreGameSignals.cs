using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Signals
{

    public class CoreGameSignals : MonoBehaviour
    {

        #region Singleton

        public static CoreGameSignals Instance;

        public Action<byte> OnLevelInitialize { get; internal set; }

        private void Awake()
        {
             if(Instance != null  && Instance !=this)
            {
                Destroy(gameObject);
                return;
            }

             Instance = this;
        }





        #endregion


       // public UnityAction<byte> OnLevelInitialize = delegate { };
        public UnityAction OnClearActiveLevel = delegate { };
        public UnityAction OnNextLevel = delegate { };
        public UnityAction OnRestartLevel = delegate { };
        public UnityAction OnReset = delegate { };
        public Func<byte> OnGetlevelValue = delegate { return 0;  };



    }



}