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

app.listen(PORT, () => console.log(`Server started on port ${PORT}.`));




async function getDigitalTwin(id) {
    let items = client.queryTwins(id).byPage();

    let items2 = await toArray(items);

    let test = {
        test: items2[0].value[0].weather.KlimaInfo.Symbole_code
    }

    //return items2[0];
    //return items2[0].value[0].weather.KlimaInfo ;
    //return test;
    return items2[0];
}
