FROM node:14-alpine AS builder
WORKDIR /ui

COPY . ./
COPY package.json ./

RUN npm install