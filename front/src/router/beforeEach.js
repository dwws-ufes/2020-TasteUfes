import { store } from '@/auth';

export default async (to, from, next) => {
  document.title = `${to.name} - TasteUfes`;
  next();
}