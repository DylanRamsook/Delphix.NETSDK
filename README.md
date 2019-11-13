https://travis-ci.com/DylanRamsook/Delphix.NETSDK

https://www.nuget.org/packages/Delphix.NETSDK/



# Delphix.NET

[![MyGet Build Status](https://travis-ci.com/DylanRamsook/Delphix.NETSDK.svg?branch=master)](https://travis-ci.com/DylanRamsook/Delphix.NETSDK)

Delphix.NET is an Delphix .NET SDK, written for the Microsoft .NET platform.  It is designed to enable developers to seamlessly interact with the Delphix API.

## Contributing

I welcome and encourage contributions from the developer community. For an overview of the contribution process,
including an explanation of our issue labels and common emoji used in discussions, please see
[CONTRIBUTING](CONTRIBUTING.md).

## Building from Source

### Prerequisites

The recommended development environment for this project is Visual Studio 2017 or Visual Studio 2019.
### Build script

Execute `build.cmd` to download all dependencies and build. Use `build.cmd help` or `build.cmd /?` to view the available command line arguments.

```bash
build.cmd [Build|UnitTest|Documentation|Package] [/Configuration Debug|Release]

# Execute Build target in Debug mode
build.cmd

# Execute UnitTest target in Debug mode
build.cmd UnitTest

# Execute Build target in Release mode
build.cmd /Configuration Release

# Execute Package target in Release mode
build.cmd Package /Configuration Release
```

### Integration Tests
You will need a Delphix environment with a valid license in order to run the integration tests. The tests look for the sensitive info (delphix instance url, username, password) in environment variables: DELPHIX_USER, DELPHIX_PASSWORD, and DELPHIX_URL. After you have set the environment variables you will need to log out then log back in.

```batchfile
setx DELPHIX_USER secretusername
setx DELPHIX_PASSWORD secretpassword
setx DELPHIX_URL https://localhost:5000
logoff
#You need to relog in order for the environment variables to refresh!!
```

### Code Examples



#### This is not an official Delphix Project


#### TODO
 - Review model 
- Provisioning VDB Method for mutiple types
- TESTS
- Models for Gets (multiple object types) vs Posts 
- Travis CI for running integration tests
- Custom wait times from environment variables
- Documentation
- Add postman
- Look into autogenerating the models from delphix.json
