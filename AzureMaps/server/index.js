const { DigitalTwinsClient } = require("@azure/digital-twins-core")
const { DefaultAzureCredential } = require("@azure/identity")
const { toArray } = require("ix/asynciterable")
const axios = require("axios").default
const mlsEndpoint = require("./mlauth.js").mlStudioEndpoint

const express = require("express")
const cors = require("cors")
const app = express()
const PORT = 8080

const dt_link = "https://dthiofadt.api.weu.digitaltwins.azure.net"
const client = new DigitalTwinsClient(dt_link, new DefaultAzureCredential())

app.use(express.json())
app.use(cors())

// GET current weather values from twin
app.get("/weather", async (req, res) => {
  const weather = await getWeather()
  res.status(200).send(weather)
})

// returns weather for all bydeler/districts and the average temperature/symbol
const getWeather = async () => {
  const query =
    "SELECT * FROM digitaltwins WHERE IS_OF_MODEL('dtmi:omrade:sted;1')"

  const dtNodes = await client.queryTwins(query)

  let districts = []

  // loop though all districts and keep temp, coordinates and symbol
  let res = await dtNodes.next()
  while (!res.done) {
    const weatherSymbol = res.value.weather.KlimaInfo.Symbole_code
    const weatherObj = {
      name: res.value.Coordinates.StedNavn,
      coordinates: {
        lat: res.value.Coordinates.Lat,
        lon: res.value.Coordinates.Lon,
      },
      temperature: res.value.weather.KlimaInfo.Air_info.Air_temperature,
      weatherSymbol,
    }

    districts.push(weatherObj)
    res = await dtNodes.next()
  }

  // calculate average temperature
  const avgTemp = districts.reduce((t, val) => {
    return t + val.temperature / districts.length
  }, 0)

  // find weather symbol with highest occurrence
  let dArr = districts.slice().map((d) => d.weatherSymbol)
  const average_symbol = dArr
    .slice()
    .sort(
      (a, b) =>
        dArr.filter((v) => v === a).length - dArr.filter((v) => v === b).length
    )
    .pop()

  return { average_temperature: avgTemp.toFixed(1), average_symbol, districts }
}
// getWeather().then((r) => console.log(JSON.stringify(r, null, 2)))

// GET list of all bike stations
app.get("/bikestations", async (req, res) => {
  let bikeStations = await getDigitalTwin(
    "SELECT * FROM digitaltwins WHERE IS_OF_MODEL('dtmi:oslo:sykkler:sykkel;1')"
  )

  // let bikeStationsArray = [...bikeStations[0].value, ...bikeStations[1].value, ...bikeStations[2].value]

  res.status(200).send(bikeStations[0].value)
})

async function getDigitalTwin(id) {
  let digitalTwinResponse = client.queryTwins(id).byPage()
  let digitalTwinArray = await toArray(digitalTwinResponse)

  return digitalTwinArray
}

// GET Azure ML Studio results
app.post("/mlstudio", (req, res) => {
  let stationId = req.body.stationId
  let day = req.body.day
  let time = req.body.time + ":00:00"
  let temperature = req.body.temperature

  // make sure all fields exist
  if (!stationId || !day || !time || !temperature) {
    console.error("Missing input variables for ML studio")
    return
  }

  let data = {
    Inputs: {
      WebServiceInput0: [
        {
          station_id: stationId,
          time: day,
          air_temperature: temperature,
          time_of_day: time,
        },
      ],
    },
    GlobalParameters: {},
  }

  const options = {
    headers: {
      "Content-Type": "application/json",
      Authorization: mlsEndpoint.authentication,
    },
  }

  axios
    .post(mlsEndpoint.apiEndpoint, data, options)
    .then((response) => {
      res.status(200).send(response.data)
      //console.log(response.data.WebServiceInput0[0]);
    })
    .catch((error) => {
      console.log(error)
    })
})

app.listen(PORT, () => console.log(`Server started on port ${PORT}.`))
