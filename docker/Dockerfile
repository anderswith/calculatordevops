# Step 1: Build the Angular app
FROM node:16 AS build

# Sæt arbejdsbibliotek
WORKDIR /app

# Kopier package.json og package-lock.json
COPY package*.json ./

# Installer afhængigheder
RUN npm install

# Kopier hele projektet
COPY . .

# Byg Angular-applikationen for produktion
RUN npm run build --prod

# Step 2: Serve the Angular app with Nginx
FROM nginx:alpine

# Kopiér de byggede filer fra build-steget
COPY --from=build /app/dist/ /usr/share/nginx/html

# Eksponer port 80
EXPOSE 80

# Kør Nginx som default webserver
CMD ["nginx", "-g", "daemon off;"]