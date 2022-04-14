async function updateStations(bikeStations) {

    await fetch('http://localhost:8080/bikestations')
        .then(response => {
            return response.json();
        })
        .then(data => {
            updateBikeStations(bikeStations, data);
        })
        .catch(error => {
            console.log(error);
        });
}

function updateBikeStations(bikeStations, data) {
    // bikeStations = stations.js (geojson object)
    // data = data from digital twin (real time data)

    // convert data from digital twin to map
    let bikeStationsMap = new Map();

    data.forEach(object => {
        bikeStationsMap.set(object.Station_information.Station_Id, object);
    });

    // update values in geojson array
    bikeStations.features.forEach(station => {
        let stationId = parseInt(station.properties.station_id);
        let stationDataObject = bikeStationsMap.get(stationId);

        let availableBikes = stationDataObject.Bicycle_Status.Station_Status.Bicycle_Available.Num_Bikes_Available;
        let availableDocks = stationDataObject.Bicycle_Status.Station_Status.Bicycle_Available.Num_Docks_Available;

        station.properties.available_bikes = availableBikes;
        station.properties.available_docks = availableDocks;
    })

}
