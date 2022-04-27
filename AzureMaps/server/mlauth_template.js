/**
 * This is a template for authenticating server towards ML Studio 
 * It's important the filename is 'mlauth.js' and located in the 'server' folder, so duplicate this file and rename it.
 */

/* ML Studio endpoint
  - Use 'Primary Key' from the endpoint, leave 'Bearer ' as prefix. 
*/
const mlStudioEndpoint = {
  apiEndpoint: "http://6547b2ed-59b8-4910-8a21-5dc030d5ee9f.westeurope.azurecontainer.io/score",
  authentication: "Bearer <token>",
}
module.exports = { mlStudioEndpoint }
