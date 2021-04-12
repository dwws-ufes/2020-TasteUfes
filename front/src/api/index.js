import { authAPI, notAuthAPI, createAuthAPI, deleteAuthAPI } from '@/config/axios/index';

// Functions

// --- User ---
function login(data) {
  return notAuthAPI({
    method: 'POST',
    url: `/users/login`,
    data: data,
  });
}

function createUser(user) {
  return authAPI({
    method: 'POST',
    url: `/users`,
    data: user,
  });
}

function getUsers() {
  return authAPI({
    method: 'GET',
    url: `/users`,
  });
}

function getUser(id) {
  return authAPI({
    method: 'GET',
    url: `/users/${id}`,
  });
}

function updateUser(id, user) {
  return authAPI({
    method: 'PUT',
    url: `/users/${id}`,
    data: user,
  });
}

function deleteUser(id) {
  return authAPI({
    method: 'DELETE',
    url: `/users/${id}`,
  });
}

function getRoles(id) {
  return authAPI({
    method: 'GET',
    url: `/roles`,
  });
}

function refreshAuthentication(data) {
  return notAuthAPI({
    method: 'POST',
    url: `/users/refresh-token`,
    data: data,
  });
}

// --- Recipe ---
function createRecipe(recipe) {
  return authAPI({
    method: 'POST',
    url: `/recipes`,
    data: recipe,
  });
}

function getRecipes() {
  return notAuthAPI({
    method: 'GET',
    url: `/recipes`,
  });
}

function getRecipe(id) {
  return notAuthAPI({
    method: 'GET',
    url: `/recipes/${id}`,
  });
}

function updateRecipe(id, recipe) {
  return authAPI({
    method: 'PUT',
    url: `/recipes/${id}`,
    data: recipe,
  });
}

function deleteRecipe(id) {
  return authAPI({
    method: 'DELETE',
    url: `/recipes/${id}`,
  });
}

// --- Foods ---
function createFood(food) {
  return authAPI({
    method: 'POST',
    url: `/foods`,
    data: food,
  });
}

function getFoods() {
  return authAPI({
    method: 'GET',
    url: `/foods`,
  });
}

function getFood(id) {
  return authAPI({
    method: 'GET',
    url: `/foods/${id}`,
  });
}

function updateFood(id, food) {
  return authAPI({
    method: 'PUT',
    url: `/foods/${id}`,
    data: food,
  });
}

function deleteFood(id) {
  return authAPI({
    method: 'DELETE',
    url: `/foods/${id}`,
  });
}

// --- Nutrients ---
function getNutrients() {
  return authAPI({
    method: 'GET',
    url: `/nutrients`,
  });
}


export {
  login,

  createUser,
  getUsers,
  getUser,
  updateUser,
  deleteUser,
  getRoles,
  refreshAuthentication,

  createAuthAPI,
  deleteAuthAPI,

  createRecipe,
  getRecipes,
  getRecipe,
  updateRecipe,
  deleteRecipe,

  createFood,
  getFoods,
  getFood,
  updateFood,
  deleteFood,

  getNutrients,
};