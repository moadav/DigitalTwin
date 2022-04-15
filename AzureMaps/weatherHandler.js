const svgDirectory = "./weatherIcons/svg/"
// sec between auto updates
const autoUpdateSec = 60 * 15
let addDistricts = true

const updateWeather = async () => {
  fetch("http://localhost:8080/weather")
    .then(async (res) => {
      const body = await res.json()
      //   console.log(body)
      // set average temperature
      document.getElementById("avgDegrees").innerHTML = body.average_temperature
      document.getElementById("avgWeatherIcon").src = `${
        svgDirectory + body.average_symbol
      }.svg`

      // generate districts ONCE 
      if (addDistricts) generateDistrictsDom(body.districts)

      // update district values
      updateDistricts(body.districts)
    })
    .catch(console.error)
}

/* Generate dom elements and append them to the weather section list
 * Template: 
 * <li>
 *  <span class="stedNavn">Grunerlokka</span>
 *  <img class="weatherIcon" />
 *  <div class="temperature">
 *    <span class="degrees">69.9</span>
 *    <span class="celcius">&deg;C</span>
 *  </div>
 * </li> 
*/
const generateDistrictsDom = (districts = []) => {
  addDistricts = false

  // container to put all <li> into
  const listBody = document.getElementById("weatherDistricts")

  for (const district of districts) {
    const { name, temperature, weatherSymbol } = district

    // list item container
    const liContainer = document.createElement("li")
    liContainer.id = name

    // district name
    const districtName = document.createElement("span")
    districtName.className = "stedNavn"
    districtName.innerHTML = name
    liContainer.appendChild(districtName)

    // weather symbol
    const weatherIcon = document.createElement("img")
    weatherIcon.className = "weatherIcon"
    liContainer.appendChild(weatherIcon)

    // container for temperature
    const tempContainer = document.createElement("div")
    tempContainer.className = "temperature"

    const degrees = document.createElement("span")
    degrees.className = "degrees"
    tempContainer.appendChild(degrees)

    const celcius = document.createElement("span")
    celcius.className = "celcius"
    celcius.innerHTML = "&deg;C"
    tempContainer.appendChild(celcius)

    liContainer.appendChild(tempContainer)

    // add list container to list body
    listBody.appendChild(liContainer)
  }
}

// update districts temperature and weatherIcon
const updateDistricts = (districts = []) => {
  for (const district of districts) {
    const { name, temperature, weatherSymbol } = district

    const liContainer = document.getElementById(name)

    const weatherIcon = liContainer.querySelector("img.weatherIcon")
    weatherIcon.src = `${svgDirectory + weatherSymbol}.svg`

    const degrees = liContainer.querySelector("div > span.degrees")
    degrees.innerHTML = temperature
  }
}

//template
{
  
}

// update weather on page load
window.addEventListener("load", (event) => {
  updateWeather()
})

// update weather every n seconds
setInterval(() => {
  updateWeather()
}, autoUpdateSec * 1000)
