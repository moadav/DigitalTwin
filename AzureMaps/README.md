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

## Visual Studio Code

Download VS Code [here](https://code.visualstudio.com/download).  
In order to run Azure Maps, you need to run it on a server, therefore the quickest and easiest way is the Live Server Extension for VS Code.

- Download **Live Server** extension [here](https://marketplace.visualstudio.com/items?itemName=ritwickdey.LiveServer).

After installing the extension in VS Code, confirm it's installed after seeing a **Go Live** button in the bottom right corner.
![go-live-live-server](https://user-images.githubusercontent.com/4765250/165154093-ab931bcb-cdca-45e2-b61a-771b230145f1.JPG)

- If you don't have this button: Troubleshooting VS Code Live Server [here](#node--npm)

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

- Open up the cloned/downloaded project in VS Code (or any editor of your choice).
  - File > Open Folder
- Navigate to [`DigitalTwin/AzureMaps/auth_template.js`](auth_template.js) and rename this file(or make a new file) to `auth.js`.
- Make sure `auth.js` is in the same folder: `DigitalTwin/AzureMaps/auth.js`.

This file has two objects: `auth` and `mlStudioEndpoint`:  
![image](https://user-images.githubusercontent.com/4765250/165169307-55199aeb-88e5-4515-a526-e74016cb4bac.png)

`auth` is for **Azure Maps** resource, and `mlStudioEndpoint` is for the _available-bikes-endpoint_ in **ML Studio**.

- In `auth` you need to type the subscription key from the **Azure Maps** resource _dthiofmaps_ > _Authentication_ > **Primary Key**.
- In `mlStudioEndpoint` you need to replace `<api endpoint url>` with the _REST endpoint_ from _Hiof-ML-Studio_ > _Endpoints_ > **available-bikes-endpoint** > _Consume_.
- Then replace `<token>` with **Primary Key** under _Authentication_ for the same endpoint. Make sure you leave 'Bearer' as is, in front of the primary key.

### API endpoint installation

Now all necessary authentication to Azure is set up, next you will intall the node packages for the local API endpoint.

1. Open the project up in a terminal, and change working directory to the _server_ folder `DigitalTwin\AzureMaps\server`.
   - if you are currently in `DigitalTwin`, run: **cd AzureMaps/server**
2. Install node packages with:

```
npm install
```

After installation is done, make sure you now have a `node_modules` folder inside the server folder.  
![2022-04-26](https://user-images.githubusercontent.com/4765250/165314712-2b46c92a-3b20-4a0c-976f-7b032bff0801.png)

## Running the project

The setup and installation for your system is now complete. You will now run the API endpoint server, and then _Live Server_ as the client. Keep in mind the API endpoint server needs to run in the background when you are using the client.

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

### Running client with Live Server

- In VS Code, open up the project and go to **index.html** located: `DigitalTwin\AzureMaps\index.html`.
- While **index.html** is selected in VS Code, click **Go Live** in the bottom right corner.
  ![image](https://user-images.githubusercontent.com/4765250/165324087-9f480558-5371-44e8-a335-09755657f2ec.png)

A web browser should now open with the project running. If you see this you have set up everything correctly.  
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

## VS Code Live Server

Visual Stuio Marketplace is not opening up VS Code extension page, or redirecting correctly:

- Navigate to _View > Extensions_ **or** open extensions with _Ctrl+Shift+X_.
- Search `Live Server` in the search field. Click on _Live Server_ by _Ritwick Dey_.  
  ![image](https://user-images.githubusercontent.com/4765250/165156520-d15b94f7-aa73-4a6f-a99f-39d60d9c567d.png)
- Click install
- If prompted to reload, click 'Reload'.
- You should now see a 'Go Live' button in bottom right corner.

## Running the project

### Node server / API endpoint

`Error: Cannot find module '<module name>'`

- The packages was not correctly installed. Do [this step](#api-endpoint-installation) again.

### index.html

When clicking **Go Live** I see a directory in the browser like so:  
![image](https://user-images.githubusercontent.com/4765250/165320969-92968366-eff5-4eeb-80cf-56c22467ec09.png)

- Click the button again to stop the **Live Server**  
  ![image](https://user-images.githubusercontent.com/4765250/165321506-2476a490-94fb-401b-9c00-c98e60c8b273.png)
- Open up index.html again located: `DigitalTwin\AzureMaps\index.html`, and make sure this file is in focus when you click **Go Live** again.

The web browser is not opening automaticly when clicking **Go Live**

- If _Live Server_ is running, the 'Go Live' button should now say **Port: &lt;number&gt;**
- Manually open the running client in your browser after replacing the port number `http://localhost:<port>/AzureMaps/index.html`

The website is missing temperatures, and looks like this:  
![image](https://user-images.githubusercontent.com/4765250/165340174-b2a35995-2d59-45ad-ba03-692ad800d1ad.png)

- This means the Node server / API endpoint is not running, or set up correctly.
- Open http://localhost:8080/weather
  - if you see JSON data, restart the Live Server on index.html
  - if you see `ERR_CONNECTION_REFUSED`, go to [starting API endpoint server](#starting-api-endpoint-server)
