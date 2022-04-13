const svgDirectory = "./weatherIcons/svg/"
// sec between auto updates
const autoUpdateSec = 60 * 5

const updateWeather = async () => {
  fetch("http://localhost:8080/weather")
    .then(async (res) => {
      const body = await res.json()
      //   console.log(body)
      document.getElementById("degrees").innerHTML = body.temperature
      document.getElementById("weatherIcon").src =
        svgDirectory + body.weatherSymbol + ".svg"
    })
    .catch(console.error)
}

// update weather on page load
window.addEventListener("load", (event) => {
  updateWeather()
})

// update weather every n seconds
setInterval(() => {
  updateWeather()
}, autoUpdateSec * 1000)
