/**
 * This is a template for authenticating server towards ML Studio & Azure Maps key
 * It's important the filename is 'auth.js' so duplicate this file and rename it.
 */

/* Azure maps resource
  - Use 'Primary Key' from your Azure Map resource.
*/
const auth = {
  authType: "subscriptionKey",
  subscriptionKey: "<key>",
}

/* ML Studio endpoint
  - Use 'Primary Key' from the endpoint, leave 'Bearer ' as prefix. 
*/
const mlStudioEndpoint = {
  apiEndpoint: "<api endpoint url>",
  authentication: "Bearer <token>",
}

function getAuth() {
  return auth
}
module.exports = { mlStudioEndpoint }
