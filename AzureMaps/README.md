# Instructions on how to run Azure Maps solution

This will guide you through running an express server as API endpoint for the client, and then the client itself - retrieving data from the local API endpoint. Resulting in displaying data from the Azure Digital Twin, incorporated into Azure Maps.

**!NB: All commands can run in cmd or powershell**

## Necessary programs:

---

### Node.js / npm

Download Node.js [here](https://nodejs.org/en/) which also includes npm.

Confirm install with

```
node --version
npm --version
```

You should now see Node version `v16.13.2` (or higher) and npm version `8.5.5` (or higher)

- Troubleshooting Node / npm [here](#node--npm)

### Visual Studio Code

Download vs code [here](https://code.visualstudio.com/download).  
In order to run Azure Maps, you need to run it on a server, therefore the quickest and easiest way is the Live Server Extension for VS Code.

- Live Server extension download [here](https://marketplace.visualstudio.com/items?itemName=ritwickdey.LiveServer).

After installing the extension in VS Code, you should see a **Go Live** button in the bottom right corner.
![go-live-live-server](https://user-images.githubusercontent.com/4765250/165154093-ab931bcb-cdca-45e2-b61a-771b230145f1.JPG)

- Troubleshooting VS Code Live Server [here](#node--npm)

### Azure CLI

To authenticate connecting to the Azure Service, you need Azure CLI. If you don't already have it installed, follow [these instructions](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli).

If you already have this installed go to [Azure authentication](#azure-authentication).

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

### Downloading the project

If you have Git bash installed, open up git bash and run:

```
git clone https://github.com/moadav/DigitalTwin
```

If you dont have Git bash or other git cloning program:

- Go to the [DigitalTwin repo](https://github.com/moadav/DigitalTwin) and click **Code** > **Download ZIP**  
  ![image](https://user-images.githubusercontent.com/4765250/165159498-abefbe52-9d00-441b-a762-3d962277d463.png)
- Extract the zipped folder to your desired location.

### Setup & first run

Disclaimer: It is not strictly nessesary to use VS Code for these next steps until you will run the client. until

- Open up the cloned/downloaded project in VS Code.
  - File > Open Folder
- Navigate to [`DigitalTwin/AzureMaps/auth_template.js`](auth_template.js) and rename this file(or make a new file) to `auth.js`.

This file has two objects: `auth` and `mlStudioEndpoint`:  
![image](https://user-images.githubusercontent.com/4765250/165169307-55199aeb-88e5-4515-a526-e74016cb4bac.png)

`auth` is for **Azure Maps** resource, and `mlStudioEndpoint` is for the _available-bikes-endpoint_ in **ML Studio**.

- In `auth` you need to type the subscription key from the **Azure Maps** resource _dthiofmaps_ > _Authentication_ > **Primary Key**.
- In `mlStudioEndpoint` you need to replace `<api endpoint url>` with the _REST endpoint_ from _Hiof-ML-Studio_ > _Endpoints_ > **available-bikes-endpoint** > _Consume_.
- Then replace `<token>` with **Primary Key** under _Authentication_ for the same endpoint. Make sure you leave 'Bearer' as is, in front of the primary key.

Now all authentication to Azure is set up, next you will intall the packages for the local API endpoint.

- Open the project up in a terminal. If you are using VS Code

## Troubleshooting

### Node / npm

```
'node'/'npm' is not recognized as an internal or external command
```

- Go to **Environmental Variables**
  - _For Node_: make sure you have `C:\Program Files\nodejs\` as a 'PATH' under 'System variables'.
  - _For npm_: make sure you have `C:\Users\<username>\AppData\Roaming\npm` under 'User variables'.

Npm is running wrong version, force version `8.5.5` with:

```
npm install npm@8.5.5 -g
```

### VS Code Live Server

Visual Stuio Marketplace is not opening up VS Code extension page, or redirecting correctly:

- Navigate to _View > Extensions_ **or** open extensions with _Ctrl+Shift+X_.
- Search `Live Server` in the search field. Click on _Live Server_ by _Ritwick Dey_.  
  ![image](https://user-images.githubusercontent.com/4765250/165156520-d15b94f7-aa73-4a6f-a99f-39d60d9c567d.png)
- Click install
- If prompted to reload, click 'Reload'.
