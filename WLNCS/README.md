# cesium-angular-dotNet

A simple web application that demonstrates integration of [Cesium](https://cesiumjs.org/) with [Angular](https://angular.io/)
That uses a [DotNet](https://dotnet.microsoft.com/apps/aspnet) back-end.

## Setup
Navigate to the `cesium-angular` folder using `cd ./cesium/cesium-angular`

Run `npm install -g @angular/cli` to install **Angular CLI**.

Run `npm install` to install all the dependencies.

## Development server

Run `dotnet run` within the ./cesium/cesium-dotNet folder to start the server.

If the user is unfamiliar with the CLI, they can instead run the cesium-startup.bat file to get cesium in browser.

Navigate to `http://localhost:5001/`. The app will automatically reload if you change any of the source files in angular. 
The source will need recompiled for any changes in dotNet. 

## Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory. Use the `-prod` flag for a production build.

## Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI README](https://github.com/angular/angular-cli/blob/master/README.md).
