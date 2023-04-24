using UnityEngine;

public class Game : MonoBehaviour
{
    [Header("Префабы")]
    [SerializeField] private GameObject _cashDeskPrefab;
    [SerializeField] private GameObject _shelfPrefab;
    [SerializeField] private GameObject _bedPrefab;
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private Buyer _buyerPrefab;

    [Space(20)] 
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Zone _zone;
    [SerializeField] private CameraTarget _cameraTarget;
    //[SerializeField] private TextMeshProUGUI _coinsTMP;
    //[SerializeField] private GameObject _coinImg;
    //[SerializeField] private float _txtScaleTime;
    //[SerializeField] private Vector3 _txtScaleMax;
    
    private InputPlayerSystem _inputPlayer;
    private Player _player;
    private int _coins = 0;
    private BuyerSpawner _buyer;

    void Awake()
    {
        _inputPlayer = new InputPlayerSystem();
        // _coinsTMP.text = _coins.ToString();
    }

    private void Start()
    {
        _zone.StartSpawn(_cashDeskPrefab, _shelfPrefab, _bedPrefab);
        _player = Instantiate(_playerPrefab, _zone.PointPlayer.position, Quaternion.identity);
        _player.Construct(_inputPlayer, _joystick);
        _cameraTarget.Construct(_player.transform);
        _buyer = new BuyerSpawner(_zone.PointBuyer, _buyerPrefab, _zone);
    }
}