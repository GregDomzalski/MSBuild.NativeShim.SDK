# MSBuild.NativeShim.SDK

The `MSBuild.NativeShim.SDK` MSBuild project SDK allows developers to easily create self-contained native dependency NuGet packages. These can serve as the basis for cross-platform native interoperability where your .NET library does not require any native prerequisites to be installed.

## Linux dependencies

1. Install VCPKG following the online documentation.
2. Set the VCPKG_INSTALLATION_ROOT environment variable to the root of the vcpkg repo that you checked out.
3. Make sure the following additional system packages are installed:
`sudo apt install build-essential cmake autoconf libtool pkg-config`
