// copy of origital stations
const _STATIONS = stations.features.slice()
// set delay on input
let inputDelay = 250
let filterTimeout

// update weather on page load
window.addEventListener("load", async (event) => {
  await updateStations(stations)
  // update range of slider
  const slider = document.getElementById("range_available_bikes")

  stations.features.forEach((station) => {
    station.properties.available_bikes > slider.max
      ? (slider.max = station.properties.available_bikes)
      : null
  })
})

// makes a copy of the original array after filtering and applies this as datasource for atlas
const filterMinAvailable = () => {
  // update output value
  const sliderVal = document.getElementById("range_available_bikes").value
  document.getElementById("available_bikes_output").innerHTML = sliderVal

  // make sure atlas datasource is loaded first
  if (!datasource) {
    console.error(`atlas datasource not found. datasource=${datasource}`)
    return
  }

  // clear timeout in case it's already called
  clearTimeout(filterTimeout)
  filterTimeout = setTimeout(() => {
    // filter on available bikes
    stations.features = _STATIONS.filter(
      (station) => station.properties.available_bikes >= sliderVal
    )

    // refresh datasource
    datasource.clear()
    datasource.add(stations.features)
  }, inputDelay)
}
