import axios from 'axios';

// --- Config
var AUTH = false;

// Common
axios.defaults.headers.common['Content-Type'] = 'application/json';
axios.defaults.headers.common.Accept = 'application/json';
let access_token = sessionStorage.getItem("access_token");
if(access_token != null){
  axios.defaults.headers.common['Authorization'] = 'Bearer ' + access_token;
  AUTH = true;
}

// --- Instances

// TasteUfes API
const tasteUfesAPI = axios.create({
  baseURL: 'https://localhost:5001/api/v1/',
});

// --- Export
export {
  AUTH,
  tasteUfesAPI,
};