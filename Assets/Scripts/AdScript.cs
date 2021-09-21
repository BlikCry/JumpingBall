using Mycom.Tracker.Unity;
using UnityEngine;
using Yodo1.MAS;

internal class AdScript: MonoBehaviour
{
    public static AdScript Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);

        Yodo1U3dMas.InitializeSdk();

        #if UNITY_ANDROID
            var myTrackerConfig = MyTracker.MyTrackerConfig;
            myTrackerConfig.Region = RegionEnum.RU;
            myTrackerConfig.IsTrackingLaunchEnabled = true;
            myTrackerConfig.BufferingPeriod = 1;
            myTrackerConfig.LaunchTimeout = 5;
            MyTracker.Init("68798549201007709471");
        #endif
    }
}