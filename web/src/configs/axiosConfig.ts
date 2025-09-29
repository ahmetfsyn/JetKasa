import axios from "axios";

export const BASE_API_URL_DEV = "http://localhost:8080";
export const BASE_API_URL_PROD =
  "https://resilient-tranquility-production.up.railway.app";
export const IS_PROD = false;

export const api = axios.create({
  baseURL: IS_PROD ? BASE_API_URL_PROD : BASE_API_URL_DEV,
  headers: {
    "Content-Type": "application/json",
  },
});
