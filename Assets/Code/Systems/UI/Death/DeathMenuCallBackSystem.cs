using Leopotam.EcsLite;
using Leopotam.EcsLite.Unity.Ugui;
using MSuhininTestovoe.B2B;
using UnityEngine;
using UnityEngine.Scripting;



namespace MSuhininTestovoe
{
    sealed class DeathMenuCallBackSystem : EcsUguiCallbackSystem, IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private EcsPool<BtnToMainMenu> _toMainMenuBtnCommandPool;
        private EcsPool<BtnQuit> _quitBtnCommandPool;
        private EcsPool<BtnRestart> _restartBtnCommandPool;
    
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<IsDeathMenu>().End();
            _toMainMenuBtnCommandPool = _world.GetPool<BtnToMainMenu>();
            _quitBtnCommandPool = _world.GetPool<BtnQuit>();
            _restartBtnCommandPool = _world.GetPool<BtnRestart>();
        }

        [Preserve]
        [EcsUguiClickEvent(UIConstants.MENU_TO_MAIN_MENU, WorldsNamesConstants.EVENTS)]
        void OnClickToMAinMEnu(in EcsUguiClickEvent e)
        {
            foreach (var entity in _filter)
            {
                _toMainMenuBtnCommandPool.Add(entity);
            }
        }
        
        [Preserve]
        [EcsUguiClickEvent(UIConstants.MENU_QUIT, WorldsNamesConstants.EVENTS)]
        void OnClickQuit(in EcsUguiClickEvent e)
        {
            foreach (var entity in _filter)
            {
                _quitBtnCommandPool.Add(entity);
            }
        }
        
        
        [Preserve]
        [EcsUguiClickEvent(UIConstants.MENU_RESTART, WorldsNamesConstants.EVENTS)]
        void OnClickRestart(in EcsUguiClickEvent e)
        {
            foreach (var entity in _filter)
            {
                _restartBtnCommandPool.Add(entity);
            }
        }
    }
}