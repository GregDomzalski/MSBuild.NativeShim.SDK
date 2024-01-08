#pragma once

#include <stdint.h>
#include <stddef.h>

// #define PALEXPORT __declspec(dllexport)
#define PALEXPORT

typedef enum {
    HidBusType_Unknown = 0,
    HidBusType_Usb = 1,
    HidBusType_Bluetooth = 2,
    HidBusType_I2C = 3,
    HidBusType_SPI = 4,
} HidBusType;

typedef void* HidDeviceInfoPtr;
typedef void* HidDevicePtr;
typedef void* HidVersionPtr;

PALEXPORT int32_t GregDomNative_HidInit(void);
PALEXPORT int32_t GregDomNative_HidExit(void);
PALEXPORT HidDeviceInfoPtr GregDomNative_HidEnumerate(uint16_t vendorId, uint16_t productId);
PALEXPORT void GregDomNative_HidFreeEnumeration(HidDeviceInfoPtr devices);
PALEXPORT HidDevicePtr GregDomNative_HidOpen(uint16_t vendorId, uint16_t productId, const wchar_t* serialNumber);
PALEXPORT HidDevicePtr GregDomNative_HidOpenPath(const char* path);
PALEXPORT int32_t GregDomNative_HidWrite(HidDevicePtr device, const uint8_t* data, uint32_t length);
PALEXPORT int32_t GregDomNative_HidReadTimeout(HidDevicePtr device, uint8_t* data, uint32_t length, uint32_t milliseconds);
PALEXPORT int32_t GregDomNative_HidRead(HidDevicePtr device, uint8_t* data, uint32_t length);
PALEXPORT int32_t GregDomNative_HidSetNonblocking(HidDevicePtr device, int32_t nonblock);
PALEXPORT int32_t GregDomNative_HidSendFeatureReport(HidDevicePtr device, const uint8_t* data, uint32_t length);
PALEXPORT int32_t GregDomNative_HidGetFeatureReport(HidDevicePtr device, uint8_t* data, uint32_t length);
PALEXPORT int32_t GregDomNative_HidGetInputReport(HidDevicePtr device, uint8_t* data, uint32_t length);
PALEXPORT void GregDomNative_HidClose(HidDevicePtr device);
PALEXPORT int32_t GregDomNative_HidGetManufacturerString(HidDevicePtr device, wchar_t* string, uint32_t maxLength);
PALEXPORT int32_t GregDomNative_HidGetProductString(HidDevicePtr device, wchar_t* string, uint32_t maxLength);
PALEXPORT int32_t GregDomNative_HidGetSerialNumberString(HidDevicePtr device, wchar_t* string, uint32_t maxLength);
PALEXPORT HidDeviceInfoPtr GregDomNative_HidGetDeviceInfo(HidDevicePtr device);
PALEXPORT int32_t GregDomNative_HidGetIndexedString(HidDevicePtr device, int stringIndex, wchar_t* string, uint32_t maxLength);
PALEXPORT int32_t GregDomNative_HidGetReportDescriptor(HidDevicePtr device, uint8_t* buf, uint32_t bufSize);
PALEXPORT const wchar_t* GregDomNative_HidError(HidDevicePtr device);
PALEXPORT const HidVersionPtr GregDomNative_HidVersion();
PALEXPORT const char* GregDomNative_HidVersionStr(void);

// HidVersion accessors
PALEXPORT int32_t GregDomNative_HidVersion_get_major(HidVersionPtr version);
PALEXPORT int32_t GregDomNative_HidVersion_get_minor(HidVersionPtr version);
PALEXPORT int32_t GregDomNative_HidVersion_get_patch(HidVersionPtr version);

// HidDeviceInfo accessors
PALEXPORT const char* GregDomNative_HidVersion_get_path(HidDeviceInfoPtr deviceInfo);
PALEXPORT const uint16_t GregDomNative_HidVersion_get_vendorId(HidDeviceInfoPtr deviceInfo);
PALEXPORT const uint16_t GregDomNative_HidVersion_get_productId(HidDeviceInfoPtr deviceInfo);
PALEXPORT const wchar_t* GregDomNative_HidVersion_get_serialNumber(HidDeviceInfoPtr deviceInfo);
PALEXPORT const uint16_t GregDomNative_HidVersion_get_releaseNumber(HidDeviceInfoPtr deviceInfo);
PALEXPORT const wchar_t* GregDomNative_HidVersion_get_manufacturerString(HidDeviceInfoPtr deviceInfo);
PALEXPORT const wchar_t* GregDomNative_HidVersion_get_productString(HidDeviceInfoPtr deviceInfo);
PALEXPORT const uint16_t GregDomNative_HidVersion_get_usagePage(HidDeviceInfoPtr deviceInfo);
PALEXPORT const uint16_t GregDomNative_HidVersion_get_usage(HidDeviceInfoPtr deviceInfo);
PALEXPORT const int32_t GregDomNative_HidVersion_get_interfaceNumber(HidDeviceInfoPtr deviceInfo);
PALEXPORT const HidBusType GregDomNative_HidVersion_get_busType(HidDeviceInfoPtr deviceInfo);
