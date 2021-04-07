import { tasteUfesAPI } from '@/config/axios/index';
import { AUTH } from '@/config/axios/index';

// Functions
function login(data) {
  return tasteUfesAPI({
    method: 'POST',
    url: `/users/login`,
    data: data,
  });
}

function getUsers() {
  return tasteUfesAPI({
    method: 'GET',
    url: `/users`,
  });
}

function getRecipes() {
  return tasteUfesAPI({
    method: 'GET',
    url: `/recipes`,
  });
}

// --- Foods ---
function createFood(food) {
  return tasteUfesAPI({
    method: 'POST',
    url: `/foods`,
    data: food,
  });
}

function getFoods() {
  return tasteUfesAPI({
    method: 'GET',
    url: `/foods`,
  });
}

function getFood(id) {
  return tasteUfesAPI({
    method: 'GET',
    url: `/foods/${id}`,
  });
}

export {
  AUTH,

  login,

  getUsers,
  
  getRecipes,

  createFood,
  getFoods,
  getFood,
};