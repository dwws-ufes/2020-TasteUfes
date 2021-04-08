import axios from 'axios';

// --- Config

// Common
axios.defaults.headers.common['Content-Type'] = 'application/json';
axios.defaults.headers.common.Accept = 'application/json';
let access_token = localStorage.getItem("access_token");
if (access_token != null) {
  createAuthAPI(access_token);
}

// --- Instances

// TasteUfes API

var authAPI;

const notAuthAPI = axios.create({
  baseURL: 'https://localhost:5001/api/v1/',
});

function createAuthAPI(access_token) {
  authAPI = axios.create({
    baseURL: 'https://localhost:5001/api/v1/',
    headers: {
      Authorization: 'Bearer ' + access_token
    }
  });
}

function deleteAuthAPI() {
  authAPI = null;
}

// --- Export
export {
  createAuthAPI,
  deleteAuthAPI,
  authAPI,
  notAuthAPI,
};