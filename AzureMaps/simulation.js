async function testGetMLStudioResults() {
  let stationName, day, time, temperature

  let resultsDiv = document.getElementById("simulation_result")

  stationName = document.getElementById("myInput").value
  day = document.getElementById("select_day").value
  time = document.getElementById("select_hours").value
  temperature = document.getElementById("weather_temp").value

  let stationId = getStationId(stationName)
  let validated = formValidation(stationName, day, time, temperature)

  if (validated) {
    let body = {
      stationId: stationId,
      day: day,
      time: time,
      temperature: temperature,
    }

    let options = {
      method: "POST",
      body: JSON.stringify(body),
      headers: {
        "Content-Type": "application/json",
      },
    }

    await fetch("http://localhost:8080/mlstudio", options)
      .then((response) => {
        return response.json()
      })
      .then((data) => {
        let results = data.Results.WebServiceOutput0[0]["Scored Labels"]
        resultsDiv.innerHTML = `Estimated number of available bikes: <br/> ${Math.round(
          results
        )}`
      })
      .catch((error) => {
        console.log(error)
      })
  } else {
    resultsDiv.innerHTML = ""
  }
}

function formValidation(stationName, day, time, temperature) {
  let errorDiv = document.getElementById("form_error")
  let errorMessage = ""

  if (stationName && day && time && temperature) {
    errorDiv.innerHTML = errorMessage
    return true
  }

  errorMessage = "Please fill out all fields."
  errorDiv.innerHTML = errorMessage
  return false
}

function getStationId(stationName) {
  let id

  bikeStations.features.forEach((station) => {
    if (stationName === station.properties.name) {
      id = station.properties.station_id
      return id
    }
  })

  return id
}
