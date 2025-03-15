# Brug Node.js som base image
FROM node:18 AS build

# Definer arbejdsmappe i containeren
WORKDIR /app

# Kopier package.json og package-lock.json (hvis du har én) til containeren
COPY ./calculator-frontend-main/package*.json ./

# Installer afhængigheder
RUN npm install

# Kopier hele frontend-koden ind i containeren
COPY ./calculator-frontend-main /app

# Byg frontend (hvis nødvendigt)
RUN npm run build

# Brug nginx til at servere den byggede applikation
FROM nginx:alpine

# Kopier de byggede filer fra build-frontend containeren
COPY --from=build /app/build /usr/share/nginx/html

# Eksponer port 80
EXPOSE 80
