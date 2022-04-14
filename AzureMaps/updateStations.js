async function updateStations(bikeStations) {

    await fetch('http://localhost:8080/bikestations')
        .then(response => {
            return response.json();
        })
        .then(data => {
            console.log('-------------------------------------------')
            console.log('data from /bikestations (direct from digital twin)...')
            console.log(data)
            console.log('-------------------------------------------')
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

    // extract array from geojson
    let bikeStationsArray = [...bikeStations.features];

    //console.log(bikeStationsArray[0]);
    //console.log(bikeStationsArray[0].properties)
    //console.log(typeof bikeStationsArray[0].properties.station_id);
    //console.log(typeof parseInt(bikeStationsArray[0].properties.station_id));

    let testStationId = parseInt(bikeStationsArray[0].properties.station_id);
    let testMap = bikeStationsMap.get(testStationId);
    //console.log(testMap);
    //console.log(testMap.Bicycle_Status.Station_Status.Bicycle_Available.Num_Bikes_Available);

    // update values in geojson array
    //bikeStationsArray.forEach(station => {
    bikeStations.features.forEach(station => {
        let stationId = parseInt(station.properties.station_id);
        let stationDataObject = bikeStationsMap.get(stationId);

        let availableBikes = stationDataObject.Bicycle_Status.Station_Status.Bicycle_Available.Num_Bikes_Available;
        let availableDocks = stationDataObject.Bicycle_Status.Station_Status.Bicycle_Available.Num_Docks_Available;

        station.properties.available_bikes = availableBikes;
        station.properties.available_docks = availableDocks;
    })


}
