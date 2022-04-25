/* Azure maps resource
  - Copy code to 'auth.js'
  - Use 'Primary Key' from your Azure Map resource.
*/
const auth = {
  authType: "subscriptionKey",
  subscriptionKey: "",
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
