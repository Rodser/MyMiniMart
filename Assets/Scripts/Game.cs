using UnityEngine;
using UnityEngine.InputSystem;

public class Game : MonoBehaviour
{
    [Header("Префабы")]
    [SerializeField] private GameObject _cashDeskPrefab;
    [SerializeField] private GameObject _shelfPrefab;
    [SerializeField] private GameObject _bedPrefab;
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private Zone _zonePrefab;
    [Space(20)] 
    [SerializeField] private Joystick _joystick;
    [SerializeField] private CameraTarget _cameraTarget;
    [SerializeField] private Joystick _screenStick;
    //[SerializeField] private TextMeshProUGUI _coinsTMP;
    //[SerializeField] private GameObject _coinImg;
    //[SerializeField] private float _txtScaleTime;
    //[SerializeField] private Vector3 _txtScaleMax;
    
    private InputPlayerSystem _inputPlayer;
    private Player _player;
    private int _coins = 0;
    private Zone _zone;

    void Awake()
    {
        _inputPlayer = new InputPlayerSystem();
        // _coinsTMP.text = _coins.ToString();
    }

    private void Start()
    {
        _zone = Instantiate(_zonePrefab);
        _zone.StartSpawn(_cashDeskPrefab, _shelfPrefab, _bedPrefab);
        _player = Instantiate(_playerPrefab, _zone.PointPlayer.position, Quaternion.identity);
        _player.Construct(_inputPlayer, _joystick);
        _cameraTarget.Construct(_player.transform);
    }
    
}