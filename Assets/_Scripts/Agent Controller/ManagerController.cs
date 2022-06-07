using System.Collections.Generic;
using System.Linq;
using UnityEngine;

    public class ManagerController : MonoBehaviour
    {
        private List<Controller> _controllers;
        private PlayerManager _playerManager;

        private int index = 1;
        private void Awake()
        {
            _controllers = FindObjectsOfType<Controller>().ToList();
            _playerManager = FindObjectOfType<PlayerManager>();

            foreach (var controller in _controllers)
            {
                controller.SetIndex(index);
                index++;
            }
        }
        private void Update()
        {
            foreach (var controller in _controllers)
            {
                if (controller.IsAssigned == false && controller.PressingAnyButton())
                {
                    AssignController(controller);
                }
            }
        }
    
        private void AssignController(Controller controller)
        {
            controller.IsAssigned = true;
            _playerManager.AddPlayerToGame(controller);
        }
    }

