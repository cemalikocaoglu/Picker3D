using Commands.Level;
using Data.UnityObjects;
using Data.ValueObjects;
using Signals;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Managers
{

    public class LevelManager : MonoBehaviour
    {


        #region Self Variables 

        #region Serialized Variables 

        [SerializeField] private Transform levelHolder;
        [SerializeField] private byte totalLevelCount;

        #endregion


        #region Private Variables 


        private OnLevelLoaderCommand _levelLoaderCommand;
        private OnLevelDestroyerCommand _levelDestroyerComamnd;

        private short _currentLevel;
        private LevelData _levelData;

        #endregion



        #endregion



        private void Awake()
        {
            _levelData = GetLevelData();
            _currentLevel = GetActiveLevel();



            Init();



        }



        private void Init()
        {
            _levelLoaderCommand = new OnLevelLoaderCommand(levelHolder);
            _levelDestroyerComamnd = new OnLevelDestroyerCommand(levelHolder);
        }


        //[Button]
        //public void LevelLoader(byte levelIndex)
        //{

        //    _levelLoaderCommand.Execute(levelIndex);

        //}

        //[Button]
        //public void LevelDestroyer()
        //{

        //    _levelDestroyerComamnd.Execute();


        //}


        private LevelData GetLevelData()
        {
            return Resources.Load<CD_Level>("Data/CD_Level").Levels[_currentLevel];
        }

        private byte GetActiveLevel()
        {
           
            return (byte)_currentLevel;

        }



        private void  OnEnable()
        {

            SubscribeEvents();

        }

        private void SubscribeEvents()
        {

            CoreGameSignals.Instance.OnLevelInitialize += _levelLoaderCommand.Execute;
            CoreGameSignals.Instance.OnClearActiveLevel += _levelDestroyerComamnd.Execute;
            CoreGameSignals.Instance.OnGetlevelValue += OnGetLevelValue;
            CoreGameSignals.Instance.OnNextLevel += OnNextLevel;
            CoreGameSignals.Instance.OnRestartLevel += OnRestartLevel;

        }
        private void OnNextLevel()
        {

            _currentLevel++;
            CoreGameSignals.Instance.OnClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.OnReset?.Invoke();
            CoreGameSignals.Instance.OnLevelInitialize?.Invoke((byte)((byte)_currentLevel % totalLevelCount));
        }


        private void OnRestartLevel()
        {


            CoreGameSignals.Instance.OnClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.OnReset?.Invoke();
            CoreGameSignals.Instance.OnLevelInitialize?.Invoke((byte)((byte)_currentLevel % totalLevelCount));
        }



        private byte OnGetLevelValue() 
        {

            return (byte)_currentLevel;
        
        }


        private void OnDisable()
        {

            UnSubscribeEvents();

        }
        private void UnSubscribeEvents()
        {

            CoreGameSignals.Instance.OnLevelInitialize -= _levelLoaderCommand.Execute;
            CoreGameSignals.Instance.OnClearActiveLevel -= _levelDestroyerComamnd.Execute;
            CoreGameSignals.Instance.OnGetlevelValue -= OnGetLevelValue;
            CoreGameSignals.Instance.OnNextLevel -= OnNextLevel;
            CoreGameSignals.Instance.OnRestartLevel -= OnRestartLevel;

        }



        private void Start()
        {

            CoreGameSignals.Instance.OnLevelInitialize?.Invoke((byte)((byte)_currentLevel % totalLevelCount));

        }


        


    }


}