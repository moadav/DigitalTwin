# How to run the Azure Maps project

This will guide you through running an express server as API endpoint for the client, and then the client itself - retrieving data from the local API endpoint. Resulting in displaying data from the Azure Digital Twin, incorporated into Azure Maps.  
I've included the most common issues in [Troubleshooting](#troubleshooting), so feel free to take a look if any problems do occur.

**!NB: All commands can run in cmd or powershell**

# Necessary programs:

## Node.js / npm

Download Node.js [here](https://nodejs.org/en/) which also includes npm.

Confirm install with

```
node --version
npm --version
```

You should now see Node version `v16.13.2` (or higher) and npm version `8.5.5` (or higher)

- Troubleshooting Node / npm [here](#node--npm)

## Azure CLI

To authenticate connecting to the Azure Service, you need Azure CLI. If you already have this installed go to [Azure authentication](#azure-authentication).  
If you don't already have it installed, follow [these instructions](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli).

To confirm Azure CLI is installed, you can run:

```
az --version
```

The output should be something like this, but you **don't** need to have these exact versions:

```
azure-cli                         2.34.1 *

core                              2.34.1 *
telemetry                          1.0.6

Dependencies:
msal                              1.16.0
azure-mgmt-resource               20.0.0
```

### Azure authentication

To authenticate run:

```
az login
```

This will open up a webbrowser where you login to Azure. After successfully logging in, you should see your user object in the terminal.

## Downloading the project

If you have Git bash installed, open up git bash and run:

```
git clone https://github.com/moadav/DigitalTwin
```

If you dont have Git bash or other git cloning program:

- Go to the [DigitalTwin repo](https://github.com/moadav/DigitalTwin) and click **Code** > **Download ZIP**  
  ![image](https://user-images.githubusercontent.com/4765250/165159498-abefbe52-9d00-441b-a762-3d962277d463.png)
- Extract the zipped folder to your desired location.

## Setup & config

### Authenticating Azure Maps

- Open up the cloned/downloaded project in any editor of your choice.
- Navigate to [`DigitalTwin/AzureMaps/auth_template.js`](auth_template.js) and rename this file(or make a new file) to `auth.js`.
- Make sure `auth.js` is in the same folder: `DigitalTwin/AzureMaps/auth.js`.

This file has an object called `auth`, which is for the **Azure Maps** resource.  
![image](https://user-images.githubusercontent.com/4765250/165597885-0c6521ac-160c-4a1e-b177-a6504362696e.png)

- Replace `<token>` with the subscription key from the **Azure Maps** resource _dthiofmaps_ > _Authentication_ > **Primary Key**.

### Authenticating ML Studio

- Navigate to [`DigitalTwin/AzureMaps/server/mlauth_template.js`](server/mlauth_template.js) and rename this file(or make a new file) to `mlauth.js`. The object `mlStudioEndpoint` is for the _available-bikes-endpoint_ in **ML Studio**.
  ![image](https://user-images.githubusercontent.com/4765250/165611904-c54972f1-825e-40f7-8b5f-80363311c1e6.png)

- Go to the _REST endpoint_ from _Hiof-ML-Studio_ > _Endpoints_ > **available-bikes-endpoint** > _Consume_.
- Then replace `<token>` with **Primary Key** under _Authentication_ for this endpoint. Make sure you leave 'Bearer' as is, in front of the primary key.

### API endpoint installation

Now all necessary authentication to Azure is set up, next you will install the node packages for the local API endpoint.

1. Open the project up in a terminal, and change working directory to the _server_ folder `DigitalTwin\AzureMaps\server`.
   - if you are currently in `DigitalTwin`, run: **cd AzureMaps/server**
2. Install node packages with:

```
npm install
```

After installation is done, make sure you now have a `node_modules` folder inside the server folder.  
![2022-04-26](https://user-images.githubusercontent.com/4765250/165314712-2b46c92a-3b20-4a0c-976f-7b032bff0801.png)

## Running the project

The setup and installation for your system is now complete. You will now run the API endpoint server, and then open [index.html](index.html) in your browser. Keep in mind the API endpoint server needs to run in the background when you are using the client.

### Starting API endpoint server

In order to run the project, first run the server with Nodejs. Open up a terminal and navigate to `DigitalTwin\AzureMaps\server` (same as [the one above](#api-endpoint-installation)), and run:

```
node index.js
```

You should see the response:

```
Server started on port 8080.
```

Troubleshooting starting the API server [here](#node-server--api-endpoint)

### Running client in your browser

All you need to do now is open up `index.html` in your browser, located in `DigitalTwin/AzureMaps/index.html`.

If you see this you have set up everything correctly.  
![image](https://user-images.githubusercontent.com/4765250/165337381-0eae4d4f-338e-4b25-ac0d-6143e5025065.png)

If you don't see this, troubleshooting [here](#indexhtml)

Done :)

# Troubleshooting

## Node / npm

```
'node'/'npm' is not recognized as an internal or external command
```

- Go to **Environmental Variables** in windows
  - _For Node_: make sure you have `C:\Program Files\nodejs\` as a 'PATH' under 'System variables'.
  - _For npm_: make sure you have `C:\Users\<username>\AppData\Roaming\npm` under 'User variables'.

Npm is running wrong version, force version `8.5.5` with:

```
npm install npm@8.5.5 -g
```

## Running the project

### Node server / API endpoint

`Error: Cannot find module '<module name>'`

- The packages was not correctly installed. Do [this step](#api-endpoint-installation) again.

### index.html

The website is missing temperatures, and looks like this:  
![image](https://user-images.githubusercontent.com/4765250/165340174-b2a35995-2d59-45ad-ba03-692ad800d1ad.png)

- This means the Node server / API endpoint is either not running, haven't loaded yet, or not set up correctly.
- Open http://localhost:8080/weather
  - if you see JSON data, refresh the web page `index.html`
  - if you see `ERR_CONNECTION_REFUSED`, the server is not running. Go to [starting API endpoint server](#starting-api-endpoint-server)
