﻿using CommonTemplate.Data;
using CommonTemplate.Resource;
using CommonTemplate.WebRequest;
using Game.Input;

namespace CommonTemplate.States.GameStates
{
    public class BootstrapState : IState
    {
        private string _bootstrapScene = "Bootstrap";
        private readonly ProjectStateMachine _stateMachine;
        private readonly GameData _gameData;
        private readonly SceneLoader _sceneLoader;
        private readonly GameSetting.GameSetting _gameSetting;
        private readonly WebRequestSender _webRequestSender;
        private readonly GameDataLoader _gameDataLoader;
        private readonly IInputService _inputService;
    
        public BootstrapState(ProjectStateMachine projectStateMachine, GameData gameData,GameDataLoader gameDataLoader,
            SceneLoader sceneLoader, GameSetting.GameSetting gameSetting,WebRequestSender webRequestSender,IInputService inputService)
        {
            _stateMachine = projectStateMachine;
            _gameDataLoader = gameDataLoader;
            _gameData = gameData;
            _sceneLoader = sceneLoader;
            _gameSetting = gameSetting;
            _webRequestSender = webRequestSender;
            _inputService = inputService;
        }

        public void EnterState()
        {
            _sceneLoader.Load(_bootstrapScene, OnSceneLoaded).Forget();
        }

        private async void OnSceneLoaded()
        {
            await _gameDataLoader.Init();
            _gameData.Init();
            _gameSetting.Init();
            _stateMachine.StartState<LoadProgressState>();
            _inputService.Init();
        }

        public void ExitState()
        {
        }
    }
}