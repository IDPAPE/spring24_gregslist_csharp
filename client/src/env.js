export const dev = window.location.origin.includes('localhost')

// NOTE don't forget to change your baseURL if using the dotnet template
export const baseURL = dev ? 'https://localhost:7045' : ''
export const useSockets = false

// TODO change these variables out to your own auth after cloning!
export const domain = 'codeworksclassroom.auth0.com'
export const clientId = 'pOXw2OGv1LsYi7LEBmDF04RLkXQvldml'
export const audience = 'https://codeworksclassroom.com'