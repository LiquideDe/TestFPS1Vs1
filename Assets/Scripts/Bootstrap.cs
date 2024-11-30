using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private AICube _aiCubePrefab;
    [SerializeField] private PlayerCube _playerCube;
    [SerializeField] private List<OrientPoint> _orientPoints;
    [SerializeField] private View _viewPrefab;
    [SerializeField] private Transform _startPositionPlayer, _startPositionAi;

    // Start is called before the first frame update
    void Start()
    {
        AICube aICube = Instantiate(_aiCubePrefab);
        PlayerCube playerCube = Instantiate(_playerCube);
        View view = Instantiate(_viewPrefab);

        aICube.Initialize(_orientPoints, playerCube.GetComponent<OrientPoint>(), _startPositionAi.position);
        playerCube.Initialize(_startPositionPlayer.position);

        Presenter presenter = new Presenter();
        presenter.Initialize(view, aICube, playerCube);
    }
}
