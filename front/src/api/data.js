import { authAPI } from '@/config/axios/index';
import { notAuthAPI } from '@/config/axios/index';
import { createAuthAPI } from '@/config/axios/index';
import { deleteAuthAPI } from '@/config/axios/index';

// Functions
function login(data) {
  return notAuthAPI({
    method: 'POST',
    url: `/users/login`,
    data: data,
  });
}

function getUsers() {
  return authAPI({
    method: 'GET',
    url: `/users`,
  });
}

function getRecipes() {
  return notAuthAPI({
    method: 'GET',
    url: `/recipes`,
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

export {
  login,

  getUsers,

  createAuthAPI,
  deleteAuthAPI,
  
  getRecipes,

  createFood,
  getFoods,
  getFood,
};