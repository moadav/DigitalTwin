const { DigitalTwinsClient } = require("@azure/digital-twins-core");
const { DefaultAzureCredential } = require("@azure/identity");
const { toArray } = require("ix/asynciterable");

const express = require('express');
const cors = require('cors');
const app = express();
const PORT = 8080;

const dt_link = "https://dthiofadt.api.weu.digitaltwins.azure.net";
const client = new DigitalTwinsClient(dt_link, new DefaultAzureCredential);

app.use( express.json() )
app.use( cors() );


app.get('/digitaltwin', async (req, res) => {
    
    let dt = await getDigitalTwin("SELECT * FROM digitaltwins WHERE $dtId = 'Nordstrand'");

    res.status(200).send(dt.value[0])

});


app.get('/bikestations', async (req, res) => {

    let bikeStations = await getDigitalTwin2("SELECT * FROM digitaltwins WHERE IS_OF_MODEL('dtmi:oslo:sykkler:sykkel;1')");
    
    let bikeStationsArray = [...bikeStations[0].value, ...bikeStations[1].value, ...bikeStations[2].value]
    

    /*
    let bikeStationsMap = new Map();

    bikeStationsArray.forEach(object => {
        bikeStationsMap.set(object.$dtId, object);
    });

    console.log(bikeStationsMap);
    */



    res.status(200).send(bikeStationsArray);
})




app.listen(PORT, () => console.log(`Server started on port ${PORT}.`));




async function getDigitalTwin(id) {
    let items = client.queryTwins(id).byPage();

    let items2 = await toArray(items);

    return items2[0];
}

async function getDigitalTwin2(id) {
    let digitalTwinResponse = client.queryTwins(id).byPage();
    let digitalTwinArray = await toArray(digitalTwinResponse);

    return digitalTwinArray;
    
}
