const { DigitalTwinsClient } = require("@azure/digital-twins-core")
const { DefaultAzureCredential } = require("@azure/identity")

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

app.listen(PORT, () => console.log(`Server started on port ${PORT}.`))

const getWeather = async () => {
  const query = "SELECT * FROM digitaltwins WHERE $dtId = 'Frogner'"

  // the whole Frogner node
  const dtNode = (await client.queryTwins(query).next()).value

  const temperature = dtNode.weather.KlimaInfo.Air_info.Air_temperature
  const weatherSymbol = dtNode.weather.KlimaInfo.Symbole_code

  return { temperature, weatherSymbol }
}
