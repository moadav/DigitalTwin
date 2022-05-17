/**
 * This is a template for authenticating towards Azure Maps
 * It's important the filename is 'auth.js' so duplicate this file and rename it.
 */

/* Azure maps resource
  - Use 'Primary Key' from your Azure Map resource.
*/
const auth = {
  authType: "subscriptionKey",
  subscriptionKey: "<token>",
}

function getAuth() {
  return auth
}
