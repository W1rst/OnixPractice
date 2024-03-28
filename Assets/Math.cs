using System;
using TMPro;
using UnityEngine;

public class Mathematics : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textDistance;

    private double _distance;
    private double _rotateArrow;

    [SerializeField] private GameObject _objectArrow;

    public void Update()
    {
        _distance = DistanceKm(GPS.instance.latitude, GPS.instance.longitude, GPS.instance.targetLat, GPS.instance.targetLon);
        _rotateArrow = RotateArrowToTarget(GPS.instance.latitude, GPS.instance.longitude, GPS.instance.targetLat, GPS.instance.targetLon);

        _textDistance.text = "Distance: " + _distance.ToString() + "km";

        _objectArrow.transform.rotation = Quaternion.Euler(-90, 0, (float)_rotateArrow - 90);
    }

    public double DistanceKm(double lat1, double lon1, double lat2, double lon2)
    { 
        double theta = lon1 - lon2;
        double distance = 60 * 1.1515 * (180 / Math.PI) * Math.Acos(
            Math.Sin(lat1 * (Math.PI / 180)) * Math.Sin(lat2 * (Math.PI / 180)) +
            Math.Cos(lat1 * (Math.PI / 180)) * Math.Cos(lat2 * (Math.PI / 180)) * Math.Cos(theta * (Math.PI / 180)));

        return Math.Round(distance * 1.609344, 2);
    }

    double RotateArrowToTarget(double lat1, double lon1, double lat2, double lon2)
    {
        lat1 = ToRadians(lat1);
        lon1 = ToRadians(lon1);
        lat2 = ToRadians(lat2);
        lon2 = ToRadians(lon2);

        double delta = lon2 - lon1;

        double y = Math.Sin(delta) * Math.Cos(lat2);
        double x = Math.Cos(lat1) * Math.Sin(lat2) - Math.Sin(lat1) * Math.Cos(lat2) * Math.Cos(delta);
        double bearing = Math.Atan2(y, x);

        bearing = ToDegrees(bearing);

        if (bearing < 0)
        {
            bearing += 360;
        }

        return bearing;
    }

    double ToRadians(double angle)
    {
        return Math.PI / 180 * angle;
    }

    double ToDegrees(double angle)
    {
        return angle * 180 / Math.PI;
    }
}
