using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private AICube _aiCubePrefab;
        [SerializeField] private PlayerCube _playerCube;
        [SerializeField] private InputFromKeyboard _inputFromKeyboard;
        [SerializeField] private List<OrientPoint> _orientPoints;
        [SerializeField] private View _viewPrefab;
        [SerializeField] private Transform _startPositionPlayer, _startPositionAi;

        // Start is called before the first frame update
        void Start()
        {
            AICube aICube = Instantiate(_aiCubePrefab);
            PlayerCube playerCube = Instantiate(_playerCube);
            View view = Instantiate(_viewPrefab);
            BattleCube[] battleCubes = new BattleCube[2] { playerCube, aICube };            

            aICube.Initialize(_orientPoints, playerCube.GetComponent<OrientPoint>(), _startPositionAi.position);
            playerCube.Initialize(_startPositionPlayer.position);

            RuleStartStopGame ruleStartStopGame = view.GetComponent<RuleStartStopGame>();
            ViewLog viewLog = view.GetComponent<ViewLog>();
            PlayerController playerController = new PlayerController(playerCube, _inputFromKeyboard);

            Presenter presenter = new Presenter(view, viewLog, ruleStartStopGame, battleCubes, playerController);
        }
    }
}


