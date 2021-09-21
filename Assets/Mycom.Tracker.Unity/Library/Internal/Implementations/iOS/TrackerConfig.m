#import <MyTrackerSDK/MyTrackerSDK.h>
#import "PlatformUtils.h"

extern const char * MRMTInternalMakeStringCopy(NSString *sourceString);

int32_t MRMTMyTrackerConfigGetBufferingPeriod()
{
    MRMyTrackerConfig *trackerConfig = [MRMyTracker trackerConfig];
    if (trackerConfig)
    {
        return (int32_t) [trackerConfig bufferingPeriod];
    }

    return -1;
}

int32_t MRMTMyTrackerConfigGetForcingPeriod()
{
    MRMyTrackerConfig *trackerConfig = [MRMyTracker trackerConfig];
    if (trackerConfig)
    {
        return (int32_t) [trackerConfig forcingPeriod];
    }

    return 0;
}

const char *MRMTMyTrackerConfigGetId()
{
    MRMyTrackerConfig *trackerConfig = [MRMyTracker trackerConfig];
    if (trackerConfig)
    {
        return MRMTInternalMakeStringCopy([trackerConfig trackerId]);
    }

    return NULL;
}

int32_t MRMTMyTrackerConfigGetLaunchTimeout()
{
    MRMyTrackerConfig *trackerConfig = [MRMyTracker trackerConfig];
    if (trackerConfig)
    {
        return (int32_t) [trackerConfig launchTimeout];
    }

    return -1;
}

int32_t MRMTMyTrackerConfigIsTrackingEnvironmentEnabled()
{
    MRMyTrackerConfig *trackerConfig = [MRMyTracker trackerConfig];
    if (trackerConfig)
    {
        return [trackerConfig trackEnvironment] ? true : false;
    }

    return false;
}

int32_t MRMTMyTrackerConfigIsTrackingLaunchEnabled()
{
    MRMyTrackerConfig *trackerConfig = [MRMyTracker trackerConfig];
    if (trackerConfig)
    {
        return [trackerConfig trackLaunch] ? true : false;
    }

    return false;
}

int32_t MRMTMyTrackerConfigIsTrackingLocationEnabled()
{
    MRMyTrackerConfig *trackerConfig = [MRMyTracker trackerConfig];
    if (trackerConfig)
    {
        return [trackerConfig locationTrackingMode] != MRLocationTrackingModeNone;
    }

    return false;
}

void MRMTMyTrackerConfigSetBufferingPeriod(int32_t value)
{
    MRMyTrackerConfig *trackerConfig = [MRMyTracker trackerConfig];
    if (trackerConfig)
    {
        [trackerConfig setBufferingPeriod:value];
    }
}

void MRMTMyTrackerConfigSetForcingPeriod(int32_t value)
{
    MRMyTrackerConfig *trackerConfig = [MRMyTracker trackerConfig];
    if (trackerConfig)
    {
        [trackerConfig setForcingPeriod:value];
    }
}

void MRMTMyTrackerConfigSetLaunchTimeout(int32_t value)
{
    MRMyTrackerConfig *trackerConfig = [MRMyTracker trackerConfig];
    if (trackerConfig)
    {
        [trackerConfig setLaunchTimeout:value];
    }
}

void MRMTMyTrackerConfigSetTrackingEnvironmentEnabled(bool value)
{
    MRMyTrackerConfig *trackerConfig = [MRMyTracker trackerConfig];
    if (trackerConfig)
    {
        [trackerConfig setTrackEnvironment:value];
    }
}

void MRMTMyTrackerConfigSetTrackingLaunchEnabled(bool value)
{
    MRMyTrackerConfig *trackerConfig = [MRMyTracker trackerConfig];
    if (trackerConfig)
    {
        [trackerConfig setTrackLaunch:value];
    }
}

void MRMTMyTrackerConfigSetTrackingLocationEnabled(bool value)
{
    MRMyTrackerConfig *trackerConfig = [MRMyTracker trackerConfig];
    if (trackerConfig)
    {
        MRLocationTrackingMode mode = value ? MRLocationTrackingModeActive : MRLocationTrackingModeNone;

        [trackerConfig setLocationTrackingMode:mode];
    }
}

void MRMTMyTrackerConfigSetRegion(int32_t value)
{
    MRMyTrackerConfig *trackerConfig = [MRMyTracker trackerConfig];
    if (trackerConfig)
    {
        MRRegion region;
        switch (value)
        {
            case 0:
                region = MRRegionRu;
                break;
            case 1:
                region = MRRegionEu;
                break;
            default:
                region = MRRegionNotSet;
                break;
        }

        [trackerConfig setRegion:region];
    }
}

void MRMTMyTrackerConfigSetProxyHost(const char *value)
{
    MRMyTrackerConfig *trackerConfig = [MRMyTracker trackerConfig];
    if (trackerConfig)
    {
        [trackerConfig setProxyHost:value ? [NSString stringWithUTF8String:value] : nil];
    }
}
