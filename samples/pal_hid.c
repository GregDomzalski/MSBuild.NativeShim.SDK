#include "pal_hid.h"
#include <hidapi.h>

int32_t GregDomNative_HidInit(void)
{
    return hid_init();
}

int32_t GregDomNative_HidExit(void)
{
    return hid_exit();
}

HidDeviceInfoPtr GregDomNative_HidEnumerate(uint16_t vendorId, uint16_t productId)
{
    return hid_enumerate(vendorId, productId); // TODO: Convert the struct
}

void GregDomNative_HidFreeEnumeration(HidDeviceInfoPtr devices)
{
    hid_free_enumeration(devices);
}

HidDevicePtr GregDomNative_HidOpen(uint16_t vendorId, uint16_t productId, const wchar_t* serialNumber)
{
    return hid_open(vendorId, productId, serialNumber);
}

HidDevicePtr GregDomNative_HidOpenPath(const char* path)
{
    return hid_open_path(path);
}

int32_t GregDomNative_HidWrite(HidDevicePtr device, const uint8_t* data, uint32_t length)
{
    return hid_write(device, data, length);
}

int32_t GregDomNative_HidGetManufacturerString(HidDevicePtr device, wchar_t* string, uint32_t maxLength)
{
    return hid_get_manufacturer_string(device, string, maxLength);
}

int32_t GregDomNative_HidGetProductString(HidDevicePtr device, wchar_t* string, uint32_t maxLength)
{
    return hid_get_product_string(device, string, maxLength);
}

int32_t GregDomNative_HidGetSerialNumberString(HidDevicePtr device, wchar_t* string, uint32_t maxLength)
{
    return hid_get_serial_number_string(device, string, maxLength);
}

HidDeviceInfoPtr GregDomNative_HidGetDeviceInfo(HidDevicePtr device)
{
    return hid_get_device_info(device);
}

int32_t GregDomNative_HidGetIndexedString(HidDevicePtr device, int stringIndex, wchar_t* string, uint32_t maxLength)
{
    return hid_get_indexed_string(device, stringIndex, string, maxLength);
}

int32_t GregDomNative_HidGetReportDescriptor(HidDevicePtr device, uint8_t* buf, uint32_t bufSize)
{
    return hid_get_report_descriptor(device, buf, bufSize);
}

const wchar_t* GregDomNative_HidError(HidDevicePtr device)
{
    return hid_error(device);
}

const HidVersionPtr GregDomNative_HidVersion(void)
{
    return (const HidVersionPtr)hid_version();
}

int32_t GregDomNative_HidVersion_get_major(HidVersionPtr version)
{
    return ((const struct hid_api_version*)version)->major;
}

int32_t GregDomNative_HidVersion_get_minor(HidVersionPtr version)
{
    return ((const struct hid_api_version*)version)->minor;
}

int32_t GregDomNative_HidVersion_get_patch(HidVersionPtr version)
{
    return ((const struct hid_api_version*)version)->patch;
}

const char* GregDomNative_HidVersionStr(void)
{
    return hid_version_str();
}

const char* GregDomNative_HidVersion_get_path(HidDeviceInfoPtr deviceInfo)
{
    return ((struct hid_device_info*)deviceInfo)->path;
}
const uint16_t GregDomNative_HidVersion_get_vendorId(HidDeviceInfoPtr deviceInfo)
{
    return ((struct hid_device_info*)deviceInfo)->vendor_id;
}
const uint16_t GregDomNative_HidVersion_get_productId(HidDeviceInfoPtr deviceInfo)
{
    return ((struct hid_device_info*)deviceInfo)->product_id;
}
const wchar_t* GregDomNative_HidVersion_get_serialNumber(HidDeviceInfoPtr deviceInfo)
{
    return ((struct hid_device_info*)deviceInfo)->serial_number;
}
const uint16_t GregDomNative_HidVersion_get_releaseNumber(HidDeviceInfoPtr deviceInfo)
{
    return ((struct hid_device_info*)deviceInfo)->release_number;
}
const wchar_t* GregDomNative_HidVersion_get_manufacturerString(HidDeviceInfoPtr deviceInfo)
{
    return ((struct hid_device_info*)deviceInfo)->manufacturer_string;
}
const wchar_t* GregDomNative_HidVersion_get_productString(HidDeviceInfoPtr deviceInfo)
{
    return ((struct hid_device_info*)deviceInfo)->product_string;
}
const uint16_t GregDomNative_HidVersion_get_usagePage(HidDeviceInfoPtr deviceInfo)
{
    return ((struct hid_device_info*)deviceInfo)->usage_page;
}
const uint16_t GregDomNative_HidVersion_get_usage(HidDeviceInfoPtr deviceInfo)
{
    return ((struct hid_device_info*)deviceInfo)->usage;
}
const int32_t GregDomNative_HidVersion_get_interfaceNumber(HidDeviceInfoPtr deviceInfo)
{
    return ((struct hid_device_info*)deviceInfo)->interface_number;
}
const HidBusType GregDomNative_HidVersion_get_busType(HidDeviceInfoPtr deviceInfo)
{
    return ((struct hid_device_info*)deviceInfo)->bus_type;
}
