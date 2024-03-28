using UnityEngine;
using TMPro;
using UnityEngine.Android;

public class GPS : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputLat;
    [SerializeField] private TMP_InputField _inputLon;

    private LocationService _locationService;

    [HideInInspector] public double latitude;
    [HideInInspector] public double longitude;

    [HideInInspector] public double targetLat;
    [HideInInspector] public double targetLon;

    public static GPS instance;

    private void Awake()
    {
        instance = this;
    }

    private void RequestLocationPermission()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }
        else
        {
            StartLocationService();
            Debug.Log("asd");
        }
    }

    private void Start()
    {
        targetLat = 48.52099148578335;
        targetLon = 32.24856899315188;

        RequestLocationPermission();
    }

    private void StartLocationService()
    {
        if (!Input.location.isEnabledByUser)
        {
            return;
        }

        _locationService = Input.location;
        _locationService.Start();
    }

    void Update()
    {
        if (_locationService != null && _locationService.status == LocationServiceStatus.Running)
        {
            UpdateLocationData();
        }
    }

    void UpdateLocationData()
    {
        latitude = _locationService.lastData.latitude;
        longitude = _locationService.lastData.longitude;

        targetLat = double.Parse(_inputLat.text);
        targetLon = double.Parse(_inputLon.text);
    }

    void OnDestroy()
    {
        if (_locationService != null)
        {
            _locationService.Stop();
        }
    }
}
