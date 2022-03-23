const { DigitalTwinsClient, ServiceClientCredentials } = require("@azure/digital-twins-core");
const { DefaultAzureCredential } = require("@azure/identity");
const { toArray } = require("ix/asynciterable");

const dt_link = "https://dthiofadt.api.weu.digitaltwins.azure.net";
const client = new DigitalTwinsClient(dt_link, new DefaultAzureCredential());


// "SELECT * FROM digitaltwins WHERE $dtId = 'Nordstrand'"
let test3 = client.queryTwins("SELECT * FROM digitaltwins").byPage();
let test4 = client.queryTwins("SELECT * FROM digitaltwins WHERE $dtId = 'Nordstrand'")

async function getDigitalTwin(id) {
    let items = client.queryTwins(id).byPage();

    let items2 = await toArray(items);
    
    return items2[0].value;
}

getDigitalTwin("SELECT * FROM digitaltwins WHERE $dtId = 'Nordstrand'").then( r => {
    console.log(r)
});





/*
(async () => {
    let items = await toArray(test3);

    console.log(items[0].value[1].weather.KlimaInfo.Air_info);
})();
*/
