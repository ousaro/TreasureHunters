using System;
using System.Collections.Generic;
using UnityEngine;
 
namespace Osaro.Utilities{

     public class FunctionTimer{


            private static List<FunctionTimer> _activeTimerList;
            private static GameObject _initGameObject;

            private static void InitIfNeeded() {
                if(_initGameObject == null) {
                    _initGameObject = new GameObject("FunctionTimer_InitGameObject");
                    _activeTimerList = new List<FunctionTimer>();
                }
            }

            public static FunctionTimer Create(Action action, float timer, string timerName = null){

                InitIfNeeded();
                GameObject gameObject = new GameObject("FunctinTimer", typeof(MonoBehaviourHook));

                FunctionTimer functionTimer = new FunctionTimer(action, timer, gameObject,timerName);

                gameObject.GetComponent<MonoBehaviourHook>().onUpdate = functionTimer.Update;

                _activeTimerList.Add(functionTimer);
                return functionTimer;

            }

            private static void RemoveTimer(FunctionTimer functionTimer){
                InitIfNeeded();
                _activeTimerList.Remove(functionTimer);
            }

            public static void StopTimer(string timerName){

                for (int i = 0; i < _activeTimerList.Count; i++)
                {
                    if(_activeTimerList[i]._timerName == timerName){
                        // stop this timer
                        _activeTimerList[i].DestroySelf();
                        i--;
                    }
                    
                }
            }

            // dummy class to have access to monobehaviour functions
            private class MonoBehaviourHook : MonoBehaviour {

                public Action onUpdate;


                private void Update() {
                    if(onUpdate != null) onUpdate();

                }
            }




            private Action action;
            private float _timer;
            private string _timerName;
            private GameObject _gameObject;
            private bool _isDestroyed;


            public FunctionTimer(Action action, float timer, GameObject gameObject, string timerName){
                this.action = action;
                this._timer = timer;
                this._isDestroyed = false;
                this._gameObject = gameObject;
                this._timerName = timerName;

            }

            public void Update()
            {
                if (_isDestroyed) return;

                _timer -= Time.deltaTime;
                if (_timer < 0)
                {
                    //trigger the action
                    action();
                    DestroySelf();
                }
            }

            public void DestroySelf() {
                _isDestroyed = true;
                UnityEngine.Object.Destroy(_gameObject);
                RemoveTimer(this);
            }

        
    }
}
 
 
