import { authAPI, notAuthAPI, createAuthAPI, deleteAuthAPI } from '@/config/axios/index';
import axios from 'axios';

// Functions
async function healthCheck() {
  return await axios.create({
    method: 'GET',
    url: process.env.VUE_HEALTH_CHECK,
  });
}

// --- User ---
async function login(data) {
  return await notAuthAPI({
    method: 'POST',
    url: `/users/login`,
    data: data,
  });
}

async function updatePassword(id, passwords) {
  return await authAPI({
    method: 'PUT',
    url: `/users/${id}/update-password`,
    data: passwords,
  });
}

async function registerUser(user) {
  return await notAuthAPI({
    method: 'POST',
    url: `/users`,
    data: user,
  });
}

async function createUser(user) {
  return await authAPI({
    method: 'POST',
    url: `/users`,
    data: user,
  });
}


async function getUsers() {
  return await authAPI({
    method: 'GET',
    url: `/users`,
  });
}

async function getUser(id) {
  return await authAPI({
    method: 'GET',
    url: `/users/${id}`,
  });
}

async function updateUser(id, user) {
  return await authAPI({
    method: 'PUT',
    url: `/users/${id}`,
    data: user,
  });
}

async function deleteUser(id) {
  return await authAPI({
    method: 'DELETE',
    url: `/users/${id}`,
  });
}

async function getRoles() {
  return await authAPI({
    method: 'GET',
    url: `/roles`,
  });
}

async function refreshAuthentication(data) {
  return await notAuthAPI({
    method: 'POST',
    url: `/users/refresh-token`,
    data: data,
  });
}

// --- Recipe ---
async function createRecipe(recipe) {
  return await authAPI({
    method: 'POST',
    url: `/recipes`,
    data: recipe,
  });
}

async function getRecipes() {
  return await notAuthAPI({
    method: 'GET',
    url: `/recipes`,
  });
}

async function getRecipe(id) {
  return await notAuthAPI({
    method: 'GET',
    url: `/recipes/${id}`,
  });
}

async function updateRecipe(id, recipe) {
  return await authAPI({
    method: 'PUT',
    url: `/recipes/${id}`,
    data: recipe,
  });
}

async function deleteRecipe(id) {
  return await authAPI({
    method: 'DELETE',
    url: `/recipes/${id}`,
  });
}

// --- Foods ---
async function createFood(food) {
  return await authAPI({
    method: 'POST',
    url: `/foods`,
    data: food,
  });
}

async function getFoods() {
  return await notAuthAPI({
    method: 'GET',
    url: `/foods`,
  });
}

async function getFood(id) {
  return await notAuthAPI({
    method: 'GET',
    url: `/foods/${id}`,
  });
}

async function getRDFById(ids) {
  return await notAuthAPI({
    method: 'POST',
    url: `/foods/ld/rdf`,
    data: ids,
  });
}

async function getLDFood(name) {
  return await notAuthAPI({
    method: 'GET',
    url: `/foods/ld/${name}`,
  });
}

async function updateFood(id, food) {
  return await authAPI({
    method: 'PUT',
    url: `/foods/${id}`,
    data: food,
  });
}

async function deleteFood(id) {
  return await authAPI({
    method: 'DELETE',
    url: `/foods/${id}`,
  });
}

// --- Nutrients ---
async function getNutrients() {
  return await authAPI({
    method: 'GET',
    url: `/nutrients`,
  });
}


// --- Services --- 
async function recalculatePerServing(recipeId, serving) {
  return await notAuthAPI({
    method: 'GET',
    url: `/recipes/${recipeId}/recalculate-per-servings/${serving}`,
  });
}


async function calculateAnonymous(recipe) {
  return await notAuthAPI({
    method: 'POST',
    url: `/recipes/calculate-anonymous`,
    data: recipe,
  });
}

async function recommendationByFoods(foods) {
  return await notAuthAPI({
    method: 'POST',
    url: `/recipes/recommend-by-foods`,
    data: foods,
  });
}

export {
  healthCheck,

  login,
  updatePassword,
  registerUser,

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
  getRDFById,
  getLDFood,
  updateFood,
  deleteFood,

  getNutrients,
  recalculatePerServing,
  calculateAnonymous,
  recommendationByFoods,
};