﻿using System;
using UnityEngine;

namespace Genso.API {

    /// <summary>
    /// A container behaviour for handling general data about the stage.
    /// </summary>
    /// Author: James Liu
    /// Authored on: 07/01/2015
    public sealed class Stage : Singleton<Stage>
    {

        [System.Serializable]
        private class PlayerData
        {

            public Transform Spawn;
            public Transform Respawn;

        }

        [SerializeField]
        private Camera mainCamera;

        [SerializeField]
        private PlayerData[] playerData;

        /// <summary>
        /// The maximum number of supported players on this Stage
        /// </summary>
        public static int SupportedPlayerCount
        {
            get { return Instance.playerData.Length; }
        }

        public static Camera Camera
        {
            get
            {
                return Instance.mainCamera;
            }
        }

        public void StartMatch(Match match)
        {
            if (match == null)
                throw new ArgumentException("match");
            if (match.PlayerCount > SupportedPlayerCount)
                throw new InvalidOperationException("Cannot start a match when there are more players participating than supported by the selected stage");

            int playerCount = Mathf.Min(match.PlayerCount, SupportedPlayerCount);

            for (var i = 0; i < playerCount; i++)
            {
                Character spawnedCharacter = match.SpawnCharacter(i, playerData[i].Spawn.position);
                PlayerIndicator indicator = GameSettings.CreatePlayerIndicator(i);
                indicator.Attach(spawnedCharacter);
            }

        }

        protected override void Awake()
        {
            base.Awake();
            if (mainCamera != null)
                return;
            if (Camera.main != null)
            {
                mainCamera = Camera.main;
            }
            mainCamera = FindObjectOfType<Camera>();
            if (mainCamera == null)
                Debug.LogError("Stage has no Camera!");
        }

    }


}
